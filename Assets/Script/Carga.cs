/*Esta clase se encarga de mostrar la pantalla de carga, con mensajes aletorias claves
 que le servir� al jugador mas adelante en cualquier partida, cabe recalcar que antes de entrar
a cualquier nivel ser� llamado primero a esta clase y posteriormente se llamar� a la escena 
cargar a trav�s del paso del nivel por par�metro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Carga : MonoBehaviour
{
    public Text[] Frases;//Array que almacena las frases a mostrar por pantalla
    public TextMeshProUGUI Muestra;//Variable de tipo text que muestra las frases por GUI
    private Scene scene;
    public int Preverload;//Varaible que almacenar� el entero de la escena a cargar
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Carga")
        {
            CargarPreverLoad();//Este m�todo se encarga de cargar el Key del Playerpref que almacena la escena previa guardada
            
        }
    }
    void Start ()
    {
        if (scene.name == "Carga")
        {
            
            StartCoroutine(Wait_Intro(Preverload));//Una vez cargado se llama a la corrutina encargada de dar un tiempo para ver la pantalla de carga, pasamos a esta corrutina por par�metro el entero cargado
        }
    }
    public void GuardarPreverLoad(int PreviewLoad)
    {
       //M�todo que guarda el entero de la escena previa a cargar 
        PlayerPrefs.SetInt("Preview", PreviewLoad);
        
    }
    public void CargarPreverLoad()
    {
        //M�todo que carga el entero anteriormente guardado
        Preverload=PlayerPrefs.GetInt("Preview");
    }
   
    IEnumerator Wait_Intro(int PreviewLevel)
    {

            int index = Random.Range(0, Frases.Length);//Devuelve un entero aleatorio desde 0 hasta la cantidad de elementos del array
            Muestra.SetText(Frases[index].text);//Se iguala la variable tipo text de GUI con el indice de tipo entero obtenido de manera aleatoria
            //Se espera que se reproduzca el video
            yield return  new WaitForSeconds(1f);
            ControlNiveles.shareLvl.CambiarNivel(PreviewLevel);
    }
    
}
