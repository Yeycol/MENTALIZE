using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script está encargado de activar la interfaz de pausa en cualquier escena a partir de las referencias establecidas en el anterior script
public class ActivarOpciones : MonoBehaviour
{
    public ReferencesPause Pause;//Variable pública de tipo de la clase ReferencesPause, el cuál permitirá controlar su canvas desde cualquier otra escena
    public Canvas Pausa;//Variable de tipo canvas que hace referencia a la interfaz de pausa
    public static ActivarOpciones shareOp;//Variable estática de tipo de esta misma clase, la misma que servirá para crear una instancia compartida

    private void Awake()
    {
        if (shareOp == null)
        {
            shareOp = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Pause = GameObject.FindGameObjectWithTag("Pausa").GetComponent<ReferencesPause>();//Localizamos por etiqueta al objeto que tenga la etiqueta pause en este caso el AudioManager
        Pausa = GameObject.Find("Pause").GetComponent<Canvas>();//Una vez recuperado este objeto, se prosigue a localizar un objeto que tenga el nombre pause, para recuperar su componente canvas y poderlo controlar
    }

    public void ActivePause()
    {
        //Activa el Canvas que muestra las opciones de pausa
        Time.timeScale = 0f;// Escala en la que pasa el tiempo, utilizados para efectos de cámara lenta
        // Cuando timeScale es = 1 el tiempo pasa tan rápido como el tiempo real
        // Cuando timeScale es = 0,5 el tiempo pasa 2 veces mas lento que el tiempo real
        // Cuando lo establecemos en 0 actua como pausa
        Pausa.enabled = true;//Activamos el canvas localizado
        AudioManager.shareaudio.CargarSlider();//Método encargado de cargar las configuraciones almacenadas la última vez de la música
        AudioManager.shareaudio.CargarEfectos();//Método encargado de cargar las configuraciones almacenada la última vez de los efectos
        GameManager.shareInstance.PauseMenu();//Pasamos a modo de juego pausa
    }
    public void DesactivatePause()
    {
        //Desactiva el Canvas que muestra las opciones de pausa
        Time.timeScale = 1f;
        Pausa.enabled = false;//Desactivamos el canvas
        GameManager.shareInstance.StarGame();//Pasamos a estado de juego InGame por estar en Pausa
    }
   public void OffCanvasPause()
    {
        //La función de este método es pasar al menú sin establecer modo de juego Ingame
        Time.timeScale = 1f;
        Pausa.enabled = false;//Se desactiva el canvas de pausa
    }
   




}
