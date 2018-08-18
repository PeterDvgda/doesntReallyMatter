using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public delegate void OnUpdateEvent();
    public static event OnUpdateEvent OnUpdate;

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
	}
}
