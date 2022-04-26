using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*Este script esta encargado de conrolar el tama�o y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporci�n del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    public CanvasScaler[] ReferencesScalerScene;//Array de tipo canvas scaler que servir� para adaptar las GUI
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posici�n,tama�o y anclaje de un rectangulo)*/
    public RectTransform []ObjectRecTransformGeneral;
    void Start()
    {
        DetecResolution();//M�todo encargado de obtener la relaci�n de aspecto de la c�mara y seg�n su ratio establecer las configuraciones para los gr�ficos de la Interfaz
    }

    public void DetecResolution()
    {
        if (GameManager.shareInstance.currentgameState == GameState.menu)
        {
            aspect = Camera.main.aspect; //Almacenamos el ratio detectado por la c�mara en nuestra variable flotante
            rounded = (int)(aspect * 100.0f) / 100.0f;//Rounded variable flotante que almacena la relaci�n de aspecto con un m�ximo de 2 decimales
                                                      //Nota: Lo que hace es multiplicar el ratio por 100 y este flotante resultante se lo pasa a entero por una conversi�n explicita(casting), y de ahi se lo divide dando como resultado un flotante con dos decimales
            switch (rounded)
            {
                /*-|RecTransformList|-*/
                /* |Inicio|
                 |0=FondoHorizontal
                 |1=MarcoProtafolio
                 |2=MarcoLogros
                 */

                /*-|CanvasScaler|-*/
                /* |Inicio|
                 |0=Inicio
                 |1=Portafolioversiones
                 |2=
                 */


                case 0.45f://Sansung Galaxy S20
                    ReferencesScalerScene[0].matchWidthOrHeight= 0.83f;//Controlamos la relaci�n de altura de las interfaces Gr�ficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f,0f);//Por si se necesitar� mover la posici�n de algun objeto utiliamos la posici�n local
                    break;
            }
            //TODO: Establecer mas relaciones de aspecto para adaptar la interfaz a diferente resoluciones
        }
    }
}