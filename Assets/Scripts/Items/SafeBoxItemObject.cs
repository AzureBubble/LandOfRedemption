using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class SafeBoxItemObject : MonoBehaviour, ISceneItem
{
    [SerializeField]
    [Tooltip("名称")]
    private string itemName;
    [SerializeField]
    [Tooltip("日记本")]
    private GameObject diaryItem;
    [SerializeField]
    [Tooltip("按键冷却时间")]
    private float coolDownTime;
    [SerializeField]
    [Tooltip("密码")]
    private string password;
    [SerializeField]
    [Tooltip("UI名称")]
    private string UserInterfaceObject = "UserInterface";

    private SafeBoxItem item;
    private bool isUsing;
    private bool isOpen;
    private bool isPressed;
    private Dictionary<KeyCode, char> keyToNumMaps;
    private StringBuilder inputChars;
    private GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        this.ui = GameObject.Find(this.UserInterfaceObject);
        Debug.Log(ui.name);
        this.item = new SafeBoxItem(this.itemName, this.ui);
        this.item.SetHolder(this.gameObject);
        this.isUsing = false;
        this.isOpen = false;
        this.isPressed = false;
        this.keyToNumMaps = new Dictionary<KeyCode, char>()
        {
            {KeyCode.Alpha0, '0' },
            {KeyCode.Alpha1, '1' },
            {KeyCode.Alpha2, '2' },
            {KeyCode.Alpha3, '3' },
            {KeyCode.Alpha4, '4' },
            {KeyCode.Alpha5, '5' },
            {KeyCode.Alpha6, '6' },
            {KeyCode.Alpha7, '7' },
            {KeyCode.Alpha8, '8' },
            {KeyCode.Alpha9, '9' },
            {KeyCode.Keypad0, '0' },
            {KeyCode.Keypad1, '1' },
            {KeyCode.Keypad2, '2' },
            {KeyCode.Keypad3, '3' },
            {KeyCode.Keypad4, '4' },
            {KeyCode.Keypad5, '5' },
            {KeyCode.Keypad6, '6' },
            {KeyCode.Keypad7, '7' },
            {KeyCode.Keypad8, '8' },
            {KeyCode.Keypad9, '9' }
        };
        this.inputChars = new StringBuilder("");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isOpen)
        {
            Instantiate(this.diaryItem, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void OnGUI()
    {
        if (this.isUsing)
        {
            //Debug.Log("Enter");
            if (!this.isPressed && Input.anyKey)
            {
                Event e = Event.current;
                if (e.isKey && this.keyToNumMaps.ContainsKey(e.keyCode))
                {
                    this.isPressed = true;
                    this.inputChars.Append(this.keyToNumMaps[e.keyCode]);

                    string methodName = "InputPassword";
                    Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                    this.ui.SendMessage(methodName, this.keyToNumMaps[e.keyCode]);

                    Debug.Log(this.inputChars.ToString());
                    Invoke("SetIsPressed", this.coolDownTime);
                }
            }

            if (this.password == this.inputChars.ToString())
            {
                //Invoke("SetIsOpen", this.coolDownTime);
                this.SetIsOpen();
                Debug.Log("保险箱打开");
                this.ItemInvoke();

            }

            if (this.inputChars.Length >= this.password.Length)
            {
                this.inputChars.Clear();
                string methodName = "InputClear";
                Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
        }
        else
        {
            this.inputChars.Clear();
            //Debug.Log("清除输入");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Item.IsHolder(collider.gameObject))
        {
            this.item.OutRangeDetect();
        }
    }

    public void ItemInvoke()
    {
        this.item.ItemInvoke();
    }

    public void SetIsUsing(bool status)
    {
        this.isUsing = status;
    }

    private void SetIsPressed()
    {
        this.isPressed = false;
    }

    private void SetIsOpen()
    {
        this.isOpen = true;
    }

}

namespace Items
{
    public class SafeBoxItem : Item
    {
        private bool isUsing;
        private GameObject ui;

        public SafeBoxItem(string name, GameObject ui)
        {
            this.name = name;
            this.ui = ui;
            this.isUsing = false;
        }

        public override void ItemInvoke()
        {
            if (!this.isUsing)
            {
                string methodName = "ShowPasswordUI";
                Debug.Log("使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
            else
            {
                string methodName = "HidePasswordUI";
                Debug.Log("退出使用" + this.name + "，向" + ui.name + "发送接口请求" + methodName);
                this.ui.SendMessage(methodName);
            }
            this.isUsing = !this.isUsing;
            this.holder.SendMessage("SetIsUsing", this.isUsing);
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
