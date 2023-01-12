using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class BlackBoardObject : MonoBehaviour, ISceneItem
{
    [SerializeField]
    [Tooltip("名称")]
    private string itemName;

    private BlackBoardItem item;
    // Start is called before the first frame update
    void Start()
    {
        this.item = new BlackBoardItem(this.itemName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemInvoke()
    {
        this.item.ItemInvoke();
    }

}

namespace Items
{
    public class BlackBoardItem: Item
    {
        private bool isUsing;
        public BlackBoardItem(string name)
        {
            this.name = name;
            this.isUsing = false;
        }

        public override void ItemInvoke()
        {
            if (!this.isUsing)
            {
                Debug.Log("使用" + this.name);
                //this.holder.SendMessage("ShowBlackBoardContent");
            }
            else
            {
                Debug.Log("退出使用" + this.name);
                //this.holder.SendMessage("HideBlackBoardContent");
            }
            this.isUsing = !this.isUsing;
        }
    }
}