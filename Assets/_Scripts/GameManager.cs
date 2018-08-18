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
    public List<GameObject> carts;
    //The timer
    private float timer;

    private float damageScore;

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
        carts = new List<GameObject>();
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
    public void AddCart(GameObject cart)
    {
        if (carts.Contains(cart))
            return;

        carts.Add(cart);
        cart.transform.localEulerAngles = Vector3.zero;
        HingeJoint2D cartHingeJoint = cart.GetComponent<HingeJoint2D>();
        if(carts.Count == 1)
        {
            cart.transform.position = PlayerManager.instance.transform.position;
            cartHingeJoint.connectedBody = PlayerManager.instance.GetComponent<Rigidbody2D>();
            cartHingeJoint.anchor = new Vector2(1.23f, 0);
            cartHingeJoint.connectedAnchor = new Vector2(-0.52f, 0);
        }
        else
        {
            Debug.Log(carts[carts.Count - 2].name);
            cart.transform.position = carts[carts.Count - 2].transform.position;
            cartHingeJoint.connectedBody = carts[carts.Count - 2].GetComponent<Rigidbody2D>();
            cartHingeJoint.anchor = new Vector2(0.55f, 0);
            cartHingeJoint.connectedAnchor = new Vector2(-0.2684855f, 0);
        }
        cartHingeJoint.autoConfigureConnectedAnchor = false;
    }

    public void updateDamageScore()
    {
        damageScore++;
        Debug.Log(damageScore);
    }
}
