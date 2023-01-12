using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class ShadowControl : MonoBehaviour, PlayerInterfaces
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShadowPosition(Vector3 vector)
    {
        transform.position = vector;
    }

    public void ActivateSheild(float time)
    {
        ;
    }

    public void ToPosition(Vector3 vector)
    {
        ;
    }

    public void ActivateFlash(float distance)
    {
        ;
    }

}
