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
        aspect = Camera.main.aspect; //Almacenamos el ratio detectado por la cámara en nuestra variable flotante
        rounded = (int)(aspect * 100.0f) / 100.0f;//Rounded variable flotante que almacena la relación de aspecto con un máximo de 2 decimales
        //Nota: Lo que hace es multiplicar el ratio por 100 y este flotante resultante se lo pasa a entero por una conversión explicita(casting), y de ahi se lo divide dando como resultado un flotante con dos decimales
        if (rounded == 0.75f)//Si al aspect ratio es igual a 0.75 que equivale a una pantalla de 768 x 1024
        {
            /*offsetMax=Desplazamiento de la esquina superior derecha del rectángulo en 
              relación con el anclaje superior derecho.}
            offsetMin= Desplazamiento de la esquina inferior izquierda del rectángulo en 
            relación con el anclaje inferior izquierdo.
            Vectoe 2= Representa la posición y vectores 2D*/
            //Se establece el tamaño del rectángulo de los objetos de nuestra interfaz, de tal forma que  cambien de tamaño en las diferentes resoluciones
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