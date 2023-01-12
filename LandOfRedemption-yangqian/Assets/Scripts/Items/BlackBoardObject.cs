using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class BlackBoardObject : MonoBehaviour, ISceneItem
{
    [SerializeField]
    [Tooltip("名称")]
    private string itemName;
    [SerializeField]
    [Tooltip("UI名称")]
    private string UserInterfaceObject = "UserInterface";

    private BlackBoardItem item;
    private GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        this.ui = GameObject.Find(this.UserInterfaceObject);
        this.item = new BlackBoardItem(this.itemName, this.ui);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemInvoke()
    {
        this.item.ItemInvoke();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Item.IsHolder(collider.gameObject))
        {
            this.item.OutRangeDetect();
        }
    }

}

namespace Items
{
    public class BlackBoardItem: Item
    {
        private bool isUsing;
        private GameObject ui;

        public BlackBoardItem(string name, GameObject ui)
        {
            this.name = name;
            this.ui = ui;
            this.isUsing = false;
        }

        public override void ItemInvoke()
        {
            if (!this.isUsing)
            {
                string methodName = "ShowBlackBoardContent";
                Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
            else
            {
                string methodName = "HideBlackBoardContent";
                Debug.Log("退出使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
            this.isUsing = !this.isUsing;
        }

        public void OutRangeDetect()
        {
            if (this.isUsing)
            {
                this.ItemInvoke();
            }
        }
    }
}