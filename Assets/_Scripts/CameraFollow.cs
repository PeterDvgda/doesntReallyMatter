using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(PlayerManager.instance.transform.position.x, PlayerManager.instance.transform.position.y, -10);
	}
}
