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
    [SerializeField]
    [Tooltip("图片")]
    private Sprite[] sprites;

    //传感器激活的门
    private GameObject sensorDoor;
    private bool isConnected;
    private SensorItem item;
    private SpriteRenderer sr;
    private int sIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.isConnected = false;
        this.item = new SensorItem(this.itemName, this.expiredTime);
        this.sr = GetComponent<SpriteRenderer>();
        this.sIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.itemName + ": " + this.sensorDoor.name);
        if (this.sensorDoor)
        {
            this.isConnected = true;
            if (this.item.GetIsActivated())
            {
                this.sIndex = (this.sIndex + 1 >= this.sprites.Length) ? this.sprites.Length - 1 : this.sIndex + 1;
                sr.sprite = sprites[sIndex];
            }
            else
            {
                this.sIndex = (this.sIndex - 1 <= 0) ? 0 : this.sIndex - 1;
                sr.sprite = sprites[sIndex];
            }
        }
        else
        {
            this.isConnected = false;
        }


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isConnected && Item.IsHolder(collider.gameObject))
        {
            Debug.Log(this.itemName + "道具触发碰撞");
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

        public bool GetIsActivated()
        {
            return this.isActivated;
        }
    }
}