
using UnityEngine;
using UnityEngine.UI;
using System;

public static class BuyEvent
{       
        //Esta clase esta encargada de evaluar si los botones han sido presionados para que el acti�n de un aviso de que ha sido seleccionado uno de ellos 
        public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
        {
            // Esta funci�n espera a que se cliquee el bot�n para que delegue y avise al action de lo sucedido
            button.onClick.AddListener(delegate ()
            {
                OnClick(param);//Se retorna el par�metro entero
            });
        }
    
}
