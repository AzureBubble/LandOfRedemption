using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class KeyItemObject : MonoBehaviour
{
    //是否被拾取
    private bool isPicked;
    private KeyItem item;

    [SerializeField]
    [Tooltip("钥匙名称")]
    private string itemName;

    // Start is called before the first frame update
    void Start()
    {
        this.isPicked = false;
        this.item = new KeyItem(this.itemName);
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
        if (!this.isPicked && Item.IsHolder(collider.gameObject))
        {
            Debug.Log(this.itemName + "道具触发碰撞");
            this.item.SetHolder(collider.gameObject);
            collider.gameObject.SendMessage("AddItem", this.item);
            this.isPicked = true;
        }
    }
}

namespace Items
{
    public class KeyItem: Item
    {
        public KeyItem(string name)
        {
            this.name = name;
        }

        public override void ItemInvoke()
        {
            Debug.Log("使用" + this.GetName() + "，但好像什么都没发生。");
        }
    }
}