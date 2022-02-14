using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeAnimation : MonoBehaviour
{
    public static RealTimeAnimation ShareRealTimeAnimator;
    public int indextime;//Varibale de tipo entera que será utilizada para sacar un entero aleatorio del Array EventTime
    public int indexvidas;//Variable de tipo entero que almacenará el entero aleatorio para las eventualidades de vida 
    public int indexwin;//Variable de tipo entero que almacenará un entero aleatorio de la posición del array que contiene los animators para Win
    public Animator[] EventTime = new Animator[3];//Array que contiene las animaciones para las eventualidades del tiempo 
    public Animator[] EventVidas = new Animator[3];//Array que contiene la componente Animator de los objetos de la animación para cuando se pierde vidas  
    public Animator[] EventWin = new Animator[4];// Array que contendrá la componente Aniamtor que controlará las animaciones para esta eventualidad
    public Animator[] EventCorrectAndError = new Animator[4]; //Array que almacena las componenetes animator de los objetos que contienen las animaciones para esta eventualidad
    public ParameterAndTime[] ValueNecesary;//Array que contiene los strings de los nombres de los parámetros para activar la animación
    public Canvas Refer;//Hace referiencia al Canvas Que contiene las animaciones
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
    private void Start()
    {
        RamdomIndex();
    }
    public void RamdomIndex()
    {
        indextime = Random.Range(0, EventTime.Length);//Se genera un número entero aleatorio desde cero hasta la cantidad de objetos que tenga el array EventTime 
        indexvidas = Random.Range(0, EventVidas.Length);//El número aleatorio generado por el método Range es almacenado en la variable entera, este número aleatorio es escogido desde 0 hasta la cantidad de elementos que tenga el array
        indexwin = Random.Range(0, EventWin.Length);//El número aleatorio generado por el método Range es almacenado en esta variable
    }

    public void EventInGame(string Event)
    {
        //Este método recibe dos parámetros de tipo string el uno almacena el tipo de evento que está por suceder y el otro el nombre del parámetro , necesario para establecer el booleano a la componente animator
        Contador.sharecont.IntroAnimation = true;//Se indica que el comportamiento EventTime de la clase Contador debe tomar en cuenta los frmaes para ser llamado
        switch (Event)
        {
            case "Time":// En caso del evento llamarse Time
                if (indextime == 0)//En caso de que el número aleatorio sea igual a 0
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[0].ParameterAnimator, ValueNecesary[0].TimeCourrutine));//Se llama a la corrutina para dar tiempo a visualizar la animación pasando por parámetro la posición de la animación, parámetro que se tiene que activar y tiempo de la corrutina, para mostrar la animación
                }
                else if (indextime == 1)//En caso que el número aleatorio sea igual a 1
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[1].ParameterAnimator, ValueNecesary[1].TimeCourrutine));
                }
                else if (indextime == 2)//En caso que el número aleatorio sea igual a 2
                {
                    StartCoroutine(StartEvenTAnimationTime(indextime, ValueNecesary[2].ParameterAnimator, ValueNecesary[2].TimeCourrutine));
                }
                break;

            case "Vida":
                //TODO: Llamar y establecer los valores que pasrán la corrutina por parámetro
                if (indexvidas == 0)
                {
                    StartCoroutine(StartEventAnimationHealth(indexvidas, ValueNecesary[3].ParameterAnimator, ValueNecesary[3].TimeCourrutine));
                }
                else if (indexvidas == 1)
                {
                    StartCoroutine(StartEventAnimationHealth(indexvidas, ValueNecesary[4].ParameterAnimator, ValueNecesary[4].TimeCourrutine));
                }
                else if (indexvidas == 2)
                {
                    StartCoroutine(StartEventAnimationHealth(indexvidas, ValueNecesary[5].ParameterAnimator, ValueNecesary[5].TimeCourrutine));
                }
                break;

            case "Win":
                if (indexwin == 0)
                {
                    StartCoroutine(StartEventWinGame(indexwin, ValueNecesary[6].ParameterAnimator, ValueNecesary[6].TimeCourrutine));
                }
                else if (indexwin == 1)
                {
                    StartCoroutine(StartEventWinGame(indexwin, ValueNecesary[7].ParameterAnimator, ValueNecesary[7].TimeCourrutine));
                }
                else if (indexwin == 2)
                {
                    StartCoroutine(StartEventWinGame(indexwin, ValueNecesary[8].ParameterAnimator, ValueNecesary[8].TimeCourrutine));
                }
                else if (indexwin == 3)
                {
                    StartCoroutine(StartEventWinGame(indexwin, ValueNecesary[9].ParameterAnimator, ValueNecesary[9].TimeCourrutine));
                }
                break;
        }

    }

    IEnumerator StartEvenTAnimationTime(int AnimationParameter, string ParameterAnimator, float TimeCorru)
    {
        EventTime[AnimationParameter].SetBool(ParameterAnimator, true);//Se activa la animación de acuerdo al entero obtenido en el ramdom y el nombre del parámetro pasado por parámetro
        yield return new WaitForSeconds(TimeCorru);
        EventTime[AnimationParameter].SetBool(ParameterAnimator, false);//Se activa la animación de salida de la eventualidad
        Contador.sharecont.IntroAnimation = false;//Se indica que el comportamiento de Contador deje de tomar en cuenta los frames
        StartCoroutine(WaitForCanvas());
    }
    IEnumerator WaitForCanvas()
    {
        //Corrutina destinada a dar tiempo a la animación de salida(Desactiva el Canvas)
        yield return new WaitForSeconds(0.9f);
        Refer.enabled = false;
    }
    //TODO: Crear la corrutina para el caso de la eventualidad del tiempo
    IEnumerator StartEventAnimationHealth(int ReferencesIndex, string ReferencesParameter, float RefrencesTimeCorrutine)
    {
        EventVidas[ReferencesIndex].SetBool(ReferencesParameter, true);
        yield return new WaitForSeconds(RefrencesTimeCorrutine);
        EventVidas[ReferencesIndex].SetBool(ReferencesParameter, false);
        Contador.sharecont.IntroAnimation = false;
    }

    IEnumerator StartEventWinGame(int ReferencesPosition, string ReferencesPar, float ReferencesTimeCo)
    {

        EventWin[ReferencesPosition].SetBool(ReferencesPar, true);
        yield return new WaitForSeconds(ReferencesTimeCo);
        EventWin[ReferencesPosition].SetBool(ReferencesPar, false);
        Contador.sharecont.IntroAnimation = false;
    }

    public void ActiveNaveCorrect()
    {
        EventCorrectAndError[1].SetBool("StartNaveCorrect", true);

    }
    public void DesactiveNaveCorrect()
    {
        EventCorrectAndError[1].SetBool("StartNaveCorrect", false);
    }
    public void ActiveNaveError()
    {
        EventCorrectAndError[0].SetBool("StartNaveError", true);

    }
    public void DesactiveNaveError()
    {
        EventCorrectAndError[0].SetBool("StartNaveError", false);
    }
    public void ActiveAnimationExtraTime()
    {
        //Activa la animación cuando se da tiempo extra
        EventCorrectAndError[2].SetBool("StartExtraTime", true);
        EventCorrectAndError[3].SetBool("StartTextTimeExtra", true);
    }
    public void DesactiveAnimationExtraTime()
    {
        //Desactiva la animación cuando se da tiempo extra
        EventCorrectAndError[2].SetBool("StartExtraTime", false);
        EventCorrectAndError[3].SetBool("StartTextTimeExtra", false);
    }
}

[System.Serializable]
public class ParameterAndTime
{
    //Esta clase tiene la funcionalidad de servir para a creación de un array de este tipo para poder asignar a sus propiedades datos
    public float TimeCourrutine;//Campo de clase que pretende almacenar un flotante del tiempo de duración de la animacíón
    public string ParameterAnimator;//Campo de clase que pretende almacenar un string del nombre del parámetro del Animator
}