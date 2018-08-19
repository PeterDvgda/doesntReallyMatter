using System;
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
    public ParticleSystem explosionParticleSystem;
    private float timer;
    private bool usedBoost;
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
        if(GameManager.instance.state != GameState.End)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if (isBoosting == false)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.transform.eulerAngles.z - horizontalInput * rotationSpeed);
                transform.Translate(Vector2.right * verticalInput * movementSpeed);
                playerAnimator.SetInteger("isMoving", (verticalInput != 0) ? 1 : 0);
                if (GameManager.instance.carts.Count != 0)
                    playerAnimator.speed = Mathf.Clamp((1 - (GameManager.instance.carts.Count * 0.05f)), 0.55f, 1);

                if (GameManager.instance.carts.Count != 0 && Input.GetKeyDown(KeyCode.Space) && usedBoost == false)
                {
                    timer = 0;
                    usedBoost = true;
                    isBoosting = true;
                }
            }
            else if (isBoosting == true)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= boostTime)
                {
                    isBoosting = false;
                    return;
                }
                transform.eulerAngles = new Vector3(0, 0, transform.transform.eulerAngles.z - horizontalInput * rotationSpeed);
                transform.Translate(Vector2.right * 1 * movementSpeed * boostSpeed);
            }
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
            GameManager.instance.AddCart(collision.gameObject);
        }
        if (tag == "Car")
        {
            if (isBoosting)
            {
                Destroy(playerBody);
                explosionParticleSystem.Play();
                GameManager.instance.EndGameDelayed(2);
            }
        }
    }
}
