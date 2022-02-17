using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script esta encargado de pasar por referencia el canvas de la interfaz de pausa, para que pueda ser activada desde cualquier otra escena
public class ReferencesPause : MonoBehaviour
{
  
    public void ReturnMenu()
    {
     //M�todo encargado de volver al men� del videojuego
        ActivarOpciones.shareOp.DesactivatePause();//Llamamos a un m�todo encargado de desactivar el canvas de la pausa sin necesidad de pasarle In game
        ControlNiveles.shareLvl.CambiarNivel(2);//Llamamos al m�todo encargado de cambiar el nivel con las transiciones, pasamos como par�metro el n�mero de la escena que corresponde al men�
    }

    public void ResetOpciones()
    {
        //M�todo encargado de resetear la partida en modo pausa
        Contador.sharecont.resetcont();//Se llama al m�todo encargado de resetear la partida
        ActivarOpciones.shareOp.DesactivatePause();//Se llama el m�todo encargado de desactivar la interfaz de Pausa y pasar al modo de juego In Game
        //ActivarOpciones.shareOp.OffCanvasPause();
    }

    public void DesactivateOpciones()
    {
            ActivarOpciones.shareOp.DesactivatePause();//Se llama el m�todo encargado de desactivar la interfaz de Pausa y pasar al modo de juego In Gam
    }
    
}
