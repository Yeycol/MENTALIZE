/*Esta clase se encarga de mostrar la pantalla de carga, con mensajes aletorias claves
 que le servir� al jugador mas adelante en cualquier partida, cabe recalcar que antes de entrar
a cualquier nivel ser� llamado primero a esta clase y posteriormente se llamar� a la escena 
cargar a trav�s del paso del nivel por par�metro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Carga : MonoBehaviour
{
    public Text[] Frases;//Array que almacena las frases a mostrar por pantalla
    public Text Muestra;//Variable de tipo text que muestra las frases por GUI
    private Scene scene;
    public int Preverload;
    void Awake()
    {
    
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Carga")
        {
            AudioManager.shareaudio.Partida.mute = false;//Al despertarse, se desmutea la m�sica del juegos
            CargarPreverLoad();
        }
    }
    void Start ()
    {
        if (scene.name == "Carga")
        {
            
            StartCoroutine(Wait_Intro(Preverload));
        }
    }
    public void GuardarPreverLoad(int PreviewLoad)
    {
        PlayerPrefs.SetInt("Preview", PreviewLoad);
        
    }
    public void CargarPreverLoad()
    {
        Preverload=PlayerPrefs.GetInt("Preview");
    }
   
    IEnumerator Wait_Intro(int PreviewLevel)
    {
        if (scene.name == "Carga")
        {
            int index = Random.Range(0, Frases.Length);//Devuelve un entero aleatorio desde 0 hasta la cantidad de elementos del array
            Muestra.text = Frases[index].text;//Se iguala la variable tipo text de GUI con el indice de tipo entero obtenido de manera aleatoria
        }
            //Se espera que se reproduzca el video
        yield return new WaitForSeconds(7f);
            ControlNiveles.shareLvl.CambiarNivel(PreviewLevel);//Se llama al m�todo encargado de cambiar la escena de menu a partir del pasado por parametro de un entero
    }

}
