using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public Text timerText;

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
    }

    // Use this for initialization
    void Start ()
    {

	}
    public void DisplayTime(string timeString)
    {
        timerText.text = timeString;
    }
	
}
