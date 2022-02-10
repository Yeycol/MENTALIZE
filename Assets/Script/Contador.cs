/*Esta clase esta encargada de controlar la ganada, perdida de partidas, cargado y guardado de monedas
 en las diferentes eventualidades, tambien se encarga del control del tiempo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego

public class Contador : MonoBehaviour
{

    public static Contador sharecont;
    public int contador; // Variable de tipo entero que almacena los números del contador de la trivia 
    public int vidas; //Variable de tipo entero que almacena la cantidad de vidas
   public int puntos;//Variable pública de tipo entero en la cual se almacenarán los puntos que se van obteniendo
    public Text textcont;// Variable de tipo Texto utilizada para que adquiera los valores de contador
    public Text text_health;// Variable de tipo Texto para que adquiera el valor de las vidas
    public Text time_ui;//Variable de tipo texto para que adquiera  el valor del cronómetro
    public Text moneda_ui;// Variable de tipo texto que va adquirir las monedas que van siendo ganadas
    public Text points_ui;// Variable de tipo texto que va adquirir los puntos que van siendo ganados
    public float time; // Variable pública que almacena el tiempo límite para resolver la trivia
    public float currentTime;// Variable que almacenará el tiempo establecido en el time
    public string range;// Variable de tipo string que almacenará el limite de cantidad de preguntas en la trivia
    public int value_Points;//Variable de tipo entera que almacena el valor de los puntos segun en el nivel que estemos
    public int value_moneda;// Variable de tipo pública en el que se almacena el valor de la moneda en determinado nivel
    public int moneda;// Variable entera que almacena la cantida de monedas obtenidas
    public Scene scene;// Variable privada de tipo escena que se utilizará para controlar y condicionar con las escenas
    public Guardado GuardadoMonedas;//Referencia a la clase Guardado
    public int pointsinv;//Sirve para almacenar los puntos invicibles que no son guardados por la clase Guardado, solo sirven para ser utilizado en condicionales
    private int numFrames = 5;//Variable de tipo entero que almacena los frames a los que queremos que se llamen a determinados métodos en el Update
    public string Oneintro = "No";//Booleano que permitirá controlar la cantidad de veces que debera dar la carta dar tiempo extra
    public float TimeReferences;//Variable que hace referencia al tiempo extra que otroga la carta equipada
    public int monedawin;//Variable tipo entera que alamacenará la cantidad de monedas conseguidas en determinado nivel
    public int monedaextra;//Variable que será utilizada para almacenar la cantidad de monedas extras que debe dar la carta
    public Text UI_WinText;//Variable tipo texto que mostrará la cantidad de monedas ganadas en el Canvas Win
    public Slider[] SliderInicio;//Array de sliders que hacen referencia a los puntos ganados e impresos en la barra de progreso
    public bool IntroAnimation = false;//Variable de tipo boooleano encargada de hace rque el tiempo vaya mas lento cuando tengamos una animación activa
    public string OnePlay = "Si";//Variable de tipo string que controla las veces que se debe ejecutar las animaciones en In Game
    public bool ActiveTime=true;
    void Awake()
    {
        if (sharecont == null)
        {
            sharecont = this;
        }
        GuardadoMonedas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Localización de la clase guardado a traves d ela busqueda del objeto ControlNiveles
        scene = SceneManager.GetActiveScene();//GetActiveScene es un método que nos permite obtener la escena activa actualmente
        ResetSound();//Se llama al método encargado de parar los sonidos para que ninguno de la anterior escena sea escuchado
    }
    void Start()
    {
        /*Solo usar en caso de querer resetear los valores almacenados en los key de los player prefs
        PlayerPrefs.DeleteKey("ActivarEvento");
        PlayerPrefs.DeleteKey("ExtraMoneda");
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("Live");*/
        if (scene.name!= "SelectLevel (Trivias)" && scene.name != "SelectLevelSpace" && scene.name != "SelectModoJuego"&& scene.name!="Tienda" && scene.name != "Inicio") {
            GuardadoMonedas.CargarList();
        }
        CargarEquipament();//Se llama al método encargado de Cargar vi    das extras y el tiempo extra pasado por referencia etc
        InicializarDatosInterfaz();//Cuando inicia el juego debemos imprimer el valor que hayan adquirido las variables al iniciar el juego     
    }
    private void FixedUpdate()
    {
        //Aqui se esta evaluando a cada frame, si estamos en en modo juego ejecutará las instrucciones establecidas en las condicionales
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            //Las siguientes condicionales estan encargadads de evaluar un booleano que permite que el llamado al método event time sea llamado a cada 5 frames 
            //Esto da el efecto que eltiempo vaya mas lento, así evitando que el tiempo sea arrebatado cuando una animación se encuentra activa 
            if (IntroAnimation)
            {
                //En el caso de ser true se llama a cada 5 frames el método Event Time 
                if (Time.frameCount % numFrames == 0)
                {
                    EventTime();//Método que se encarga de aumentar el tiempo del cronómetro y hacer las acciones establecidas cuando se cumplan ciertas eventualidades
                }
            } else if (!IntroAnimation)
            { 
                EventTime();//En el caso de ser falso este debe ser llamado sin tener en cuenta la cantidad de frames en la que debe ser llamado el método 
            }


            if (Time.frameCount % numFrames == 0)
            {
                //La condicional es verdadera cuando el restante de la división entre el número total de frames desde el inicio del juego y el número de frames es igual a 0
                EventEndLevel();//Método encargado de controlar la lógica de ganado, guardado y desbloquedo de niveles
            }
        }
    }

    public void InicializarDatosInterfaz()
    {
        GuardadoMonedas.CargarMonedas();// Se carga las monedas para poderlas visualizar la cantidad de monedas que se tiene
        if (scene.name != "SelectLevel (Trivias)" && scene.name != "Tienda" && scene.name!="SelectLevelSpace" && scene.name!="Inicio") //Solo si estamos en escenas distintas a las mencionadas
        {
            textcont.text = contador.ToString() + range;//Se imprime el contador y el rango de la cantidad de preguntas que habrá en el nivel
            text_health.text = vidas.ToString();//Imprime las vidas en la interfaz
            currentTime = time;//Se establece que el tiempo actual es igual a el tiempo establecido en la variable publica
        }
        if (scene.name != "SelectLevel (Trivias)" && scene.name != "SelectLevelSpace")//Solo si la escena esdistinta a la establecida
        {
            moneda_ui.text = moneda.ToString();//Imprime las monedas en la interfaz
            points_ui.text = puntos.ToString();
            if (scene.name == "Inicio")
            {
                SliderInicio[0].value = puntos;
                points_ui.text = puntos.ToString() + "/500";//Se imprime los puntos con el valor máximo en las barras de progreso del Inicio
            }
        }
        
    }
    public void EventTime()
    {
        if (currentTime > 0)
        {
            //Si el tiempo establecido es mayor que cero se debera disminuir el tiempo establecido
            currentTime -= Time.deltaTime;//Time.deltatime es un método que nos permite saber el tiempo transcurrido desde el último frame, es decir iremos decrementando la variable current time de acuerdo al tiempo que haya transcurrido
                                          //Si el juego va a 60 frames, update jecutará 60 frames por segundo, donde el tiempo transcurrido es 1seg
            time_ui.text = (int)currentTime + "seg";//Aqui asignamos de manera explicita el valor de la variable current time a la variable tipo texto apra que esta se muestre en la UI
        }

        if ((int)currentTime == 0)
        {
            //Si el tiempo es igual a 0 se hacen las siguientes acciones
            GameManager.shareInstance.GameOver();

        }
        if ((int)currentTime == 20)
        { //Esta condicional solo sucede siempre y cuando el tiempo sea igual a 20
          //TODO: No olvidar arreglar un pequeño bug de que la aniamción pueda volverse a repetir en esta instacia, esto solo debe suceder una vez
            if (ActiveTime)
            {
                ActiveTime = false;
                AnimaCon.ShareAnimation.EventInGame("Time");//Se llama al método encargado de evaluar en que eventualidad estamos y proceder a reproducir la animación de acuerdo l número aleatorio conseguido   
            }
        }else if ((int)currentTime == 10){
            //Si el tiempo es igual a 10 se hacen las siguientes acciones
                AudioManager.shareaudio.Efectos[3].Play();//Iniciamos el Audio del efecto del Alarma cuando se acaba el tiempo
                AnimaCon.ShareAnimation.ActiveRedTime();// Se habilita la animación de Reloj Rojo
                
        }
        else if ((int)currentTime == 7)//Si el tiempo actual es igual a 7 e entra a la condicional
        {
            if (Oneintro == "Si")//Dentro de este if anidado se evalua si la variable booleana OneIntro es verdadera
            {
                //La animación solo se activará si estamos en estado de juego InGame
                if (GameManager.shareInstance.currentgameState == GameState.InGame)
                {
                    StartCoroutine(WaitExtraTime());//Si es el caso de ser verdad se llama a la corrutina encargada de habilitar y deshabilitar las animaciones
                    Oneintro = "No";//La primera vez que entre en este condicional debe ser la única vez, puesto que se pasará false para que en el siguiente frame no haya la posibilidad de repetir la eventualidad
                }
            }
        }
        if (GameManager.shareInstance.currentgameState == GameState.InGame && TimeReferences != 0 && vidas == 1 && OnePlay == "Si" &&(int)currentTime!=20)
        {
            OnePlay = "No";
            AnimaCon.ShareAnimation.EventInGame("Vida");
        }
        if ((int)currentTime == 21&&ActiveTime)
        {
            AnimaCon.ShareAnimation.ObjectAnimation.SetActive(true);
        }

    }

    public void EventEndLevel()
    {
        if (scene.name == "Level 1" || scene.name == "Level 2")
        {
            //Esta sección evalua si el contador de las trivias son iguales que 6 estas apliquen las condicionales en el interior
            if (contador == 6)
            {
                //Si es el caso esta aplicará esta condicional donde se llamará a un método si se consigue los puntos requeridos
                if (pointsinv == 5)
                {
                    /*Si los puntos invicibles son iguales hacen las acciones establecidas, los puntos que son guradados 
                     solo son aumentados siempre y cuando lo permitan las condicionales del método PointsAdd */
                    /*Los puntos invicibles nacen después de darse cuenta que al limitar la ganada de puntos
                     usados para guardar llegan a 5 al repetir el nivel estos no aumentaban su valor y rompian
                    la lógica impidia ganar o perder la partida, por ello los puntos invicibles aunque se repita
                    la partida estos no son limitados ni guardados*/
                    GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                    monedawin += monedaextra;//Se incrementa el valor de la moneda siempre y cuando la moneda extra tenga un valor a incrementar
                    UI_WinText.text = monedawin.ToString();//Se muestra por interfaz de canvas Win el valor total de monedas ganadas
                    moneda += monedaextra;//Se incrementa el valor de la moneda global
                    moneda_ui.text = moneda.ToString();//Se imprime su valor para actualizar los datos
                    GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas y puntos solo si se gana
                    ControlNiveles.shareLvl.DesbloquearNivel();//Método encargado de desbloquear los niveles ganados
                }
                else
                {
                    GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                }
            }
        }
    }
    public static void sumar()
    {
        /* Este método es encargado de aumentar en uno al contador
         cada vez que se pase a la siguiente pregunta.*/

        sharecont.contador++;
        if (sharecont.contador <= 5)//Se imprime el valor de la variable contador solo hasta que esta se igual a 5, para evitar que imprima una de mas, que no es necesario para al cantidad de preguntas
        {
            sharecont.textcont.text = sharecont.contador.ToString() + sharecont.range;
        }
    }
    public void resetcont()
    {
        ResetSound();//Llamamos al método encargado de resetear los sonidos de la anterior Escena
        //Método encargado de resetear la Trivia
        switch (scene.name)
        {
            /*Al pasar a una nueva escena las variables y configuraciones que se estaban ejecutando
             en la anterior escena estas se restablecen dando el efecto de reseteo de partida*/
            case "Level 1":
                ControlNiveles.shareLvl.CambiarNivel(1);//Llamamos a la escena del Nivel 1
                AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva  la animación del Relo digital en In Game
                break;
            case "Level 2":
                ControlNiveles.shareLvl.CambiarNivel(3);//Llamamos a la escena del Nivel 2
                AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva  la animación del Relo digital en In Game
                break;
        }
    }
    public static void ResetHealth()
    {
        //Método encargado de quitar las vidas
        sharecont.vidas--;
        sharecont.text_health.text = sharecont.vidas.ToString();
        if (sharecont.vidas == 0)
        {
            //Se llamá a la pantalla de GameOver solo si las vidas son igual a 0
            GameManager.shareInstance.GameOver();//Pasamos a estado de Juego Game Over

        }
       
    }

    public static void PointsAdd()
    {
        //Método encargado de aumentar el valor de la moneda y puntos de acuerdo al valor establecido por pregunta
        if (sharecont.scene.name == "Level 1")
        {
            /*El objetivo de esta condicional es el de limitar el guardado de puntos cuando se juegue 
             por segunda vez un mismo nivel superado*/
            if (sharecont.puntos < 5)
            {
                //Si los puntos son menor que 5, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gráfica
            }
        } else if (sharecont.scene.name == "Level 2")
        {
            if (sharecont.puntos < 10)
            {
                //Si los puntos son menor que 10, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gráfica
            }
        }
        sharecont.pointsinv += sharecont.value_Points;//Se incrementa el valor de los puntos invicibles de acuerdo al valor establecido en valor de puntos
        sharecont.moneda += sharecont.value_moneda;//Se incrementa el valor de la moneda de acuerdo al valor establecido de la moneda por nivel
        sharecont.moneda_ui.text = sharecont.moneda.ToString();//Se imprime el valor de monedas ganadas por interfaz  
        sharecont.monedawin+=sharecont.value_moneda;//Se incrementa el valor de las monedas del win de acuerdo al valor que se esten dando en este nivel, con la finalidad de ser mostradas al ganar la partida en el canvas
    }

    public void SaveEquipament(int vidas, float extratime,string ReferencesTurn, int MonedaExtra)
    {
        //Este método guarda los valores pasados por parámetro  que otorgaran las habilidades de la carta equipada
        PlayerPrefs.SetInt("Live", vidas);//Se almacenan las vidas Extras
        PlayerPrefs.SetFloat("Time", extratime);//Se almacena el tiempo extra
        PlayerPrefs.SetString("ActivarEvento","Si");//Se guarda en Si la variable que permitirá activar la eventualidad de extra time
        PlayerPrefs.SetInt("ExtraMoneda", MonedaExtra); //Se guarda la cantidad de monedas extras que se darán dependiendo de la carta
    }
    public void CargarEquipament()
    {
        //Este método se encarga de incializar las variables de la clase Contador 
        vidas += PlayerPrefs.GetInt("Live");//Se incrementa la cantidad de vidas extras almacenadas en el PLayer Prefs 
        TimeReferences = PlayerPrefs.GetFloat("Time");//Se establece la cantidad de tiempo extra que da la carta 
        Oneintro = PlayerPrefs.GetString("ActivarEvento");//Se carga en este caso la activación del evento , para que se otrogue vidas extras
        monedaextra = PlayerPrefs.GetInt("ExtraMoneda");//Se establece la cantidad de monedas extras que se darán al ganar una partida

    }
 
    IEnumerator WaitExtraTime()
    {
        IntroAnimation = true;// Se activa la lentitud al tiempo para que la animación no haga sentir que el jugador a perdido tiempo
        AudioManager.shareaudio.Efectos[3].Stop();  
        AnimaCon.ShareAnimation.ObjectAnimation.SetActive(true) ;//Lalamamos al método encargado de desahabilitar los botones de la Trivia
        AnimaCon.ShareAnimation.DesactivateRedTime();//Desactivamos la animación cuando el tiempo se está acabando
        AnimaCon.ShareAnimation.ActiveAnimationExtraTime();//Activamos la animación de tiempo extra
        yield return new WaitForSeconds(2.3f);
        currentTime += TimeReferences;//Se incrementa el tiempo actual de acuerdo al valor que otrogue la carta
        AnimaCon.ShareAnimation.ObjectAnimation.SetActive(false);//Activamos nuevamente los botones de las trivias para que se pueda seleccionar las respuestas sin ningun problema
        IntroAnimation = false;// Se le indica a la variable booleano que quite la lentitud a tiempo y asi volver todo a la normalidad puesto que no hay animación activa 
        AnimaCon.ShareAnimation.DesactiveAnimationExtraTime();//Desactivamos la animación de tiempo extra
    }
    public void ResetSound()
    {
        //Método encargado de parar los sonidos de la anterior escena al resetearse
        AudioManager.shareaudio.Efectos[3].Stop();//Para el sonido de Time End
        AudioManager.shareaudio.Efectos[6].Stop();//Para el Efecto Disparo
        AudioManager.shareaudio.Efectos[7].Stop();//Para el Efecto Llegada Nave
        AudioManager.shareaudio.Efectos[8].Stop();//Para el Efeco Salida Nave
        AudioManager.shareaudio.Efectos[9].Stop();//Para el Efeco Roto
        AudioManager.shareaudio.Efectos[10].Stop();//Para el Efeco Abducir Nave
        AudioManager.shareaudio.Efectos[15].Stop();// Para  la música de Trivias d ela anterior escena
        AudioManager.shareaudio.Efectos[16].Stop();//Para la música de Space Yue
        AudioManager.shareaudio.Efectos[17].Stop();//Se para la Frase A toda Máquina gogo
        AudioManager.shareaudio.Efectos[18].Stop();//Se para la frase de Se te acaba Tiempo Tic Tac
        AudioManager.shareaudio.Efectos[19].Stop();//Paramos el sonido de la frase Mira el reloj no te queda tiempo
        AudioManager.shareaudio.Efectos[20].Stop();//Paramos el sonido de la frace Concentrate tu puedes hacerlo mejor
        AudioManager.shareaudio.Efectos[21].Stop();//Paramos el sonido de la frase Hey no te distraigas te queda una vida
        AudioManager.shareaudio.Efectos[22].Stop();//Paramos el sonido de la frase Mira en donde presionas tienes una vida menos
        AudioManager.shareaudio.Efectos[23].Stop();//Paramos el sonido de la frase Hit Hit Hurra|
        AudioManager.shareaudio.Efectos[24].Stop();//Paramos el sonido de la frase JIJIJI
        AudioManager.shareaudio.Efectos[25].Stop();//Paramos el sonido de la frase KABOM Vamos por otra
        AudioManager.shareaudio.Efectos[26].Stop();//Paramos el sonido de la frase Eso fue excelente quieres ir por MUFFINS
        AudioManager.shareaudio.Efectos[13].Stop();//Se para el sonido del WiGame
        AudioManager.shareaudio.Efectos[0].Stop();//Se para el sonido del OverGame
    }
}
