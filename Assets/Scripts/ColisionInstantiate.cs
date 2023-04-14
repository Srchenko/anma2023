using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionInstantiate : MonoBehaviour
{
    
    private int colisiones = 0;

    void Start(){
        
    }

    void OnTriggerEnter2D(Collider2D col){
        colisiones++;
        GameManager.instance.colisionador = col.transform;
    }

    void OnTriggerStay2D(Collider2D col){
        
    }

    void OnTriggerExit2D(Collider2D col){
        colisiones--;
    }

    public int SinColision(){
        return colisiones;
    }

}
