using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSpawner : MonoBehaviour {

    public GameObject cart;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Use this for initialization
    void Start()
    {
        // Spawn cart every 1 seconds, starting in 1 second
        InvokeRepeating("Spawn", 1, 1);
    }

    // Spawn one cart
    public void Spawn()
    {
        Collider2D[] results;

        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x + 2, borderRight.position.x - 2);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y + 2, borderTop.position.y - 2);

        Vector2 spawnPoint = new Vector2(x, y);
        if ()
        {
            Spawn();
            Debug.Log("at point " + spawnPoint +" respawning");
        }
        else
            Instantiate(cart, new Vector2(x, y), Quaternion.identity); // default rotation

     

    }
}
