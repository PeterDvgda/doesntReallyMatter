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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.eulerAngles = new Vector3(0, 0, transform.transform.eulerAngles.z - horizontalInput * rotationSpeed);
        transform.Translate(Vector2.right * verticalInput * movementSpeed);
        playerAnimator.SetInteger("isMoving", (verticalInput != 0) ? 1 : 0);
        if (GameManager.instance.carts.Count != 0)
            playerAnimator.speed = Mathf.Clamp((1- (GameManager.instance.carts.Count * 0.05f)), 0.55f, 1);
    }

    // Use this for initialization
    void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            playerAnimator.SetBool("isGrabbing", true);
            GameManager.instance.AddCart(collision.gameObject);
        }
    }
}
