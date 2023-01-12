锘縰sing System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class FlashItemObject : MonoBehaviour
{
    private bool isPicked;
    private FlashItem item;

    [SerializeField]
    [Tooltip("閬撳叿鍚嶇О")]
    private string itemName;

    [SerializeField]
    [Tooltip("浣跨敤鍐峰嵈鏃堕棿")]
    private float coolDownTime;

    [SerializeField]

    [Tooltip("闂幇璺濈")]
    private float flashDistance;

    // Start is called before the first frame update
    void Start()
    {
        this.isPicked = false;
        this.item = new FlashItem(this.itemName, this.coolDownTime, this.flashDistance);
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
        Debug.Log(this.itemName + "閬撳叿瑙﹀彂纰版挒");
        if (Item.IsHolder(collider.gameObject))
        {
            this.item.SetHolder(collider.gameObject);
            this.item.Start();
            collider.gameObject.SendMessage("AddItem", this.item);
            this.isPicked = true;
        }
    }


}

namespace Items
{
    public class FlashItem: Item
    {
        private bool isReady;
        private float coolDownTime;
        private float flashDistance;

        public FlashItem(string name, float time, float dis)
        {
            this.name = name;
            this.coolDownTime = time;
            this.flashDistance = dis;
            this.isReady = false;
        }

        public void Start()
        {
            this.isReady = true;
        }

        private void CoolDown()
        {
            Timer timer = new Timer(args =>
            {
                try
                {
                    this.isReady = true;
                    Debug.Log(this.name + "鍐峰嵈鏃堕棿缁撴潫");
                } catch (Exception)
                {
                    Debug.Log(this.name + "鍐峰嵈鏃舵姏鍑哄紓甯�,绋嶅悗閲嶈瘯");
                    Thread.Sleep(100);
                    this.CoolDown();
                }
            }, null, (int)(this.coolDownTime * 1000), Timeout.Infinite);
        }

        public override void ItemInvoke()
        {
            if (this.isReady)
            {
                this.isReady = false;
                this.holder.SendMessage("ActivateFlash", this.flashDistance);
                this.CoolDown();
            } 
        }
    }
}
