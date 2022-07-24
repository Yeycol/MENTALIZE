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
    public Slider[] SliderInicio;//Array de sliders que hacen referencia a los puntos ganados e impresos en la barra de progreso
    public bool IntroAnimation = false;//Variable de tipo boooleano encargada de hace rque el tiempo vaya mas lento cuando tengamos una animaci�n activa
    public bool RepeatAnimation=true;//Booleano que permite establecer si debe o no repetirse la animaci�n
    public int maxvaluecontador;//Variable de tipo entero que pretende almacenar el valor m�ximo del contador en los diferentes nieveles
    public int MaxPoints;//Variable de tipo entero que almacena el valor max de puntos para ganar
    public int OneMinPoints;//Variable de tipo entera que almacena el primer valor minimo para ganar partida
    public int TwoMinPoints;//Variable de tipo entera que almacena el segundo valor minimo para ganar partida
    public int ThreeMinPoints;//Variable de tipo entera que almacena el tercer valor minimo para ganar partida 
    public Button ReferContinue;//Varibale de tipo button que prentende desactivar la interacci�n del bot�n continuar
    public int pointsYue;//Variable que pretende almacenar los puntos conseguidos en el modo de juego Space Yue
    public int controlCoinsYue;
    void Awake()
    {
        if (sharecont == null)
        {
            sharecont = this;
        }
        scene = SceneManager.GetActiveScene();//GetActiveScene es un m�todo que nos permite obtener la escena activa actualmente
        
    }
    void Start()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame) ResetSound();//Se llama al m�todo encargado de parar los sonidos para que ninguno de la anterior escena sea escuchado
        /*Solo usar en caso de querer resetear los valores almacenados en los key de los player prefs
        PlayerPrefs.DeleteKey("ActivarEvento");
        PlayerPrefs.DeleteKey("ExtraMoneda");
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("Live");
        PlayerPrefs.DeleteKey("MonedaYue");*/
        CargarEquipament();//Se llama al m�todo encargado de Cargar vi    das extras y el tiempo extra pasado por referencia etc
        InicializarDatosInterfaz();//Cuando inicia el juego debemos imprimer el valor que hayan adquirido las variables al iniciar el juego     
    }
     private void FixedUpdate()
    {

        //Aqui se esta evaluando a cada frame, si estamos en en modo juego ejecutar� las instrucciones establecidas en las condicionales
        if (GameManager.shareInstance.currentgameState == GameState.InGame && scene.name != "YueScene" && scene.name != "SceneCard")
        {
            //Las siguientes condicionales estan encargadads de evaluar un booleano que permite que el llamado al m�todo event time sea llamado a cada 5 frames 
            //Esto da el efecto que el tiempo vaya mas lento, as� evitando que el tiempo sea arrebatado cuando una animaci�n se encuentra activa 
            if (IntroAnimation)
            {
                //En el caso de ser true se llama a cada 5 frames el m�todo Event Time 
                if (Time.frameCount % numFrames == 0)
                {
                    EventTime();//M�todo que se encarga de aumentar el tiempo del cron�metro y hacer las acciones establecidas cuando se cumplan ciertas eventualidades
                }
            }
            else if (!IntroAnimation)
            {
                EventTime();//En el caso de ser falso este debe ser llamado sin tener en cuenta la cantidad de frames en la que debe ser llamado el m�todo 
            }

            if (Time.frameCount % numFrames == 0)
            {
                //Se evalua a cada 5 frames si se han cumplido las condicionales necesarias para que el nivel llegue a su finalizaci�n
                EventEndLevel();
            }
        }
    } 

    public void InicializarDatosInterfaz()
    {
        if (scene.name != "SelectLevel (Trivias)" && scene.name != "Tienda" && scene.name!="SelectLevelSpace" && scene.name!="Inicio"&& scene.name != "YueScene" && scene.name != "SceneCard") //Solo si estamos en escenas distintas a las mencionadas
        {
            textcont.text = contador.ToString() + range;//Se imprime el contador y el rango de la cantidad de preguntas que habr� en el nivel
            text_health.text = vidas.ToString();//Imprime las vidas en la interfaz
            currentTime = time;//Se establece que el tiempo actual es igual a el tiempo establecido en la variable publica
        }


        if (scene.name != "SelectLevel (Trivias)" && scene.name != "SelectLevelSpace" )//Solo si la escena esdistinta a la establecida

        {
            GuardadoMonedas.CargarMonedas();// Se carga las monedas para poderlas visualizar la cantidad de monedas que se tiene
            moneda_ui.text = moneda.ToString();//Imprime las monedas en la interfaz
            GuardadoMonedas.CargarPoints();//Se carga los puntos para ser mostrados por interfaz
            if (scene.name != "YueScene" && scene.name != "SceneCard")
            { 
                points_ui.text = puntos.ToString();
            }
            else if (scene.name == "SceneCard" && controlCoinsYue == 0) // En este caso se evalua si nos encontramos en la escena de Scene Card para que solo en esta se cargue las monedas de los player prefs
            {
                monedawin = PlayerPrefs.GetInt("MonedaYue");// Cargamos el valor almacenado de las monedas que se guardaron previamente a entrar a la escena del juego de cartas
                moneda += monedawin;
                moneda_ui.text = moneda.ToString();
                controlCoinsYue = 1;
                PlayerPrefs.SetInt("controlCoinsYue", controlCoinsYue);
            }

            if (scene.name == "Inicio")
            {
                SliderInicio[0].value = puntos;
                SliderInicio[1].value = pointsYue;
                points_ui.text = puntos.ToString() + "/555pts";//Se imprime los puntos con el valor m�ximo en las barras de progreso del Inicio
                UI_WinText.text =pointsYue.ToString()+ "/555pts";
            }
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
            GameManager.shareInstance.GameOver();

        }
        
        if ((int)currentTime == 20)
        { //Esta condicional solo sucede siempre y cuando el tiempo sea igual a 20
          //TODO: No olvidar arreglar un peque�o bug de que la aniamci�n pueda volverse a repetir en esta instacia, esto solo debe suceder una vez
            if (RepeatAnimation)//Si el repetir la animaci�n es verdadero
            {
                RepeatAnimation = false;//Se establece como falso si es la primera vez que ingresa
                RealTimeAnimation.ShareRealTimeAnimator.EventInGame("Time");//A la misma clase se le indica que ventualidad es la que esta por pasar
                
            }
        }else if ((int)currentTime == 10){
            //Si el tiempo es igual a 10 se hacen las siguientes acciones
                AudioManager.shareaudio.Efectos[3].Play();//Iniciamos el Audio del efecto del Alarma cuando se acaba el tiempo
                AnimaCon.ShareAnimation.ActiveRedTime();// Se habilita la animaci�n de Reloj Rojo
                
        }
        else if ((int)currentTime == 7)//Si el tiempo actual es igual a 7 e entra a la condicional
        {
            if (Oneintro == "Si")//Dentro de este if anidado se evalua si la variable booleana OneIntro es verdadera
            {
                //La animaci�n solo se activar� si estamos en estado de juego InGame
                if (GameManager.shareInstance.currentgameState == GameState.InGame)
                {
                   
                    RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[2].gameObject.SetActive(true);
                    RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[3].gameObject.SetActive(true);
                    Oneintro = "No";//La primera vez que entre en este condicional debe ser la �nica vez, puesto que se pasar� false para que en el siguiente frame no haya la posibilidad de repetir la eventualidad
                    StartCoroutine(WaitExtraTime());//Si es el caso de ser verdad se llama a la corrutina encargada de habilitar y deshabilitar las animaciones
                }
            }
        }
       else if ((int)currentTime == 21 && RepeatAnimation)//Esta condicional evalua si el tiempo actual es igual a 21 y si el booleano es verdadero para evitar repeticiones 
        {
            RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = true;//Se indica que debe activarse el Canvas de la clase RealTimeAnimation
        }else if ((int)currentTime == 8 && Oneintro=="Si")
        {
            RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = true;//Se indica que debe activarse el Canvas de la clase RealTimeAnimation
        }


    }

    public void EventEndLevel()
    {
    
            //Esta secci�n evalua si el contador de las trivias son iguales que 6 estas apliquen las condicionales en el interior
            if (contador == maxvaluecontador+1)
            {
                //Si es el caso esta aplicar� esta condicional donde se llamar� a un m�todo si se consigue los puntos requeridos
                if (pointsinv == OneMinPoints || pointsinv ==TwoMinPoints|| pointsinv == ThreeMinPoints)
                {
                    /*En esta primera condicional se evalua si los puntos invisibles son iguales a 3
                     0 4, entonces al entrar en esta condicional unicamente se mostrar�n los valores
                    ganados sin incremento(siempre y cuando hayan monedas extras activas) en la
                    pantalla del Win, se guardar�n las monedas ganadas en esta partida, es decir el valor
                    general de las monedas ser�n guardadas, pero no los puntos, ni habr� desbloqueo de nivel.*/
                    ReferContinue.enabled = false;//Desahabilitamos el bot�n continuar para que no permita ir a la siguiente escena hast que el nivel sea desbloqueado
                    AnimaCon.ShareAnimation.ActiveFalsePadLock();//Llamamos a la animaci�n del candado cuando aun no se ha ganado el nivel
                    UI_WinText.text = monedawin.ToString();//En esta condicional el valor de las monedas ganadas solo ser�n impresas las ganadas en esa partida mas no se aplicar� incremento
                    GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas y puntos solo si se gana
                    GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                }
                else if (pointsinv ==MaxPoints)
                {
                    /*En esta condicional los increentos ser�n aplicados tanto a las monedas mostradas en win
                     como a las monedas generales, sumado a esto el guardado de monedas, puntos y desbloqueo
                    de niveles.*/
                    AnimaCon.ShareAnimation.StartPadlock();//Se activa la animaci�n del evento Padlock
                    monedawin += monedaextra;//Se incrementa el valor d elas monedas mostradas en la pantalla del win
                    UI_WinText.text=monedawin.ToString();//Se imprime el valor de las monedas para el win en la interfaz de texto del win
                    moneda += monedaextra;//Se incrementa la moneda general su valor
                    GuardadoMonedas.GuardarMonedas();//Se hace un guardado de las monedas
                    GuardadoMonedas.GuardarPoints();//Se hace un guardado de los Puntos
                    GameManager.shareInstance.WinGame();//Llamamos al Canvas que permit� mostrar la panatalla de ganado de partida
                    ControlNiveles.shareLvl.DesbloquearNivel();//Se desbloquea el nivel siguiente
                } else 
                {
                    //Si ninguno de los casos anteriores son cumplidos entonces entraremos a esta condicional y pasaremos al Game Over
                    GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                }
            }
        
    }
    public static void sumar()
    {
        /* Este m�todo es encargado de aumentar en uno al contador
         cada vez que se pase a la siguiente pregunta.*/

        sharecont.contador++;
        if (sharecont.contador <= sharecont.maxvaluecontador)//Se imprime el valor de la variable contador solo hasta que esta se igual a 5, para evitar que imprima una de mas, que no es necesario para al cantidad de preguntas
        {
            sharecont.textcont.text = sharecont.contador.ToString() + sharecont.range;
        }
    }
    public void resetcont()
    {
        ResetSound();//Llamamos al m�todo encargado de resetear los sonidos de la anterior Escena
        //AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva  la animaci�n del Relo digital en In Game
        //M�todo encargado de resetear la Trivia
        switch (scene.name)
        {
            /*Al pasar a una nueva escena las variables y configuraciones que se estaban ejecutando
             en la anterior escena estas se restablecen dando el efecto de reseteo de partida*/
            case "Level 1":
                ControlNiveles.shareLvl.CambiarNivel(1);
                break;
            case "Level 2":
                ControlNiveles.shareLvl.CambiarNivel(8);
                break;
            case "Level 3":
                ControlNiveles.shareLvl.CambiarNivel(9);
                break;
            case "Level 4":
                ControlNiveles.shareLvl.CambiarNivel(10);
                break;
            case "Level 5":
                ControlNiveles.shareLvl.CambiarNivel(11);
                break;
            case "Level 6":
                ControlNiveles.shareLvl.CambiarNivel(12);
                break;
            case "Level 7":
                ControlNiveles.shareLvl.CambiarNivel(13);
                break;
            case "Level 8":
                ControlNiveles.shareLvl.CambiarNivel(14);
                break;
            case "Level 9":
                ControlNiveles.shareLvl.CambiarNivel(15);
                break;
            case "Level 10":
                ControlNiveles.shareLvl.CambiarNivel(16);
                break;
            case "Level 11":
                ControlNiveles.shareLvl.CambiarNivel(17);
                break;
            case "Level 12":
                ControlNiveles.shareLvl.CambiarNivel(18);
                break;
            case "Level 13":
                ControlNiveles.shareLvl.CambiarNivel(19);
                break;
            case "Level 14":
                ControlNiveles.shareLvl.CambiarNivel(20);
                break;
            case "Level 15":
                ControlNiveles.shareLvl.CambiarNivel(21);
                break;
            case "Level 16":
                ControlNiveles.shareLvl.CambiarNivel(22);
                break;
            case "Level 17":
                ControlNiveles.shareLvl.CambiarNivel(23);
                break;
            case "Level 18":
                ControlNiveles.shareLvl.CambiarNivel(24);
                break;
            case "Level 19":
                ControlNiveles.shareLvl.CambiarNivel(25);
                break;
            case "Level 20":
                ControlNiveles.shareLvl.CambiarNivel(26);
                break;
            case "Level 21":
                ControlNiveles.shareLvl.CambiarNivel(27);
                break;
            case "Level 22":
                ControlNiveles.shareLvl.CambiarNivel(28);
                break;
            case "Level 23":
                ControlNiveles.shareLvl.CambiarNivel(29);
                break;
            case "Level 24":
                ControlNiveles.shareLvl.CambiarNivel(30);
                break;
            case "Level 25":
                ControlNiveles.shareLvl.CambiarNivel(31);
                break;
            case "Level 26":
                ControlNiveles.shareLvl.CambiarNivel(32);
                break;
            case "Level 27":
                ControlNiveles.shareLvl.CambiarNivel(33);
                break;
            case "Level 28":
                ControlNiveles.shareLvl.CambiarNivel(34);
                break;
            case "Level 29":
                ControlNiveles.shareLvl.CambiarNivel(35);
                break;
            case "Level 30":
                ControlNiveles.shareLvl.CambiarNivel(36);
                break;
            case "Level 31":
                ControlNiveles.shareLvl.CambiarNivel(37);
                break;
            case "Level 32":
                ControlNiveles.shareLvl.CambiarNivel(38);
                break;
            case "Level 33":
                ControlNiveles.shareLvl.CambiarNivel(39);
                break;
            case "Level 34":
                ControlNiveles.shareLvl.CambiarNivel(40);
                break;
            case "Level 35":
                ControlNiveles.shareLvl.CambiarNivel(41);
                break;
            case "Level 36":
                ControlNiveles.shareLvl.CambiarNivel(42);
                break;
            case "Level 37":
                ControlNiveles.shareLvl.CambiarNivel(43);
                break;
            case "Level 38":
                ControlNiveles.shareLvl.CambiarNivel(44);
                break;
            case "Level 39":
                ControlNiveles.shareLvl.CambiarNivel(45);
                break;
            case "Level 40":
                ControlNiveles.shareLvl.CambiarNivel(46);
                break;
            case "Level 41":
                ControlNiveles.shareLvl.CambiarNivel(47);
                break;
            case "Level 42":
                ControlNiveles.shareLvl.CambiarNivel(48);
                break;
            case "Level 43":
                ControlNiveles.shareLvl.CambiarNivel(49);
                break;
            case "Level 44":
                ControlNiveles.shareLvl.CambiarNivel(50);
                break;
            case "Level 45":
                ControlNiveles.shareLvl.CambiarNivel(51);
                break;
            case "Level 46":
                ControlNiveles.shareLvl.CambiarNivel(52);
                break;
            case "Level 47":
                ControlNiveles.shareLvl.CambiarNivel(53);
                break;
            case "Level 48":
                ControlNiveles.shareLvl.CambiarNivel(54);
                break;
            case "Level 49":
                ControlNiveles.shareLvl.CambiarNivel(55);
                break;
            case "Level 50":
                ControlNiveles.shareLvl.CambiarNivel(56);
                break;
            case "Level 51":
                ControlNiveles.shareLvl.CambiarNivel(57);
                break;
            case "Level 52":
                ControlNiveles.shareLvl.CambiarNivel(58);
                break;
            case "Level 53":
                ControlNiveles.shareLvl.CambiarNivel(59);
                break;
            case "Level 54":
                ControlNiveles.shareLvl.CambiarNivel(60);
                break;
            case "Level 55":
                ControlNiveles.shareLvl.CambiarNivel(61);
                break;
            case "Level 56":
                ControlNiveles.shareLvl.CambiarNivel(62);
                break;
            case "Level 57":
                ControlNiveles.shareLvl.CambiarNivel(63);
                break;
            case "Level 58":
                ControlNiveles.shareLvl.CambiarNivel(64);
                break;
            case "Level 59":
                ControlNiveles.shareLvl.CambiarNivel(65);
                break;
            case "Level 60":
                ControlNiveles.shareLvl.CambiarNivel(66);
                break;
            case "YueScene":
                ControlNiveles.shareLvl.CambiarNivel(67);
                break;
            case "SceneCard":
                ControlNiveles.shareLvl.CambiarNivel(69);
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
            GameManager.shareInstance.GameOver();//Pasamos a estado de Juego Game Over

        }
       
    }

    public static void PointsAdd()
    {
        //M�todo encargado de aumentar el valor de la moneda y puntos de acuerdo al valor establecido por pregunta
   
            /*El objetivo de esta condicional es el de limitar el guardado de puntos cuando se juegue 
             por segunda vez un mismo nivel superado*/
            if (sharecont.puntos <sharecont.MaxPoints&& sharecont.scene.name!="YueScene")
            {
                //Si los puntos son menores al valor m�ximo de puntos en el nivel har� las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gr�fica
            }
                     
        sharecont.pointsinv += sharecont.value_Points;//Se incrementa el valor de los puntos invicibles de acuerdo al valor establecido en valor de puntos
        sharecont.moneda += sharecont.value_moneda;//Se incrementa el valor de la moneda de acuerdo al valor establecido de la moneda por nivel
        sharecont.moneda_ui.text = sharecont.moneda.ToString();//Se imprime el valor de monedas ganadas por interfaz  
        sharecont.monedawin+=sharecont.value_moneda;//Se incrementa el valor de las monedas del win de acuerdo al valor que se esten dando en este nivel, con la finalidad de ser mostradas al ganar la partida en el canvas
        if(Contador.sharecont.scene.name=="YueScene")//Solo Si estamos en Yue Scene Va a guardarse la cantidad de monedas obtenidas
        PlayerPrefs.SetInt("MonedaYue",sharecont.monedawin);//Cada que llamamos al método encargado de aumentar el valor de las monedas conseguidas mandamos a que se guarden la cantidad de monedas obtenidas en ese momento
    }

    public void SaveEquipament(int vidas, float extratime, int MonedaExtra)
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
        controlCoinsYue = PlayerPrefs.GetInt("controlCoinsYue");

    }
    IEnumerator WaitExtraTime()
    {
        IntroAnimation = true;// Se activa la lentitud al tiempo para que la animaci�n no haga sentir que el jugador a perdido tiempo
        AudioManager.shareaudio.Efectos[3].Stop();  
        AnimaCon.ShareAnimation.DesactivateRedTime();//Desactivamos la animaci�n cuando el tiempo se est� acabando
        RealTimeAnimation.ShareRealTimeAnimator.ActiveAnimationExtraTime();//Activamos la animaci�n de tiempo extra
        yield return new WaitForSeconds(2.3f);
        currentTime += TimeReferences;//Se incrementa el tiempo actual de acuerdo al valor que otrogue la carta
        IntroAnimation = false;// Se le indica a la variable booleano que quite la lentitud a tiempo y asi volver todo a la normalidad puesto que no hay animaci�n activa 
        RealTimeAnimation.ShareRealTimeAnimator.DesactiveAnimationExtraTime();//Desactivamos la animaci�n de tiempo extra
        StartCoroutine(WaitForCanvas());//Se llama al Canvas encargado de dar tiempo para la animaci�n de salida
    }
    IEnumerator WaitForCanvas()
    {
        //Corrutina destinada a dar tiempo a la animaci�n de salida
        yield return new WaitForSeconds(0.9f);
        RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[2].gameObject.SetActive(false);
        RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[3].gameObject.SetActive(false);
        RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = false;
    }
    public void ResetSound()
    {
        //M�todo encargado de parar los sonidos de la anterior escena al resetearse
        AudioManager.shareaudio.Efectos[3].Stop();//Para el sonido de Time End
        /*AudioManager.shareaudio.Efectos[6].Stop();//Para el Efecto Disparo
        AudioManager.shareaudio.Efectos[7].Stop();//Para el Efecto Llegada Nave
        AudioManager.shareaudio.Efectos[8].Stop();//Para el Efeco Salida Nave
        AudioManager.shareaudio.Efectos[9].Stop();//Para el Efeco Roto
        AudioManager.shareaudio.Efectos[10].Stop();//Para el Efeco Abducir Nave*/
        AudioManager.shareaudio.Efectos[15].Stop();// Para  la m�sica de Trivias d ela anterior escena
        AudioManager.shareaudio.Efectos[16].Stop();//Para la m�sica de Space Yue
       /* AudioManager.shareaudio.Efectos[17].Stop();//Se para la Frase A toda M�quina gogo
        AudioManager.shareaudio.Efectos[18].Stop();//Se para la frase de Se te acaba Tiempo Tic Tac
        AudioManager.shareaudio.Efectos[19].Stop();//Paramos el sonido de la frase Mira el reloj no te queda tiempo
        AudioManager.shareaudio.Efectos[20].Stop();//Paramos el sonido de la frace Concentrate tu puedes hacerlo mejor
        AudioManager.shareaudio.Efectos[21].Stop();//Paramos el sonido de la frase Hey no te distraigas te queda una vida
        AudioManager.shareaudio.Efectos[22].Stop();//Paramos el sonido de la frase Mira en donde presionas tienes una vida menos*/
        AudioManager.shareaudio.Efectos[23].Stop();//Paramos el sonido de la frase Hit Hit Hurra
        AudioManager.shareaudio.Efectos[24].Stop();//Paramos el sonido de la frase JIJIJI
        AudioManager.shareaudio.Efectos[25].Stop();//Paramos el sonido de la frase KABOM Vamos por otra
        AudioManager.shareaudio.Efectos[26].Stop();//Paramos el sonido de la frase Eso fue excelente quieres ir por MUFFINS
        AudioManager.shareaudio.Efectos[27].Stop();//Paramos el sonido de la frase Cuanto m�s dificil es la victoria mayor es la felicidad al ganar
        AudioManager.shareaudio.Efectos[28].Stop();//Paramos el sonido de la frase Nos equivocamos es momento de nutrir nuestras mentes
        AudioManager.shareaudio.Efectos[29].Stop();//Paramos el sonido de la frase No te sientas mal sigue intentndolo
        AudioManager.shareaudio.Efectos[13].Stop();//Se para el sonido del WiGame
        AudioManager.shareaudio.Efectos[0].Stop();//Se para el sonido del OverGame
    }

   
}
