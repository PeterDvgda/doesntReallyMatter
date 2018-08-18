using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Singleton instance of the player
    public static PlayerManager instance;
    public GameObject playerBody;
    public float rotationSpeed;
    public float movementSpeed;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerBody.transform.eulerAngles = new Vector3(0, 0, playerBody.transform.eulerAngles.z - horizontalInput * rotationSpeed);
        playerBody.transform.Translate(Vector2.right * verticalInput * movementSpeed);
    }


    // Use this for initialization
    void Start()
    {

    }
}
