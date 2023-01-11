using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlool : MonoBehaviour
{
    public static ShadowPlool instance; // 单例

    [SerializeField]
    private GameObject shadowPrefabs; // 残影预制体
    [SerializeField]
    private int shadowCount; // 残影数量

    // 使用队列存储残影
    private Queue<GameObject> shadowQueue = new Queue<GameObject>();

    private void Start()
    {
        // 初始化对象池
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    #region 填充对象池
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            // 生成一个 shadowPrefabs
            var newShadow = Instantiate(shadowPrefabs);
            // 当前物体为生成物体的父级对象
            newShadow.transform.SetParent(transform);

            // 取消启用，返回对象池
            ReturnPool(newShadow);
        }
    }
    #endregion

    #region 返回对象池
    public void ReturnPool(GameObject gameObject)
    {
        // 取消启用
        gameObject.SetActive(false);
        // 添加进队队尾
        shadowQueue.Enqueue(gameObject);
    }
    #endregion

    #region 从对象池中取出预制体
    public GameObject GetFromPool()
    {
        // 队列中元素不够，则再次填充
        if(shadowQueue.Count == 0)
        {
            FillPool();
        }
        // 从队首获得
        var outShadow = shadowQueue.Dequeue();
        // 设置启用，即调用自身脚本中的OnEnable函数
        outShadow.SetActive(true);
        return outShadow;
    }
    #endregion
}
