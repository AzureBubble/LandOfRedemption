using ClearSky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Sprint参数")]
    [SerializeField]
    private float sprintTime; // 冲刺时间
    private float sprintLeftTime; // 剩余时间
    private float lastSprint = -10f; // 上一次冲刺时间
    [SerializeField]
    private float sprintCoolTime; // 冷却时间
    [SerializeField]
    private float sprintSpeed; // 冲刺速度
    private bool isSprint = false;
    private float currentGrivity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGrivity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastSprint + sprintCoolTime))
            {
                ReadySprint();
            }
        }
    }

    private void FixedUpdate()
    {
        SprintRun();
        if (isSprint)
        {
            return;
        }
    }

    void ReadySprint()
    {
        isSprint = true;
        // 冲刺剩余时间 = 冲刺时间
        sprintLeftTime = sprintTime;
        // 上一次时间等于按下冲刺键的时间
        lastSprint = Time.time;
    }

    void SprintRun()
    {
        if (isSprint)
        {
            if (sprintLeftTime > 0)
            {
                rb.gravityScale = 0;
                GetComponent<PlayController>().enabled = false;
                rb.velocity = Vector2.right * sprintSpeed * transform.localScale.x;
                sprintLeftTime -= Time.deltaTime;
                ShadowPlool.instance.GetFromPool();
            }
            if (sprintLeftTime < 0)
            {
                isSprint = false;
                rb.gravityScale = currentGrivity;
                GetComponent<PlayController>().enabled = true;
            }
        }
    }
}
