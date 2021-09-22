using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Este script esta encargado de conrolar el tama�o y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporci�n del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posici�n,tama�o y anclaje de un rectangulo)*/
    public RectTransform []RectTrivias ;
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
                /*offsetMax=Desplazamiento de la esquina superior derecha del rect�ngulo en 
               relaci�n con el anclaje superior derecho.
             offsetMin= Desplazamiento de la esquina inferior izquierda del rect�ngulo en 
             relaci�n con el anclaje inferior izquierdo.
             Vectoe 2= Representa la posici�n y vectores 2D*/
                //Se establece el tama�o del rect�ngulo de los objetos de nuestra interfaz, de tal forma que  cambien de tama�o en las diferentes resoluciones
                /*0= ScrollView
                  1= Puerto
                  2= Letrero Digital
                  3=Contenedor de botones*/
                case 0.66f://Resoluci�n Iphone 3GS (320x480), Iphone (4,4S)(640x960)
                    RectTrivias[0].offsetMin = new Vector2(40, -1);
                    RectTrivias[0].offsetMax = new Vector2(-40, -1);
                    RectTrivias[1].offsetMin = new Vector2(30, -1);
                    RectTrivias[1].offsetMax = new Vector2(-45, -1);
                    RectTrivias[2].offsetMin = new Vector2(40, -1);
                    RectTrivias[2].offsetMax = new Vector2(-40, -1);
                    break;
              
                case 0.46f://Resoluci�n iPhone XR, Iphone XS, Iphone XS Max, Iphone 11,Iphone 11 Pro
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

                case 0.48f://Resoluci�n Samsung Galaxy S8,S9,S9+, Note 8, Note 9, Google Pixel 3 XL, Xiaomi Mi 8
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

                case 0.47f://Resoluci�n One PLus 6
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

                case 0.62f://Resoluci�n Samsung Galaxi Note
                    RectTrivias[0].offsetMin = new Vector2(25, -1);
                    RectTrivias[0].offsetMax = new Vector2(-25, -1);
                    RectTrivias[1].offsetMin = new Vector2(20, -1);
                    RectTrivias[1].offsetMax = new Vector2(-30, -1);
                    RectTrivias[2].offsetMin = new Vector2(30, -1);
                    RectTrivias[2].offsetMax = new Vector2(-30, -1);
                    break;
                case 0.5f://Resoluci�n LG G6, Google Pixel 2 Xl, Google Pixel 3,Huawei  Mate 10 Plus
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

                case 0.75f://Resoluci�n de Ipad 1 y 2,3,4, Ipad Air 1, 2,
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