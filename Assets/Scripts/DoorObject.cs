using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DoorObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("名称")]
    private string doorName;
    [SerializeField]
    [Tooltip("配对钥匙名称")]
    private string matchedKeyName;
    [SerializeField]
    [Tooltip("贴图")]
    private Sprite[] sprites;

    //是否打开
    private bool isOpen;
    private SpriteRenderer sr;
    private int sIndex;
    private List<float> spriteOffsetsX;
    private List<float> spriteOffsetsY;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        this.isOpen = false;
        this.sr = GetComponent<SpriteRenderer>();
        this.sIndex = 0;
        this.spriteOffsetsX = new List<float>() { 0f, -0.4f, -0.7f, -0.8f };
        this.spriteOffsetsY = new List<float>() { 0f, -0.1f, -0.2f, -0.4f };
        this.origin = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.isOpen ? "Open" : "Close");
        if (this.isOpen)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            //动画
            this.sIndex = (this.sIndex < this.sprites.Length - 1) ? this.sIndex + 1 : this.sprites.Length - 1;
            this.sr.sprite = this.sprites[sIndex];

            Vector3 pos = this.origin;
            pos.x += this.spriteOffsetsX[this.sIndex];
            pos.y += this.spriteOffsetsY[this.sIndex];
            this.gameObject.transform.position = pos;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.doorName + "触发碰撞");
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
