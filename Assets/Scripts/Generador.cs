using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Generador : MonoBehaviour
{
    public Transform listado;

    public void GenerarNumeros(int nivel){
        nivel++;
        for (int i = 0; i < listado.transform.childCount; i++)
        {
            if (i < nivel){
                listado.GetChild(i).gameObject.SetActive(true);
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1,10).ToString();
                listado.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                listado.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color (0,0,0,255);
                listado.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
            }
            else{
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = "";
                listado.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}