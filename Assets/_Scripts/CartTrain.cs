using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//working on cart part we want objects we want the carts to follow player
//they have joints and can grow. work on having carts follow the mouse to get a sense
//of the angles 

public class CartTrain : MonoBehaviour
{
    public GameObject CartTrainInstance;
    public Rigidbody2D cartTrainBody;
    float vertical;
    float horizontal;

    public float rotationSpeed;
    public float movementSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        cartTrainBody.transform.eulerAngles = new Vector3(0, 0, cartTrainBody.transform.eulerAngles.z - horizontalInput * rotationSpeed);
        cartTrainBody.transform.Translate(Vector2.left * verticalInput * movementSpeed);
        //cartTrainBody.velocity = new Vector2(vertical, horizontal);
    }
}


