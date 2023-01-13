using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolControl : MonoBehaviour
{
    public float coldTime=3.5f;
    public bool isUsed = false;
    public string MaskName;
    private float timer = 0;
    private bool isColding = false;
    private Image coldMask;
    // Start is called before the first frame update
    private void Start()
    {
        coldMask = transform.Find(MaskName).GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isUsed && isColding)
        {
            timer += Time.deltaTime;
            coldMask.fillAmount = (coldTime - timer) / coldTime;
            if(timer > coldTime)
            {
                isUsed = false;
                isColding=false;
                coldMask.fillAmount = 0;
                timer = 0;
            }
        }
    }

    public void SetIsUsed(bool Used)
    {
        isUsed= Used;
        if (isColding ==false)
        {
            isColding=true;
            timer = 0;
            coldMask.fillAmount = 1;
        } 
    }
 }
