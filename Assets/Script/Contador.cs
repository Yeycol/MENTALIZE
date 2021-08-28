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
    public int pointsinv;
    private int numFrames = 5;
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
        //Cuando inicia el juego debemos imprimer el valor que hayan adquirido las variables al iniciar el juego
        textcont.text = contador.ToString() + range;//Se imprime el contador y el rango de la cantidad de preguntas que habrá en el nivel
        points_ui.text = puntos.ToString();//Imprime Los puntos en la interfaz
        text_health.text = vidas.ToString();//Imprime las vidas en la interfaz
        moneda_ui.text = moneda.ToString();//Imprime las monedas en la interfaz
        currentTime = time;//Se establece que el tiempo actual es igual a el tiempo establecido en la variable publica
   
    }

    private void FixedUpdate()
    {
        //Aqui se esta evaluando a cada frame, si estamos en en modo juego ejecutará las instrucciones establecidas en las condicionales
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            EventTime();
            if (Time.frameCount % numFrames == 0)
            {
                EventEndLevel();
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
                //Si el tiempo es igual a 0 se llamará a la pantalla Game Over
                AnimaCon.ShareAnimation.DesactivateRedTime();
                AudioManager.shareaudio.Partida.mute = true;
                GameManager.shareInstance.GameOver();
            }

            if ((int)currentTime == 10)
            {
                //Si el tiempo es igual a 10 llama a un método encargado de activar la animación  del reloj
                AnimaCon.ShareAnimation.ActiveRedTime();
                AudioManager.shareaudio.Efectos[3].Play();
            }
        }

    public void EventEndLevel()
    {
        switch (scene.name)
        {
            case "Level 1":
                //Esta sección evalua si el contador de las trivias son mayores que 5 estas apliquen las condicionales en el interior
                if (contador == 6)
                {

                    //Si es el caso esta aplicará esta condicional donde se llamará a un método si se consigue los puntos requeridos
                    if (puntos == 5 || pointsinv == 5)
                    {
                        AudioManager.shareaudio.Partida.mute = true;
                        givefive();//Método encargado de dar 5 al contador
                        GuardadoMonedas.GuardarMonedas();
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        ControlNiveles.shareLvl.DesbloquearNivel();//Método encargado de desbloquear los niveles ganados
                        //ManagerScene.shareMscen.loadesne();
                    }
                    else
                    {
                        AudioManager.shareaudio.Partida.mute = true;
                        givefive();
                        GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                    }
                }
                break;

         case "Level 2":
                if (contador == 6)
                {

                    //Si es el caso esta aplicará esta condicional donde se llamará a un método si se consigue los puntos requeridos
                    if (puntos == 10 || pointsinv == 10)
                    {
                        AudioManager.shareaudio.Partida.mute = true;
                        givefive();//Método encargado de dar 5 al contador
                        GuardadoMonedas.GuardarMonedas();
                        GameManager.shareInstance.WinGame();//Llama a la pantalla de ganaste
                        ControlNiveles.shareLvl.DesbloquearNivel();//Método encargado de desbloquear los niveles ganados
                        //ManagerScene.shareMscen.loadesne();
                    }
                    else
                    {
                        AudioManager.shareaudio.Partida.mute = true;
                        givefive();
                        GameManager.shareInstance.GameOver();//Llama a la pantalla de Game Over
                    }
                }
                break;

        }
    }
            
    // Update is called once per frame
    public static void sumar()  
    {
        /* 1.-Este método es encargado de aumentar en uno al contador
         cada vez que se pase a la siguiente pregunta.*/
        sharecont.contador++;
        sharecont.textcont.text = sharecont.contador.ToString()+sharecont.range; 
    }

    public static void givefive()
    {
        //Este método devuelve 5 al contador
        sharecont.contador = 5;
        sharecont.textcont.text = sharecont.contador.ToString() + sharecont.range;
    }
    public void resetcont()
    {
        //Método encargado de resetear la Trivia
        switch (scene.name)
        {
            case "Level 1":
                ControlNiveles.shareLvl.CambiarNivel(1);
                AudioManager.shareaudio.Efectos[3].Stop();
                break;
            case "Level 2":
                ControlNiveles.shareLvl.CambiarNivel(2);
                AudioManager.shareaudio.Efectos[3].Stop();
                break;
         
        }
        
        //AnimaCon.ShareAnimation.DesactivateRedTime();
       // contador = 0;
        //textcont.text = contador.ToString() + range;
        //vidas = 3;
        //text_health.text = vidas.ToString();
        //puntos = 0;
        //time = 61;
        //currentTime = time;
        //puntos = 0;
        //GuardadoMonedas.CargarMonedas();
        //moneda_ui.text = moneda.ToString();
        //points_ui.text = puntos.ToString();
       // GameManager.shareInstance.StarGame();//Llamamos al estado de juego 
        //manQ.Nexquestion();//Llamamos a la siguiente pregunta 

    }
    public static void ResetHealth()
    {
        //Método encargado de quitar las vidas
        sharecont.vidas--;
        sharecont.text_health.text = sharecont.vidas.ToString();
        if (sharecont.vidas == 0)
        {
            //Se llamá a la pantalla de GameOver solo si las vidas son igual a 0
            AudioManager.shareaudio.Partida.mute = true;
            GameManager.shareInstance.GameOver();
          
        }
    }

    public static void PointsAdd()
    {
        //Método encargado de aumentar el valor de la moneda y puntos de acuerdo al valor establecido por pregunta
        if(sharecont.scene.name=="Level 1")
        {
            if (sharecont.puntos<5)
            {
                sharecont.puntos += sharecont.value_Points;
                sharecont.points_ui.text = sharecont.puntos.ToString();
            }
        } else if (sharecont.scene.name == "Level 2")
        {
            if (sharecont.puntos <10)
            {
                sharecont.puntos += sharecont.value_Points;
                sharecont.points_ui.text = sharecont.puntos.ToString();
            }
        }
        sharecont.pointsinv += sharecont.value_Points;
        sharecont.moneda += sharecont.value_moneda;
        sharecont.moneda_ui.text = sharecont.moneda.ToString();
        sharecont.points_ui.text = sharecont.puntos.ToString();
        
    }

}
