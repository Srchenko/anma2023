using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Colision : MonoBehaviour
{
    private int colisiones = 0;

    void OnTriggerEnter2D(Collider2D col){
        colisiones++;
    }

    void OnTriggerStay2D(Collider2D col){
        
    }

    void OnTriggerExit2D(Collider2D col){
        colisiones--;
    }
    
}
