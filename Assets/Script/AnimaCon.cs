//Script destinado al inicio y pausa de las animaciones en el videojuego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaCon : MonoBehaviour
{
    public List<Animator> AnimationLis = null;//Lista de las animaciones a controlar
    public static AnimaCon ShareAnimation;
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
        AnimationLis[2].SetBool(ACTIVE_ALERT, true);// Activamos la animación de la alerta
    }

    public void AlertOff()
    {
        AnimationLis[2].SetBool(ACTIVE_ALERT, false);// Desactivamos  la animación de la alerta
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
        AudioManager.shareaudio.Efectos[5].Play();
        AlertOff();
        StartCoroutine(DescativateCanvasAlert());// damos un tiempo de llamado al método para que el canvas no desaparesca al instante
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
    }
    public void DesactiveAnimationExtraTime()
    {
        //Desactiva la animación cuando se da tiempo extra
        AnimationLis[13].SetBool(START_EXITTIME, false);
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
}
