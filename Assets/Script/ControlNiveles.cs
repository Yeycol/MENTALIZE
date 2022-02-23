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
    public Color ShadowColor;// Hace referencia al color de la sombra de los textos 
    public static ControlNiveles shareLvl;// Variable que hace referencia a esta misma clase, servirá para hacerla una instancia compartida   
    private Scene Getscene;
    public Carga ReferCar;// Variable de tipo de clase carga que es encargada de almacenar un entero de la escena previa que debe ser cargado después de la pantalla de carga
    private void Awake()
    {
        if (shareLvl == null)
        {
            shareLvl = this;
        }
       cargaryguardar = GetComponent<Guardado>();//Como este script esta agregado al mismo objeto llamado control niveles, simplemente recuperamos su componente sin buscarlo
       Getscene = SceneManager.GetActiveScene();
    }
    private void Start()
    {
            if (Getscene.name == "SelectLevel (Trivias)")
            {
                //Solo si estamos en estado de juego pasaran las acciones establecidas dentro de este condicional 
                RefreshButton();// Método encargado de actualizar los botones desbloqueados 
                FalseButton();// Método encargado de desbloquear los botones bloqueados 
            }        
    }
    public void CambiarNivel(int Nivel)
    {
        //Este método recibe como parámetro un entero del indice de la escena a la que queremos dirigirnos 
        
        if (Nivel == 6 || Nivel == 2||Nivel==8||Nivel==7)// Si el número de la escena pasada por argumento es igual a los valores establecidos en el condicional
        {
            //Entonces llamará a la corrrutina encargada de cargar la escena del menú
            StartCoroutine(AnimatorTransitionSceneMenu(Nivel));
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
        AnimaCon.ShareAnimation.AnimationLis[8].SetTrigger("ExitScene");//Referencia al parámetro booleano que se reinicia desde controlador cuando se efectúa una trasicción 
       
        yield return new WaitForSeconds(0.9f);// Tiempo que se le da a la corrutina para realizar las acciones                                     
        SceneManager.LoadScene(Nivel);//Una vez que termina la corrutina se carga la siguiente escena pasada por parámetro
    }

    IEnumerator AnimatorTransitionSceneMenu(int Nivel)
    {
        AnimaCon.ShareAnimation.AnimationLis[8].SetTrigger("ExitScene");
        yield return new WaitForSeconds(0.9f);
        ManagerScene.shareMscen.LoadMenu(Nivel);
    }
     
    public void EventButtonPass(int referenceButton)
    {
        if (GameManager.shareInstance.currentgameState==GameState.Win)
        {
            GameManager.shareInstance.LoadPartyandGame(true);//Pasamos estado de juego a partida
            ReferCar.GuardarPreverLoad(referenceButton);//LLamamos al método encargado de guardar el entero que nos permitirá acceder a la siguiente escena
            CambiarNivel(4);//Por predeterminado iremos a la escena de carga antes de cargar la escena del juego
        } else
        {
            GameManager.shareInstance.LoadPartyandGame();//Pasamos estado de juego a partida
            ReferCar.GuardarPreverLoad(referenceButton);//LLamamos al método encargado de guardar el entero que nos permitirá acceder a la siguiente escena
            CambiarNivel(4);//Por predeterminado iremos a la escena de carga antes de cargar la escena del juego
        }
           
    
    }

public void DesbloquearNivel()
    {
        //Método llamado cuando se gana una partida
        if (LvlDesbloqueado<Lvlcurrent)//Entra a la condicional siempre y cuando el nivel desbloqueado sea menor que el nivel actual
        {
            //Se le asigna a la variable Lvl Desbloqueado el level actual que se tenga almacenado 
            LvlDesbloqueado = Lvlcurrent;
            cargaryguardar.Guardar();// Se llama al método de la clase Guardado, para que este guarde los niveles desbloqueados 

        }
    }
public void FalseButton()
    {
        //Método encargado de colocar los botones bloqueados
            for (int i = 0; i < Buttonslevel.Length; i++)//Bucle que repite su ciclo hasta la cantidad de botones almacenados en el array
            {
            //Dentro del  ciclo se evalua si los botones de la lista tienen desahabilitado la interacción entonces cargan una imagen de la carpeta de resources
            if (Buttonslevel[i].interactable == false)
                {
                Level[i].fontSize = 80;
                LevelShadow[i].effectDistance = new Vector2(5f, -0.5f);
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
            if (Buttonslevel[i].interactable == true)// Hacemos esto solo si los botones tienen la interacción habilitada
            {
                Imagelevel[i].sprite = Resources.Load<Sprite>("Sprite/Desbloqueado");//Le asignamos al array de componentes imágenes el cargado de una de las imágenes almacenada en resources
                Level[i].color = ActiveColor;// Cambiamos el color de la componente texto
                LevelShadow[i].effectColor = ShadowColor;
                Level[i].fontSize = 80;
                LevelShadow[i].effectDistance = new Vector2(8f,2f);
                Level[i].text = (i+1).ToString();
            }

        }
       
    }
}
