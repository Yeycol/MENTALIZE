
[System.Serializable]
/*Nuestra clase debe ser serealizable, 
 para que nos permita editar nuestras variables desde el editor de unity 
La funcionalidad de este script es poner cada opci�n de la trivia por separado*/
public class Option 
{
    public string text=null;//Esta variable almacenar� el texto de las opciones que ver� el jugador 
    public bool correct = false; //Esta variable booleana nos servir� par indicar si la opci�n seleccionada es correcta
}
