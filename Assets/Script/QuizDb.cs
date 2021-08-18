// Este script nos servirá para mostrar en el editor una lista de preguntas que contendrá la pregunta en si y sus opciones//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizDb : MonoBehaviour
{
    [SerializeField]private List<Questions> QuestionLis = null;// Esta variable declarada como semiprivada nos permitirá mostrarla en el editor
    private List<Questions> db_backup = null;/*Si nosostros tenemos 10 preguntas en la base de datos y se removieron todas,
                                              colocamos de nuevo todas las preguntas y empezamos de nuevo(Reseteador)*/

    private void Awake()
    {
        db_backup = QuestionLis.ToList() ; // Como en nuestra copia de la base de datos la inicializamos como nula, debemos decir que es igual a lo que tenga en ese momento la question lis para luego restaurarla
    }
    public Questions questionrandom (bool remove = true)
    {
        /*Este método nos permite que salga una pregunta ramdom de nuestra base de datos, el parámetro que 
         esperamos recibir es un booleano que simplemente servirá para remover una pregunta de la base datos
        para que esta no salga repetida*/

        if (QuestionLis.Count == 0)
        {
            // Aquí existe una llamada al método que Restaura las preguntas en la base de datos, siempre y cuando la lista sea igual a 0
            RestoreBackup();
        }
            
        int index = Random.Range(0, QuestionLis.Count);// Este método devuelve un número aleatorio del rango establecido
      
        if (!remove)
        {
            // Aqui retornamoes al index para que nos otorgue la siguiente pregunta random en caso de que el remove sea diferente de true
            return QuestionLis[index];
        }
        else
        {
            //En caso de que romove sea true  se removerá uno de los elementos de la lista
            Questions q = QuestionLis[index];// Primero se almacena el elmento que buscamos remover
            QuestionLis.RemoveAt(index);//Se remueve de la lista
            return q; //Retornamos a q (Question) que almacena la pregunta que sacamos de la base de datos
                     
        }
        }

    public void RestoreBackup()
    {
        //Metodo encargado de restaurar la base de datos 
        QuestionLis = db_backup.ToList();
    }
}
