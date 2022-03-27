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
    //Referencia a los parámetros de las animaciones
    const string START_FALSEPADLOCK= "StartFalsePadlock";//Variable costante que hace referencia al parámetro booleano de la animación del false PadLock
    const string ACTIVE_OVER = "Active";// Variable constante que hace referencia al parámetro booleano de la interfaz de Game Over
    const string ACTIVE_ALERT = "startAlert";//Variable constante que hace referencia al parámetro de la alerta para salir de partida
    const string ACTIVE_REDTIME = "EventStart";//Variable constante que hace referencia al parámetro de  la animación del fondo del reloj
    const string ACTIVE_TEXTIME = "StartText";//Variable constante que hace referencia al parámetro que controla la animación de cambio de color de texto
    const string ACTIVE_CANDADO = "StartJumpCan";//Variable constante que hace referencia al parámetro del candado en Game Over
    const string ACTIVE_PADLOCK = "StartDesblock";//Variable constante que hace referencia al parámetro del candado del Win 
    const string START_WIN = "StartWin";//Variable constante que hace referencia al parámetro booleano d ela interfaz Win
    const string START_HEART = "StartHeart";//Variable constante que hace referencia al parámetro booleano que controla la animación 
    const string START_CEROMONEY = "StartCero";//Variable constante que hace referencia al parámetro booleano que controla la animación al no tener dinero
    const string START_NOLOGRO = "StartNoLogro";//Variable constante que hace referencia al parámetro booleano que controla la animación al no poder conseguir algo por no tener el logro desbloqueado
    const string START_NOCOMPRA = "StartNoCompra";//Variable constante que hace referencia al parámetro booleano que controla la animación al no poder equipar algo por no estar comprado
    const string START_ITEMCONSEGUIDO = "StartNewObject";//Variable constante de tipo string que hace referencia al parámetro booleano que controla la animación de item conseguido
    /* 0 = Animación del FalsePadLock
       1 = Animación de la Interfaz de Game Over
       2 = Animación de la Alerta para salida (background)
       3 = Animación Fondo Red del Reloj Digital
       4 = Animación letras Time del Reloj digital
       5 = Animación del Candado en Interfaz Game Over
       6 = Animación de Candado al Desbloquear un Nivel
       7 = Animación de la interfaz de Win
       8 = Animación del Confeti al Ganar Partida//Remover
       8 = Animación del Panel para las transiciones de Escena
       9 = Animación del corazón cuando es disparado 
       10 = Animación de cantidad de monedas insuficientes para compra
       11 = Animación de objeto no conseguido por logro desbloqueado faltante
       12 = Animación de objeto no disponible por que aun no ha sido comprado 
       13 = Animación de nuevo objeto conseguido, ya sea cuando suceda la eventualidad de comprado o desbloqueaod por logro
      */
    private void Awake()
    {
        if (ShareAnimation == null)
        {
            ShareAnimation = this;

        }
        scene = SceneManager.GetActiveScene();
    }


    public void  AlertActive()
    {
        AnimationLis[2].SetBool(ACTIVE_ALERT, true);// Activamos la animación de la alerta
    }

    public void ActiveFalsePadLock()
    {
        AnimationLis[0].SetBool(START_FALSEPADLOCK, true);//Se activa la animación del desbloqueo falso
    }
    public void DesactivateFalsePadLock()
    {
        AnimationLis[0].SetBool(START_FALSEPADLOCK, false);//Desactivamos la animación del desbloqueo falso
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
        if (scene.name == "SelectLevel (Trivias)" || scene.name == "SelectLevelSpace" || scene.name == "Tienda" || scene.name == "SelectModoJuego" || scene.name=="Inicio")
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
            AudioManager.shareaudio.Efectos[20].mute = false;//Desmuteamos el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio .Efectos[21].mute = false;//Desmuteamos el sonido de la frase Ey no te distraigas tienes una vida menos
            AudioManager .shareaudio .Efectos[22].mute = false;//Desmuteamos el sonido de la frase Mira en donde presionas tienes una vida menos
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
            AudioManager.shareaudio.Efectos[20].mute = false;//Desmuteamos el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio.Efectos[21].mute = false;//Desmuteamos el sonido de la frase Ey no te distraigas tienes una vida menos
            AudioManager.shareaudio.Efectos[22].mute = false;//Desmuteamos el sonido de la frase Mira en donde presionas tienes una vida menos
        }
    
    }

    public void StartHeart()
    {
        AnimationLis[9].SetBool(START_HEART, true);
    }
    public void StopHeart()
    {
        AnimationLis[9].SetBool(START_HEART, false);
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


    public void ActiveEventCeroMoney()
    {
        //Activa la animación cuando no hay suficientes monedas para comprar
        AnimationLis[10].SetBool(START_CEROMONEY, true);
    }
    public void DesactiveEventCeroMoney()
    {
        //Desactiva la animación cuando no hay suficientes monedas para comprar
        AnimationLis[10].SetBool(START_CEROMONEY, false);
    }

    public void ActivateNologro()
    {
        //Método que se encarga de activar la animación de logro aun no desbloqueado
        AnimationLis[11].SetBool(START_NOLOGRO, true);
    }

    public void DesactivateNologro()
    {
        //Método que se encarga de desactivar la animación de logro aun no desbloqueado
        AnimationLis[11].SetBool(START_NOLOGRO, false);
    }

    public void ActiveNocompra()
    {
        //Método que se encarga de activar la aniamción que muestra que aun no se ha comprado
        AnimationLis[12].SetBool(START_NOCOMPRA,true);
    }

    public void DesactiveNocompra()
    {
        //Método que se encarga de desactivar la aniamción que muestra que aun no se ha comprado
        AnimationLis[12].SetBool(START_NOCOMPRA, false);
    }

    public void ActiveAnimationObjectGet()
    {
        //Método encargado de inicializar la animación de los items conseguidos
        AnimationLis[13].SetBool(START_ITEMCONSEGUIDO, true);
    }
    public void DesactivateAnimationObjectGet()
    {
        //Método encargado de deshabilitar la animación de los items conseguidos
        AnimationLis[13].SetBool(START_ITEMCONSEGUIDO, false);
    }
    
    public void DesactivateConfeti()
    {
        //Método encargado de forzar el reinicio de la animación del confeti
        RealTimeAnimation.ShareRealTimeAnimator.StoptAnimationConfeti();
        RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = false;//Desahabilitamos el canvas para que este no este habilitado al reiniciar escena
    }

}


