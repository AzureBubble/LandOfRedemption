using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatFrom : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftPoint;
    public Transform rightPoint;
    float left, right;
    public float moveSpeed = 2f;
    bool isLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        left = leftPoint.position.x;
        right = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //判断移动方向
        if (isLeft)
        {
            //移动
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (transform.position.x < left)
            {
                isLeft = false;
            }
        }
        else
        {
            //移动
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (transform.position.x > right)
            {
                isLeft = true;
            }
        }
    }
}
