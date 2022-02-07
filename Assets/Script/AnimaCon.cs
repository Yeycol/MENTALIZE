//Script destinado al inicio y pausa de las animaciones en el videojuego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego



public class AnimaCon : MonoBehaviour
{
    public Scene scene;
    public List<Animator> AnimationLis = null;//Lista de las animaciones a controlar
    public static AnimaCon ShareAnimation;
    public Animator[] EventTime = new Animator[3];//Array que contiene las animaciones para las eventualidades del tiempo 
    public Animator[] EventVidas = new Animator[3];//Array que contiene la componente Animator de los objetos de la animación para cuando se pierde vidas  
    public GameObject ObjectAnimation;//Hace referencia al objeto padre que contiene en su interior a los demás hijos que son las animaciones
    public ParameterAndTime[]ValueNecesary;//Array que contiene los strings de los nombres de los parámetros para activar la animación
    public int indextime;//Varibale de tipo entera que será utilizada para sacar un entero aleatorio del Array EventTime
    public int indexvidas;//Variable de tipo entero que alamcenará el entero aleatorio para las eventualidades de vida 
    //Referencia a los parámetros de las animaciones
    const string START_PIZARRA = "startPizarra";//Variable costante que hace referencia al parámetro booleano de la pizarra
    const string ACTIVE_OVER = "Active";// Variable constante que hace referencia al parámetro booleano de la interfaz de Game Over
    const string ACTIVE_ALERT = "startAlert";//Variable constante que hace referencia al parámetro de la alerta para salir de partida
    const string ACTIVE_REDTIME = "EventStart";//Variable constante que hace referencia al parámetro de  la animación del fondo del reloj
    const string ACTIVE_TEXTIME = "StartText";//Variable constante que hace referencia al parámetro que controla la animación de cambio de color de texto
    const string ACTIVE_CANDADO = "StartJumpCan";//Variable constante que hace referencia al parámetro del candado en Game Over
    const string ACTIVE_PADLOCK = "StartDesblock";//Variable constante que hace referencia al parámetro del candado del Win 
    const string START_WIN = "StartWin";//Variable constante que hace referencia al parámetro booleano d ela interfaz Win
    const string START_CONFETI = "StartConfeti";//Variable constante que hace referencia al parámetro booleano que controla la animación del confeti
    const string START_NAVE_ERROR = "StartNaveError";//Variable constante que hace referencia al booleano que controla la animación cuando se responde mal una pregunta
    const string START_HEART = "StartHeart";//Variable constante que hace referencia al parámetro booleano que controla la animación 
    const string START_NAVE_CORRECT = "StartNaveCorrect";//Variable constante que hace referencia al parámetro booleano que controla la animación de la nave al responder al correctamente
    const string START_EXITTIME = "StartExtraTime";//Variable constante que hace referencia al parámetro booleano que controla la animación al dar tiempo extra
    const string START_CEROMONEY = "StartCero";//Variable constante que hace referencia al parámetro booleano que controla la animación al no tener dinero
    const string START_NOLOGRO = "StartNoLogro";//Variable constante que hace referencia al parámetro booleano que controla la animación al no poder conseguir algo por no tener el logro desbloqueado
    const string START_NOCOMPRA = "StartNoCompra";//Variable constante que hace referencia al parámetro booleano que controla la animación al no poder equipar algo por no estar comprado
    const string START_ITEMCONSEGUIDO = "StartNewObject";//Variable constante de tipo string que hace referencia al parámetro booleano que controla la animación de item conseguido
    const string START_EXTRATIME = "StartTextTimeExtra";//Variable constaten  de tipo string que almacena el nombre del parámetro necesario para controlar el parámeteo booleano establecido en el animator 
    /* 0 = Animación d ela Pizarra en las Trivias
       1 = Animación de la Interfaz de Game Over
       2 = Animación de la Alerta para salida (background)
       3 = Animación Fondo Red del Reloj Digital
       4 = Animación letras Time del Reloj digital
       5 = Animación del Candado en Interfaz Game Over
       6 = Animación de Candado al Desbloquear un Nivel
       7 = Animación de la interfaz de Win
       8 = Animación del Confeti al Ganar Partida
       9 = Animación del Panel para las transiciones de Escena
       10 = Animación de la nave cuando una pregunta es contestada de manera erronea 
       11 = Animación del corazón cuando es disparado
       12 = Animación de la nave cuando la respuesta es respondida de manera correcta
       13 = Animación activada cuando el tiempo extra es otorgad0
       14 = Animación de cantidad de monedas insuficientes para compra
       15 = Animación de objeto no conseguido por logro desbloqueado faltante
       16 = Animación de objeto no disponible por que aun no ha sido comprado 
       17 = Animación de nuevo objeto conseguido, ya sea cuando suceda la eventualidad de comprado o desbloqueaod por logro
       18 = Referencia al controlador de la animación encargada de mostrar en UI el Texto de Time Extra     
      */
    private void Awake()
    {
        if (ShareAnimation == null)
        {
            ShareAnimation = this;

        }
        indextime = Random.Range(0, EventTime.Length);//Se genera un número entero aleatorio desde cero hasta la cantidad de objetos que tenga el array EventTime 
        indexvidas = Random.Range(0, EventVidas.Length);//El número aleatorio generado por el método Range es almacenado en la variable entera, este número aleatorio es escogido desde 0 hasta la cantidad de elementos que tenga el array
        scene = SceneManager.GetActiveScene();
    }

