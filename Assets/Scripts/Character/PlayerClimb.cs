using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{

    private Rigidbody2D rb;
    public float climbSpeed = 5;

    public bool canClimb = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S) && canClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (canClimb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void CanClimb()
    {
        canClimb = true;
    }

    public void CantClimb()
    {
        canClimb = false;
    }
}

