using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Analytics;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance {get; private set;}
    public int nivel;
    public Generador generador;
    public string instantiate_valor = "";
    public Transform colisionador;
    public int vidaJugadorMax = 20;
    public int vidaJugadorActual = 20;
    public int vidaEnemigaMax = 30;
    public int vidaEnemigaActual = 30;
    public Transform vida_jugador;
    public Transform vida_enemiga;
    public Transform cuadricula_jugador;
    public Transform cuadricula_enemiga;
    public Transform listado;
    public Transform boton_turno;
    public TextMeshProUGUI texto_turno;
    public Transform derrota;
    public Transform victoria;
    public Transform victoria_posta;
    public GameObject tutorial1;
    private bool tutorial1_hecho = false;
    public GameObject tutorial2;
    public bool turno_extra = false;
    public int max_num_enemigo = 10;
    public int turno = 0;

    public float time = 0;
    public bool stop_time = false;

    private int[] vidaEnemigaExtraNivel = new int[] {20, 70, 80, 100, 100};

    private void Awake() {
        instance = this;
    }

    public void ActualizarVidaEnemiga(int total){
        vidaEnemigaActual -= total;
        if(vidaEnemigaActual < 0 || vidaEnemigaActual == 0){
            vidaEnemigaActual = 0;
            boton_turno.GetComponent<Button>().interactable = false;
            for (int i = 0; i < listado.childCount; i++)
            {
                listado.GetChild(i).GetComponent<DragAndDrop>().enabled = false;
            }
        }
        StartCoroutine(EsperarEnemigo());
    }
    IEnumerator EsperarEnemigo(){
        yield return new WaitForSeconds(0.8f);
        vida_enemiga.GetChild(0).GetComponent<Image>().fillAmount = 100.0f / vidaEnemigaMax * vidaEnemigaActual / 100.0f;
        vida_enemiga.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaEnemigaActual.ToString()+"/"+vidaEnemigaMax.ToString();
        if(vidaEnemigaActual == 0){
            Debug.Log("terminaste nivel " + nivel + " en " + turno + " turnos.");
            Debug.Log("terminaste nivel " + nivel + " en " + (int)time + " segundos.");
            GameManager.instance.stop_time = true;
            turno = 0;
            time = 0;
            yield return new WaitForSeconds(1.2f);
            if(nivel == 6){
                victoria_posta.gameObject.SetActive(true);
                string nros = "";
                for (int i = 0; i < cuadricula_jugador.childCount; i++)
                {
                    nros = nros + cuadricula_jugador.GetChild(i).GetComponent<Trigger>().id;
                }
                Debug.Log("Terminaste el juego con esta combinación: "+ int.Parse(nros));
            }
            else{
                victoria.gameObject.SetActive(true);
            }
        }
    }

    public void ActualizarVidaJugador(int total){
        vidaJugadorActual -= total;
        if(vidaJugadorActual < 0){
            vidaJugadorActual = 0;
        }
        if(vidaJugadorActual > vidaJugadorMax){
            vidaJugadorActual = vidaJugadorMax;
        }
        StartCoroutine(EsperarJugador());
    }

    IEnumerator EsperarJugador(){
        yield return new WaitForSeconds(0.8f);
        vida_jugador.GetChild(0).GetComponent<Image>().fillAmount = 100.0f / vidaJugadorMax * vidaJugadorActual / 100.0f;
        vida_jugador.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaJugadorActual.ToString()+"/"+vidaJugadorMax.ToString();
        if(vidaJugadorActual == 0){
            Debug.Log("perdiste nivel " + nivel + " en " + turno + " turnos y el enemigo tenía " + vidaEnemigaActual + " de vida.");
            GameManager.instance.stop_time = true;
            turno = 0;
            time = 0;
            yield return new WaitForSeconds(1.2f);
            derrota.gameObject.SetActive(true);
        }
    }

    public void VueltaTurnoJugador(){
        GameManager.instance.stop_time = false;
        turno++;
        turno_extra = false;
        boton_turno.GetComponentInChildren<TextMeshProUGUI>().text = "TERMINAR TURNO";
        max_num_enemigo = 10;
        cuadricula_jugador.gameObject.SetActive(true);
        for (int i = 0; i < cuadricula_jugador.childCount; i++)
        {
            if(cuadricula_jugador.GetChild(i).gameObject.activeSelf){
                cuadricula_jugador.GetChild(i).localScale = new Vector3(1,1,1);
                cuadricula_jugador.GetChild(i).GetComponent<Trigger>().dejar_update = false;
                Transform tr = cuadricula_jugador.GetChild(i).GetChild(1);
                for (int x = 0; x < tr.childCount; x++)
                {
                    if(tr.GetChild(x).name == "N1" || tr.GetChild(x).name == "N2" || tr.GetChild(x).name == "N3" || tr.GetChild(x).name == "Total"){
                        tr.GetChild(x).GetComponentInChildren<TextMeshProUGUI>().text = "";
                        if(tr.GetChild(x).name != "Total"){
                            tr.GetChild(x).GetComponent<BoxCollider2D>().enabled = true;
                            tr.GetChild(x).GetComponent<DragAndDrop>().enabled = false;
                            tr.GetChild(x).GetComponentInChildren<TextMeshProUGUI>().color = new Color(0,0,0,0);
                        }
                    }
                }
            }
        }
        listado.gameObject.SetActive(true);
        generador.GenerarNumeros(nivel);
        boton_turno.gameObject.SetActive(true);
        if(tutorial1_hecho){
            boton_turno.GetComponent<Button>().interactable = true;
        }
        cuadricula_enemiga.gameObject.SetActive(false);
        texto_turno.text = "TU\nTURNO";
    }

    public void BotonDerrota(){
        vidaJugadorActual = vidaJugadorMax;
        vidaEnemigaActual = vidaEnemigaMax;
        vida_jugador.GetChild(0).GetComponent<Image>().fillAmount = 1;
        vida_jugador.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaJugadorActual.ToString()+"/"+vidaJugadorMax.ToString();
        vida_enemiga.GetChild(0).GetComponent<Image>().fillAmount = 1;
        vida_enemiga.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaEnemigaActual.ToString()+"/"+vidaEnemigaMax.ToString();
        VueltaTurnoJugador();
        victoria.gameObject.SetActive(false);
        derrota.gameObject.SetActive(false);
    }

    public void BotonVictoria(GameObject obj){

        int[] objetos_a_elegir = new int[2];
        int indice = 0;
        int siguiente_habilidad = 0;

        for (int i = 0; i < victoria.GetChild(0).childCount; i++)
        {
            if(victoria.GetChild(0).GetChild(i).gameObject.activeSelf){
                objetos_a_elegir[indice] = victoria.GetChild(0).GetChild(i).GetSiblingIndex();
                indice++;
            }
            if(indice == 2){
                siguiente_habilidad = i;
                break;
            }
        }

        if(siguiente_habilidad+2 <= victoria.GetChild(0).childCount){
            victoria.GetChild(0).GetChild(siguiente_habilidad+1).gameObject.SetActive(true);
            victoria.GetChild(0).GetChild(siguiente_habilidad+2).gameObject.SetActive(true);
        }

        Transform habilidad_elegida;

        if(obj.name == "Elegir1"){
            victoria.GetChild(0).GetChild(objetos_a_elegir[1]).gameObject.SetActive(false);
            habilidad_elegida = victoria.GetChild(0).GetChild(objetos_a_elegir[0]);
            victoria.GetChild(0).GetChild(objetos_a_elegir[0]).SetParent(cuadricula_jugador);
        }
        else{
            victoria.GetChild(0).GetChild(objetos_a_elegir[0]).gameObject.SetActive(false);
            habilidad_elegida = victoria.GetChild(0).GetChild(objetos_a_elegir[1]);
            victoria.GetChild(0).GetChild(objetos_a_elegir[1]).SetParent(cuadricula_jugador);
        }
        nivel++;
        Dictionary<string, object> habilidades_elegidas = new Dictionary<string, object>(){
            {"indice_habilidad", habilidad_elegida.GetComponent<Trigger>().id},
            {"indice_nivel", nivel}
        };

        Debug.Log("level start " + nivel + ", skill " + habilidad_elegida.GetComponent<Trigger>().id);

        //AnalyticsService.Instance.CustomData("habilidad_elegida_entre_nivel",habilidades_elegidas);

        cuadricula_enemiga.GetChild(nivel-2).gameObject.SetActive(false);
        cuadricula_enemiga.GetChild(nivel-1).gameObject.SetActive(true);
        vidaEnemigaMax += vidaEnemigaExtraNivel[nivel-2];
        vidaJugadorMax += 10;
        vidaJugadorActual = vidaJugadorMax;
        vidaEnemigaActual = vidaEnemigaMax;
        vida_jugador.GetChild(0).GetComponent<Image>().fillAmount = 1;
        vida_jugador.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaJugadorActual.ToString()+"/"+vidaJugadorMax.ToString();
        vida_enemiga.GetChild(0).GetComponent<Image>().fillAmount = 1;
        vida_enemiga.GetChild(1).GetComponent<TextMeshProUGUI>().text = vidaEnemigaActual.ToString()+"/"+vidaEnemigaMax.ToString();
        victoria.gameObject.SetActive(false);
        derrota.gameObject.SetActive(false);
        VueltaTurnoJugador();

    }

    public void TutoUnoHecho(){
        tutorial1.SetActive(false);
        if(!tutorial1_hecho){
            StartCoroutine(EsperarTexto());
        }
        tutorial1_hecho = true;
    }

    IEnumerator EsperarTexto(){
        yield return new WaitForSeconds(1.5f);
        tutorial2.SetActive(true);
        boton_turno.GetComponent<Button>().interactable = true;
    }

    public void TutoDosHecho(){
        tutorial2.SetActive(false);
    }

    public void AgregarNroListadoMenor(int original, int resultado){
        for (int i = 0; i < listado.childCount; i++)
        {
            if(listado.GetChild(i).gameObject.activeSelf){
                if(listado.GetChild(i).GetComponent<RawImage>().color == new Color (255,255,255,0)){
                    if(listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text == original.ToString()){
                        listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = resultado.ToString();
                        listado.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                        listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().color = new Color (0,0,0,255);
                        listado.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < listado.childCount; i++)
        {
            if(!listado.GetChild(i).gameObject.activeSelf){
                listado.GetChild(i).gameObject.SetActive(true);
                listado.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().color = new Color (0,0,0,255);
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = resultado.ToString();
                listado.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
                break;
            }
        }
    }

    public void AgregarNroListadoMayor(int resultado){
        for (int i = 0; i < listado.childCount; i++)
        {
            if(listado.GetChild(i).gameObject.activeSelf){
                if(listado.GetChild(i).GetComponent<RawImage>().color == new Color (255,255,255,0)){
                    if(listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text == resultado.ToString()){
                        listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = resultado.ToString();
                        listado.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                        listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().color = new Color (0,0,0,255);
                        listado.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < listado.childCount; i++)
        {
            if(!listado.GetChild(i).gameObject.activeSelf){
                listado.GetChild(i).gameObject.SetActive(true);
                listado.GetChild(i).GetComponent<RawImage>().color = new Color (255,255,255,255);
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().color = new Color (0,0,0,255);
                listado.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = resultado.ToString();
                listado.GetChild(i).GetComponent<DragAndDrop>().enabled = true;
                break;
            }
        }
    }

    public void TiempoTuto(){
        int tiempito = (int)time;
        Debug.Log("el tuto se hizo en " + tiempito.ToString() + " segundos.");
        Debug.Log("level start 1, skill 1");
        time = 0;
    }

    void Start()
    {
        //VueltaTurnoJugador();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop_time){
            time += Time.deltaTime;
        }
    }
}
