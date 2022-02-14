
//Este script sirve para controlar la interfaz de la trivia
using System;// Librer�a necesaria para que el m�todo Acti�n funcion�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;//Libreria necesaria para manipular las componentes de Interfaces de Usuario

public class QuizUi : MonoBehaviour
{
    public GameObject q_question;// Esta variable almacenar� el texto de la pregunta 
    public TextMeshProUGUI questions;
    public List<OptionButton> q_buttonl = null; // Esta lista nos permitir� tener una referencia de los botones que hemos creado(Button Option)
 
    public void Construcman(Questions q , Action <OptionButton> callback)// Pasamos como par�metro el question retornada por el m�todo question random ,por que es lo que vamos a requerir para trabajar dentro del m�todo
    {
        // M�todo que construir� el objeto para as� poderlo utilizar
        questions.SetText (q.text);// Esto coloca el texto de la pregunta que vayamos hacer
        for (int i = 0; i < q_buttonl.Count; i++)
            q_buttonl[i].Construct(q.options[i],callback); 
        // Esta secci�n se encarga de llamar al constructor del option but�n, para que este construya los botones hasta la cantidad de botones que tenga la lista
    }
}
