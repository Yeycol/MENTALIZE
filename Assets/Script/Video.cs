/*Esta clase esta encargada de reproducir el video de intro*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Video : MonoBehaviour
{
    public float wite = 6f;//Variable de tipo flotante que especifica el tiempo para que la corrutina ejecute sus acciones
    
    void Awake()
    {
        AudioManager.shareaudio.Partida.mute=true;//Al despertarse, se mutea la música del juego, debido a que se reproduce el video de intro
    }
    void Start()
    {
        ControlNiveles.shareLvl.ReferCar.GuardarPreverLoad(3);
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de Carga
        StartCoroutine(Wait_Intro());//Se llama a la corrutina
    }
    IEnumerator Wait_Intro()
    {
        //Se espera que se reproduzca el video
        yield return new WaitForSeconds(wite);
        ControlNiveles.shareLvl.CambiarNivel(5);//Se llama al método encargado de cambiar la escena a partir del pasado por parametro de un entero     
        AudioManager.shareaudio.Partida.Play();//Se vuelve a reproducir el audio desde un inicio para que no se escuche entrecortado al desmutear
    }


}
