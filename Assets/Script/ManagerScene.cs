using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que permite usar los métodos para controlar las escenas
using TMPro;
public class ManagerScene : MonoBehaviour
{
    public static ManagerScene shareMscen;//Variable de tipo stática utilizada para hacer un singleton o instancia compartida
    public Canvas Gameview;//Referencia al Canvas llamada Ingame
    public Canvas Win;//Referencia al Canvas llamado Win
    public Canvas GameOver;//Referencia al Canvas llamado GameOver
    public Canvas Alert;// Referencia al Canvas llamado Alert
    public Canvas SceneAnima;//Referencia al canvas encargado de hacer la transición al pasar a otra escena
    public Canvas[] Descripciones;//Referencia  a los canvas que tienen las descripciones d elos objetos comprados
    public Canvas ListadeLogros;//Variable de tipo Canvas que hace referencia al canvas que permitirá mostrar la lista de logros conseguidos
    public Canvas PortafolioDeVersiones;//Variable de tipo Canvas que hace referencia al canvas que mostrará La interfaz del pritafolio de Versiones
    public TextMeshProUGUI NoteOfVer;//Variable de tipo Tesh Mesh Pro que mostrará el texto d elas descripciones d ela nota de versión 
    public Image[] ReferencesButtonPor;//Array de tipo imagen que hacen referencia a las imágenes de los botones de Portafolio de Versiones 
    public GameObject[] ReferencesObject;//Array de tipo objeto que almacenara los objetos con las componentes imagenes para que estas expandan las imágenes y permitan una mejor visualización 
    public GameObject[] MarcoDesripciones;//Array que contiene los objetos a deshabilitar para mostrar la imagen Expandida
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
    public void LoadMenu(int ReferencesMenus)
    {
        GameManager.shareInstance.BackToMenu();//Pasamos aa estado de menú
        SceneManager.LoadScene(ReferencesMenus);//Cargamos directamente la escena pasada por parámetro
    }

    public void ActiveAlert()
    {
        //Activa el Canvas de la Alerta    
        Alert.enabled = true;
        AnimaCon.ShareAnimation.AlertActive();
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
    public void CanvasListadeLogros()
    {
        //Método encargado de habilitar el canvas que mostrará los elementos GUI de los logros
        Descripciones[22].enabled = true;
        Descripciones[23].enabled = false;
        ListadeLogros.enabled = true;
    }
    public void DesactivateCanvasListadeLogros()
    {
        //Método encargado de desahabilitar el canvas de la Lista de Objetos
        Descripciones[22].enabled = false;
        Descripciones[23].enabled = true;
        ListadeLogros.enabled = false;
    }
    public void ActivatePortafolioDeVersiones() {
        //Método que habilita la interfaz del Portafolio de Versiones
        Descripciones[22].enabled = true;//Activa el Canvas del fondo
        Descripciones[23].enabled = false;//Desactiva el canvas de la Interfaz para evitar aumento de batches
        NumForText(0);//Pasamos cero al método para que establezca el texto predeterminado
        PortafolioDeVersiones.enabled = true;
    }
    public void DesactivatePortafolioDeVersiones()
    {
        //Método que desabilita la interfaz del Portafolio de Versiones
        PortafolioDeVersiones.enabled = false;
        Descripciones[22].enabled = false;
        Descripciones[23].enabled = true;
    }
    public void NumForText(int NumOfbutton)
    {
        ReferencesButtonPor[NumOfbutton].color = Color.black;//Según el entero pasado por parámetro establecemos la imagen del botón en negro
        //Método encargado de establecerle el texto a la variable TextMeshPro que mostrará las notas de versiones
        if (NumOfbutton == 0)//Si es el caso de ser cero hará lo siguiente
        {
            //En el caso de  que el entero pasado por parámetro sea O
            ReferencesButtonPor[1].color = Color.white;//Establecemos en blanco los botones que no han sido presionados, dependiendo de la posición pasado por parámetro
            ReferencesButtonPor[2].color = Color.white;
            NoteOfVer.SetText("2022/03/1 — Ver 1.0 " + "\n● Se cambiaron los colores de las cartas, a unos mas vivos." +
                "\n● Se arreglo el bug de las animaciones, al resetear la partida.");
        }
        else if (NumOfbutton == 1)
        {
            ReferencesButtonPor[0].color = Color.white;
            ReferencesButtonPor[2].color = Color.white;
            NoteOfVer.SetText("Sin Fecha — Ver 2.0");
        }
        else if (NumOfbutton == 2)
        {
            ReferencesButtonPor[0].color = Color.white;
            ReferencesButtonPor[1].color = Color.white;
            NoteOfVer.SetText("Sin Fecha — Ver 3.0");
        }
    }
    public void ActiveEventDescripciones(int IdObjectDecrip)
    {

        Descripciones[23].enabled = false;//Desahabilitamos el canvas de la tienda, para reducir los batches
        Descripciones[22].enabled = true;//Habilitamos el fondo de las interfaces de Descripciones
        Descripciones[IdObjectDecrip].enabled = true;   //Se habilita el canvas de acuerdo al valor Pasado por parámetro
        //Método que recibe por parámetro el entero de l botón de las descripciones d ela carta presionado 
    }

    public void DesactiveEventDescripciones(int IdObjectDecrip)
    {
        //Método que recibe por parámetro el entero de el botón de las descripciones de la carta presionada
        Descripciones[23].enabled = true;//Habilitamos el canvas de la tienda
        Descripciones[22].enabled = false;//Deshabilitamos el canvas del fondo de las Descripciones
        Descripciones[IdObjectDecrip].enabled = false;//Se desactiva el Canvas de acuerdo al Valor pasado por parámetro
    }
    public void PassExpansiónImage(int Expand)
    {
        //Método encargado de pasar por parámetro un entero que indicará que imagen de las cartas deben expandirse
        StartCoroutine(WaitforCarta(Expand));//Iniciliamos la corrutina encarga de mostrar la imagen epandida de la carta

    }
    IEnumerator WaitforCarta(int RexEpand)
    {
        MarcoDesripciones[22].SetActive(true);//Habilitamos el objeto en este caso el ojo que indicará el tiempo limite para ver la carta
        MarcoDesripciones[RexEpand].SetActive(false);//Deshabilitamos el marco de acuerdo al valor entero pasado por parámetro
        ReferencesObject[RexEpand].SetActive(true);//Se habilita el objeto de acuerdo al valor entero pasado por referencia 
        yield return new WaitForSeconds(5f);
        StartCoroutine(WaitforView(RexEpand));//Llamamos a la otra corrutina que da tiempo para que se ejecuten las animaciones del ojo y luego se deshabiliten los objetos
    }
    IEnumerator WaitforView(int RexEpand)
    {
        AnimaCon.ShareAnimation.ActivateTimeForView();//Llamamos al método encargado de activar la animación que indica el límite para ver la carta
        yield return new WaitForSeconds(5f);
        ReferencesObject[RexEpand].SetActive(false);//Se deshabilita el objeto de acuerdo al valor entero pasado por referencia
        MarcoDesripciones[22].SetActive(false);//Deshabiltamos el icono de ojo 
        MarcoDesripciones[RexEpand].SetActive(true);//Se vuelve  activar la Marco de la Carta que contiene las descripciones
    }

}





