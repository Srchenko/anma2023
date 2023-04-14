using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turno : MonoBehaviour
{
    public Transform cuadricula_jugador;
    public Transform cuadricula_enemiga;
    public Transform listado;
    public TextMeshProUGUI texto_turno;
    public HabilidadesEnemigas he;

    public void Desaparecer(){
        if(GameManager.instance.turno_extra){
            GameManager.instance.VueltaTurnoJugador();
            return;
        }
        GameManager.instance.TutoDosHecho();
        cuadricula_jugador.gameObject.SetActive(false);
        listado.gameObject.SetActive(false);
        gameObject.SetActive(false);
        cuadricula_enemiga.gameObject.SetActive(true);
        texto_turno.text = "TURNO\nENEMIGO";
        he.HacerHabilidadEnemiga(GameManager.instance.nivel-1, cuadricula_enemiga.GetChild(GameManager.instance.nivel-1).GetChild(0));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
