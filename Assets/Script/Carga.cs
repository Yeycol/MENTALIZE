using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Carga : MonoBehaviour
{
    // Start is called before the first frame update
    public float wite2 = 3f;//Variable de tipo flotante que especifica el tiempo para que la corrutina ejecute sus acciones
    public Text[] Frases;//Array que almacena las frases a mostrar por pantalla
    public Text Muestra;//Variable de tipo text que muestra las frases por GUI
    void Awake()
    {
        AudioManager.shareaudio.Partida.mute = false;//Al despertarse, se desmutea la música del juego
    }
    void Start()
    {
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de Carga
        StartCoroutine(Wait_Intro());//Se llama a la corrutina
    }
    IEnumerator Wait_Intro()
    {
        int index = Random.Range(0, Frases.Length);//Devuelve un entero aleatorio desde 0 hasta la cantidad de elementos del array
        Muestra.text = Frases[index].text;//Se iguala la variable tipo text de GUI con el indice de tipo entero obtenido de manera aleatoria
        //Se espera que se reproduzca el video
        yield return new WaitForSeconds(wite2);
            ControlNiveles.shareLvl.CambiarNivel(3);//Se llama al método encargado de cambiar la escena de menu a partir del pasado por parametro de un entero
    }

}
