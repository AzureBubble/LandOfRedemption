using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListen : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                //Resume
                pauseMenuUI.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                GameIsPaused = false;
            }
            else
            {
                //Paused
                pauseMenuUI.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                GameIsPaused = true;
            }
        }
        //测试代码,用按钮的方式测试代码是否可执行
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<UIController>().UseItem();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            FindObjectOfType<UIController>().ShowItem("SkillDash");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<UIController>().ShowItem("SkillShield");
        }
    }
}
