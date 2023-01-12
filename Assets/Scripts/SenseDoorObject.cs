using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseDoorObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("名称")]
    private string doorName;
    [SerializeField]
    [Tooltip("配对钥匙名称")]
    private GameObject[] sensors;
    //是否打开
    private int activatedCount;

    // Start is called before the first frame update
    void Start()
    {
        this.activatedCount = 0;
        foreach (var sensor in sensors)
        {
            Debug.Log("与" + sensor.name + "建立链接");
            sensor.SendMessage("BuildLink", this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.activatedCount >= this.sensors.Length)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    public void BeActivated(float expiredTime)
    {
        if (activatedCount >= this.sensors.Length) return;

        Debug.Log("门被激活，计数+1");
        activatedCount++;
        this.CountDown(expiredTime);
    }

    private void CountDown(float expiredTime)
    {
        Debug.Log("开始倒计时" + expiredTime.ToString() + "秒");
        Timer timer = new Timer(args =>
        {
            try
            {
                this.activatedCount--;
            }
            catch (Exception)
            {
                Debug.Log("倒计时异常，重新计时");
                Thread.Sleep(10);
                this.CountDown(expiredTime);
            }
        }, null, (int)(expiredTime * 1000), Timeout.Infinite);
    }
}
