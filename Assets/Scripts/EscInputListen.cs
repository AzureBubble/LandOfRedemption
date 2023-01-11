using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscInputListen : MonoBehaviour
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
                Resume();
            }
            else
            {
                Paused();
            }
        }

    }
    public void Resume()
    {
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Paused()
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }
}
