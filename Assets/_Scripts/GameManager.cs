using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public float maxTime = 60;
    private float timer;
    public delegate void OnUpdateEvent();
    public static event OnUpdateEvent OnUpdate;
    public delegate void OnEndGameEvent();
    public static event OnEndGameEvent OnEndGame;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OnUpdate();
        timer -= Time.deltaTime;
        UIManager.instance.DisplayTime("00:" + timer.ToString("F0"));

    }
}
