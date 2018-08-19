using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedMovement : MonoBehaviour {

	public float speed = 6;
	private float startingX;
	private bool dirRight = true;

	// Use this for initialization
	void Start () {
		startingX = transform.position.x;
	}

  
	// Update is called once per frame
	void Update () {
		
		if (dirRight) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		else {
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}
		
		if(startingX - transform.position.x < -3) { 
			dirRight = false;
		}
		
		if(startingX - transform.position.x > 3) {
			dirRight = true;
		}


		if (transform.position.x < -15) {
		//	teleport();
		}
	}

	private void teleport() {
		Vector3 temp = transform.position;
		temp.x = 15;
		transform.position = temp;
	}
}
