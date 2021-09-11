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
        aspect = Camera.main.aspect; //Almacenamos el ratio detectado por la c�mara en nuestra variable flotante
        rounded = (int)(aspect * 100.0f) / 100.0f;//Rounded variable flotante que almacena la relaci�n de aspecto con un m�ximo de 2 decimales
        //Nota: Lo que hace es multiplicar el ratio por 100 y este flotante resultante se lo pasa a entero por una conversi�n explicita(casting), y de ahi se lo divide dando como resultado un flotante con dos decimales
        if (rounded == 0.75f)//Si al aspect ratio es igual a 0.75 que equivale a una pantalla de 768 x 1024
        {
            /*offsetMax=Desplazamiento de la esquina superior derecha del rect�ngulo en 
              relaci�n con el anclaje superior derecho.}
            offsetMin= Desplazamiento de la esquina inferior izquierda del rect�ngulo en 
            relaci�n con el anclaje inferior izquierdo.
            Vectoe 2= Representa la posici�n y vectores 2D*/
            //Se establece el tama�o del rect�ngulo de los objetos de nuestra interfaz, de tal forma que  cambien de tama�o en las diferentes resoluciones
            /*0= ScrollView
              1= Puerto
              2= Letrero Digital*/
                                               //Left //Top
            RectTrivias[0].offsetMin = new Vector2(56,-1);
                                              //Right //Bottom
            RectTrivias[0].offsetMax = new Vector2(-70, -1);
            RectTrivias[1].offsetMin = new Vector2(45,5 );
            RectTrivias[1].offsetMax = new Vector2(-70, -10);
            RectTrivias[2].offsetMin = new Vector2(55, 1);
            RectTrivias[2].offsetMax = new Vector2(-75, 1);
        }
     //TODO: Establecer mas relaciones de aspecto para adaptar la interfaz a diferente resoluciones
    
    }
}