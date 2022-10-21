using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private SweetCounter counter;

    private void PickSweet()
    {
        counter.PickSweet();
    }
}
