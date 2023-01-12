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
    [Tooltip("配对传感器")]
    private GameObject[] sensors;
    [SerializeField]
    [Tooltip("贴图")]
    private Sprite[] sprites;
    //是否打开
    private int activatedCount;
    private bool isOpen;
    private SpriteRenderer sr;
    private int sIndex;
    private List<float> spriteOffsetsX;
    private List<float> spriteOffsetsY;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        this.activatedCount = 0;
        this.sr = GetComponent<SpriteRenderer>();
        this.sIndex = 0;
        this.spriteOffsetsX = new List<float>() { 0f, -0.4f, -0.7f, -0.8f };
        this.spriteOffsetsY = new List<float>() { 0f, -0.1f, -0.2f, -0.4f };
        this.origin = this.gameObject.transform.position;
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
            this.isOpen = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (this.isOpen)
        {
            this.sIndex = (this.sIndex < this.sprites.Length - 1) ? this.sIndex + 1 : this.sprites.Length - 1;
            this.sr.sprite = this.sprites[sIndex];
            
            Vector3 pos = this.origin;
            pos.x += this.spriteOffsetsX[this.sIndex];
            pos.y += this.spriteOffsetsY[this.sIndex];
            this.gameObject.transform.position = pos;
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
