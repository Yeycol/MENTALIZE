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
    public void DesactivateOver()
    {
        AnimationLis[1].SetBool(ACTIVE_OVER, false);//Se desactiva la animaci�n de la pantalla de Game Over
    }
    public void ActiveCandado()
    {
        AnimationLis[5].SetBool(ACTIVE_CANDADO, true);//Activamos la animaci�n del candado
    }
    public void DesactivateCandado()
    {
        AnimationLis[5].SetBool(ACTIVE_CANDADO, false);//Desactivamos la animaci�n del candado
    }
   public void StartPadlock()
    {
        Invoke("WaitPadlock", 0.80f);
    }
    public void StopPadlock() {
        AnimationLis[6].SetBool(ACTIVE_PADLOCK, false);//Se desactiva la animaci�n de desbloqueo 
    }
    public void ActiveWin()
    {
    AnimationLis[7].SetBool(START_WIN, true);//Activamos la animaci�n de win
    }
    public void DesactivateWin()
    {
    AnimationLis[7].SetBool(START_WIN, false);//Se desactiva la animaci�n para la ventana de Win
    }
    public void AtivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, true);//Se activa animaci�n del confeti
    }
    public void DesactivateConfeti()
    {
        AnimationLis[8].SetBool(START_CONFETI, false);//Se activa animaci�n del confeti
    }

    public void DesactivateAlert()
    {
        //M�todo encargado  de desactivar la alerta con animaci�n
        AnimaCon.ShareAnimation.AlertOff();
        AudioManager.shareaudio.Efectos[5].Play();
        Invoke("DescativateCanvasAlert", 0.5f);// damos un tiempo de llamado al m�todo para que el canvas no desaparesca al instante
    }

    public void DescativateCanvasAlert()
    {
        //Desactiva el Canvas de la Alerta
        ManagerScene.shareMscen.OffAlert();
        AudioManager.shareaudio.Partida.mute = false;
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

   
}
