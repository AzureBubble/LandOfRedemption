using System;
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
    //特殊主动道具对象
    private List<string> activeItemNames;
    // 当前指向的道具
    private int itemIndex;

    //携带道具在这帧是否被使用
    private bool isItemUsed;
    //携带道具切换键在这帧是否按下
    private bool isItemSwitched;
    //附近的场景道具
    private List<GameObject> nearbyItems;

    [SerializeField]
    [Tooltip("键盘输入冷却时间")]
    private float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        this.itemNames = new List<string>();
        this.collectedItems = new Dictionary<string, Item>();
        this.activeItemNames = new List<string>();
        this.itemIndex = 0;
        this.isItemUsed = false;
        this.isItemSwitched = false;
        this.nearbyItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardItemInvoke();
        KeyBoardItemSwitch();
        ActivateItemProcess();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Item.IsItem(collider.gameObject))
        {
            this.nearbyItems.Add(collider.gameObject);
            Debug.Log(collider.gameObject.name + "碰撞。检测到" + this.nearbyItems.Count.ToString() + "个场景道具");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Item.IsItem(collider.gameObject))
        {
            this.nearbyItems.Remove(collider.gameObject);
            Debug.Log(collider.gameObject.name + "离开。检测到" + this.nearbyItems.Count.ToString() + "个场景道具");
        }
    }

    #region 调用itemIndex指向的道具: K键使用 / 调用场景道具：E键使用
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

        if (!this.isItemUsed && Input.GetKey(KeyCode.E) && this.nearbyItems.Count > 0)
        {
            this.isItemUsed = true;
            this.nearbyItems[0].SendMessage("ItemInvoke");
            Invoke("SetIsItemUsed", this.coolDownTime);
            Debug.Log("使用物体" + this.nearbyItems[0].name);
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

    #region 主动道具（需要主动随帧更新参数的道具）处理
    void ActivateItemProcess()
    {
        foreach(string itemName in this.activeItemNames)
        {
            this.collectedItems[itemName].Update(this.gameObject);
        }
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

    public void AddActiveItem(Item item)
    {
        this.AddItem(item);
        this.activeItemNames.Add(item.GetName());
    }
    #endregion

    #region 道具持有者移除道具
    public void RemoveItem(string name)
    {
        this.itemNames.Remove(name);
        this.collectedItems.Remove(name);
        if (this.itemNames.Count > 0)
        {
            this.SetItemIndex(0);
        }
        Debug.Log("移除道具" + name);
    }

    public void RemoveActiveItem(string name, bool isActiveItem)
    {
        this.RemoveItem(name);
        this.activeItemNames.Remove(name);
    } 
    #endregion

    public bool Contains(string itemName)
    {
        return this.itemNames.Contains(itemName);
    }
}
