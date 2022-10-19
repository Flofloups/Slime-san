using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemiLife : MonoBehaviour {

    public float Evie = 3;
    private Animator anim;
    //public killCountHandler killCountHandler;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        //killCountHandler = GetComponent<killCountHandler>();
        //killCountHandler.enemyKilled = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Evie <= 0) {
            anim.SetTrigger("dead");
            if (GetComponent<ennemiPatrol>() != null) {
                GetComponent<ennemiPatrol>().enabled = false;
            }
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            //killCountHandler.enemyKilled += 1;
            Destroy(this.gameObject);
        }
    }

    public void takeDamage (float damage) {
        Evie = Evie - damage;
        anim.SetTrigger("ouch");
    }
}
