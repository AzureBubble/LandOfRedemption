using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneLoad : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeInTime;
    public float fadeOutTime;
    private void Awake()
    {
        canvasGroup= GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);  
    }

    
}
