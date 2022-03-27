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
    public Object[] ReferencesObject;
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
    public void LoadMenu( int ReferencesMenus)
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
    Descripciones[22].enabled = true;
    Descripciones[23].enabled = false;
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
        Descripciones[23].enabled = true;//Habilitamos el canvas de la tienda
        Descripciones[22].enabled = false;//Deshabilitamos el canvas del fondo de las Descripciones
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
 
  
    
   

