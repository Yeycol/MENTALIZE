using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*Este script esta encargado de conrolar el tamaño y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporción del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    public CanvasScaler[] ReferencesScalerScene;//Array de tipo canvas scaler que servirá para adaptar las GUI
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posición,tamaño y anclaje de un rectangulo)*/
    public RectTransform []ObjectRecTransformGeneral;
    void Start()
    {
        DetecResolution();//Método encargado de obtener la relación de aspecto de la cámara y según su ratio establecer las configuraciones para los gráficos de la Interfaz
    }

    public void DetecResolution()
    {
        if (GameManager.shareInstance.currentgameState == GameState.menu)
        {
            aspect = Camera.main.aspect; //Almacenamos el ratio detectado por la cámara en nuestra variable flotante
            rounded = (int)(aspect * 100.0f) / 100.0f;//Rounded variable flotante que almacena la relación de aspecto con un máximo de 2 decimales
                                                      //Nota: Lo que hace es multiplicar el ratio por 100 y este flotante resultante se lo pasa a entero por una conversión explicita(casting), y de ahi se lo divide dando como resultado un flotante con dos decimales
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
                    ReferencesScalerScene[0].matchWidthOrHeight= 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f,0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                    break;
            }
            //TODO: Establecer mas relaciones de aspecto para adaptar la interfaz a diferente resoluciones
        }
    }
}