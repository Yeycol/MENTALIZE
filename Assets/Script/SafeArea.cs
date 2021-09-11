using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Esta clase se encarga de ajustar nuestra interfaz a la área segura de los diferentes dispositivos
 para que la interfaz se pueda apreciar correctamente*/
public class SafeArea : MonoBehaviour
{
    RectTransform rectransform;/*Hace referencia a la posición, tamaño, anchors del objeto a ajustar,
   como los hijos se ajustan tal como su padre, los objetos dentro de este se autoajustaran*/
    Rect safeArea;//Hace referencia a al posición, tamaño y anchors de la área segura. según la resolución
    Vector2 minAnchor;//Almacenan los valores minimos del anchor
    Vector2 maxAnchor;//Almacenan los valores máximos del anchor

    void Awake()
    {
        rectransform = GetComponent<RectTransform>();//Obtenemos la componente RectTransform  del objeto al cual asigneos este script
        safeArea = Screen.safeArea;//La devolución del área segura de la pantalla en píxeles es almacena ene sta variable
        //Se establece los valores de safe Área en nuestra variables
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;
        //Dividimos los valores obtenidos por el ancho y alto de la pantalla detectada
        minAnchor.x /= Screen.width; 
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width; 
        maxAnchor.y /= Screen.height;
        //Y establecemos los anchors finales del objeto al que queremos autoajustar a la área segura
        rectransform.anchorMin = minAnchor;
        rectransform.anchorMax = maxAnchor;
    }
}
