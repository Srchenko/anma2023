using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Habilidades : MonoBehaviour
{
    private GameObject[] cuadraditos;
    private GameObject resultado;
    private int total;
    
    public void TodasLasHabilidades(int id, GameObject[] cuadritos, GameObject resultadito){

        cuadraditos = cuadritos;
        resultado = resultadito;

        switch (id)
        {
            case 1:
                Habilidad1();
                break;
            case 2:
                Habilidad2();
                break;
            case 3:
                Habilidad3();
                break;
            case 4:
                Habilidad4();
                break;
            case 5:
                Habilidad5();
                break;
            case 6:
                Habilidad6();
                break;
            case 7:
                Habilidad7();
                break;
            case 10:
                Habilidad10();
                break;
            default:
                break;
        }

    }

    void Habilidad1(){
        total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) + int.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text);
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaEnemiga(total);
    }

    void Habilidad2(){
        total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) - 1;
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaJugador(-total);
    }

    void Habilidad3(){
        float division = float.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) / float.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text);
        total = Mathf.CeilToInt(division * 5);
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaEnemiga(total);
    }

    void Habilidad4(){
        float division = float.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) / 2;
        total = Mathf.CeilToInt(division);
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.AgregarNroListadoMenor(int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text), total);
    }

    void Habilidad5(){
        GameManager.instance.ActualizarVidaEnemiga(7);
    }

    void Habilidad6(){
        total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text);
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaJugador(-total);
        GameManager.instance.ActualizarVidaEnemiga(total);
    }

    void Habilidad7(){
        int resta = int.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text) - int.Parse(cuadraditos[2].GetComponentInChildren<TextMeshProUGUI>().text);
        total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) * resta;
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaEnemiga(total);
    }

    void Habilidad10(){
        total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text);
        resultado.GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.AgregarNroListadoMayor(total);
    }

    public bool ComprobarHabilidad(int id, GameObject[] cuadritos){
        cuadraditos = cuadritos;
        switch (id)
        {
            case 8:
                total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) + int.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text) * int.Parse(cuadraditos[2].GetComponentInChildren<TextMeshProUGUI>().text);
                if(total == 64){
                    GameManager.instance.boton_turno.GetComponentInChildren<TextMeshProUGUI>().text = "TURNO EXTRA";
                    GameManager.instance.turno_extra = true;
                    return true;
                }
                else{
                    return false;
                }
            case 9:
                total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) * int.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text);
                if(total == 81){
                    GameManager.instance.max_num_enemigo = 6;
                    return true;
                }
                else{
                    return false;
                }
            case 11:
                total = int.Parse(cuadraditos[0].GetComponentInChildren<TextMeshProUGUI>().text) * int.Parse(cuadraditos[1].GetComponentInChildren<TextMeshProUGUI>().text) * int.Parse(cuadraditos[2].GetComponentInChildren<TextMeshProUGUI>().text);
                if(total == 729){
                    GameManager.instance.ActualizarVidaEnemiga(729);
                    return true;
                }
                else{
                    return false;
                }
            default:
                break;
        }
        return true;
    }

}
