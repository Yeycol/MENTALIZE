using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Esta clase se encarga de ajustar nuestra interfaz a la �rea segura de los diferentes dispositivos
 para que la interfaz se pueda apreciar correctamente*/
public class SafeArea : MonoBehaviour
{
    RectTransform rectransform;/*Hace referencia a la posici�n, tama�o, anchors del objeto a ajustar,
   como los hijos se ajustan tal como su padre, los objetos dentro de este se autoajustaran*/
    Rect safeArea;//Hace referencia a al posici�n, tama�o y anchors de la �rea segura. seg�n la resoluci�n
    Vector2 minAnchor;//Almacenan los valores minimos del anchor
    Vector2 maxAnchor;//Almacenan los valores m�ximos del anchor

    void Awake()
    {
        rectransform = GetComponent<RectTransform>();//Obtenemos la componente RectTransform  del objeto al cual asigneos este script
        safeArea = Screen.safeArea;//La devoluci�n del �rea segura de la pantalla en p�xeles es almacena ene sta variable
        //Se establece los valores de safe �rea en nuestra variables
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;
        //Dividimos los valores obtenidos por el ancho y alto de la pantalla detectada
        minAnchor.x /= Screen.width; 
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width; 
        maxAnchor.y /= Screen.height;
        //Y establecemos los anchors finales del objeto al que queremos autoajustar a la �rea segura
        rectransform.anchorMin = minAnchor;
        rectransform.anchorMax = maxAnchor;
    }
}
