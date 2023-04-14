using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject[] cuadraditos;
    public GameObject resultado;
    public int id;
    private DragAndDrop[] dad;
    public bool dejar_update;
    private int contador;
    public Habilidades habilidades;
    private float esperar;

    void Start()
    {
        dejar_update = false;
        contador = 0;
        esperar = 0;
        dad = new DragAndDrop[cuadraditos.Length];
        for (int i = 0; i < cuadraditos.Length; i++)
        {
            dad[i] = cuadraditos[i].GetComponent<DragAndDrop>();
        }
    }

    void HacerHabilidad(){
        for (int i = 0; i < dad.Length; i++)
        {
            dad[i].enabled = false;
        }
        habilidades.TodasLasHabilidades(id, cuadraditos, resultado);
    }

    public void NumerosPuestos(){
        for (int i = 0; i < dad.Length; i++)
        {
            if (dad[i].enabled == true){
                contador++;
            }
        }

        if(contador == dad.Length){
            if(resultado != null){
                dejar_update = true;
                esperar = 0;
                HacerHabilidad();
                GameManager.instance.TutoUnoHecho();
            }
            else{
                if(dad.Length == 1){
                    dejar_update = true;
                    esperar = 0;
                    HacerHabilidad();
                }
                else{
                    if(habilidades.ComprobarHabilidad(id, cuadraditos)){
                        dejar_update = true;
                        esperar = 0;
                        for (int i = 0; i < dad.Length; i++)
                        {
                            dad[i].enabled = false;
                        }
                    }
                }
            }
        }

        contador = 0;
    }

    void FixedUpdate()
    {
        if(dejar_update){
            if(transform.localScale.x < 0.001f){
                return;
            }
            esperar += Time.fixedDeltaTime;
            if(esperar > 0.8f){
                transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(0,0,1), Time.fixedDeltaTime*5);
            }
            return;
        }
    }
}
