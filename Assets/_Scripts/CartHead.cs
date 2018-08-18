using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CartHead : MonoBehaviour
{
    // Was a cart added?
    bool added = false;

    // Cart Tail
    public GameObject CartTail;

    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

    List<Transform> tail = new List<Transform>();


    // Use this for initialization
    void Start()
    {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (added)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(CartTail, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            added = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Cart?
        if (coll.name.StartsWith("Cart"))
        {
            // Get longer in next Move call
            added = true;

            // Remove the Cart
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
            Debug.Log("Collided with border");
        }
    }
}


