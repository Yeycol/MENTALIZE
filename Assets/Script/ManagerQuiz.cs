
/*Esta clase esta encargada de controlar el pasado de la siguiente pregunta,
activación de animaciones de acuerdo al contestado correcto e incorrecto de
las preguntas, también controla el llamado de colores en los botones dependiendo de la 
eventualidad*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManagerQuiz : MonoBehaviour
{
    public Color correctColor = Color.black;// Variable que almacena el color verde 
    public Color incorrectColor = Color.black;//Variable que almacena el color rojo
    public float waitTime = 0.0f;// Esta variable indica el tiempo a esperar para la siguiente pregunta 

    public QuizDb g_quizDB = null; // Variable de tipo de la clase QuizDb, la cual contiene una lista de las preguntas
    public QuizUi g_quizUI = null; //Variable de tipo de la clase QuizUi, la cual contiene el método para contruir las opciones en los botones
    public Control_Button Control;// Se localiza un objeto por referencia en este caso la clase que nos permita activar y desativar la interacción con los botones
    private void Start()
    {
        AudioManager.shareaudio.Efectos[15].Play();//Reproducimos la música destinada para las trivias
        AudioManager.shareaudio.Efectos[15].loop = true;//Establecemos en bucle la música 
        RealTimeAnimation.ShareRealTimeAnimator.RamdomIndex();//Llamamos al método encargado de generar nuevos Index de las animaciones en trivias 
        GameManager.shareInstance.StarGame();//Establecemos como modo en partida
        Nexquestion();// Lo llamamos para tener la primera pregunta
    }


    public void Nexquestion()
    {
        //Método encargado de indicar al constructor que debe mostrar las opciones, solo si estamos en estado de juego InGame
        if (GameManager.shareInstance.currentgameState == GameState.InGame || GameManager.shareInstance.currentgameState == GameState.Alert)
        {
            RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[1].gameObject.SetActive(false);//Se desahabilita la aniamción del objeto NaveCorrect 
            RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[0].gameObject.SetActive(false);//Se desahabilita la animación del objeeto NaveError
            RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = false;//Desahabilitamos el canvas de la clase RealTimeAnimations
            Contador.sharecont.IntroAnimation = false;//Desahabilitamos el tiempo lento cuando una animación está activa
            Control.OffOutlineRed();//Se llama al método encargado de restablecer el color de los Outline originales
            Control.activebutton(); // Si estamos en modo de juego llamará al método que activa la interacción con los botones
            Contador.sumar();//LLama al metodo encargado de sumar el contador de la trivia
            g_quizUI.Construcman(g_quizDB.questionrandom(), GiveAnswer);
            //Este método permitirá obtener la siguiente pregunta 

        }

    }

    private void GiveAnswer(OptionButton optionButton)
    {
        //Contador.sharecont.EventEndLevel();//Llamamos al método que nos permitirá saber si realmente ya hemos terminadoel nivel
        RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = true;//Habilitamos el Canvas de la clase RealTimeAnimation
        Contador.sharecont.IntroAnimation = true;//Habilitamos el tiempo lento para cuando una animación está activa
        //Método que se va a llamar cuando el jugador seleecione un respuesta
        //Ya sea correcto o incorrecto pasará a la siguiente pregunta, se verificará si la respuesta coorrecta o no 
        StartCoroutine(Answeroutine(optionButton));// Iniciamos la corrutina 
    }

    private IEnumerator Answeroutine(OptionButton optionButton)
    {
        /* En este método estamos estableciendo una corrutina que pausa la ejecución 
           para luego devolversela a Unity, la finalidad de este es que nos permita ver el efecto de colores si
        la respuesta no es correccta, sino aplicamos corrutina pasará en un instante a la siguiente pregunta sin dejarnos ver efectos*/
        optionButton.SetColor(optionButton.Option.correct ? correctColor : incorrectColor);
        /* El operador ternario es un operador que hace básicamente el trabajo de una estructura condicional,
            hace una evaluación de una expresión y dependiendo el resultado nos asignará un resultado u otro.
           Podemos verlo como un if “express” para algo sencillo.*/
        //Condicionales que evalua si el boleano del botón seleccionado es true o false
        if (optionButton.Option.correct == false && Contador.sharecont.contador <= Contador.sharecont.maxvaluecontador)
        {
            //En el caso de haber seleccionado una respuesta incorrecta, se hace las siguientes acciones
            RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[0].gameObject.SetActive(true);//Habilitamos el objeto de la aniamación NaveError
            AudioManager.shareaudio.Efectos[2].Play();//Reproducimos el sonido de Answer Bad
            StartCoroutine(WaitforNave());//Se llama a la corrutina encargada de pausar la ejecución para ver la animación cuando la repsuesta es incorrecta 
        }
        else if (optionButton.Option.correct == true && Contador.sharecont.contador<= Contador.sharecont.maxvaluecontador)
        {
            //En el caso de que se haya seleccionado una respuesta correcta, se hacen las siguientes acciones
            RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[1].gameObject.SetActive(true);//Habilitamos el objeto de la animación NaveCorrect
            AudioManager.shareaudio.Efectos[1].Play();//Reproducimos el sonido de Answer Good
            Control.OutlineGreen();// Llamamos al método encargado de poner el outline de los botones para que los coloque en verde 
            StartCoroutine(WaitforNaveCorrect());// Se llama a la corrutina encargada de pausar la ejecución para poder visualizar la animación cuando la respuesta es correcta
        }



        yield return new WaitForSeconds(waitTime); // Tiempo que damos para que las corrutina ejecute las acciones establecidas anteriormente
    }
    IEnumerator NextError()
    {
        //Método que tiene la finalidad de ser usado para pasar a la siguiente pregunta
        // Pasamos a la siguiente pregunta sin importar que esta sea incorrecta o correcta
        AnimaCon.ShareAnimation.StopHeart();//Se llama al método encargado de reproducir la animación  del corazón restaurandoses
        yield return new WaitForSeconds(0.8f);
        //Contador.sharecont.IntroAnimation = false;
        Nexquestion();
    }

    IEnumerator NextCorrect()
    {
        //Método que tiene la finalidad de ser usado para pasar a la siguiente pregunta 
        // Pasamos a la siguiente pregunta sin importar que esta sea incorrecta o correcta
        RealTimeAnimation.ShareRealTimeAnimator.DesactiveNaveCorrect();//Se llama al método encargado de reproducir la animación de la nave saliendo de escena
        yield return new WaitForSeconds(1.8f);
        //Contador.sharecont.IntroAnimation=false;//Luego de que las acciones establecidas en la corrutina se cumpla se le indica al comportamiento Event Time de la Clase Contador que ya no se tengan en cuenta la cantidad de frames para ser llamado  
        Nexquestion();
    }

    IEnumerator WaitforHeart()
    {
        AnimaCon.ShareAnimation.StartHeart();//Se llama al método encargado de reproducir la animación del corazón rompiendose
        RealTimeAnimation.ShareRealTimeAnimator.DesactiveNaveError();//Se llama al método encargado de reproducir la animación de la Nave Saliendo de escena
        yield return new WaitForSeconds(1.1f);//Tiempo que se le otroga a la corrutina para que haga las acciones anteriores
        Contador.ResetHealth();//Método encargado de restar las vidas cuando se seleccione la respuesta incorrecta
        /*Para que lo anterior se pueda visualizar, se da un tiempo para invocar al método encargado
         de llamar al ´método que´pasa a la siguiente pregunta*/
            StartCoroutine(NextError());//Corrutina que se llama para dar un tiempo a la reproducción de las animaciones
    }
    IEnumerator WaitforNave()
    {
        RealTimeAnimation.ShareRealTimeAnimator.ActiveNaveError();//Se llama al método encargado de reproducir la animación de la entrada de la nave y disparo 
        yield return new WaitForSeconds(1.3f);//Tiempo que se le otorga a la corrutina para que haga las acciones anteriores
        /*Luego de que se ejecuten las acciones de la corrutina, se llama a otra corrutina encargada de 
         reproducir la animación del corazón y de pasar a la siguiente pregunta*/
        StartCoroutine(WaitforHeart());//Corrutina llamada para dar tiempo a la aniamción de la desfragmentación del corazón
    }
    IEnumerator WaitforNaveCorrect()
    {
        Contador.PointsAdd();//Método encargado de sumar los puntos si se seleccioná la respuesta correcta
        RealTimeAnimation.ShareRealTimeAnimator.ActiveNaveCorrect();//Se llama al método encargado de reproducir la animación de la entrada de la nave para cuando se responde bien 
        yield return new WaitForSeconds(1f);//Tiempo que se le otroga a la corrutina para que haga las acciones anteriores
          //Condicional que evalua si el valor es menor igual a 5, permitiendo limitar el llamado de el método que pasa a la siguiente pregunta        
            StartCoroutine(NextCorrect());//Corrutina que se llama para dar un tiempo a la reproducción de las animaciones
    }
}


