using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    public GameObject randombs;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            randombs.GetComponent<lifeplayer>().heal(5);
            Destroy(this.gameObject);
        }
    }
}