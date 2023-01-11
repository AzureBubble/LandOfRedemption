using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPool : MonoBehaviour
{
    public static StackPool instance;

    [SerializeField]
    private GameObject shadowPrefabs; // Ô¤ÖÆÌå
    [SerializeField]
    private int shadowCount;

    private List<GameObject> shadowStack = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefabs);
            newShadow.transform.SetParent(transform);
            newShadow.SetActive(false);
            shadowStack.Insert(0,newShadow);
        }
    }

    public GameObject GetFromPool()
    {
        if (shadowStack.Count == 0)
        {
            FillPool();
        }
        if(shadowStack.Count > 0)
        {
            var outShadow = shadowStack[0];
            outShadow.SetActive(true);
            shadowStack.Remove(outShadow);
            return outShadow;
        }
        return null;
    }
}
