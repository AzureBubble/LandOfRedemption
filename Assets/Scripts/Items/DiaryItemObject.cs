using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DiaryItemObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("名称")]
    private string itemName;
    [SerializeField]
    [Tooltip("UI名称")]
    private string UserInterfaceObject = "UserInterface";

    private bool isReady;
    private bool isPicked;
    private DiaryItem item;
    private GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        this.isReady = false;
        this.isPicked = false;
        this.ui = GameObject.Find(this.UserInterfaceObject);
        this.item = new DiaryItem(this.itemName, this.ui);
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
        if (!this.isPicked && Item.IsHolder(collider.gameObject))
        {
            Debug.Log(this.itemName + "道具触发碰撞");
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
        private GameObject ui;
        public DiaryItem(string name, GameObject ui)
        {
            this.name = name;
            this.ui = ui;
            this.isUsing = false;
        }

        public override void ItemInvoke()
        {
            if (!this.isUsing)
            {
                string methodName = "ShowDiaryContent";
                Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
            else
            {
                string methodName = "HideDiaryContent";
                Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }

            this.isUsing = !this.isUsing;
        }
    }
}