
/*Este script nos permitirá escribir la pregunta y mostrar una lista de
opciones de la trivia que estan serealizadas y nos permite mostrarlas directamente en el editor*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    // Start is called before the first frame update
   
   

// La pregunta debe descender de MonoBehaviour por que esta la añadiremos en un Game Object


    public string text = null;// este sera el texto que se mostrará en la pregunta 
    public List<Option> options = null;//Lista de las opciones


}
