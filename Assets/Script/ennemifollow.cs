using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemifollow : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Animator animatotor;
    private float dazedTime;
    private ennemiAttack ennemiAttack;
    private SpriteRenderer samnite_sprite;
    private Transform colleague;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        colleague = GameObject.FindGameObjectWithTag("Ennemi").GetComponent<Transform>();
        animatotor = gameObject.GetComponent<Animator>();
        ennemiAttack = GetComponent<ennemiAttack>();
        samnite_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x - transform.position.x > 0){
            samnite_sprite.flipX = false;
        }
        if(target.position.x - transform.position.x < 0){
            samnite_sprite.flipX = true;
        }
        if(Vector2.Distance(transform.position, target.position) > 1.5){
            animatotor.SetBool("chasing", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        // if((Vector2.Distance(target.position, colleague.position) <= 1.5) && (Vector2.Distance(transform.position, target.position) > 2)){
        //     animatotor.SetBool("chasing", false);
        //}
        else{
            animatotor.SetBool("chasing", false);
        }
    }
}

