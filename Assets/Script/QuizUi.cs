
//Este script sirve para controlar la interfaz de la trivia
using System;// Librería necesaria para que el método Actión funcioné
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUi : MonoBehaviour
{
    [SerializeField] private Text q_question = null;// Esta variable almacenará el texto de la pregunta 
    [SerializeField] private List<OptionButton> q_buttonl = null; // Esta lista nos permitirá tener una referencia de los botones que hemos creado(Button Option)

public void Construcman(Questions q , Action <OptionButton> callback)// Pasamos como parámetro el question por que es lo que vamos a requerir para trabajar dentro de la función
    {
        // Método que construirá el objeto para así poderlo utilizar
        q_question.text = q.text;// Esto coloca el texto de la pregunta que vayamos hacer
        for (int i = 0; i < q_buttonl.Count; i++)
            q_buttonl[i].Construct(q.options[i],callback); 
        // Esta sección se encarga de llamar al constructor del option butón, para que este construya los botones hasta la cantidad de opciones que tenga la lista
    }
}
