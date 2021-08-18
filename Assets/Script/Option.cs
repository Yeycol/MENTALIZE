
[System.Serializable]
/*Nuestra clase debe ser serealizable, 
 para que nos permita editar nuestras variables desde el editor de unity 
La funcionalidad de este script es poner cada opción de la trivia por separado*/
public class Option 
{
    public string text=null;//Esta variable almacenará el texto de las opciones que verá el jugador 
    public bool correct = false; //Esta variable booleana nos servirá par indicar si la opción seleccionada es correcta
}
