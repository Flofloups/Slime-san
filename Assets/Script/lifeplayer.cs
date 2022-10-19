using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeplayer : MonoBehaviour {

    public GameObject panelDef;
    private control_V3 control_V3;// this is how you call
    private Rigidbody2D rb;
    public int vie = 10;
    public int vieMax = 10;
    private Vector2 spawnPoint;
    public control_V3 player; 
    public GameObject DefMenu;
    //public GameObject bloodEffect;

    //public Animator camAnim;    



    void Start(){
        if (PlayerPrefs.GetInt("playerLIFE") == 0){
            PlayerPrefs.SetInt("playerLIFE", 10);
        }
        vieMax = 10;
        vie = vieMax;
        control_V3 = GetComponent<control_V3>(); // this is how you call
    }

    // Update is called once per frame
    void Update(){
        // if (vie <= 0){
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
        if (vie <= 0){
            panelDef.SetActive(true);
            //if (Time.timeScale == 0){
            //    Time.timeScale = 1;
            //    DefMenu.SetActive(false);
            //}    
            if (Time.timeScale == 1){
                Time.timeScale = 0;
                DefMenu.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

    public void takeDamage (int damage1){
        vie = vie - damage1;
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //camAnim.SetTrigger("shake");
    }

    public void heal (int heal){
        vieMax = PlayerPrefs.GetInt("playerLIFE");
        vie = vie + heal;
        if (vie > 10){
            vie = 10;
        }
    }

    void OnTriggerEnter2D(Collider2D truc){
        if (truc.tag == "checkPoint"){
            spawnPoint = truc.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D truc){
        // if (truc.gameObject.tag == "Ennemi"){
        //     vie -= 1;
        //     if (player.skin.flipX){
        //         control_V3.rb.velocity = new Vector2(8, 0);
        //     } 
        //     else{
        //         control_V3.rb.velocity = new Vector2(-8, 0);
        //     }
        // }
        if (truc.gameObject.tag == "Collectible"){
            vie += 5;  
        }
    }
}
