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

    Collider2D[] colliders; 

    // Use this for initialization
    void Start()
    {
        // Spawn cart every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 4, 3);
    }

    // Spawn one cart
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        Vector2 spawnPoint = new Vector2(x, y);

        colliders = Physics2D.OverlapCircleAll(spawnPoint, 3);

        if (colliders.Length > 0)
        {
            Debug.Log("at point " + spawnPoint + " respawning");
            Spawn();
        }
        else
        {
            cart.GetComponent<HingeJoint2D>().connectedAnchor = spawnPoint;
            Instantiate(cart, spawnPoint, Quaternion.identity); // default rotation
        }
    }
}
