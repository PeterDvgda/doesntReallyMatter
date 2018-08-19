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
        // Spawn cart every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 3, 4);
    }

    // Spawn one cart
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        // Instantiate the cart at (x, y)
        Instantiate(cart,new Vector2(x, y), Quaternion.identity); // default rotation
    }
}
