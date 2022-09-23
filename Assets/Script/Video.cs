/*Esta clase esta encargada de reproducir el video de intro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Video : MonoBehaviour
{
    void Start()
    {
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de Carga
        StartCoroutine(Wait_Intro());//Se llama a la corrutina
    }
    IEnumerator Wait_Intro()
    {
        //Se espera que se reproduzca el video
        yield return new WaitForSeconds(7f);    //PONER 7f
        ControlNiveles.shareLvl.CambiarNivel(78);//Se llama al método encargado de cambiar la escena a partir del pasado por parametro de un entero     
    }


}
