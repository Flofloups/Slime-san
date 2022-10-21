using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y <= -20) SceneManager.LoadScene("Level 1");
    }
}
