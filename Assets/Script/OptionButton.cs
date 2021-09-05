//Este script nos permitir� presentar nuestras opciones en botones
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Librer�a usada para accedera los componentes de interfaz gr�fica

[RequireComponent(typeof(Button))]//Esto permite que el game object siempre tenga este componente agreagdo autom�ticamente
[RequireComponent(typeof(Image))]
public class OptionButton: MonoBehaviour
{

    private Text text = null;// Hace referencia al texto de la opci�n
    private Button buttons = null;//Hace referencia a los botones de las opciones
    private Image img = null;//Hace referencia a la imagend e los botones
    public Option Option { get; set; } //Aqui estamos almamcenando y recuperando un valor, en este caso la opci�n seleccionada
    private Color origincolor = Color.white;//Referencia del color original de la imagen de los botones

    // Acti�n es un m�todo que nos permite encapsular una funci�n donde nos retorna un Valor nulo//
    // Si esta complejo entenderlo pero, dir� que lo que esta haciendo ahi es avisar al creador del objeto que opci�n seleccion� el usuario
    // En base a eso el otro componente decidir� si continuamos la pregunta, activar sonidos, pausar el juego, etc 
    private void Awake()
    {
        buttons = GetComponent<Button>();//Se localiza la componente botones
        img = GetComponent<Image>();//Se localiza la componenete Imagenes
        text = transform.GetChild(0).GetComponent<Text>();// Devuelve un Transform (Posici�n, rotaci�n y escala) hijo por �ndice.
        origincolor = img.color;//Se establece el color  de la variable origin color a la componente imagen

    }
    public void Construct(Option option, Action<OptionButton> callback)
    {
        buttons.onClick.RemoveAllListeners();//Se limpia todos los llamados
        text.text = option.text;//Se le asigna a la variable texto de los botones el texto que tenga la opci�n pasada por par�metro
        buttons.enabled = true;//Se habilita la interacci�n del bot�n 
        img.color = origincolor;//A la imagen se le establece esl color de la variable Origin
        Option = option;
        // Esta funci�n permite construir el objeto(opciones)
        buttons.onClick.AddListener(delegate
        {
            callback(this);
        }
       
        /*Esta parte del script lo que esta haciendo es evaluar que cuando
        seleccionemos esta opci�n, le indiquemos al creador del objeto la opci�n que se seleccion�
        para luego pasar evaluar si es la correcta*/                                               
        );
    }
   
    public void SetColor(Color C)
    {
        //Funci�n que coloca un color dependiendo si la opci�n seleccionada es correcta
        
       buttons.enabled = false;//Desactivamos el boton para que cuando pasemos el puntero por encima de el no controle el color, sino que lo haga manualmente
        img.color = C;
        
    }
     
}


