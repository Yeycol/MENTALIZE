using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego
/*Este script esta encargado de conrolar el tamaño y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporción del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    public CanvasScaler[] ReferencesScalerScene;//Array de tipo canvas scaler que servirá para adaptar las GUI
    public Scene ScenaAdaptable;//Variable que almacenará las escenas detectadas 
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posición,tamaño y anclaje de un rectangulo)*/
    public RectTransform []ObjectRecTransformGeneral;
    void Awake()
    {
        ScenaAdaptable = SceneManager.GetActiveScene();//Se devuelve y almacena en la variable la escena activa    
    }
    void Start()
    {
        DetecResolution();//Método encargado de obtener la relación de aspecto de la cámara y según su ratio establecer las configuraciones para los gráficos de la Interfaz   
    }

    public void DetecResolution()
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

             */

            /*-|CanvasScaler|-*/
            /* |Inicio|
             |0=Inicio,SelectModo de juego 
             |1=Portafolioversiones
             |2= Canvas de Logros
             |3= Canvas de Carga
             */


            case 0.44f://Sansung Galaxy S20,S21 Ultra, Samsung Galaxy M23
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.85f;
                } else if (ScenaAdaptable.name=="SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.84f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.84f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } 
                break;
            case 0.46f://Sansung Galaxy S22+
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.88f;
                }else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.85f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.85f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;
            case 0.47f://Sansung Galaxy Note 10 +
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.89f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.86f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.87f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;
            case 0.48f://Samsung Galaxy J6+
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.90f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.89f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.89f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;
            case 0.4f://Samsung Galaxy Z Flip3 5G
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.78f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.66f, 1.66f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.80f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.66f, 1.66f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.80f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;
            case 0.36f://Samsung W22 5G
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.73f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.75f;
                }else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.74f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.74f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;
            case 0.5f://Samsung Galaxy A6s
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.91f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.92f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                break;

                
                //TODO: Establecer mas relaciones de aspecto para adaptar la interfaz a diferente resoluciones
        }
    }
}