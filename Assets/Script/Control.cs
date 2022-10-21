using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 8f;
    public float dashSpeed = 10f;
    public float dashLenght = 5f;
    private float dashTarget;
    public float longueurCheckJump = 1.1f;
    [HideInInspector]public bool canJump;
    private float dashrecharge;

    public Rigidbody2D rb;
    public SpriteRenderer skin;
    //private Animator animatotor;
    private Collider2D monCollider;

    [HideInInspector]public bool hooking;
    private bool dashing;

    private RaycastHit2D hit;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        monCollider = gameObject.GetComponent<Collider2D>();
        //animatotor = gameObject.GetComponent<Animator>();

        rb.freezeRotation = true;
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
        dashrecharge = 0;
    }

    void Update() {
        jumpCheck();
        lookCheck();

        if (Input.GetButtonDown("Jump") && canJump) {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump) {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && !hooking && dashrecharge <= 0) {
            dashing = true;
            dashrecharge = 10;
            if (!skin.flipX) {
                dashTarget = transform.position.x + dashLenght;
            }
            if (skin.flipX) {
                dashTarget = transform.position.x - dashLenght;
            }
        }

        else{
            dashrecharge -= 0.01f;
        }

        if (dashing) {
            if (!skin.flipX) {
                rb.velocity = new Vector2(dashSpeed, 0);
                if (transform.position.x > dashTarget) {
                    dashing = false;
                }
                
            }
            if (skin.flipX) {
                rb.velocity = new Vector2(-dashSpeed, 0);
                if (transform.position.x < dashTarget) {
                    dashing = false;
                }
            }
        }

        if (!hooking && !dashing) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }

        if (hooking) {
            rb.AddForce (new Vector2 (Input.GetAxisRaw("Horizontal") * speed/2, 0));
        }

        // if (animatotor != null) {
        //     animatotor.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        //     animatotor.SetFloat("velocityY", rb.velocity.y);
        // }
    }

    void jumpCheck() {
        hit = Physics2D.Raycast(transform.position, -Vector2.up, (monCollider.bounds.extents.y + Mathf.Abs(monCollider.offset.y) * transform.localScale.y) * longueurCheckJump);

        Debug.DrawRay(transform.position, -Vector2.up * (monCollider.bounds.extents.y + Mathf.Abs(monCollider.offset.y) * transform.localScale.y) * longueurCheckJump, Color.red);
        if (hit && !hit.collider.isTrigger){
            canJump = true;
            //animatotor.SetBool("jump", false);
        } else{
            canJump = false;
            //animatotor.SetBool("jump", true);
        }
    }

    void lookCheck() {
        if (Input.GetKeyDown(KeyCode.D)){
            skin.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.A)){
            skin.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            skin.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            skin.flipX = true;
        }
    }
    // public void ApplyDamage(float damage){
    //     life -= damage;
    //     FindObjectOfType<Life>().SetLife(life);
    // }
}
