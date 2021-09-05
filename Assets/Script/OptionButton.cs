//Este script nos permitirá presentar nuestras opciones en botones
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Librería usada para accedera los componentes de interfaz gráfica

[RequireComponent(typeof(Button))]//Esto permite que el game object siempre tenga este componente agreagdo automáticamente
[RequireComponent(typeof(Image))]
public class OptionButton: MonoBehaviour
{

    private Text text = null;// Hace referencia al texto de la opción
    private Button buttons = null;//Hace referencia a los botones de las opciones
    private Image img = null;//Hace referencia a la imagend e los botones
    public Option Option { get; set; } //Aqui estamos almamcenando y recuperando un valor, en este caso la opción seleccionada
    private Color origincolor = Color.white;//Referencia del color original de la imagen de los botones

    // Actión es un método que nos permite encapsular una función donde nos retorna un Valor nulo//
    // Si esta complejo entenderlo pero, diré que lo que esta haciendo ahi es avisar al creador del objeto que opción seleccionó el usuario
    // En base a eso el otro componente decidirá si continuamos la pregunta, activar sonidos, pausar el juego, etc 
    private void Awake()
    {
        buttons = GetComponent<Button>();//Se localiza la componente botones
        img = GetComponent<Image>();//Se localiza la componenete Imagenes
        text = transform.GetChild(0).GetComponent<Text>();// Devuelve un Transform (Posición, rotación y escala) hijo por índice.
        origincolor = img.color;//Se establece el color  de la variable origin color a la componente imagen

    }
    public void Construct(Option option, Action<OptionButton> callback)
    {
        buttons.onClick.RemoveAllListeners();//Se limpia todos los llamados
        text.text = option.text;//Se le asigna a la variable texto de los botones el texto que tenga la opción pasada por parámetro
        buttons.enabled = true;//Se habilita la interacción del botón 
        img.color = origincolor;//A la imagen se le establece esl color de la variable Origin
        Option = option;
        // Esta función permite construir el objeto(opciones)
        buttons.onClick.AddListener(delegate
        {
            callback(this);
        }
       
        /*Esta parte del script lo que esta haciendo es evaluar que cuando
        seleccionemos esta opción, le indiquemos al creador del objeto la opción que se seleccionó
        para luego pasar evaluar si es la correcta*/                                               
        );
    }
   
    public void SetColor(Color C)
    {
        //Función que coloca un color dependiendo si la opción seleccionada es correcta
        
       buttons.enabled = false;//Desactivamos el boton para que cuando pasemos el puntero por encima de el no controle el color, sino que lo haga manualmente
        img.color = C;
        
    }
     
}


