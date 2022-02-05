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
    private float currentTime;// Variable que almacenará el tiempo establecido en el time
    public string range;// Variable de tipo string que almacenará el limite de cantidad de preguntas en la trivia
    public int value_Points;//Variable de tipo entera que almacena el valor de los puntos segun en el nivel que estemos
    public int value_moneda;// Variable de tipo pública en el que se almacena el valor de la moneda en determinado nivel
    public int moneda;// Variable entera que almacena la cantida de monedas obtenidas
    private Scene scene;// Variable privada de tipo escena que se utilizará para controlar y condicionar con las escenas
    public Guardado GuardadoMonedas;//Referencia a la clase Guardado
    public int pointsinv;//Sirve para almacenar los puntos invicibles que no son guardados por la clase Guardado, solo sirven para ser utilizado en condicionales
    private int numFrames = 5;//Variable de tipo entero que almacena los frames a los que queremos que se llamen a determinados métodos en el Update
    void Awake()
    {
        if (sharecont == null)
        {
            sharecont = this;
        }
        GuardadoMonedas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Localización de la clase guardado a traves d ela busqueda del objeto ControlNiveles

    }
    void Start()
    {
        GuardadoMonedas.CargarMonedas();// Se carga las monedas para poderlas visualizar la cantidad de monedas que se tiene
        scene = SceneManager.GetActiveScene();//GetActiveScene es un método que nos permite obtener la escena activa actualmente
        InicializarDatosInterfaz();//Cuando inicia el juego debemos imprimer el valor que hayan adquirido las variables al iniciar el juego
       
        
    }

    private void FixedUpdate()
    {
        //Aqui se esta evaluando a cada frame, si estamos en en modo juego ejecutará las instrucciones establecidas en las condicionales
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            EventTime();//Método que se encarga de aumentar el tiempo dle cronómetro y hacer las acciones establecidas cuando se cumplan ciertas eventualidades
           
            if (Time.frameCount % numFrames == 0)
            {
                //La condicional es verdadera cuando el restante de la división entre el número total de frames desde el inicio del juego y el número de frames es igual a 0
                EventEndLevel();//Método encargado de controlar la lógica de ganado, guardado y desbloquedo de niveles
            }
        }
    }

    public void InicializarDatosInterfaz()
    {
 
            textcont.text = contador.ToString() + range;//Se imprime el contador y el rango de la cantidad de preguntas que habrá en el nivel
            text_health.text = vidas.ToString();//Imprime las vidas en la interfaz
            moneda_ui.text = moneda.ToString();//Imprime las monedas en la interfaz
            currentTime = time;//Se establece que el tiempo actual es igual a el tiempo establecido en la variable publica
           points_ui.text = puntos.ToString();       
    }
    public void EventTime()
        {
            if (currentTime > 0)
            {
            Time.timeScale = 0.3f;
                //Si el tiempo establecido es mayor que cero se debera disminuir el tiempo establecido
                currentTime -= Time.deltaTime;//Time.deltatime es un método que nos permite saber el tiempo transcurrido desde el último frame, es decir iremos decrementando la variable current time de acuerdo al tiempo que haya transcurrido
                                              //Si el juego va a 60 frames, update jecutará 60 frames por segundo, donde el tiempo transcurrido es 1seg
                time_ui.text = (int)currentTime + "seg";//Aqui asignamos de manera explicita el valor de la variable current time a la variable tipo texto apra que esta se muestre en la UI
            }

            if ((int)currentTime == 0)
            {
                //Si el tiempo es igual a 0 se hacen las siguientes acciones
                AnimaCon.ShareAnimation.DesactivateRedTime();//Método de la clase AnimaCon, encargada de desahabilitar la animación del Reloj Rojo
                AudioManager.shareaudio.Partida.mute = true;//Pasamos un mute a la música que se repite en fondo
                GameManager.shareInstance.GameOver();//Pasamos a estado de juego Game Over
            }

            if ((int)currentTime == 10)
            {
                //Si el tiempo es igual a 10 se hacen las siguientes acciones
                AnimaCon.ShareAnimation.ActiveRedTime();// Se habilita la animación de Reloj Rojo
                AudioManager.shareaudio.Efectos[3].Play();//Iniciamos el Audio del efecto del Alarma cuando se acaba el tiempo
            }
        }

    public void EventEndLevel()
    {
        switch (scene.name)
        {
            case "Level 1":
                //Esta sección evalua si el contador de las trivias son iguales que 6 estas apliquen las condicionales en el interior
                if (contador==6)
                {

                    //Si es el caso esta aplicará esta condicional donde se llamará a un método si se consigue los puntos requeridos
                    if (pointsinv == 5)
                    {
                    /*Si los puntos invicibles son iguales hacen las acciones establecidas, los puntos que son guradados 
                     solo son aumentados siempre y cuando lo permitan las condicionales del método PointsAdd */
                    /*Los puntos invicibles nacen después de darse cuenta que al limitar la ganada de puntos
                     usados para guardar llegan a 5 al repetir el nivel estos no aumentaban su valor y rompian
                    la lógica impidiendo ganar o perder la partida, por ello los puntos invicibles aunque se repita
                    la partida estos no son limitados ni guardados*/
                        GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas solo si se gana
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        ControlNiveles.shareLvl.DesbloquearNivel();//Método encargado de desbloquear los niveles ganados
                    }
                    else
                    {
                        AudioManager.shareaudio.Partida.mute = true;//Se mutea el volumen de la música
                        GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                    }
                }
                break;

         case "Level 2":
                if (contador == 11)
                {

                    //Si es el caso esta aplicará esta condicional donde se llamará a un método si se consigue los puntos requeridos
                    if (puntos == 10 || pointsinv == 10)
                    {
                        GuardadoMonedas.GuardarMonedas();//Se hace el guardado de monedas solo si se gana
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        ControlNiveles.shareLvl.DesbloquearNivel();//Método encargado de desbloquear los niveles ganados
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
        /* Este método es encargado de aumentar en uno al contador
         cada vez que se pase a la siguiente pregunta.*/

        sharecont.contador++;
        if (sharecont.contador <=5)//Se imprime el valor de la variable contador solo hasta que esta se igual a 5, para evitar que imprima una de mas, que no es necesario para al cantidad de preguntas
        {
            sharecont.textcont.text = sharecont.contador.ToString() + sharecont.range;
        }
    }
    public void resetcont()
    {
        //Método encargado de resetear la Trivia
        switch (scene.name)
        {
            /*Al pasar a una nueva escena las variables y configuraciones que se estaban ejecutando
             en la anterior escena estas se restablecen dando el efecto de reseteo de partida*/
            case "Level 1":
                ControlNiveles.shareLvl.CambiarNivel(1);//Llamamos a la escena del Nivel 1
                AudioManager.shareaudio.Efectos[3].Stop();//Detenemos el efecto de Alarma, cuando se acaba el tiempo
                AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva  la animación del Relo digital en In Game
                break;
            case "Level 2":
                ControlNiveles.shareLvl.CambiarNivel(2);//Llamamos a la escena del Nivel 2
                AudioManager.shareaudio.Efectos[3].Stop();//Detenemos el efecto de Alarma, cuando se acaba el tiempo
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
            AudioManager.shareaudio.Partida.mute = true;//Se mutea el volumen de la música
            GameManager.shareInstance.GameOver();//Pasamos a estado de Juego Game Over
          
        }
    }

    public static void PointsAdd()
    {
        //Método encargado de aumentar el valor de la moneda y puntos de acuerdo al valor establecido por pregunta
        if(sharecont.scene.name=="Level 1")
        {
            /*El objetivo de esta condicional es el de limitar el guardado de monedas cuando se juegue 
             por segunda vez un mismo nivel superado*/
            if (sharecont.puntos<5)
            {
                //Si los puntos son menor que 5, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gráfica
            }
        } else if (sharecont.scene.name == "Level 2")
        {
            if (sharecont.puntos <10)
            {
                //Si los puntos son menor que 10, se hacen las siguientes acciones
                sharecont.puntos += sharecont.value_Points;//Se incrementa el valor de la variable puntos de acuerdo al valor almacena en la variable Value Points
                sharecont.points_ui.text = sharecont.puntos.ToString();//Se imprime el valor de puntos por la variable tipo texto ubicada en interfaz gráfica
            }
        }
        sharecont.pointsinv += sharecont.value_Points;//Se incrementa el valor de los puntos invicibles de acuerdo al valor establecido en valor de puntos
        sharecont.points_ui.text = sharecont.pointsinv.ToString();
        sharecont.moneda += sharecont.value_moneda;//Se incrementa el valor de la moneda de acuerdo al valor establecido de la moneda por nivel
        sharecont.moneda_ui.text = sharecont.moneda.ToString();//Se imprime el valor de monedas ganadas por interfaz  
    }
 
}
