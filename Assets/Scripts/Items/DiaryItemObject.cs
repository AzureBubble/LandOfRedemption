using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DiaryItemObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("名称")]
    private string itemName;

    private bool isReady;
    private bool isPicked;
    private DiaryItem item;

    // Start is called before the first frame update
    void Start()
    {
        this.isReady = false;
        this.isPicked = false;
        this.item = new DiaryItem(this.itemName);
        Invoke("Ready", 1);
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
        if (!this.isReady) return;
        Debug.Log(this.itemName + "道具触发碰撞");
        if (Item.IsHolder(collider.gameObject))
        {
            this.item.SetHolder(collider.gameObject);
            collider.gameObject.SendMessage("AddItem", this.item);
            this.isPicked = true;
        }
    }

    private void Ready()
    {
        this.isReady = true;
    }

}

namespace Items
{
    public class DiaryItem: Item
    {
        private bool isUsing;
        public DiaryItem(string name)
        {
            this.name = name;
            this.isUsing = false;
        }

        public override void ItemInvoke()
        {
            if (!this.isUsing)
            {
                Debug.Log(this.name + ": 查看日记本");
                //this.holder.SendMessage("ShowDiaryContent");
            }
            else
            {
                Debug.Log(this.name + ": 收起日记本");
                //this.holder.SendMessage("HideDiaryContent");
            }

            this.isUsing = !this.isUsing;
        }
    }
}