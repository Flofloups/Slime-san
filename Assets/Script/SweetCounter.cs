using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private int pickedSweets;
    [SerializeField] private int maxSweets;

    public void PickSweet()
    {
        pickedSweets += 1;
        textMesh.SetText(pickedSweets + " / " + maxSweets);
    }
}
