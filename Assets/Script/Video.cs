/*Esta clase esta encargada de reproducir el video de intro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Video : MonoBehaviour
{
    public float wite = 7f;//Variable de tipo flotante que especifica el tiempo para que la corrutina ejecute sus acciones
     void Awake()
    {
       
    }
    void Start()
    {
        ControlNiveles.shareLvl.ReferCar.GuardarPreverLoad(7);//Llamamos al método encargado de enviar el entero de la escena que debe ser cargada después de la pantalla de carga
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de Carga
        AudioManager.shareaudio.Efectos[14].Pause();//Establecemos en pause por que al pasar modo de juego Load este tiene establecido en su método despausar la musica del menu, colocarlo en Play y que esta se repita en bucle
        StartCoroutine(Wait_Intro());//Se llama a la corrutina
    }
    IEnumerator Wait_Intro()
    {
        //Se espera que se reproduzca el video
        yield return new WaitForSeconds(wite);
        ControlNiveles.shareLvl.CambiarNivel(5);//Se llama al método encargado de cambiar la escena a partir del pasado por parametro de un entero     
        GameManager.shareInstance.LoadPartyandGame();//Llamamos nuevamente al estado de cargado con la finalidad que se quite la pausa de la música y este pase a modo load por la pantalla de carga
    }


}
