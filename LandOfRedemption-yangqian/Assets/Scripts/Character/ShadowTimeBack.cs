using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTimeBack : MonoBehaviour
{
    private Transform player; // Player 坐标

    private SpriteRenderer currentSprite; // 当前图像
    private SpriteRenderer playerSprite; // 玩家图像

    private Color color;

    [Header("时间控制参数")]
    [SerializeField]
    private float activeTime; // 显示时间
    [SerializeField]
    private float activeStart; // 开始时间

    [Header("不透明度")]
    [SerializeField]
    private float alpha; // 不透明度
    [SerializeField]
    [Range(0f, 1f)]
    private float alphaSet; // 初始透明度
    [SerializeField]
    [Range(0f, 1f)]
    private float alphaMultiplier; // 透明度削减

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        currentSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;
        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= activeTime*alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        currentSprite.color = color;
        if (Time.time > activeTime + activeStart)
        {
            // 删除栈堆里的对象
            Destroy(gameObject);
        }
    }
}
