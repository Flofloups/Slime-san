using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemiAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public int damage1;
    private Transform target1;
    public float distance1;

    private Animator animatotor;

    private void Start()
    {
        startTimeBtwAttack = 5f;
        target1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animatotor = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        distance1 = Vector2.Distance(transform.position, target1.transform.position);
        Debug.Log("Step0");
        if(timeBtwAttack <= 0){
            Debug.Log("Step1");
            if(distance1 <= attackRange){
                Debug.Log("Step2");
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
                Debug.Log("Step3");
                timeBtwAttack = 5f;
                Debug.Log("Step4");
                   // this thing, this thing is fucked, don't touch it, don't try to do anything with it, fuck it.
                Debug.Log("Step5");
                playerToDamage[1].GetComponent<lifeplayer>().takeDamage(damage1);
                Debug.Log("Step5.5");
                animatotor.SetBool("attacking", true);
                Debug.Log("Step6");
                
            }
        }
        else{
            Debug.Log("Step7");
            animatotor.SetBool("attacking", false);
            timeBtwAttack -= 0.008f;
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}