using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Public enumeration for the state of the game
public enum GameState
{
    Gameplay,
    End
}

public class GameManager : MonoBehaviour {
    //Singleton instance
    public static GameManager instance;
    //The games current state
    public GameState state;
    //Max time for a round of gameplay
    public float maxTime = 60;
    //The timer
    private float timer;

    //Event syntax for undate
    public delegate void OnUpdateEvent();
    public static event OnUpdateEvent OnUpdate;
    //Event syntax for the game ending
    public delegate void OnEndGameEvent();
    public static event OnEndGameEvent OnEndGame;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        state = GameState.Gameplay;
    }
    // Use this for initialization
    void Start ()
    {
        timer = maxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Call the Update event
        OnUpdate();

        //If the state is in current gameplay
        if(state == GameState.Gameplay)
        {
            //Decrement timer
            timer -= Time.deltaTime;
            //Call the SINGLETON INSTANCE of the UI manager and call the DisplayTime function
            UIManager.instance.DisplayTime("00:" + timer.ToString("F0"));
            //If the timer is less than 0
            if(timer <= 0)
            {
                //Set game state to end
                state = GameState.End;
                //Call the end
                OnEndGame();
            }
        }
    }
}
