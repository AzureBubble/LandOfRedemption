using ClearSky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadLine : MonoBehaviour
{
    public PlayController player;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.isActivateSheild)
        {
            anim.SetTrigger("die");
            Invoke("LoadScene", 1.5f);
        }
            
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.isActivateSheild)
        {
            anim.SetTrigger("die");
            Invoke("LoadScene", 1.5f);
        }
            
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
