using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System.Linq;

public class UIController : MonoBehaviour , IUIforItemInterface
{
    Dictionary<string, GameObject> dic_total_ui = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_current = new Dictionary<string, GameObject>();
    public string currentDisplayName;
    public GameObject skillReversePanel;
    public GameObject skillShieldPanel;
    public GameObject skillDashPanel;
    public GameObject inputListen;
    void Start()
    {
        dic_total_ui.Add("SkillReverse", skillReversePanel);
        dic_total_ui.Add("SkillShield", skillShieldPanel);
        dic_total_ui.Add("SkillDash", skillDashPanel);
    }
    //显示特定道具
    public void ShowItem(string name)
    {
        if (name.Equals(""))
        {
            for (int i = 0; i < dic_total_ui.Count; i++) 
            { 
                dic_total_ui.ElementAt(i).Value.SetActive(false);
                currentDisplayName = null;
            }
        }
        else
        {
            //显示参数name对应的gameobject
            for (int i = 0; i < dic_total_ui.Count; i++)
            {
                if (name.Equals(dic_total_ui.ElementAt(i).Key))
                {
                    dic_total_ui.ElementAt(i).Value.SetActive(true);
                    currentDisplayName = dic_total_ui.ElementAt(i).Key;
                }
                else dic_total_ui.ElementAt(i).Value.SetActive(false);
            }
        }
    }
    //道具移除时的UI展示，name：道具名称，type：道具种类
    public void AddItem(string name)
    {
        for (int i = 0; i < dic_total_ui.Count; i++)
        {
            if (name.Equals(dic_total_ui.ElementAt(i).Key))
            {
                dic_current.Add(name,dic_total_ui.ElementAt(i).Value);
            }
        }
    }
    //道具移除时的UI展示，name：道具名称，type：道具种类
    public void RemoveItem(string name)
    {
        for (int i = 0; i < dic_total_ui.Count; i++)
        {
            if (name.Equals(dic_total_ui.ElementAt(i).Key))
            {
                dic_current.Remove(name);
            }
        }
    }
    //使用道具后的反馈
    public void UseItem()
    {
        //调用SkillCoolControl类中的SetIsUsed函数让技能图标开始冷却
        FindObjectOfType<SkillCoolControl>().SetIsUsed(true);
    }
    //与黑板互动时显示密码UI
    public void ShowBlackBoardContent()
    {
        
    }
    //与黑板互动时关闭密码UI
    public void HideBlackBoardContent()
    {
        
    }

    //查看日记本内容UI
    public void ShowDiaryContent()
    {
        
    }
    //收起日记本内容UI
    public void HideDiaryContent()
    {

    }
    //打开保险箱的密码界面
    public void ShowPasswordUI()
    {
        
    }
    //关闭保险箱的密码界面
    public void HidePasswordUI()
    {
        
    }

    //输入密码UI， number：0-9的数字字符
    public void InputPassword(char number)
    {
        
    }

    public void InputClear()
    {

    }

    
}
