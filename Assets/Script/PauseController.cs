using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            SetPause();
        }
    }
    private bool _isPause;
    public GameObject PauseMenu;
    public void SetPause(){
        Debug.Log(Time.timeScale);
        if (Time.timeScale == 0){
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
        else if (Time.timeScale == 1){
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
