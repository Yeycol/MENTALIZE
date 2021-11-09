/*Esta clase esta encargada de controlar la ganada, perdida de partidas, cargado y guardado de monedas
 en las diferentes eventualidades, tambien se encarga del control del tiempo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librer�a que nos permitira controlar las scenas del videojuego

public class Contador : MonoBehaviour
{

    public static Contador sharecont;
    public int contador; // Variable de tipo entero que almacena los n�meros del contador de la trivia 
    public int vidas; //Variable de tipo entero que almacena la cantidad de vidas
    public int puntos;//Variable p�blica de tipo entero en la cual se almacenar�n los puntos que se van obteniendo
    public Text textcont;// Variable de tipo Texto utilizada para que adquiera los valores de contador
    public Text text_health;// Variable de tipo Texto para que adquiera el valor de las vidas
    public Text time_ui;//Variable de tipo texto para que adquiera  el valor del cron�metro
    public Text moneda_ui;// Variable de tipo texto que va adquirir las monedas que van siendo ganadas
    public Text points_ui;// Variable de tipo texto que va adquirir los puntos que van siendo ganados
    public float time; // Variable p�blica que almacena el tiempo l�mite para resolver la trivia
    public float currentTime;// Variable que almacenar� el tiempo establecido en el time
    public string range;// Variable de tipo string que almacenar� el limite de cantidad de preguntas en la trivia
    public int value_Points;//Variable de tipo entera que almacena el valor de los puntos segun en el nivel que estemos
    public int value_moneda;// Variable de tipo p�blica en el que se almacena el valor de la moneda en determinado nivel
    public int moneda;// Variable entera que almacena la cantida de monedas obtenidas
    public Scene scene;// Variable privada de tipo escena que se utilizar� para controlar y condicionar con las escenas
    public Guardado GuardadoMonedas;//Referencia a la clase Guardado
    public int pointsinv;//Sirve para almacenar los puntos invicibles que no son guardados por la clase Guardado, solo sirven para ser utilizado en condicionales
    private int numFrames = 5;//Variable de tipo entero que almacena los frames a los que queremos que se llamen a determinados m�todos en el Update
    public string Oneintro = "No";//Booleano que permitir� controlar la cantidad de veces que debera dar la carta dar tiempo extra
    public float TimeReferences;//Variable que hace referencia al tiempo extra que otroga la carta equipada
    public int monedawin;//Variable tipo entera que alamacenar� la cantidad de monedas conseguidas en determinado nivel
    public int monedaextra;//Variable que ser� utilizada para almacenar la cantidad de monedas extras que debe dar la carta
    public Text UI_WinText;//Variable tipo texto que mostrar� la cantidad de monedas ganadas en el Canvas Win
    void Awake()
    {
        if (sharecont == null)
        {
            sharecont = this;
        }
        GuardadoMonedas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Localizaci�n de la clase guardado a traves d ela busqueda del objeto ControlNiveles
    }
    void Start()
    {
        /*
         Solo usar en caso de querer resetear los valores almacenados en los key de los player prefs
        PlayerPrefs.DeleteKey("ActivarEvento");
        PlayerPrefs.DeleteKey("ExtraMoneda");
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("Live");*/
        GuardadoMonedas.CargarMonedas();// Se carga las monedas para poderlas visualizar la cantidad de monedas que se tiene
        scene = SceneManager.GetActiveScene();//GetActiveScene es un m�todo que nos permite obtener la escena activa actualmente
        CargarEquipament();//Se llama al m�todo encargado de Cargar vidas extras y el tiempo extra pasado por referencia etc
        InicializarDatosInterfaz();//Cuando inicia el juego debemos imprimer el valor que hayan adquirido las variables al iniciar el juego     
    }
    private void FixedUpdate()
    {
        //Aqui se esta evaluando a cada frame, si estamos en en modo juego ejecutar� las instrucciones establecidas en las condicionales
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
           // if (Time.frameCount % numFrames == 0)
            //{
                EventTime();//M�todo que se encarga de aumentar el tiempo del cron�metro y hacer las acciones establecidas cuando se cumplan ciertas eventualidades
           // }
            if (Time.frameCount % numFrames == 0)
            {
                //La condicional es verdadera cuando el restante de la divisi�n entre el n�mero total de frames desde el inicio del juego y el n�mero de frames es igual a 0
                EventEndLevel();//M�todo encargado de controlar la l�gica de ganado, guardado y desbloquedo de niveles
            }
        }
    }

    public void InicializarDatosInterfaz()
    {
        if (scene.name != "SelectLevel(Trivias)" || scene.name != "Tienda") //Solo si estamos en escenas distintas a las mencionadas
        {
            textcont.text = contador.ToString() + range;//Se imprime el contador y el rango de la cantidad de preguntas que habr� en el nivel
            text_health.text = vidas.ToString();//Imprime las vidas en la interfaz
            currentTime = time;//Se establece que el tiempo actual es igual a el tiempo establecido en la variable publica
        }
        if (scene.name != "SelectLevel(Trivias)")//Solo si la escena esdistinta a la establecida
        {
            moneda_ui.text = moneda.ToString();//Imprime las monedas en la interfaz
        }
        if (scene.name != "Tienda")//Solo si el nombre de la escena es distinto
        {
            points_ui.text = puntos.ToString();//Se imprime los puntos globales en GUI
        }
    }
    public void EventTime()
    {
        if (currentTime > 0)
        {
            //Si el tiempo establecido es mayor que cero se debera disminuir el tiempo establecido
            currentTime -= Time.deltaTime;//Time.deltatime es un m�todo que nos permite saber el tiempo transcurrido desde el �ltimo frame, es decir iremos decrementando la variable current time de acuerdo al tiempo que haya transcurrido
                                          //Si el juego va a 60 frames, update jecutar� 60 frames por segundo, donde el tiempo transcurrido es 1seg
            time_ui.text = (int)currentTime + "seg";//Aqui asignamos de manera explicita el valor de la variable current time a la variable tipo texto apra que esta se muestre en la UI
        }

        if ((int)currentTime == 0)
        {
            //Si el tiempo es igual a 0 se hacen las siguientes acciones
            AnimaCon.ShareAnimation.DesactivateRedTime();//M�todo de la clase AnimaCon, encargada de desahabilitar la animaci�n del Reloj Rojo
            GameManager.shareInstance.GameOver();//Pasamos a estado de juego Game Over
        }

        if ((int)currentTime == 10)
        {
            //Si el tiempo es igual a 10 se hacen las siguientes acciones

            AnimaCon.ShareAnimation.ActiveRedTime();// Se habilita la animaci�n de Reloj Rojo
            AudioManager.shareaudio.Efectos[3].Play();//Iniciamos el Audio del efecto del Alarma cuando se acaba el tiempo
        } else if ((int)currentTime == 7)//Si el tiempo actual es igual a 7 e entra a la condicional
        {
            if (Oneintro=="Si")//Dentro de este if anidado se evalua si la variable booleana OneIntro es verdadera
            {
                StartCoroutine(WaitExtraTime());//Si es el caso de ser verdad se llama a la corrutina encargada de habilitar y deshabilitar las animaciones
                Oneintro = "No";//La primera vez que entre en este condicional debe ser la �nica vez, puesto que se pasar� false para que en el siguiente frame no haya la posibilidad de repetir la eventualidad
            }
        }

       
    }

    public void EventEndLevel()
    {
        switch (scene.name)
        {
            case "Level 1":
                //Esta secci�n evalua si el contador de las trivias son iguales que 6 estas apliquen las condicionales en el interior
                if (contador == 6)
                {

                    //Si es el caso esta aplicar� esta condicional donde se llamar� a un m�todo si se consigue los puntos requeridos
                    if (pointsinv == 5)
                    {
                        /*Si los puntos invicibles son iguales hacen las acciones establecidas, los puntos que son guradados 
                         solo son aumentados siempre y cuando lo permitan las condicionales del m�todo PointsAdd */
                        /*Los puntos invicibles nacen despu�s de darse cuenta que al limitar la ganada de puntos
                         usados para guardar llegan a 5 al repetir el nivel estos no aumentaban su valor y rompian
                        la l�gica impidia ganar o perder la partida, por ello los puntos invicibles aunque se repita
                        la partida estos no son limitados ni guardados*/
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        monedawin += monedaextra;//Se incrementa el valor de la moneda siempre y cuando la moneda extra tenga un valor a incrementar
                        UI_WinText.text = monedawin.ToString();//Se muestra por interfaz de canvas Win el valor total de monedas ganadas
                        moneda += monedaextra;//Se incrementa el valor de la moneda global
                        moneda_ui.text = moneda.ToString();//Se imprime su valor para actualizar los datos
                        GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas y puntos solo si se gana
                        ControlNiveles.shareLvl.DesbloquearNivel();//M�todo encargado de desbloquear los niveles ganados
                    }
                    else
                    {
                        AudioManager.shareaudio.Partida.mute = true;//Se mutea el volumen de la m�sica
                        GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                    }
                }
                break;

            case "Level 2":
                if (contador == 11)
                {

                    //Si es el caso esta aplicar� esta condicional donde se llamar� a un m�todo si se consigue los puntos requeridos
                    if (puntos == 10 || pointsinv == 10)
                    {
                        GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas solo si se gana
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        ControlNiveles.shareLvl.DesbloquearNivel();//M�todo encargado de desbloquear los niveles ganados
                    }
                    else
                    {
                        GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                    }
                }
                break;

        }
    }

    public static void sumar()
    {
        /* Este m�todo es encargado de aumentar en uno al contador
         cada vez que se pase a la siguiente pregunta.*/

        sharecont.contador++;
        if (sharecont.contador <= 5)//Se imprime el valor de la variable contador solo hasta que esta se igual a 5, para evitar que imprima una de mas, que no es necesario para al cantidad de preguntas
        {
            sharecont.textcont.text = sharecont.contador.ToString() + sharecont.range;
        }
    }
    public void resetcont()
    {
        //M�todo encargado de resetear la Trivia
        switch (scene.name)
        {
            /*Al pasar a una nueva escena las variables y configuraciones que se estaban ejecutando
             en la anterior escena estas se restablecen dando el efecto de reseteo de partida*/
            case "Level 1":
                ControlNiveles.shareLvl.CambiarNivel(1);//Llamamos a la escena del Nivel 1
                AudioManager.shareaudio.Efectos[3].Stop();//Detenemos el efecto de Alarma, cuando se acaba el tiempo
                AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva  la animaci�n del Relo digital en In Game
                break;
            case "Level 2":
                ControlNiveles.shareLvl.CambiarNivel(2);//Llamamos a la escena del Nivel 2
                AudioManager.shareaudio.Efectos[3].Stop();//Detenemos el efecto de Alarma, cuando se acaba el tiempo
                break;
        }
    }
    public static void ResetHealth()
    {
        //M�todo encargado de quitar las vidas
        sharecont.vidas--;
        sharecont.text_health.text = sharecont.vidas.ToString();
        if (sharecont.vidas == 0)
        {
            //Se llam� a la pantalla de GameOver solo si las vidas son igual a 0
            AudioManager.shareaudio.Partida.mute = true;//Se mutea el volumen de la m�sica
            GameManager.shareInstance.GameOver();//Pasamos a estado de Juego Game Over

        }
    }

    public static void PointsAdd()
    {
        //M�todo encargado de aumentar el valor de la moneda y puntos de acuerdo al valor establecido por pregunta
        if (sharecont.scene.name == "Level 1")
        {
            /*El objetivo de esta condicional es el de limitar el guardado de puntos cuando se juegue 
             por segunda vez un mismo nivel superado*/
            if (sharecont.puntos < 5)
            {
                //Si los puntos son menor que 5, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gr�fica
            }
        } else if (sharecont.scene.name == "Level 2")
        {
            if (sharecont.puntos < 10)
            {
                //Si los puntos son menor que 10, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gr�fica
            }
        }
        sharecont.pointsinv += sharecont.value_Points;//Se incrementa el valor de los puntos invicibles de acuerdo al valor establecido en valor de puntos
        sharecont.points_ui.text = sharecont.pointsinv.ToString();
        sharecont.moneda += sharecont.value_moneda;//Se incrementa el valor de la moneda de acuerdo al valor establecido de la moneda por nivel
        sharecont.moneda_ui.text = sharecont.moneda.ToString();//Se imprime el valor de monedas ganadas por interfaz  
        sharecont.monedawin+=sharecont.value_moneda;//Se incrementa el valor de las monedas del win de acuerdo al valor que se esten dando en este nivel, con la finalidad de ser mostradas al ganar la partida en el canvas
    }

    public void SaveEquipament(int vidas, float extratime,string ReferencesTurn, int MonedaExtra)
    {
        //Este m�todo guarda los valores pasados por par�metro  que otorgaran las habilidades de la carta equipada
        PlayerPrefs.SetInt("Live", vidas);//Se almacenan las vidas Extras
        PlayerPrefs.SetFloat("Time", extratime);//Se almacena el tiempo extra
        PlayerPrefs.SetString("ActivarEvento","Si");//Se guarda en Si la variable que permitir� activar la eventualidad de extra time
        PlayerPrefs.SetInt("ExtraMoneda", MonedaExtra); //Se guarda la cantidad de monedas extras que se dar�n dependiendo de la carta
    }
    public void CargarEquipament()
    {
        //Este m�todo se encarga de incializar las variables de la clase Contador 
        vidas += PlayerPrefs.GetInt("Live");//Se incrementa la cantidad de vidas extras almacenadas en el PLayer Prefs 
        TimeReferences = PlayerPrefs.GetFloat("Time");//Se establece la cantidad de tiempo extra que da la carta 
        Oneintro = PlayerPrefs.GetString("ActivarEvento");//Se carga en este caso la activaci�n del evento , para que se otrogue vidas extras
        monedaextra = PlayerPrefs.GetInt("ExtraMoneda");//Se establece la cantidad de monedas extras que se dar�n al ganar una partida

    }
 
    IEnumerator WaitExtraTime()
    {
        AnimaCon.ShareAnimation.DesactivateRedTime();//Desactivamos la animaci�n cuando el tiempo se est� acabando
        AudioManager.shareaudio.Efectos[3].Stop();//Paramos el efecto de sonido TimeEnd
        AnimaCon.ShareAnimation.ActiveAnimationExtraTime();//Activamos la animaci�n de tiempo extra
        currentTime += TimeReferences;//Se incrementa el tiempo actual de acuerdo al valor que otrogue la carta
        yield return new WaitForSeconds(2f);
        AnimaCon.ShareAnimation.DesactiveAnimationExtraTime();//Desactivamos la animaci�n de tiempo extra
    }
}
