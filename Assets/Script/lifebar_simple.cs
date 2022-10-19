using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lifebar_simple : MonoBehaviour
{
    public TextMeshProUGUI txtLife;
    public lifeplayer life;

    void Update(){
        txtLife.text = "Health : " + life.vie.ToString(); // ahahahahaffs why is it so unnnnbelivableeeee
    }
}
