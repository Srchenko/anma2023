using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    private RectTransform rectTransform;
    private GameObject duplicado;
    public Transform padre;
    public string id = "";
    public Transform array;

    void Start(){
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.instance.instantiate_valor = "";
        
        duplicado = Instantiate(gameObject,transform.position,transform.rotation,padre);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color (0,0,0,0);
        
        if(id == "Listado"){
            transform.GetComponent<RawImage>().color = new Color (255,255,255,0);
        }
        else{
            transform.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(duplicado.GetComponent<Rigidbody2D>());
            Destroy(duplicado.GetComponent<BoxCollider2D>());
            Destroy(duplicado.GetComponent<Colision>());
            duplicado.GetComponent<RawImage>().material = null;
        }
        duplicado.GetComponent<RawImage>().color = new Color (255,255,255,255);
        duplicado.AddComponent<BoxCollider2D>().size = new Vector2(25,25);
        duplicado.GetComponent<BoxCollider2D>().isTrigger = true;
        duplicado.AddComponent<ColisionInstantiate>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        duplicado.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.instance.instantiate_valor = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text;
        if (duplicado.GetComponent<ColisionInstantiate>().SinColision() == 0){
            if(id == "Listado"){
                transform.GetComponent<RawImage>().color = new Color (255,255,255,255);
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color (0,0,0,255);
            }
            else{
                for (int i = 0; i < array.childCount; i++)
                {
                    if (array.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text == GameManager.instance.instantiate_valor){
                        if (array.GetChild(i).GetComponent<RawImage>().color == new Color (255,255,255,0))
                        {
                            array.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                            array.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color (0,0,0,255);
                            array.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
                            transform.GetComponent<BoxCollider2D>().enabled = true;
                            transform.GetComponent<DragAndDrop>().enabled = false;
                            GameManager.instance.colisionador.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().color = new Color(0,0,0,0);
                            break;
                        }
                    }
                }
            }
            Destroy(duplicado);
        }
        else{
            Destroy(duplicado);
            GameManager.instance.colisionador.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.colisionador.GetComponent<DragAndDrop>().enabled = true;
            transform.GetComponent<DragAndDrop>().enabled = false;
            GameManager.instance.colisionador.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.instantiate_valor;
            GameManager.instance.colisionador.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().color = new Color(0,0,0,255);
            GameManager.instance.colisionador.parent.GetComponentInParent<Trigger>().NumerosPuestos();
        }
    }
}
