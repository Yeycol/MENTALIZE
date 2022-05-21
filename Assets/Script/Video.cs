/*Esta clase esta encargada de reproducir el video de intro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Video : MonoBehaviour
{
    void Start()
    {
        ControlNiveles.shareLvl.ReferCar.GuardarPreverLoad(7);//Llamamos al método encargado de enviar el entero de la escena que debe ser cargada después de la pantalla de carga
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de Carga
        StartCoroutine(Wait_Intro());//Se llama a la corrutina
    }
    IEnumerator Wait_Intro()
    {
        //Se espera que se reproduzca el video
        yield return new WaitForSeconds(0.5f);    //PONER 7f
        ControlNiveles.shareLvl.CambiarNivel(4);//Se llama al método encargado de cambiar la escena a partir del pasado por parametro de un entero     
        GameManager.shareInstance.LoadPartyandGame(true);//Llamamos nuevamente al estado de cargado con la finalidad que se quite la pausa de la música y este pase a modo load por la pantalla de carga, se pasa un booleano con la finalidad que se ejecuten las acciones a pasarle este modo de juego
    }


}
