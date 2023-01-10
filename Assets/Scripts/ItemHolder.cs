using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemHolder : MonoBehaviour
{
    // 列表存储道具名称
    private List<string> itemNames;
    //字典存储道具对象
    private Dictionary<string, Item> collectedItems;
    // 当前指向的道具
    private int itemIndex;

    private bool isItemUsed;
    private bool isItemSwitched;
    [SerializeField]
    [Tooltip("键盘输入冷却时间")]
    private float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        this.itemNames = new List<string>();
        this.collectedItems = new Dictionary<string, Item>();
        this.itemIndex = 0;
        this.isItemUsed = false;
        this.isItemSwitched = false;
}

    // Update is called once per frame
    void Update()
    {
        KeyBoardItemInvoke();
        KeyBoardItemSwitch();
        /*if (Input.GetKey(KeyCode.M))
        {
            string s = "Item Names: ";
            foreach (string name in itemNames)
            {
                s += (name + ", ");
            }
            Debug.Log(s);
            s = "Items: ";
            foreach (string name in collectedItems.Keys)
            {
                s += (name + ", ");
            }
            Debug.Log(s);
        }*/
    }

    #region 调用itemIndex指向的道具: K键使用
    void KeyBoardItemInvoke()
    {
        if (!this.isItemUsed && Input.GetKey(KeyCode.K) && this.itemNames.Count > 0)
        {
            this.isItemUsed = true;
            string itemName = this.itemNames[this.itemIndex];
            if (this.collectedItems.ContainsKey(itemName))
            {
                Debug.Log("使用" + itemName + "道具");
                this.collectedItems[itemName].ItemInvoke();
            }
            Invoke("SetIsItemUsed", this.coolDownTime);
            Debug.Log("剩余道具数量" + collectedItems.Count);
        }
    }

    private void SetIsItemUsed()
    {
        this.isItemUsed = false;
    }
    #endregion

    #region 道具切换: J、L键切换左右道具
    void KeyBoardItemSwitch()
    {
        if (!this.isItemSwitched && itemNames.Count > 0)
        {
            if (Input.GetKey(KeyCode.J))
            {
                this.isItemSwitched = true;
                this.SetItemIndex(this.itemNames.Count - 1);
                Invoke("SetIsItemSwitched", this.coolDownTime);
                Debug.Log("选中第" + this.itemIndex.ToString() + "个道具" + this.itemNames[this.itemIndex] + " 道具总数为" + this.itemNames.Count.ToString());
            } else if (Input.GetKey(KeyCode.L))
            {
                this.isItemSwitched = true;
                this.SetItemIndex(1);
                Invoke("SetIsItemSwitched", this.coolDownTime);
                Debug.Log("选中第" + this.itemIndex.ToString() + "个道具" + this.itemNames[this.itemIndex] + " 道具总数为" + this.itemNames.Count.ToString());
            }
        }
    }

    private void SetIsItemSwitched()
    {
        this.isItemSwitched = false;
    }

    private void SetItemIndex(int offset)
    {
        this.itemIndex = (this.itemIndex + offset) % this.itemNames.Count;
    }
    #endregion

    #region 道具持有者添加道具
    public void AddItem(Item item)
    {
        this.itemNames.Add(item.GetName());
        this.collectedItems.Add(item.GetName(), item);
        this.SetItemIndex(0);
        Debug.Log("添加道具" + item.GetName());
    }
    #endregion

    #region 道具持有者移除道具
    public void RemoveItem(string name)
    {
        this.itemNames.Remove(name);
        this.collectedItems.Remove(name);
        Debug.Log("移除道具" + name);
    }
    #endregion

    public bool Contains(string name)
    {
        return this.itemNames.Contains(name);
    }

}
