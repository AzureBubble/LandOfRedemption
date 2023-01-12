using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player; // Player 的坐标

    private SpriteRenderer currentSprite; // 当前图像
    private SpriteRenderer playerSprite; // 玩家图像

    private Color color; // 颜色

    [Header("时间控制参数")]
    [SerializeField]
    private float activeTime; // 显示时间
    [SerializeField]
    private float activeStart; // 开始时间

    [Header("不透明度")]
    private float alpha; // 不透明度
    [SerializeField]
    [Range(0f,1f)]
    private float alphaSet; // 初始透明度
    [SerializeField]
    [Range(0f, 1f)]
    private float alphaMultiplier; // 不透明度削减值

    /// <summary>
    /// 启动时候执行一次
    /// </summary>
    private void OnEnable()
    {
        // 找到Player的坐标,当前图像，player图像
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        // 把当前需要记录的player参数全部获得
        currentSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;
        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 每帧削减透明度
        alpha *= alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        // 修改残影颜色
        currentSprite.color = color;
        // 当前时间 >= 开始时间 + 显示时间
        if (Time.time >= activeStart + activeTime)
        {
            // 返回对象池
            ShadowPlool.instance.ReturnPool(this.gameObject);
        }
    }
}
