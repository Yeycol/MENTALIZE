//Script destinado al inicio y pausa de las animaciones en el videojuego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaCon : MonoBehaviour
{
    public List<Animator> AnimationLis = null;//Lista de las animaciones a controlar
    public static AnimaCon ShareAnimation;
    //Referencia a los par�metros de las animaciones
    const string START_PIZARRA = "startPizarra";//Variable costante que hace referencia al par�metro booleano de la pizarra
    const string ACTIVE_OVER = "Active";// Variable constante que hace referencia al par�metro booleano de la interfaz de Game Over
    const string ACTIVE_ALERT = "startAlert";//Variable constante que hace referencia al par�metro de la alerta para salir de partida
    const string ACTIVE_REDTIME = "EventStart";//Variable constante que hace referencia al par�metro de  la animaci�n del fondo del reloj
    const string ACTIVE_TEXTIME = "StartText";//Variable constante que hace referencia al par�metro que controla la animaci�n de cambio de color de texto
    const string ACTIVE_CANDADO = "StartJumpCan";//Variable constante que hace referencia al par�metro del candado en Game Over
    const string ACTIVE_PADLOCK = "StartDesblock";//Variable constante que hace referencia al par�metro del candado del Win 
    const string START_WIN = "StartWin";//Variable constante que hace referencia al par�metro booleano d ela interfaz Win
    const string START_CONFETI = "StartConfeti";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n del confeti
    const string START_NAVE_ERROR = "StartNaveError";//Variable constante que hace referencia al booleano que controla la animaci�n cuando se responde mal una pregunta
    const string START_HEART = "StartHeart";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n 
    const string START_NAVE_CORRECT = "StartNaveCorrect";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n de la nave al responder al correctamente
    const string START_EXITTIME = "StartExtraTime";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n al dar tiempo extra
    const string START_CEROMONEY = "StartCero";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n al no tener dinero
    const string START_NOLOGRO = "StartNoLogro";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n al no poder conseguir algo por no tener el logro desbloqueado
    const string START_NOCOMPRA = "StartNoCompra";//Variable constante que hace referencia al par�metro booleano que controla la animaci�n al no poder equipar algo por no estar comprado
    const string START_ITEMCONSEGUIDO = "StartNewObject";//Variable constante de tipo string que hace referencia al par�metro booleano que controla la animaci�n de item conseguido
    /* 0 = Animaci�n d ela Pizarra en las Trivias
       1 = Animaci�n de la Interfaz de Game Over
       2 = Animaci�n de la Alerta para salida (background)
       3 = Animaci�n Fondo Red del Reloj Digital
       4 = Animaci�n letras Time del Reloj digital
       5 = Animaci�n del Candado en Interfaz Game Over
       6 = Animaci�n de Candado al Desbloquear un Nivel
       7 = Animaci�n de la interfaz de Win
       8 = Animaci�n del Confeti al Ganar Partida
       9 = Animaci�n del Panel para las transiciones de Escena
       10 = Animaci�n de la nave cuando una pregunta es contestada de manera erronea 
       11 = Animaci�n del coraz�n cuando es disparado
       12 = Animaci�n de la nave cuando la respuesta es respondida de manera correcta
       13 = Animaci�n activada cuando el tiempo extra es otorgad0
       14 = Animaci�n de cantidad de monedas insuficientes para compra
       15 = Animaci�n de objeto no conseguido por logro desbloqueado faltante
       16 = Animaci�n de objeto no disponible por que aun no ha sido comprado 
       17 = Animaci�n de nuevo objeto conseguido, ya sea cuando suceda la eventualidad de comprado o desbloqueaod por logro
    */
    private void Awake()
    {
        if (ShareAnimation == null)
        {
            ShareAnimation = this;
           
        }
      
    }

    public void  AlertActive()
    {
        AnimationLis[2].SetBool(ACTIVE_ALERT, true);// Activamos la animaci�n de la alerta
    }

    public void AlertOff()
    {
        AnimationLis[2].SetBool(ACTIVE_ALERT, false);// Desactivamos  la animaci�n de la alerta
    }

    public void ActivePizarra()
    {
        AnimationLis[0].SetBool(START_PIZARRA, true);//Se activa la animaci�n de la pizarra
    }
    public void DesactivatePizarra()
    {
        AnimationLis[0].SetBool(START_PIZARRA, false);//Desactivamos la animaci�n de la pizarra
    }
    public void ActivateOver()
    {
     AnimationLis[1].SetBool(ACTIVE_OVER, true);//Activamos la animaci�n de la pantalla de Game Over
    }
    public void ActiveCandado()
    {
        AnimationLis[5].SetBool(ACTIVE_CANDADO, true);//Activamos la animaci�n del candado
    }
   public void StartPadlock()
    {
        Invoke("WaitPadlock", 0.80f);
    }
    public void ActiveWin()
    {
    AnimationLis[7].SetBool(START_WIN, true);//Activamos la animaci�n de win
    }
    public void AtivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, true);//Se activa animaci�n del confeti
    }
    public void DesactivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, false);//Se desactiva animaci�n del confeti
    }

    public void DesactivateAlert()
    {
        //M�todo encargado  de desactivar la alerta con animaci�n
        AudioManager.shareaudio.Efectos[5].Play();
        AlertOff();
        StartCoroutine(DescativateCanvasAlert());// damos un tiempo de llamado al m�todo para que el canvas no desaparesca al instante
    }

    IEnumerator DescativateCanvasAlert()
    {
        //Desactiva el Canvas de la Alerta
        yield return new WaitForSeconds(0.3f);
        ManagerScene.shareMscen.OffAlert();
        AudioManager.shareaudio.Partida.mute = false;
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
     AnimationLis[6].SetBool(ACTIVE_PADLOCK, true);//Se activa la animaci�n de desbloqueo
    }
    public void ActiveRedTime()
    {
        AnimationLis[3].SetBool(ACTIVE_REDTIME, true);//Activa la animaci�n  del reloj parpadeo en rojo
        AnimationLis[4].SetBool(ACTIVE_TEXTIME, true);//Activa la animaci�n texto parpadeo en blanco
    }
    public void DesactivateRedTime()
    {
        //Desactiva las animaciones antes descritas
        AnimationLis[3].SetBool(ACTIVE_REDTIME, false);//Desactiva la animaci�n del reloj parpadeo en rojo
        AnimationLis[4].SetBool(ACTIVE_TEXTIME, false);//Desactiva la animaci�n texto parpadeo en blanco
    }
    public void ActiveAnimationExtraTime()
    {
        //Activa la animaci�n cuando se da tiempo extra
        AnimationLis[13].SetBool(START_EXITTIME, true);
    }
    public void DesactiveAnimationExtraTime()
    {
        //Desactiva la animaci�n cuando se da tiempo extra
        AnimationLis[13].SetBool(START_EXITTIME, false);
    }

    public void ActiveEventCeroMoney()
    {
        //Activa la animaci�n cuando no hay suficientes monedas para comprar
        AnimationLis[14].SetBool(START_CEROMONEY, true);
    }
    public void DesactiveEventCeroMoney()
    {
        //Desactiva la animaci�n cuando no hay suficientes monedas para comprar
        AnimationLis[14].SetBool(START_CEROMONEY, false);
    }

    public void ActivateNologro()
    {
        //M�todo que se encarga de activar la animaci�n de logro aun no desbloqueado
        AnimationLis[15].SetBool(START_NOLOGRO, true);
    }

    public void DesactivateNologro()
    {
        //M�todo que se encarga de desactivar la animaci�n de logro aun no desbloqueado
        AnimationLis[15].SetBool(START_NOLOGRO, false);
    }

    public void ActiveNocompra()
    {
        //M�todo que se encarga de activar la aniamci�n que muestra que aun no se ha comprado
        AnimationLis[16].SetBool(START_NOCOMPRA,true);
    }

    public void DesactiveNocompra()
    {
        //M�todo que se encarga de desactivar la aniamci�n que muestra que aun no se ha comprado
        AnimationLis[16].SetBool(START_NOCOMPRA, false);
    }

    public void ActiveAnimationObjectGet()
    {
        //M�todo encargado de inicializar la animaci�n de los items conseguidos
        AnimationLis[17].SetBool(START_ITEMCONSEGUIDO, true);
    }
    public void DesactivateAnimationObjectGet()
    {
        //M�todo encargado de deshabilitar la animaci�n de los items conseguidos
        AnimationLis[17].SetBool(START_ITEMCONSEGUIDO, false);
    }
}
