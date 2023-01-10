using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class BlackBoardObject : MonoBehaviour, ISceneItem
{
    [SerializeField]
    [Tooltip("名称")]
    private string name;
    private BlackBoardItem item;
    // Start is called before the first frame update
    void Start()
    {
        this.item = new BlackBoardItem(this.name);
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
        public BlackBoardItem(string name)
        {
            this.name = name;
        }

        public override void ItemInvoke()
        {
            // 发给UI
            Debug.Log("使用" + this.name);
        }
    }
}