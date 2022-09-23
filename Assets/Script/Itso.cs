using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itso : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ControlNiveles.shareLvl.ReferCar.GuardarPreverLoad(7);//Llamamos al método encargado de enviar el entero de la escena que debe ser cargada después de la pantalla de carga
        GameManager.shareInstance.LoadPartyandGame();//Pasamos el estado de juego en carga, sin establecer el booleano en true para que la música del menu no se reproduzca
        StartCoroutine(StayoneMoment());
    }

    // Update is called once per frame
   IEnumerator StayoneMoment()
    {
        //Se da un momento para que se muestre el logo del Itso
        yield return new WaitForSeconds(1f);
        GameManager.shareInstance.LoadPartyandGame(true);//Se establece en true la reproducción de la música del Menú
        ControlNiveles.shareLvl.CambiarNivel(4);//Nos dirijimos a la escena de carga para que nos direccione a la escena de inicio
    }
}
