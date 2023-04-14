using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HabilidadesEnemigas : MonoBehaviour
{
    private Transform obj;
    private Transform obj_padre;
    private bool achicar = false;
    private float esperar = 0;
    private int x = 0;

    public void HacerHabilidadEnemiga(int child, Transform trans){

        obj = trans;
        obj_padre = trans.parent;
        obj_padre.localScale = new Vector3 (1,1,1);
        x = GameManager.instance.max_num_enemigo;
        switch (child)
        {
            case 0:
                StartCoroutine(Habilidad1());
                break;
            case 1:
                StartCoroutine(Habilidad2());
                break;
            case 2:
                StartCoroutine(Habilidad3());
                break;
            case 3:
                StartCoroutine(Habilidad4());
                break;
            case 4:
                StartCoroutine(Habilidad5());
                break;
            case 5:
                StartCoroutine(Habilidad6());
                break;
            default:
                break;
        }

    }

    IEnumerator Habilidad1(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        n--;
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        GameManager.instance.ActualizarVidaJugador(n);
        achicar = true;
    }

    IEnumerator Habilidad2(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        int o = Random.Range(1,x);
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = o.ToString();
        yield return new WaitForSeconds(0.8f);
        n = (n*2) - o;
        if(n < 0){
            n = 0;
        }
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        GameManager.instance.ActualizarVidaJugador(n);
        achicar = true;
    }

    IEnumerator Habilidad3(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        n += 5;
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        GameManager.instance.ActualizarVidaJugador(n);
        achicar = true;
    }

    IEnumerator Habilidad4(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        n = n * 2;
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        GameManager.instance.ActualizarVidaJugador(n);
        achicar = true;
    }

    IEnumerator Habilidad5(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        int m = Random.Range(1,x);
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = m.ToString();
        yield return new WaitForSeconds(0.8f);
        int o = Random.Range(1,x);
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = o.ToString();
        yield return new WaitForSeconds(0.8f);
        int total = n + m + o;
        obj.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaJugador(total);
        achicar = true;
    }
    
    IEnumerator Habilidad6(){
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1.2f);
        int n = Random.Range(1,x);
        obj.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
        yield return new WaitForSeconds(0.8f);
        int m = Random.Range(1,x);
        obj.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = m.ToString();
        yield return new WaitForSeconds(0.8f);
        int total = n * m - 15;
        if(total < 0){
            total = 0;
        }
        obj.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = total.ToString();
        GameManager.instance.ActualizarVidaJugador(total);
        achicar = true;
    }

    void FixedUpdate()
    {
        if(achicar){
            if(obj_padre.localScale.x < 0.001f){
                achicar = false;
                esperar = 0;
                if(GameManager.instance.vidaJugadorActual == 0){
                    return;
                }
                GameManager.instance.VueltaTurnoJugador();
            }
            esperar += Time.fixedDeltaTime;
            if(esperar > 0.8f){
                obj_padre.localScale = Vector3.Lerp (obj_padre.localScale, new Vector3(0,0,1), Time.fixedDeltaTime*5);
            }
            return;
        }
    }

}
