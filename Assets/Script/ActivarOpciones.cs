using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script est� encargado de activar la interfaz de pausa en cualquier escena a partir de las referencias establecidas en el anterior script
public class ActivarOpciones : MonoBehaviour
{
    public Canvas Pausa;//Variable de tipo canvas que hace referencia a la interfaz de pausa
    public static ActivarOpciones shareOp;//Variable est�tica de tipo de esta misma clase, la misma que servir� para crear una instancia compartida
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
        Pausa = GameObject.FindGameObjectWithTag("Pausa").GetComponent<Canvas>();//Una vez recuperado este objeto, se prosigue a localizar un objeto que tenga el nombre pause, para recuperar su componente canvas y poderlo controlar
    }

    public void ActivePause()
    {
        //Activa el Canvas que muestra las opciones de pausa
        Time.timeScale = 0f;// Escala en la que pasa el tiempo, utilizados para efectos de c�mara lenta
                            // Cuando timeScale es = 1 el tiempo pasa tan r�pido como el tiempo real
                            // Cuando timeScale es = 0,5 el tiempo pasa 2 veces mas lento que el tiempo real
                            // Cuando lo establecemos en 0 actua como pausa
        Pausa.enabled = true;//Activamos el canvas localizado
        AudioManager.shareaudio.CargarSlider();//M�todo encargado de cargar las configuraciones almacenadas la �ltima vez de la m�sica
        AudioManager.shareaudio.CargarEfectos();//M�todo encargado de cargar las configuraciones almacenada la �ltima vez de los efectos
        GameManager.shareInstance.PauseMenu();//Pasamos a modo de juego pausa
    }
    public void DesactivatePause()
    {
        //Desactiva el Canvas que muestra las opciones de pausa
        Time.timeScale = 1f;
        AudioManager.shareaudio.Efectos[3].UnPause();//Se desmutea el efecto TimeEnd
        AudioManager.shareaudio.Efectos[6].UnPause();//Despausamos Efecto Disparo
        AudioManager.shareaudio.Efectos[7].UnPause();//Despausamos Efecto Llegada Nave
        AudioManager.shareaudio.Efectos[8].UnPause();//Despausamos Efecto Salida nave
        AudioManager.shareaudio.Efectos[9].UnPause();//Despausamos el Efeco Roto
        AudioManager.shareaudio.Efectos[10].UnPause();//Despausamos el Efeco Abducir Nave
        Pausa.enabled = false;//Desactivamos el canvas
        if (Contador.sharecont.scene.name == "Tienda")//Se evalua si el nombre de la escena es Tienda
            GameManager.shareInstance.BackToMenu();//Se pasar� el estado de juego a Menu
        else//Sino es el caso se pasar� a en partida
            GameManager.shareInstance.StarGame();//Se pasar� estado de juego en partida  
    }
   public void OffCanvasPause()
    {
        //La funci�n de este m�todo es pasar al men� sin establecer modo de juego Ingame
        Time.timeScale = 1f;
        Pausa.enabled = false;//Se desactiva el canvas de pausa
    }
   




}
