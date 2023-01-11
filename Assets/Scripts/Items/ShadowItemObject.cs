using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Items;

public class ShadowItemObject : MonoBehaviour
{
    private bool isPicked;
    private ShadowItem item;

    [SerializeField]
    [Tooltip("道具名称")]
    private string itemName;

    [SerializeField]
    [Tooltip("使用冷却时间")]
    private float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        this.isPicked = false;
        this.item = new ShadowItem(this.itemName, this.coolDownTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isPicked)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(this.itemName + "道具触发碰撞");
        if (Item.IsHolder(collider.gameObject))
        {
            this.item.SetHolder(collider.gameObject);
            this.item.Start();
            collider.gameObject.SendMessage("AddActiveItem", this.item);
            this.isPicked = true;
        }
    }
}

namespace Items
{
    public class ShadowItem: Item
    {
        private bool isAlive;
        private bool isReady;
        private Queue<Vector3> records;
        private float coolDownTime;

        public ShadowItem(string name, float coolDownTime)
        {
            this.name = name;
            this.isReady = false;
            this.isAlive = false;
            this.coolDownTime = coolDownTime;
            this.records = new Queue<Vector3>();
        }

        public void Start()
        {
            Debug.Log(this.name + "开始运行");
            this.reset();
            Timer timer = new Timer(args =>
            {
                try
                {
                    this.isReady = true;
                    Debug.Log(this.name + "冷却时间结束");
                }
                catch (Exception)
                {
                    Debug.Log(this.name + "运行抛出异常，将重新运行");
                    Thread.Sleep(100);
                    this.Start();
                }
            }, null, (int)(this.coolDownTime * 1000), Timeout.Infinite);
        }

        public override void Update(GameObject obj)
        {
            if (isAlive)
            {
                if (isReady)
                {
                    this.records.Dequeue();
                    this.records.Enqueue(obj.transform.position);
                }
                else
                {
                    this.records.Enqueue(obj.transform.position);
                }
                //Debug.Log(this.records.Count.ToString());
            }
        }

        private void reset()
        {
            this.isAlive = true;
            this.isReady = false;
            this.records.Clear();
        }

        public override void ItemInvoke()
        {
            if (this.isAlive && this.isReady)
            {
                this.isAlive = false;
                this.holder.SendMessage("ToPosition", this.records.Dequeue());
                /*Array tracks = this.records.ToArray();
                Array.Reverse(tracks);
                foreach (Vector3 each in tracks)
                {
                    this.holder.transform.position = each;
                }*/
                this.Start();
            }
        }
    }
}
