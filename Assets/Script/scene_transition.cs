using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_transition : MonoBehaviour
{
    public string nameScene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
        }
    }
}
