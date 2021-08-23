
//Este script controla toda la l�gica de la trivia, es el que dirije lo que se tiene que hacer si sigues jugando o perdiste
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Librer�a que nos permitira controlar las scenas del videojuego
using UnityEngine.UI;

public class ManagerQuiz : MonoBehaviour
{
    [SerializeField] private Color correctColor = Color.black;// Variable que almacena el color verde 
    [SerializeField] private Color incorrectColor = Color.black;//Variable que almacena el color rojo
    [SerializeField] private float waitTime = 0.0f;// Esta variable indica el tiempo a esperar para la siguiente pregunta 

    private QuizDb g_quizDB = null; // Variable que almacena la lista de las preguntas
    private QuizUi g_quizUI = null; //Referencia a la interfaz del quizz
    private Scene Scene;// Variable privada de tipo escena que se utilizar� para controlar y condicionar con las escenas
    public Control_Button Control;// Se localiza un objeto por referencia en este caso la clase que nos permita activar y desativar la interacci�n con los botones
    private void Start()
    {
        g_quizDB = GameObject.FindObjectOfType<QuizDb>();//Localizando al Game Object que tiene el script de base de datos
        g_quizUI = GameObject.FindObjectOfType<QuizUi>();// Localizando al Game Object que tiene el script de QuizUi
        Scene = SceneManager.GetActiveScene();//GetActiveScene es un m�todo que nos permite obtener la escena activa actualmente  
        GameManager.shareInstance.StarGame();
        Nexquestion();// Lo llamamos para tener la primera pregunta
      

    }


    public void Nexquestion()
    {
        //M�todo encargado de indicar al constructor que debe mostrar las opciones, solo si estamos en estado de juego InGame
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
          
            Control.OffOutlineRed();
            Control.activebutton(); // Si estamos en modo de juego llamar� al m�todo que activa la interacci�n con los botones
            Contador.sumar();//LLama al metodo encargado de sumar el contador de la trivia

            g_quizUI.Construcman(g_quizDB.questionrandom(), GiveAnswer);
            //Este m�todo permitir� obtener la siguiente pregunta 

        }

    }

    private void GiveAnswer(OptionButton optionButton)
    {
        //M�todo que se va a llamar cuando el jugador seleecione un respuesta
        //ya sea correcto o incorrecto pasar� a la siguiente pregunta, se verificar� si la respuesta coorrecta o no 
        StartCoroutine(Answeroutine(optionButton));// Iniciamos la corrutina 
    }

    private IEnumerator Answeroutine(OptionButton optionButton)
    {

        /* En este m�todo estamos estableciendo una corrutina que pausa la ejecuci�n 
           para luego devolversela a Unity, la finalidad de este es que nos permita ver el efecto de colores si
        la respuesta no es correccta, sino aplicamos corrutina pasar� en un instante a la siguiente pregunta sin dejarnos ver efectos*/
        optionButton.SetColor(optionButton.Option.correct ? correctColor : incorrectColor);
        if (optionButton.Option.correct == false)
        {
            StartCoroutine(WaitforNave());
            AudioManager.shareaudio.Efectos[2].Play();
            Control.OutlineRed(0);
            
           


        }
        else if (optionButton.Option.correct == true)
        {
            AudioManager.shareaudio.Efectos[1].Play();
            Control.OutlineGreen();
        }

            /* El operador ternario es un operador que hace b�sicamente el trabajo de una estructura condicional,
             hace una evaluaci�n de una expresi�n y dependiendo el resultado nos asignar� un resultado u otro.
            Podemos verlo como un if �express� para algo sencillo.*/
        
        yield return new WaitForSeconds(waitTime); // Tiempo que damos para que las corrutina ejecute las acciones establecidas anteriormente
 
        if (optionButton.Option.correct == false) // Condicinal que evalua si la pregunta es correcta o no
        {
           
            Contador.ResetHealth();//M�todo encargado de restar las vidas cuando se seleccione la respuesta incorrecta
            
        }
        else if (optionButton.Option.correct == true)
        {
            Contador.PointsAdd();//M�todo encargado de sumar los puntos si se seleccion� la respuesta correcta
            Invoke("Nexquestion",1f);// Pasamos a la siguiente pregunta sin importar que esta sea incorrecta o correcta
        }

    }

    public void Next()
    {
        Nexquestion();
    }
    IEnumerator WaitforHeart()
    { 
        AnimaCon.ShareAnimation.StartHeart();
        yield return new WaitForSeconds(1f);
        AnimaCon.ShareAnimation.StopHeart();
        AnimaCon.ShareAnimation.DesactiveNaveError();
        Invoke("Nexquestion", 0.1f);
    }
    IEnumerator WaitforNave()
    {
        AnimaCon.ShareAnimation.ActiveNaveError();
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitforHeart());
    }
 

}
