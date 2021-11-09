using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que permite usar los métodos para controlar las escenas

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene shareMscen;//Variable de tipo stática utilizada para hacer un singleton o instancia compartida
    public Canvas Gameview;//Referencia al Canvas llamada Ingame
    public Canvas Win;//Referencia al Canvas llamado Win
    public Canvas GameOver;//Referencia al Canvas llamado GameOver
    public Canvas Alert;// Referencia al Canvas llamado Alert
    public Canvas SceneAnima;//Referencia al canvas encargado de hacer la transición al pasar a otra escena
    public Button[] ButtonsInterface;//Array que hace referencia a los botones de las trivias que sirven apra habilitar su interacción cuando se muestre por completo el Win y el Over del Canvas
    public Canvas[] Descripciones;//Referencia  a los canvas que tienen las descripciones d elos objetos comprados

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
        ActiveCanvasAnimaScene();//Método encargado de activar el canvas de las transiciones

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
        if (GameManager.shareInstance.currentgameState == GameState.InGame)//Se limita el del método a solo cuando el nombre de la escena sea igual a InGame
        {
            StartCoroutine(EnableButton());//Corrutina encargada de habilitar los botones de la interfaz Win
        }
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
        if (GameManager.shareInstance.currentgameState == GameState.InGame)//Se limita el del método a solo cuando el nombre de la escena sea igual a InGame
        {
            StartCoroutine(EnableButton());//Corrutina encargada de habilitar los botones de la interfaz Over
        }
    }
    public void OffOver()
    {
        //Desactiva el Canvas del GameOver 
        GameOver.enabled = false;
    }
    public void LoadMenu(int Nivel)
    {
        //Método encargado de cargar la escena del menu
        switch (Nivel)
        {
            case 3:
                AudioManager.shareaudio.Partida.mute = false;
                GameManager.shareInstance.BackToMenu();//Pasamos al estado de juego menú
                SceneManager.LoadScene(3);//Carga el menú de selección de niveles 
                Time.timeScale = 1f;// Escala en la que pasa el tiempo, utilizados para efectos de cámara lenta
                                    // Cuando timeScale es = 1 el tiempo pasa tan rápido como el tiempo real
                                    // Cuando timeScale es = 0,5 el tiempo pasa 2 veces mas lento que el tiempo real
                                    // Cuando lo establecemos en cero actua como pausa
                break;

            case 4:
                AudioManager.shareaudio.Partida.mute = false;//Se desmutea el audio de la música dle videojuego
                GameManager.shareInstance.BackToMenu();//Pasamos al estado de juego menú
                SceneManager.LoadScene(4);//Carga el menú de selección de niveles 
                Time.timeScale = 1f;// Escala en la que pasa el tiempo, utilizados para efectos de cámara lenta
                                    // Cuando timeScale es = 1 el tiempo pasa tan rápido como el tiempo real
                                    // Cuando timeScale es = 0,5 el tiempo pasa 2 veces mas lento que el tiempo real
                                    // Cuando lo establecemos en cero actua como pausa
                break;
        }


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
        //Método encargado de habilitar el canvas de las trancisiones entre escenas
        SceneAnima.enabled = true;
    }

    public void QuitApplication()
    {
        //Método encargado de salir del juego
#if UNITY_EDITOR //Si nos encontramos en el editor, apagamos el play del editor
        UnityEditor.EditorApplication.isPlaying = false;
#else //Si estamos en un dispositivo móvil quitamos la aplicación
         Application.Quit();
#endif
    }
    IEnumerator EnableButton()
    {
        //Esta corrutina se encarga de habilitar los botones solo hasta que las animaciones de la interfaz aparescan 
        //Evitando ir al menu o repetir partida sin ver los botones
        yield return new WaitForSeconds(1.5f);//Tiempo que se le asigna a la corrutina para hacer las acciones estañecidas
        ButtonsInterface[0].interactable = true;//Reset del Over
        ButtonsInterface[1].interactable = true;//Salir del Over
        ButtonsInterface[2].interactable = true;//Cerrar del Over
        ButtonsInterface[3].interactable = true;//Salir del Win
        ButtonsInterface[4].interactable = true;//Reintentar Win 
        ButtonsInterface[5].interactable = true;//Continuar Win 
        ButtonsInterface[6].interactable = true;//Cerrar Win
    }

    public void ActiveEventDescripciones(int IdObjectDecrip)
    {
        //Método que recibe por parámetro el entero de l botón de las descripciones d ela carta presionado
        switch (IdObjectDecrip)
        {
            case 0://Si es cero el entero enviado por referencia 
                Descripciones[0].enabled = true;   //Se habilitará el cambas del array ubicado en la posición cero
                break;

            case 1:
                Descripciones[1].enabled = true;
                break;

            case 2:
                Descripciones[2].enabled = true;
                break;
            case 3:
                Descripciones[3].enabled = true;
                break;
            case 4:
                Descripciones[4].enabled = true;
                break;
            case 5:
                Descripciones[5].enabled = true;
                break;
            case 6:
                Descripciones[6].enabled = true;
                break;
            case 7:
                Descripciones[7].enabled = true;
                break;
            case 8:
                Descripciones[8].enabled = true;
                break;
            case 9:
                Descripciones[9].enabled = true;
                break;
            case 10:
                Descripciones[10].enabled = true;
                break;
            case 11:
                Descripciones[11].enabled = true;
                break;
            case 12:
                Descripciones[12].enabled = true;
                break;
            case 13:
                Descripciones[13].enabled = true;
                break;
            case 14:
                Descripciones[14].enabled = true;
                break;
            case 15:
                Descripciones[15].enabled = true;
                break;
            case 16:
                Descripciones[16].enabled = true;
                break;
            case 17:
                Descripciones[17].enabled = true;
                break;
            case 18:
                Descripciones[18].enabled = true;
                break;
            case 19:
                Descripciones[19].enabled = true;
                break;
            case 20:
                Descripciones[20].enabled = true;
                break;
            case 21:
                Descripciones[21].enabled = true;
                break;
            case 22:
                Descripciones[22].enabled = true;
                break;
            case 23:
                Descripciones[23].enabled = true;
                break;
        }
    }

    public void DesactiveEventDescripciones(int IdObjectDecrip)
    {
        //Método que recibe por parámetro el entero de el botón de las descripciones de la carta presionada
        switch (IdObjectDecrip)
        {
            case 0://Si el número pasado por referencia en este caso es 0 desabilita el canvas del array guardado en esa posición 
                Descripciones[0].enabled = false;
                break;

            case 1:
                Descripciones[1].enabled = false;
                break;

            case 2:
                Descripciones[2].enabled = false;
                break;
            case 3:
                Descripciones[3].enabled = false;
                break;
            case 4:
                Descripciones[4].enabled = false;
                break;
            case 5:
                Descripciones[5].enabled = false;
                break;
            case 6:
                Descripciones[6].enabled = false;
                break;
            case 7:
                Descripciones[7].enabled = false;
                break;
            case 8:
                Descripciones[8].enabled = false;
                break;
            case 9:
                Descripciones[9].enabled = false;
                break;
            case 10:
                Descripciones[10].enabled = false;
                break;
            case 11:
                Descripciones[11].enabled = false;
                break;
            case 12:
                Descripciones[12].enabled = false;
                break;
            case 13:
                Descripciones[13].enabled = false;
                break;
            case 14:
                Descripciones[14].enabled = false;
                break;
            case 15:
                Descripciones[15].enabled = false;
                break;
            case 16:
                Descripciones[16].enabled = false;
                break;
            case 17:
                Descripciones[17].enabled = false;
                break;
            case 18:
                Descripciones[18].enabled = false;
                break;
            case 19:
                Descripciones[19].enabled = false;
                break;
            case 20:
                Descripciones[20].enabled = false;
                break;
            case 21:
                Descripciones[21].enabled = false;
                break;
            case 22:
                Descripciones[22].enabled = false;
                break;
            case 23:
                Descripciones[23].enabled = false;
                break;
        }
    }

}
 
  
    
   

