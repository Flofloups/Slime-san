using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public GameObject panelVic;
    public GameObject panelDef;
    public lifeplayer player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Finish")){
            panelVic.SetActive(true);
        }
    }
}
