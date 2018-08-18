using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        GameManager.OnUpdate += OnUpdateHandler;
    }

    private void OnUpdateHandler()
    {
        Debug.Log("PLAYER UPDATE");
    }

    private void OnDisable()
    {
        GameManager.OnUpdate -= OnUpdateHandler;
    }
    // Use this for initialization
    void Start () {
		
	}
}
