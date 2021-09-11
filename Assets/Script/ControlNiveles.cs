/*Este script esta encargado de controlar el desbloqueo de niveles al ganar una partida 
 Si se da el caso de que hemos perdido o repetido la partida esta no desbloqueaará ningun nivel*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlNiveles : MonoBehaviour
{
    static public int LvlDesbloqueado;// Variable estática de tipo entero encargada de almacenar el nivel desbloqueado 
    public int Lvlcurrent;//Variable de tipo entero que almacena el nivel actual
    public Button[] Buttonslevel;//Array de componentes de tipo botón que serviran para localizar estos objetos por referencia
    public Image[] Imagelevel;//Array de componentes de tipo Imagen que serviran para localizar estos objetos por referencia
    public Text[] Level;//Array de componentes de tipo texto que serviran para localizar estos objetos por referencia
    public Shadow[] LevelShadow;
    Guardado cargaryguardar;// Variable de tipo de la clase Guardado, que servirá para hacer referencia a la clase encargada de guardar los niveles y monedas conseguidos 
    public Color ActiveColor;// Color que establecemos desde el editor de Unity para el texto de los botones
    public static ControlNiveles shareLvl;// Variable que hace referencia a esta misma clase, servirá para hacerla una instancia compartida   
    private void Awake()
    {
        if (shareLvl == null)
        {
            shareLvl = this;
        }
       cargaryguardar = GetComponent<Guardado>();//Como este script esta agregado al mismo objeto llamado control niveles, simplemente recuperamos su componente sin buscarlo
    }
    private void Start()
    {
        if (GameManager.shareInstance.currentgameState == GameState.menu)
        {
        //Solo si estamos en estado de juego pasaran las acciones establecidas dentro de este condicional
           cargaryguardar.Guardar();// Se llama al método de la clase Guardado, para que este guarde los niveles desbloqueados  
            RefreshButton();// Método encargado de actualizar los botones desbloqueados 
            FalseButton();// Método encargado de desbloquear los botones bloqueados 
        }
    }
    public void CambiarNivel(int Nivel)
    {
        //Este método recibe como parámetro un entero del indice de la escena a la que queremos dirigirnos 
        
        if (Nivel == 3)// Si el número de la escena pasada por argumento es igual a 3 
        {
            //Entonces llamará al método encargado de cargar la escena del menú
            StartCoroutine(AnimatorTransitionSceneMenu());
        }
        else
        {
            //Sino llamará a la corrutina que pausa la ejecución y ejecuta las acciones establecidas en su interior
            //Nota: En la corrutina se pasa por parámetro el entero del nivel al que se quiere ir 
            StartCoroutine(AnimatorTransitionScene(Nivel));
        }
    }
    IEnumerator AnimatorTransitionScene(int Nivel)
    {
        // Se pausa la ejecución para ir a la corrutina y hacer las accciones establecidas en su interior
        if (GameManager.shareInstance.currentgameState == GameState.Win)
        {
            //En caso de que al entrar a la corrutina se este en modo de Ganado desactiva el canvas para transicionar a la siguiente escena
            ManagerScene.shareMscen.OffWin();
        }
        else if (GameManager.shareInstance.currentgameState == GameState.GameOver)
        {
            //En caso de que al entrar a la corrutina se este en modo de Ganado desactiva el canvas para transicionar a la siguiente escena
            ManagerScene.shareMscen.OffOver();
        }
        AnimaCon.ShareAnimation.AnimationLis[9].SetTrigger("ExitScene");//Referencia al parámetro booleano que se reinicia desde controlador cuando se efectúa una trasicción 
        yield return new WaitForSeconds(0.9f);// Tiempo que se le da a la corrutina para realizar las acciones
        SceneManager.LoadScene(Nivel);// Una vez que termina la corrutina se carga la siguienre escena pasada por parámetro
    }

    IEnumerator AnimatorTransitionSceneMenu()
    {
        AnimaCon.ShareAnimation.AnimationLis[9].SetTrigger("ExitScene");
        yield return new WaitForSeconds(0.9f);
        ManagerScene.shareMscen.LoadMenu();
    }


public void DesbloquearNivel()
    {
        //Método llamado cuando se gana una partida
        if (LvlDesbloqueado<Lvlcurrent)//Entra a la condicional siempre y cuando el nivel desbloqueado sea menor que el nivel actual
        {
            //Se le asigna a la variable Lvl Desbloqueado el level actual que se tenga almacenado 
            LvlDesbloqueado = Lvlcurrent;
         
        }
    }
public void FalseButton()
    {
        //Método encargado de colocar los botones bloqueados
            for (int i = 0; i < Buttonslevel.Length; i++)//Bucle que repite su ciclo hasta la cantidad de botones almacenados en el array
            {
            Level[i].fontSize = 80;
            LevelShadow[i].effectDistance = new Vector2(5f, -0.5f);
            //Dentro del  ciclo se evalua si los botones de la lista tienen desahabilitado la interacción entonces cargan una imagen de la carpeta de resources
            if (Buttonslevel[i].interactable == false)
                {
                 Imagelevel[i].sprite = Resources.Load<Sprite>("Sprite/Bloqueado");//Le asignamos al array de componentes imágenes el cargado de una de las imágenes almacenada en resources
                 Level[i].text = "?";//Le asignamos a los arrays tipo texto el signo de interrogación
                }
            }
    }
    public void RefreshButton()
    {
        //Método encargado de habilitar los botones que nos llevan a los niveles desbloqueados
        for (int i = 0; i <LvlDesbloqueado+1 ; i++)//Bucle que repite su ciclo solo hasta la cantidad de niveles a desbloquear
        {
            Buttonslevel[i].interactable = true;//Se habilita los botones para poder cambiar de nivel (escena)
            Imagelevel[i].sprite = Resources.Load<Sprite>("Sprite/Desbloqueado");//Le asignamos al array de componentes imágenes el cargado de una de las imágenes almacenada en resources
            if (Buttonslevel[i].interactable == true)// Hacemos esto solo si los botones tienen la interacción habilitada
            {
                Level[i].color = ActiveColor;// Cambiamos el color de la componente texto
                Level[i].fontSize = 80;
                LevelShadow[i].effectDistance = new Vector2(8f,2f) ;
                Level[0].text = "1";
                Level[1].text = "2";
                Level[2].text = "3";
                Level[3].text = "4";
                Level[4].text = "5";
                Level[5].text = "6";
                Level[6].text = "7";
                Level[7].text = "8";
                Level[8].text = "9";
                Level[9].text = "10";
                Level[10].text = "11";
                Level[11].text = "12";
                Level[12].text = "13";
                Level[13].text = "14";
                Level[14].text = "15";
                Level[15].text = "16";
                Level[16].text = "17";
                Level[17].text = "18";
                Level[18].text = "19";
                Level[19].text = "20";
            }

        }
       
    }
}
