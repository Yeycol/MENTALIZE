using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Este script esta encargado de conrolar el tamaño y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporción del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posición,tamaño y anclaje de un rectangulo)*/
    public RectTransform []RectTrivias ;
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
                /*offsetMax=Desplazamiento de la esquina superior derecha del rectángulo en 
               relación con el anclaje superior derecho.
             offsetMin= Desplazamiento de la esquina inferior izquierda del rectángulo en 
             relación con el anclaje inferior izquierdo.
             Vectoe 2= Representa la posición y vectores 2D*/
                //Se establece el tamaño del rectángulo de los objetos de nuestra interfaz, de tal forma que  cambien de tamaño en las diferentes resoluciones
                /*0= ScrollView
                  1= Puerto
                  2= Letrero Digital
                  3=Contenedor de botones*/
                case 0.66f://Resolución Iphone 3GS (320x480), Iphone (4,4S)(640x960)
                    RectTrivias[0].offsetMin = new Vector2(40, -1);
                    RectTrivias[0].offsetMax = new Vector2(-40, -1);
                    RectTrivias[1].offsetMin = new Vector2(30, -1);
                    RectTrivias[1].offsetMax = new Vector2(-45, -1);
                    RectTrivias[2].offsetMin = new Vector2(40, -1);
                    RectTrivias[2].offsetMax = new Vector2(-40, -1);
                    break;
              
                case 0.46f://Resolución iPhone XR, Iphone XS, Iphone XS Max, Iphone 11,Iphone 11 Pro
                           //Iphone Pro Max, Iphone 12 Mini, Iphone 12 Pro, Iphone 12, Iphone 12 Pro Max
                           //Iphone 13 Pro, Iphone 13 Pro Max, Iphone 13 Mini
                                                        //Left //Bottom
                    RectTrivias[0].offsetMin = new Vector2(-5, -1);
                                                     //Right //Top
                    RectTrivias[0].offsetMax = new Vector2(-5, -1);
                    RectTrivias[1].offsetMin = new Vector2(-10, -5);
                    RectTrivias[1].offsetMax = new Vector2(-5, -10);
                    RectTrivias[2].offsetMin = new Vector2(-5, -1);
                    RectTrivias[2].offsetMax = new Vector2(-5, -4);
                                                           //PosX  //Pos Y
                    RectTrivias[3].anchoredPosition = new Vector2(0, 0);
                                                         //Width //High
                    RectTrivias[3].sizeDelta = new Vector2(0, 2830);
                    break;

                case 0.48f://Resolución Samsung Galaxy S8,S9,S9+, Note 8, Note 9, Google Pixel 3 XL, Xiaomi Mi 8
                           //Left //Bottom
                    RectTrivias[0].offsetMin = new Vector2(-5, -1);
                    //Right //Top
                    RectTrivias[0].offsetMax = new Vector2(-5, -1);
                    RectTrivias[1].offsetMin = new Vector2(-10, -5);
                    RectTrivias[1].offsetMax = new Vector2(-5, -10);
                    RectTrivias[2].offsetMin = new Vector2(-5, -1);
                    RectTrivias[2].offsetMax = new Vector2(-5, -4);
                    //PosX  //Pos Y
                    RectTrivias[3].anchoredPosition = new Vector2(0, 0);
                    //Width //High
                    RectTrivias[3].sizeDelta = new Vector2(0, 2830);
                    break;

                case 0.47f://Resolución One PLus 6
                           //Left //Bottom
                    RectTrivias[0].offsetMin = new Vector2(-5, -1);
                    //Right //Top
                    RectTrivias[0].offsetMax = new Vector2(-5, -1);
                    RectTrivias[1].offsetMin = new Vector2(-10, -5);
                    RectTrivias[1].offsetMax = new Vector2(-5, -10);
                    RectTrivias[2].offsetMin = new Vector2(-5, -1);
                    RectTrivias[2].offsetMax = new Vector2(-5, -4);
                    //PosX  //Pos Y
                    RectTrivias[3].anchoredPosition = new Vector2(0, 0);
                    //Width //High
                    RectTrivias[3].sizeDelta = new Vector2(0, 2830);
                    break;

                case 0.62f://Resolución Samsung Galaxi Note
                    RectTrivias[0].offsetMin = new Vector2(25, -1);
                    RectTrivias[0].offsetMax = new Vector2(-25, -1);
                    RectTrivias[1].offsetMin = new Vector2(20, -1);
                    RectTrivias[1].offsetMax = new Vector2(-30, -1);
                    RectTrivias[2].offsetMin = new Vector2(30, -1);
                    RectTrivias[2].offsetMax = new Vector2(-30, -1);
                    break;
                case 0.5f://Resolución LG G6, Google Pixel 2 Xl, Google Pixel 3,Huawei  Mate 10 Plus
                    RectTrivias[0].offsetMin = new Vector2(-5, -1);
                    //Right //Top
                    RectTrivias[0].offsetMax = new Vector2(-5, -1);
                    RectTrivias[1].offsetMin = new Vector2(-10, -5);
                    RectTrivias[1].offsetMax = new Vector2(-5, -10);
                    RectTrivias[2].offsetMin = new Vector2(-5, -1);
                    RectTrivias[2].offsetMax = new Vector2(-5, -4);
                    //PosX  //Pos Y
                    RectTrivias[3].anchoredPosition = new Vector2(0, 0);
                    //Width //High
                    RectTrivias[3].sizeDelta = new Vector2(0, 2830);
                    break;

                case 0.75f://Resolución de Ipad 1 y 2,3,4, Ipad Air 1, 2,
                                                          //Left //Bottom
                    RectTrivias[0].offsetMin = new Vector2(56, -1);
                                                         //Right //Top
                    RectTrivias[0].offsetMax = new Vector2(-70, -1);
                    RectTrivias[1].offsetMin = new Vector2(45, 5);
                    RectTrivias[1].offsetMax = new Vector2(-70, -10);
                    RectTrivias[2].offsetMin = new Vector2(55, 1);
                    RectTrivias[2].offsetMax = new Vector2(-75, 1);
                    break;
                case 0.69f:
                                                        //Left  //Bottom
                    RectTrivias[0].offsetMin = new Vector2(35, -1);
                                                       //Right  //Top
                    RectTrivias[0].offsetMax = new Vector2(-35, 1);
                    RectTrivias[1].offsetMin = new Vector2(30, -10);
                    RectTrivias[1].offsetMax = new Vector2(-40, -2);
                    RectTrivias[2].offsetMin = new Vector2(40, 1);
                    RectTrivias[2].offsetMax = new Vector2(-40, 5);
                    break;
            }

            //TODO: Establecer mas relaciones de aspecto para adaptar la interfaz a diferente resoluciones
        }
    }
}