using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterfaces
{
    // Start is called before the first frame update


    public void ActivateSheild(float time)
    {
        Debug.Log("¼¤»î»¤¶Ü" + time.ToString() + "Ãë");
        // to do
               
        // end to do
    }
    public bool IsDie();
    public bool IsHurt();
}
