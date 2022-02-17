using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeAnimation : MonoBehaviour
{
    public static RealTimeAnimation ShareRealTimeAnimator;
    public int indextime;//Varibale de tipo entera que ser� utilizada para sacar un entero aleatorio del Array EventTime
    public int indexvidas;//Variable de tipo entero que almacenar� el entero aleatorio para las eventualidades de vida 
    public int indexwin;//Variable de tipo entero que almacenar� un entero aleatorio de la posici�n del array que contiene los animators para Win
    public Animator[] EventTime = new Animator[3];//Array que contiene las animaciones para las eventualidades del tiempo 
    public Animator[] EventVidas = new Animator[3];//Array que contiene la componente Animator de los objetos de la animaci�n para cuando se pierde vidas  
    public Animator[] EventWin = new Animator[4];// Array que contendr� la componente Aniamtor que controlar� las animaciones para esta eventualidad
    public Animator[] EventCorrectAndError = new Animator[4]; //Array que almacena las componenetes animator de los objetos que contienen las animaciones para esta eventualidad
    public ParameterAndTime[] ValueNecesary;//Array que contiene los strings de los nombres de los par�metros para activar la animaci�n
    public Canvas Refer;//Hace referiencia al Canvas Que contiene las animaciones
    public GameObject BlockTrivias;//Variable de tipo GameObject que hace referencia al objeto que es habilitado para evitar interacci�n
    private void Awake()
    {
        if (ShareRealTimeAnimator == null)
        {
            ShareRealTimeAnimator = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    public void RamdomIndex()
    {
        //M�todo encargado de generar los index aleatorios de las animaciones ha habilitar
        indextime = Random.Range(0, EventTime.Length);//Se genera un n�mero entero aleatorio desde cero hasta la cantidad de objetos que tenga el array EventTime 
        indexvidas = Random.Range(0, EventVidas.Length);//El n�mero aleatorio generado por el m�todo Range es almacenado en la variable entera, este n�mero aleatorio es escogido desde 0 hasta la cantidad de elementos que tenga el array
        indexwin = Random.Range(0, EventWin.Length);//El n�mero aleatorio generado por el m�todo Range es almacenado en esta variable
        //Seg�n el index generado habilitamos los objetos de los arrays que contienen las animaciones con la finalidad de reducir los batches
        EventTime[indextime].gameObject.SetActive(true);
        //EventVidas[indexvidas].gameObject.SetActive(true);
        EventWin[indexwin].gameObject.SetActive(true);
        BlockTrivias.SetActive(true);//Habilitamos el objeto que permite quitar la interacci�n de los botones
    }

    public void ResetIndex()
    {
        //M�todo en cargado de desahabilitar los objetos anteriores al reinicio de una partida
        EventTime[indextime].gameObject.SetActive(false);
        //EventVidas[indexvidas].gameObject.SetActive(false);
        EventWin[indexwin].gameObject.SetActive(false);
    }

    public void EventInGame(string Event)
    {

        //Este m�todo recibe dos par�metros de tipo string el uno almacena el tipo de evento que est� por suceder y el otro el nombre del par�metro , necesario para establecer el booleano a la componente animator
        Contador.sharecont.IntroAnimation = true;//Se indica que el comportamiento EventTime de la clase Contador debe tomar en cuenta los frmaes para ser llamado
        switch (Event)
        {
            case "Time":// En caso del evento llamarse Time
                if (indextime == 0)//En caso de que el n�mero aleatorio sea igual a 0
                {
                    StartCoroutine(StartEvenTAnimationTime(ValueNecesary[0].ParameterAnimator, ValueNecesary[0].TimeCourrutine));//Se llama a la corrutina para dar tiempo a visualizar la animaci�n pasando por par�metro la posici�n de la animaci�n, par�metro que se tiene que activar y tiempo de la corrutina, para mostrar la animaci�n
                }
                else if (indextime == 1)//En caso que el n�mero aleatorio sea igual a 1
                {
                    StartCoroutine(StartEvenTAnimationTime( ValueNecesary[1].ParameterAnimator, ValueNecesary[1].TimeCourrutine));
                }
                else if (indextime == 2)//En caso que el n�mero aleatorio sea igual a 2
                {
                    StartCoroutine(StartEvenTAnimationTime( ValueNecesary[2].ParameterAnimator, ValueNecesary[2].TimeCourrutine));
                }
                break;

            case "Vida":
                if (indexvidas == 0)
                {
                    StartCoroutine(StartEventAnimationHealth( ValueNecesary[3].ParameterAnimator, ValueNecesary[3].TimeCourrutine));
                }
                else if (indexvidas == 1)
                {
                    StartCoroutine(StartEventAnimationHealth( ValueNecesary[4].ParameterAnimator, ValueNecesary[4].TimeCourrutine));
                }
                else if (indexvidas == 2)
                {
                    StartCoroutine(StartEventAnimationHealth( ValueNecesary[5].ParameterAnimator, ValueNecesary[5].TimeCourrutine));
                }
                break;

            case "Win":
                if (indexwin == 0)
                {
                    StartCoroutine(StartEventWinGame( ValueNecesary[6].ParameterAnimator, ValueNecesary[6].TimeCourrutine));
                }
                else if (indexwin == 1)
                {
                    StartCoroutine(StartEventWinGame( ValueNecesary[7].ParameterAnimator, ValueNecesary[7].TimeCourrutine));
                }
                else if (indexwin == 2)
                {
                    StartCoroutine(StartEventWinGame( ValueNecesary[8].ParameterAnimator, ValueNecesary[8].TimeCourrutine));
                }
                else if (indexwin == 3)
                {
                    StartCoroutine(StartEventWinGame( ValueNecesary[9].ParameterAnimator, ValueNecesary[9].TimeCourrutine));
                }
                break;
        }

    }

    IEnumerator StartEvenTAnimationTime( string ParameterAnimator, float TimeCorru)
    {
        EventTime[indextime].SetBool(ParameterAnimator, true);//Se activa la animaci�n de acuerdo al entero obtenido en el ramdom y el nombre del par�metro pasado por par�metro
        yield return new WaitForSeconds(TimeCorru);
        Contador.sharecont.IntroAnimation = false;// Se desahabilita el tiempo lento para cuando una animaci�n esta activa
        EventTime[indextime].SetBool(ParameterAnimator, false);//Se activa la animaci�n de salida de la eventualidad
        StartCoroutine(WaitForCanvas());//Llamamos a la corrutina encargada de dar tiempo a la animaci�n de salida
    }
    IEnumerator WaitForCanvas()
    {
        //Corrutina destinada a dar tiempo a la animaci�n de salida(Desactiva el Canvas)
        yield return new WaitForSeconds(0.5f);
        Refer.enabled = false;//Desahabilitamos el canvas pasado por referencia
    }
   
    IEnumerator StartEventAnimationHealth( string ReferencesParameter, float RefrencesTimeCorrutine)
    {
        EventVidas[indexvidas].SetBool(ReferencesParameter, true);
        yield return new WaitForSeconds(RefrencesTimeCorrutine);
        EventVidas[indexvidas].SetBool(ReferencesParameter, false);
        Contador.sharecont.IntroAnimation = false;
        EventVidas[indexvidas].gameObject.SetActive(false);
        StartCoroutine(WaitForCanvas());
    }

    IEnumerator StartEventWinGame( string ReferencesPar, float ReferencesTimeCo)
    {
        EventWin[indexwin].SetBool(ReferencesPar, true);
        yield return new WaitForSeconds(ReferencesTimeCo);
        EventWin[indexwin].SetBool(ReferencesPar, false);
        Contador.sharecont.IntroAnimation = false;
        StartCoroutine(WaitForReset());//LLamamos a la corrutina despu�s de un tiempo establecidopara que habilite los botones del canvas y poder reiniciar

    }
    IEnumerator WaitForReset()
    {
        //Corrutina encargada de permitir resetear despu�s de la salida de una animaci�n 
        yield return new WaitForSeconds(0.6f);
        BlockTrivias.SetActive(false);//Desactivamos el obejto que permite evitar la interacci�n con el GUI
    }
    public void ActiveNaveCorrect()
    {
        //M�todo encargado de habilitar la animaci�n de Nave cuando una pregunta es contestada correctamente
        EventCorrectAndError[1].SetBool("StartNaveCorrect", true);

    }
    public void DesactiveNaveCorrect()
    {
        //M�todo encargado de desahabilitar la animaci�n de salida cuando una pregunta es contestada correctamente
        EventCorrectAndError[1].SetBool("StartNaveCorrect", false);
    }
    public void ActiveNaveError()
    {
        //M�todo encargado de habilitar la animaci�n para cuando se contesta mal una pregunta
        EventCorrectAndError[0].SetBool("StartNaveError", true);

    }
    public void DesactiveNaveError()
    {
        //M�todo encargado de habilitar la animaci�n para cuando una pregunta es mal contestada
        EventCorrectAndError[0].SetBool("StartNaveError", false);
    }
    public void ActiveAnimationExtraTime()
    {
        //Activa la animaci�n cuando se da tiempo extra
        EventCorrectAndError[2].SetBool("StartExtraTime", true);//Activa la animaci�n del reloj
        EventCorrectAndError[3].SetBool("StartTextTimeExtra", true);//Activa la animaci�n de las letras
    }
    public void DesactiveAnimationExtraTime()
    {
        //Desactiva la animaci�n cuando se da tiempo extra
        EventCorrectAndError[2].SetBool("StartExtraTime", false);//Desactiva la animaci�n del reloj
        EventCorrectAndError[3].SetBool("StartTextTimeExtra", false);//Desactiva la animaci�n de las letras
    }
    public void StartAnimationConfeti(){
        //M�todo encargado de habilitar la animaci�n del confeti
        EventCorrectAndError[4].SetBool("StartConfeti", true);
    }
    public void StoptAnimationConfeti()
    {
        //M�todo encargado de habilitar la animaci�n del confeti
        EventCorrectAndError[4].Rebind();
        EventCorrectAndError[4].Update(0f);
        RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[4].gameObject.SetActive(false);//Desactivamos el objeto de confeti para evitar acumulaci�n de batches  
    }
  
}

[System.Serializable]
public class ParameterAndTime
{
    //Esta clase tiene la funcionalidad de servir para a creaci�n de un array de este tipo para poder asignar a sus propiedades datos
    public float TimeCourrutine;//Campo de clase que pretende almacenar un flotante del tiempo de duraci�n de la animac��n
    public string ParameterAnimator;//Campo de clase que pretende almacenar un string del nombre del par�metro del Animator
}