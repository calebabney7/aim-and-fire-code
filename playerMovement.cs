using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed;
    private float moveHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSpeed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        //Get player key inputs
        moveHorizontal = Input.GetAxis("Horizontal");

        //move
       // if(lhRb.constraints == 0 &&  rhRb.constraints == 0)
        rb.velocity = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);
    }
}
