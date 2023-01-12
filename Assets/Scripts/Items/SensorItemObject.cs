using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class SensorItemObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("名称")]
    private string itemName;
    [SerializeField]
    [Tooltip("激活过期时间")]
    private float expiredTime;
    //传感器激活的门
    private GameObject sensorDoor;
    private bool isConnected;
    private SensorItem item;

    // Start is called before the first frame update
    void Start()
    {
        this.isConnected = false;
        this.item = new SensorItem(this.itemName, this.expiredTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.itemName + ": " + this.sensorDoor.name);
        if (this.sensorDoor)
        {
            this.isConnected = true;
        }
        else
        {
            this.isConnected = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(this.itemName + "道具触发碰撞");
        if (this.isConnected && Item.IsHolder(collider.gameObject))
        {
            this.item.SetHolder(this.sensorDoor);
            this.item.ItemInvoke();
        }
    }

    public void BuildLink(GameObject obj)
    {
        this.sensorDoor = obj;
    }
}

namespace Items
{
    public class SensorItem: Item
    {
        private bool isActivated;
        private float expiredTime;
        public SensorItem(string name, float time)
        {
            this.name = name;
            this.expiredTime = time;
            this.isActivated = false;
        }

        public override void ItemInvoke()
        {
            if (!isActivated)
            {
                this.holder.SendMessage("BeActivated", this.expiredTime);
                this.isActivated = true;
                this.CoolDown();
            }
            
        }

        private void CoolDown()
        {
            Timer timer = new Timer(args =>
            {
                try
                {
                    this.isActivated = false;
                }
                catch (Exception)
                {
                    Debug.Log("激活冷却抛出异常,重新冷却");
                    Thread.Sleep(100);
                    this.CoolDown();
                }
            }, null, (int)(this.expiredTime * 1000), Timeout.Infinite);
        }
    }
}