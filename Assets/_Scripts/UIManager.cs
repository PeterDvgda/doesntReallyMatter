using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	//Singleton instance of the UI manager
	public static UIManager instance;
	//Reference to the text to display the timer
	public Text timerText;

	private void OnEnable()
	{
		//Set the singleton instance
		if (instance == null)
			instance = this;
		//Subscribe to the OnUpdate event
		GameManager.OnUpdate += OnUpdateHandler;
		//Subscribe to the EndGame event
		GameManager.OnEndGame += OnEndGameHandler;
	}
	private void OnDisable()
	{
		//Unsubscribe from the Update event
		GameManager.OnUpdate -= OnUpdateHandler;
		//Unsubscribe from the End Game event
		GameManager.OnEndGame -= OnEndGameHandler;
	}

	//Handler for when the End Game event is called
	private void OnEndGameHandler()
	{
		Debug.Log("GAME END");
	}

	//Handler for when the Update event is called
	private void OnUpdateHandler()
	{
	}

	// Use this for initialization
	void Start ()
	{

	}

	//Display the current time
	public void DisplayTime(string timeString)
	{
		timerText.text = timeString;
	}


	// Menu Functions
	public void ClickedPlay()
	{
		SceneManager.LoadScene("TestScene");
	}

	public void ClickedQuit()
	{
		Application.Quit();
	}

	public void ClickedBack()
	{
		SceneManager.LoadScene("MenuScene");
	}

	public void ClickedCredits()
	{
		SceneManager.LoadScene("CreditsScene");
	}
	
}
