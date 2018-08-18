using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //Singleto instance of the player
    public static PlayerManager instance;

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
        Debug.Log("PLAYER UPDATE");
    }

    
    // Use this for initialization
    void Start () {
		
	}
}