    public void  AlertActive()
    {
        AnimationLis[2].SetBool(ACTIVE_ALERT, true);// Activamos la animación de la alerta
    }

    public void ActivePizarra()
    {
        AnimationLis[0].SetBool(START_PIZARRA, true);//Se activa la animación de la pizarra
    }
    public void DesactivatePizarra()
    {
        AnimationLis[0].SetBool(START_PIZARRA, false);//Desactivamos la animación de la pizarra
    }
    public void ActivateOver()
    {
     AnimationLis[1].SetBool(ACTIVE_OVER, true);//Activamos la animación de la pantalla de Game Over
    }
    public void ActiveCandado()
    {
        AnimationLis[5].SetBool(ACTIVE_CANDADO, true);//Activamos la animación del candado
    }
   public void StartPadlock()
    {
        Invoke("WaitPadlock", 0.80f);
    }
    public void ActiveWin()
    {
    AnimationLis[7].SetBool(START_WIN, true);//Activamos la animación de win
    }
    public void AtivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, true);//Se activa animación del confeti
    }
    public void DesactivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, false);//Se desactiva animación del confeti
    }

    public void DesactivateAlert()
    {
        //Método encargado  de desactivar la alerta con animación
            StartCoroutine(DescativateCanvasAlert());// Damos un tiempo de llamado al método para que el canvas no desaparesca al instante
    }

    IEnumerator DescativateCanvasAlert()
    {
        AudioManager.shareaudio.Efectos[5].Play();//Reproducimos el efecto de sonido al salir de la alerta
        AnimationLis[2].SetBool(ACTIVE_ALERT, false);// Desactivamos  la animación de la alerta
        yield return new WaitForSeconds(0.3f);
        ManagerScene.shareMscen.OffAlert();//Desactivamos el canvas de la alerta
        if (scene.name == "SelectLevel (Trivias)" || scene.name == "SelectLevelSpace" || scene.name == "Tienda" || scene.name == "SelectModoJuego")
        {
            //Se pasa a modo de juego en menú solo si estamos en las escenas establecidas en la condicional
            GameManager.shareInstance.BackToMenu();
        }
        else
        {
            //En el caso que sea de que el nombre de la escena actual sea cualquier otro pasará estado de juego InGame
            GameManager.shareInstance.StarGame();
        }
        if (GameManager.shareInstance.currentgameState == GameState.InGame)//Solo si estamos en modo de juego sucederá lo establecido dentro de la condicional
        {
            AudioManager.shareaudio.Efectos[3].UnPause();//Se desmutea el efecto TimeEnd
            AudioManager.shareaudio.Efectos[6].mute=false;//Desmuteamos el Efecto Disparo
            AudioManager.shareaudio.Efectos[7].mute=false;//Desmuteamos el Efecto Llegada Nave
            AudioManager.shareaudio.Efectos[8].mute=false;//Desuteamos el Efecto Salida nave
            AudioManager.shareaudio.Efectos[9].mute=false;//Desmuteamos el Efeco Roto
            AudioManager.shareaudio.Efectos[10].mute=false;//Desmuteamos el Efeco Abducir Nave
            AudioManager.shareaudio.Efectos[15].UnPause();//Se despausa la canción para Trivias
            AudioManager.shareaudio.Efectos[16].UnPause();//Se despausa la canción para Space Yue
            //TODO: Arreglar lo del sonido para las animaciones cuando sale la alerta
            AudioManager.shareaudio.Efectos[17].mute = false;//Desmuteamos el sonido de la frase A toda Máquina
            AudioManager.shareaudio.Efectos[18].mute = false;//Desmuteamos el sonido de la frase Se te acaba el tiempo
            AudioManager.shareaudio.Efectos[19].mute = false;//Desmuteamos el sonido de la frase Mira el Reloj no te queda tiempo
        }
        else if (GameManager.shareInstance.currentgameState == GameState.menu)//Solo si estamos en menú sucederá lo del interior
        {
            AudioManager.shareaudio.Efectos[6].mute = false;//Desmuteamos el Efecto Disparo
            AudioManager.shareaudio.Efectos[7].mute = false;//Desmuteamos el Efecto Llegada Nave
            AudioManager.shareaudio.Efectos[8].mute = false;//Desuteamos el Efecto Salida nave
            AudioManager.shareaudio.Efectos[9].mute = false;//Desmuteamos el Efeco Roto
            AudioManager.shareaudio.Efectos[10].mute = false;//Desmuteamos el Efeco Abducir Nave
            AudioManager.shareaudio.Efectos[12].mute=false;//Despausamos el sonido de Focos Dañados
            AudioManager.shareaudio.Efectos[14].UnPause();//Despausa la canción del menú
            AudioManager.shareaudio.Efectos[17].mute = false;//Desmuteamos el sonido de la frase A toda Máquina
            AudioManager.shareaudio.Efectos[18].mute = false;//Desmuteamos el sonido de la frase Se te acaba el tiempo
            AudioManager.shareaudio.Efectos[19].mute = false;//Desmuteamos el sonido de la frase Mira el Reloj no te queda tiempo
        }
    
    }
    public void ActiveNaveError()
    {
        AnimationLis[10].SetBool(START_NAVE_ERROR,true);
       
    }
    public void DesactiveNaveError()
    {
        AnimationLis[10].SetBool(START_NAVE_ERROR,false);
    }
    public void ActiveNaveCorrect()
    {
        AnimationLis[12].SetBool(START_NAVE_CORRECT, true);

    }
    public void DesactiveNaveCorrect()
    {
        AnimationLis[12].SetBool(START_NAVE_CORRECT, false);
    }
    public void StartHeart()
    {
        AnimationLis[11].SetBool(START_HEART, true);
    }
    public void StopHeart()
    {
        AnimationLis[11].SetBool(START_HEART, false);
    }
    public void WaitPadlock()
    {
     AnimationLis[6].SetBool(ACTIVE_PADLOCK, true);//Se activa la animación de desbloqueo
    }
    public void ActiveRedTime()
    {
        AnimationLis[3].SetBool(ACTIVE_REDTIME, true);//Activa la animación  del reloj parpadeo en rojo
        AnimationLis[4].SetBool(ACTIVE_TEXTIME, true);//Activa la animación texto parpadeo en blanco
    }
    public void DesactivateRedTime()
    {
        //Desactiva las animaciones antes descritas
        AnimationLis[3].SetBool(ACTIVE_REDTIME, false);//Desactiva la animación del reloj parpadeo en rojo
        AnimationLis[4].SetBool(ACTIVE_TEXTIME, false);//Desactiva la animación texto parpadeo en blanco
    }
    public void ActiveAnimationExtraTime()
    {
        //Activa la animación cuando se da tiempo extra
        AnimationLis[13].SetBool(START_EXITTIME, true);
        AnimationLis[18].SetBool(START_EXTRATIME, true);
    }
    public void DesactiveAnimationExtraTime()
    {
        //Desactiva la animación cuando se da tiempo extra
        AnimationLis[13].SetBool(START_EXITTIME, false);
        AnimationLis[18].SetBool(START_EXTRATIME, false);
    }

    public void ActiveEventCeroMoney()
    {
        //Activa la animación cuando no hay suficientes monedas para comprar
        AnimationLis[14].SetBool(START_CEROMONEY, true);
    }
    public void DesactiveEventCeroMoney()
    {
        //Desactiva la animación cuando no hay suficientes monedas para comprar
        AnimationLis[14].SetBool(START_CEROMONEY, false);
    }

    public void ActivateNologro()
    {
        //Método que se encarga de activar la animación de logro aun no desbloqueado
        AnimationLis[15].SetBool(START_NOLOGRO, true);
    }

    public void DesactivateNologro()
    {
        //Método que se encarga de desactivar la animación de logro aun no desbloqueado
        AnimationLis[15].SetBool(START_NOLOGRO, false);
    }

    public void ActiveNocompra()
    {
        //Método que se encarga de activar la aniamción que muestra que aun no se ha comprado
        AnimationLis[16].SetBool(START_NOCOMPRA,true);
    }

    public void DesactiveNocompra()
    {
        //Método que se encarga de desactivar la aniamción que muestra que aun no se ha comprado
        AnimationLis[16].SetBool(START_NOCOMPRA, false);
    }

    public void ActiveAnimationObjectGet()
    {
        //Método encargado de inicializar la animación de los items conseguidos
        AnimationLis[17].SetBool(START_ITEMCONSEGUIDO, true);
    }
    public void DesactivateAnimationObjectGet()
    {
        //Método encargado de deshabilitar la animación de los items conseguidos
        AnimationLis[17].SetBool(START_ITEMCONSEGUIDO, false);
    }
    public void EventInGame(string Event)
    {
        //Este método recibe dos parámetros de tipo string el uno almacena el tipo de evento que está por suceder y el otro el nombre del parámetro , necesario para establecer el booleano a la componente animator

        switch (Event)
        {
            case "Time":// En caso del evento llamarse Time
                if (indextime == 0)//En caso de que el número aleatorio sea igual a 0
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[0].ParameterAnimator, ValueNecesary[0].TimeCourrutine));//Se llama a la corrutina para dar tiempo a visualizar la animación pasando por parámetro la posición de la animación, parámetro que se tiene que activar y tiempo de la corrutina, para mostrar la animación
                } else if (indextime == 1)//En caso que el número aleatorio sea igual a 1
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[1].ParameterAnimator, ValueNecesary[1].TimeCourrutine));
                }
                else if (indextime == 2)//En caso que el número aleatorio sea igual a 2
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[2].ParameterAnimator, ValueNecesary[2].TimeCourrutine));
                }
                break;

            case "Vida":
                //TODO: Llamar y establecer los valores que pasrán la corrutina por parámetro
                if (indexvidas == 0)
                {

                } else if (indexvidas == 1)
                {

                } else if (indexvidas == 2) { 
                
                }
                break;
        }
        
    }
    IEnumerator StartEvenTAnimationTime(int AnimationParameter, string ParameterAnimator, float TimeCorru)
    { 
        Contador.sharecont.IntroAnimation = true;//Se indica que el comportamiento EventTime de la clase Contador debe tomar en cuenta los frmaes para ser llamado
        ObjectAnimation.SetActive(true);//Hablitamos el objeto con Raycast engargado de evitar que los botones sean presionados cuando la animación estaá activa 
        EventTime[AnimationParameter].SetBool(ParameterAnimator, true);//Se activa la animación de acuerdo al entero obtenido en el ramdom y el nombre del parámetro pasado por parámetro
        yield return new WaitForSeconds(TimeCorru);
        Contador.sharecont.IntroAnimation = false;//Se indica que el comportamiento de Contador deje de tomar en cuenta los frames
        EventTime[AnimationParameter].SetBool(ParameterAnimator, false);//Se activa la animación de salida de la eventualidad
        ObjectAnimation.SetActive(false);//Deshabilitamos el objeto encargado dehabilitar el Raycast Para evitar que los botones de las trivias activen otra animación
    }
    //TODO: Crear la corrutina para el caso de la eventualidad del tiempo         
  
    [System.Serializable]
    public class ParameterAndTime
    {
        //Esta clase tiene la funcionalidad de servir para a creación de un array de este tipo para poder asignar a sus propiedades datos
        public float TimeCourrutine;//Campo de clase que pretende almacenar un flotante del tiempo de duración de la animacíón
        public string ParameterAnimator;//Campo de clase que pretende almacenar un string del nombre del parámetro del Animator
    }
}


