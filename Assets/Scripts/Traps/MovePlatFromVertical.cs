using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatFromVertical : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform topPoint;
    public Transform bottomPoint;
    float top, bottom;
    public float moveSpeed = 2f;
    bool isBottom = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        top = topPoint.position.y;
        bottom = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //判断移动方向
        if (isBottom)
        {
            //移动
            rb.velocity = new Vector2( rb.velocity.x , -moveSpeed);
            if (transform.position.y < bottom)
            {
                isBottom = false;
            }
        }
        else
        {
            //移动
            rb.velocity = new Vector2(rb.velocity.x,moveSpeed);
            if (transform.position.y > top)
            {
                isBottom = true;
            }
        }
    }
}
