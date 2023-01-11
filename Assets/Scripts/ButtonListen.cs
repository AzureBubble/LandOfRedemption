using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListen : MonoBehaviour
{
    public Button GameStartButton;//游戏开始按钮
    public Button GameOptionButton;//游戏选项按钮
    public Button GameOverButton;//游戏结束按钮

    void Start()
    {
        GameStartButton = GameObject.Find("btnPlay").GetComponent<Button>();//通过Find查找名称获得我们要的Button组件
        GameStartButton.onClick.AddListener(GameStartClickListener);//监听点击事件
    }

    //开始游戏点击监听
    public void GameStartClickListener()
    {
        print("StartGameButtonIsClick");
        //SceneManager.LoadScene(1);//跳转到关卡1
    }

    //游戏选项点击监听
    public void GameOptionButtonClickListener()
    {

    }

    //退出游戏点击监听
    public void GameOverClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
