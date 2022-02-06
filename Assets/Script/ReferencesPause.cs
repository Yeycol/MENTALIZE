using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script esta encargado de pasar por referencia el canvas de la interfaz de pausa, para que pueda ser activada desde cualquier otra escena
public class ReferencesPause : MonoBehaviour
{
    public GameObject Pause;//Variable pública de tipo Game Object cuyá finalidad es la de pasar por referencia el canvas de Pausa
    public void ReturnMenu()
    {
     //Método encargado de volver al menú del videojuego
        ActivarOpciones.shareOp.OffCanvasPause();//Llamamos a un método encargado de desactivar el canvas de la pausa sin necesidad de pasarle In game
        ControlNiveles.shareLvl.CambiarNivel(3);//Llamamos al método encargado de cambiar el nivel con las transiciones, pasamos como parámetro el número de la escena que corresponde al menú
    }

    public void ResetOpciones()
    {
        //Método encargado de resetear la partida en modo pausa
        Contador.sharecont.resetcont();//Se llama al método encargado de resetear la partida
        ActivarOpciones.shareOp.DesactivatePause();//Se llama el método encargado de desactivar la interfaz de Pausa y pasar al modo de juego In Game
    }

    public void DesactivateOpciones()
    {
        ActivarOpciones.shareOp.DesactivatePause();//Se llama el método encargado de desactivar la interfaz de Pausa y pasar al modo de juego In Game
    }
    
}
