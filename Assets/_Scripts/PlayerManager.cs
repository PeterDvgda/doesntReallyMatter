﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Singleton instance of the player
    public static PlayerManager instance;
    public GameObject playerBody;
    public Animator playerAnimator;
    public float rotationSpeed;
    public float movementSpeed;
    public float horizontalInput;
    public float verticalInput;
    public bool isBoosting;
    public float boostTime;
    public float boostSpeed;
    public float explosionStrength;
    public SpriteRenderer rocketSprite1;
    public SpriteRenderer rocketSprite2;
    public GameObject deathCrater;
    public ParticleSystem explosionParticleSystem;
    public ParticleSystem rocketParticleSystem1;
    public ParticleSystem rocketParticleSystem2;
    public bool isPortal;
    public bool isDead;
    private float timer;
    private bool usedBoost;
    public SpriteRenderer PortalSprite;

    private void OnEnable()
    {
        //Set reference for the player singleton
        if (instance == null)
            instance = this;

        //Subscribe to the Update event
        GameManager.OnUpdate += OnUpdateHandler;
    }

    private void OnDisable()
    {
        //Unsubscribe from the update event
        GameManager.OnUpdate -= OnUpdateHandler;
    }

    //Handler for the Update event
    private void OnUpdateHandler()
    {
        if(isDead == false && isPortal == false && GameManager.instance.state != GameState.End)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if (isBoosting == false)
            {
                Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
                rb2d.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                transform.eulerAngles = new Vector3(0, 0, transform.transform.eulerAngles.z - horizontalInput * rotationSpeed);
                transform.Translate(Vector2.right * verticalInput * movementSpeed);
                playerAnimator.SetInteger("isMoving", (verticalInput != 0) ? 1 : 0);
                if (GameManager.instance.carts.Count != 0)
                    playerAnimator.speed = Mathf.Clamp((1 - (GameManager.instance.carts.Count * 0.05f)), 0.55f, 1);

                if (GameManager.instance.carts.Count != 0 && Input.GetButtonDown("Jump") && usedBoost == false)
                {
                    timer = 0;
                    usedBoost = true;
                    isBoosting = true;
                    rocketSprite1.enabled = true;
                    rocketSprite2.enabled = true;
                    rocketParticleSystem1.Play();
                    rocketParticleSystem2.Play();
                    UIManager.instance.updateRocketUses();
                    AudioManager.instance.PlayStopRocketBoost(true);
                    playerAnimator.SetBool("isBoosting", true);
                }
            }
            else if (isBoosting == true)
            {
                timer += Time.deltaTime;
                Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
                rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                if (timer >= boostTime || Input.GetButtonDown("Jump"))
                {
                    rocketParticleSystem1.Stop();
                    rocketParticleSystem2.Stop();
                    isBoosting = false;
                    playerAnimator.SetBool("isBoosting", false);
                    AudioManager.instance.PlayStopRocketBoost(false);
                    rocketSprite1.enabled = false;
                    rocketSprite2.enabled = false;
                    return;
                }
                transform.eulerAngles = new Vector3(0, 0, transform.transform.eulerAngles.z - horizontalInput * rotationSpeed);
                transform.Translate(Vector2.right * 1 * movementSpeed * boostSpeed);
            }
        }    
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if(GameManager.instance.carts.Count != 0)
                AudioManager.instance.PlayStopCartPull(true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            if(GameManager.instance.carts.Count != 0)
                AudioManager.instance.PlayStopCartPull(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        isBoosting = false;
        usedBoost = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Cart")
        {
            playerAnimator.SetBool("isGrabbing", true);
            if (GameManager.instance.carts.Count == 0)
                AudioManager.instance.PlayStopCartPull(true);
            GameManager.instance.AddCart(collision.gameObject);
            GameManager.instance.updateScore();
            GameManager.instance.updateTotalScore();
            UIManager.instance.UpdateScoreText();
        }
        if (tag == "Car")
        {
            if (isBoosting)
            {
                isDead = true;
                playerAnimator.SetBool("isDead", true);
                AudioManager.instance.PlayStopRocketBoost(false);
                AudioManager.instance.PlayOneShotExplosion();
                Destroy(playerBody.transform.parent.GetComponent<CircleCollider2D>());
                Destroy(playerBody.transform.parent.GetComponent<Rigidbody2D>());
                Destroy(rocketSprite1.gameObject);
                Destroy(rocketSprite2.gameObject);
                explosionParticleSystem.Play();
                deathCrater.SetActive(true);
                transform.eulerAngles = Vector3.zero;
                Collider2D[] ObjectsAround = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach(Collider2D obj in ObjectsAround)
                {
                    Rigidbody2D rb2d = obj.gameObject.GetComponent<Rigidbody2D>();
                    if(rb2d != null)
                    {
                        if (obj.name != "Player" || obj.gameObject.tag != "Bounds")
                        {
                            Vector2 forceVector = rb2d.transform.position - transform.position;
                            rb2d.constraints = RigidbodyConstraints2D.None;
                            rb2d.velocity = forceVector * explosionStrength;
                        }
                    }
                    
                }
                GameManager.instance.EndGameDelayed(3);
            }
        }
        if (tag == "Submission")
        {
            GameManager.instance.updateScore();
            GameManager.instance.updateTotalScore();
            UIManager.instance.UpdateScoreText();
            GameManager.instance.EndGameDelayed(0);
        }
        if (tag == "Portal")
        {
            if(isBoosting)
            {
                if (isBoosting)
                {
                    isPortal = true;
                    PortalSprite.enabled = true;
                    GameManager.instance.EndGameDelayed(0);

                }
            }
        }
    }

    public void PlayRandomFootStep()
    {
        AudioManager.instance.PlayRandomFootStep();
    }
}
