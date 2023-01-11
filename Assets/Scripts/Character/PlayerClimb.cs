using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    /*public enum States
    {
        NotClimb,Climb
    }*/

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private float climbSpeed = 5;

    public bool canClimb = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * playerMovement.inputY);
        }
        if (Input.GetKeyDown(KeyCode.S) && canClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, -climbSpeed * playerMovement.inputY);
        }
    }

    private void FixedUpdate()
    {
        var jumpUpGraivity = playerMovement.jumpUpGraivity;
        var fallDownGraivity = playerMovement.fallDownGraivity;
        var gravityScale = rb.gravityScale;
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

