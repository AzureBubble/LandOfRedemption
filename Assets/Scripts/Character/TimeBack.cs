using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBack : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("TimeBack 参数")]
    [SerializeField]
    private float recordTime; // 技能时间
    private float recordLeftTime; // 剩余时间
    private float lastRecordTime = -1f; // 上一次记录时间
    [SerializeField]
    private float coolTime; // 冷却时间
    private bool isTimeBack;
    private GameObject BackPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (Time.time >= lastRecordTime + coolTime)
            {
                ReadyTimeBack();
            }
        }
        if (Input.GetKeyDown(KeyCode.I) && BackPos != null)// && BackPos != null
        {
            ReturnToPos();
        }
    }

    void FixedUpdate()
    {
        UseTimeBack();
    }

    public void ReadyTimeBack()
    {
        isTimeBack = true;
        recordLeftTime = recordTime;
        lastRecordTime = Time.time;
    }

    void UseTimeBack()
    {
        if (isTimeBack)
        {
            if (recordLeftTime > 0)
            {
                recordLeftTime -= Time.time;
                BackPos = StackPool.instance.GetFromPool();
            }
            if (recordLeftTime < 0)
            {
                isTimeBack = false;
                //Destroy(BackPos);
            }
        }
    }

    void ReturnToPos()
    {
        transform.position = BackPos.transform.position;
        transform.rotation = BackPos.transform.rotation;
        transform.localScale = BackPos.transform.localScale;
        
        Destroy(BackPos);
        //StartCoroutine(DestroyBackPos(lastRecordTime + coolTime));
    }

    /*IEnumerator DestroyBackPos(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(BackPos);
    }*/
}
