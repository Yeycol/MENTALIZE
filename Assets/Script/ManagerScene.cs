using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librer�a que permite usar los m�todos para controlar las escenas

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene shareMscen;
    public Canvas Gameview;//Referencia al Canvas llamada Ingame
    public Canvas Win;//Referencia al Canvas llamado Win
    public Canvas GameOver;//Referencia al Canvas llamado GameOver
    public Canvas Alert;// Referencia al Canvas llamado Alert
    //public Canvas Pause;//Referencia al Canvas de la Pausa
    public Canvas SceneAnima;//Referencia al canvas encargado de hacer la transici�n al pasar a otra escena
   

    private void Awake()
    {
        if (shareMscen == null)
        {
            shareMscen = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Es llamado cada vez que se inicia en una nueva escena
        ActiveCanvasAnimaScene();//M�todo encargado de activar el canvas de las transiciones
        
    }

    public void ActiveGameView()
    {
        //Activa el canvas Ingame
        Gameview.enabled = true;
    }
    public void OffGameview()
    {
        //Desactiva el Canvas Ingame 
        Gameview.enabled = false;
    }
    public void ActiveWin()
    {
        //Activa el Canvas Win 
        Win.enabled = true;
    }

    public void OffWin()
    {
        //Desactiva el Canvas Win 
        Win.enabled = false;
    }
    public void ActiveOver()
    {
        //Activa el Canvas del Game Over
        GameOver.enabled = true;
    }
    public void OffOver()
    {
        //Desactiva el Canvas del GameOver 
        GameOver.enabled = false;
    }
    public void LoadMenu()
    {
        //M�todo encargado de cargar la escena del menu
           
        AudioManager.shareaudio.Partida.mute = false;//Se reinicia el audio seg�n lo establecido en la condicional
       
        GameManager.shareInstance.BackToMenu();//Pasamos al estado de juego men�
        SceneManager.LoadScene(3);//Carga el men� de selecci�n de niveles 
        Time.timeScale = 1f;// Escala en la que pasa el tiempo, utilizados para efectos de c�mara lenta
        // Cuando timeScale es = 1 el tiempo pasa tan r�pido como el tiempo real
        // Cuando timeScale es = 0,5 el tiempo pasa 2 veces mas lento que el tiempo real
        // Cuando lo establecemos en cero actua como pausa
    }

    public void ActiveAlert()
    {
        //Activa el Canvas de la Alerta    
        Alert.enabled = true;
    }
    public void OffAlert()
    {
        //Desactiva el Canvas de la Alerta   
        Alert.enabled = false;
    }
   
    public void ActiveCanvasAnimaScene()
    {
        //M�todo encargado de habilar el canvas de las trancisiones entre escenas
        SceneAnima.enabled = true;
    }

    public void QuitApplication()
    {
        //M�todo encargado de salir del juego
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }



}
 
  
    
   

