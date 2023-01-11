using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DoorObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("名称")]
<<<<<<< HEAD
    private string nameDoor;
=======
    private string name;
>>>>>>> main
    [SerializeField]
    [Tooltip("配对钥匙名称")]
    private string matchedKeyName;
    //是否打开
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        this.isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.isOpen ? "Open" : "Close");
        if (this.isOpen)
        {
            BoxCollider2D box = this.gameObject.GetComponent<BoxCollider2D>();
            box.isTrigger = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.name + "触发碰撞");
        if (Item.IsHolder(collision.collider.gameObject))
        {
            ItemHolder visitor = (ItemHolder) collision.collider.gameObject.GetComponent("ItemHolder");
            if (visitor && visitor.Contains(this.matchedKeyName))
            {
                this.isOpen = true;
            }
        }
    }

}
