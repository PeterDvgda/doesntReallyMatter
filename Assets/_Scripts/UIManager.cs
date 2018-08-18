using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        GameManager.OnUpdate += OnUpdateHandler;
    }
    private void OnDisable()
    {
        GameManager.OnUpdate -= OnUpdateHandler;
    }
    //On Update
    private void OnUpdateHandler()
    {
        Debug.Log("TEST");
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
