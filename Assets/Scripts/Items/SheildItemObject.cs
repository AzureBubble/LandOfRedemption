using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class SheildItemObject : MonoBehaviour
{
    //是否被拾取
    private bool isPicked;
    private SheildItem item;

    [SerializeField]
    [Tooltip("护盾名称")]
    private string itemName;
    [SerializeField]
    [Tooltip("护盾持续时间（单位秒）")]
    private float sheildDurationTime;
    [SerializeField]
    [Tooltip("护盾有效次数")]
    private int sheildCount;

    // Start is called before the first frame update
    void Start()
    {
        isPicked = false;
        item = new SheildItem(this.itemName, this.sheildDurationTime, this.sheildCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(this.itemName + "道具触发碰撞");
        if (this.item.IsHolder(collider.gameObject))
        {
            this.item.SetHolder(collider.gameObject);
            collider.gameObject.SendMessage("AddItem", this.item);
            this.isPicked = true;
        }
    }
}

namespace Items
{
    public class SheildItem : Item
    {
        //护盾剩余量
        private int count;
        //护盾持续时间
        private float time;

        public SheildItem(string name, float time, int count)
        {
            this.name = name;
            this.time = time;
            this.count = count;
        }

        public override void ItemInvoke()
        {
            this.count--;
            this.holder.SendMessage("ActivateSheild", time);
            if (this.count <= 0)
            {
                this.holder.SendMessage("RemoveItem", this.GetName());
            }
        }
    }
}
