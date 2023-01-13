using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // 做一个游戏场景切换的淡出效果
    [Header("场景转换 属性")]
    [Tooltip("UI 淡入淡出图像")]
    [SerializeField]
    public UnityEngine.UI.Image transtionImage;
    [Tooltip("UI 淡出淡入时间")]
    [SerializeField]
    float fadeTime = 1f;

    // 在这里设置 要转到场景的名字
    const int GAMEPLAY = 0;
    const string GoBack = "TestDemo";

    Color color;

    IEnumerator LoadCoroutine(int newScene)
    {
        // 异步加载 解决画面卡顿问题
        var loadingOperation = SceneManager.LoadSceneAsync(newScene);
        loadingOperation.allowSceneActivation = false;

        transtionImage.gameObject.SetActive(true);
        // 淡出
        while (color.a < 1f)  // 调整图片alpha 透明度
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime); // unscaledDetaltaTime不会受到时间规模的影响
            //Mathf.Clamp01()  将浮点数变量控制在0——1直接 防止溢出
            transtionImage.color = color;
            yield return null;
        }

        //yield return new WaitUntil(() => loadingOperation.progress >= 0.9);
        loadingOperation.allowSceneActivation = true;// 加载场景
        //Load(sceneName);


        // 淡入
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            transtionImage.color = color;
            yield return null;
        }

        transtionImage.gameObject.SetActive(false);
    }

    public void NextScene()
    {
       // StopAllCoroutines();

        StartCoroutine(LoadCoroutine(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void LoadGoBack()
    {
        StopAllCoroutines();

        StartCoroutine(LoadCoroutine(SceneManager.GetActiveScene().buildIndex - 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("NextScene",0f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
