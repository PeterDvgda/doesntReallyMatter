using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public GameObject tracker;       //Public variable to store a reference to the empty leading game object
    public float camera_modifier;

    private Vector3 lastPos;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start() {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        // offset = transform.position - player.transform.position;
        //unity lerptowards
    }

    void Update() {
        lastPos = player.transform.position;

        LookForward();
    }

    private void LookForward() {
        if (playerIdle()) {
            var newX = tracker.transform.position.x;
            var newY = tracker.transform.position.y;
            var targetPosition = new Vector3 (newX, newY, -10);
            transform.position = Vector3.Lerp (transform.position, targetPosition , camera_modifier); 
        }
        else {
            var newX = player.transform.position.x;
            var newY = player.transform.position.y;
            var targetPosition = new Vector3 (newX, newY, -10);
            transform.position = Vector3.Lerp (transform.position, targetPosition , camera_modifier);
        }
    }

    bool playerIdle() {
        Vector3 curPos = player.transform.position;
        if(lastPos == curPos) {
            lastPos = curPos;
            return true;
        }
        else {
            return false;
        }
    }
}