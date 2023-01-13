using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTextLoad : MonoBehaviour
{

    [Header("UI")]
    [Tooltip("文本框")]
    [SerializeField]
    public Text textLabel;

    [Header("文本文件")]
    public TextAsset textFile;

    [Header("插入对话框")]
    public GameObject TalkUI;

    [Header("对话设置")]
    [Tooltip("索引")]
    [SerializeField]
    public int index;
    [Tooltip("文本行数")]
    [SerializeField]
    public int columnNum;
    [Tooltip("控制文本速度")]
    [SerializeField]
    public float textSpeed;

    bool textFinished;
    bool isDoor;




    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFormFile(textFile);
        index = 0;
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && index == columnNum)  // index 一共有多少行 关掉对话框
        {
            index = 0;
            TalkUI.SetActive(false);
            Invoke("LoadScene", 1f);
            return;
        }

        if (Input.GetButtonDown("Fire1") && textFinished)  // 按下鼠标左键
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();  // 将list里面东西清空
        index = 0;

        var lineDate = file.text.Split('\n');   // 用回车做分割

        foreach (var line in lineDate)
        {
            textList.Add(line);  // 将分隔好的东西添加进LIst
        }
    }

    void LoadScene()
    {
        if (isDoor)
            Invoke("LoadSceneManager",1f);
            
    }

    void LoadSceneManager()
    {
        SceneManager.LoadScene("StartGame");
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDoor = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isDoor = false;
    }
}
