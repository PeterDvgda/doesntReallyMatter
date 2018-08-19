using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Singleton instance of the UI manager
    public static UIManager instance;
    //Reference to the text to display the timer
    public Text timerText;

    public GameObject GameOverUI;

    public GameObject PauseUI;

    bool paused = false;

    private void OnEnable()
    {
        //Set the singleton instance
        if (instance == null)
            instance = this;
        //Subscribe to the OnUpdate event
        GameManager.OnUpdate += OnUpdateHandler;

        GameManager.OnPause += PauseHandler;

        //Subscribe to the EndGame event
        GameManager.OnEndGame += OnEndGameHandler;
    }
    private void OnDisable()
    {
        //Unsubscribe from the Update event
        GameManager.OnUpdate -= OnUpdateHandler;

        GameManager.OnPause -= PauseHandler;

        //Unsubscribe from the End Game event
        GameManager.OnEndGame -= OnEndGameHandler;
    }

    //Handler for when the End Game event is called
    private void OnEndGameHandler()
    {
        Debug.Log("GAME END");
        Time.timeScale = 0;
        Debug.Log("GAME END");
        GameOverUI.SetActive(true);

    }

    //Handler for when the Update event is called
    private void OnUpdateHandler()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    //Display the current time
    public void DisplayTime(string timeString)
    {
        timerText.text = timeString;
    }


    // Menu Functions

    public void ClickedResume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        GameManager.instance.state = GameState.Gameplay;

        if (paused == true)
            paused = false;
    }
    public void ClickedPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TestScene");
        if (paused == true)
            paused = false;
    }

    public void ClickedQuit()
    {
        Application.Quit();
    }

    public void ClickedBack()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
        if (paused == true)
            paused = false;
    }

    public void ClickedCredits()
    {
        SceneManager.LoadScene("CreditsScene");
        if (paused == true)
            paused = false;
    }

    public void PauseHandler()
    {
        Debug.Log("You pressed escape");
        Debug.Log(paused);
        if (paused == false)
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
            paused = true;
        }
        else if (paused)
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            paused = false;
        }

    }
}