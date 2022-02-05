/* Esta clase  est� encargado de resolver un bug en la trivias, cuando suced�a el evento de presionar los botones
 y llamar a la courrutina este permitia presionar los dem�s botones aunque ya se haya seleccionado uno, provocando 
que el contadr se corrompa, no cumpliendo con lo establecido en las instrucciones.
Tambi�n controla el outline d ela letras para las eventualidades de responder bien o mal.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Libreria necesaria para controlar el aspecto de Interfaces de Uusuario

public class Control_Button : MonoBehaviour
{
    public Button buton1, buton2, buton3, buton4;//Variable de tipo Button, que sirve de referencia para los botones
    public Color Origin = Color.black;//Variable de tipo color utilizada para restablecer el color incial de el Outline del texto de las opciones
    public Outline b1, b2, b3, b4;//Variable de tipo Outline, sirve para lozalizar por referencia el outline de los Textos de los botones
    public int ButtonSelection;//Variable de tipo entera que almacena el n�mero de boton cliqueado

    // Start is called before the first frame update
    void Awake()
    {
        // Se lo caliza los hijos del objeto al cual le otorguemos este script
        buton1 = transform.GetChild(0).GetComponent<Button>();//Devuelve la posici�n, rotaci�n y escala de un hijo por �ndice 0
        buton2 = transform.GetChild(1).GetComponent<Button>();//Devuelve la posici�n, rotaci�n y escala de un hijo por �ndice 1
        buton3 = transform.GetChild(2).GetComponent<Button>();//Devuelve la posici�n, rotaci�n y escala de un hijo por �ndice 2
        buton4 = transform.GetChild(3).GetComponent<Button>();//Devuelve la posici�n, rotaci�n y escala de un hijo por �ndice 3
        // Se localizan los cuatros botones de las opciones
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        EventoPorBotones();  // Al ejecutarse este m�todo a cada frame evalua si los eventos suceden
    }

    public void EventoPorBotones()
    {
        // Si se presiona el uno de los botones llamr� a su m�todo corrrespondiente
        buton1.onClick.AddListener(button1Off);
        buton2.onClick.AddListener(button2Off);
        buton3.onClick.AddListener(button3Off);
        buton4.onClick.AddListener(button4Off);
        if(GameManager.shareInstance.currentgameState == GameState.GameOver || 
            GameManager.shareInstance.currentgameState == GameState.Win || 
            GameManager.shareInstance.currentgameState == GameState.Pause)
        {
            //Se evalua si nos encontramos en estos estados de juego para que los botones no sean presionados al estar en Game Over, Win o Pausa
            DesactivateButton();
        }
    }

    void button1Off()
    {
        //Este m�todo se encarga de desahabilitar la interacci�n con los botones 2,3,4
        buton2.interactable = false;
        buton3.interactable = false;
        buton4.interactable = false;
    }
    void button2Off()
    {
        //Este m�todo se encarga de desahabilitar la interacci�n con los botones 1,3,4
        buton1.interactable = false;
        buton3.interactable = false;
        buton4.interactable = false;
    }
    void button3Off()
    {
        //Este m�todo se encarga de desahabilitar la interacci�n con los botones 1,2,4
        buton1.interactable = false;
        buton2.interactable = false;
        buton4.interactable = false;
    }

    void button4Off()
    {
        //Este m�todo se encarga de desahabilitar la interacci�n con los botones 1,2,3
        buton1.interactable = false;
        buton2.interactable = false;
        buton3.interactable = false;
    }

    public void activebutton()
    {
        //Este m�todo activa todos los botones al pasar a la siguiente pregunta 
        buton1.interactable = true;
        buton2.interactable = true;
        buton3.interactable = true;
        buton4.interactable = true;
    }

    public void DesactivateButton()
    {
        //Este m�todo desactiva todos los botones dependiendo de la eventualidad
        buton1.interactable = false;
        buton2.interactable = false;
        buton3.interactable = false;
        buton4.interactable = false;
    }
  
    public void OutlineRed(int button)
    {
        //M�todo encargado de establecer el Outline de los botones en color Rojo cuando se conteste mal
        /*Esto se logr� pasando por parametro el n�mero del bot�n cliqueado, al establecer 
         el llamado de este m�todo a travez del Onclick en el editor de Unity*/
        ButtonSelection = button;/*Se le asigna a esta variable el entero pasado por par�metro,
        para que evalue el bot�n seleccionado, permitiendo activarse el color verde cuando las
        preguntas sean contestadas correctamente*/
        if (button==1)
        {
            //Para cuando el entero es 1
            b1.effectColor = Color.red;
        }else if (button == 2)
        {
            //Para cuando el entero es 2
            b2.effectColor = Color.red;
        }else if (button == 3)
        {
            //Para cuando el entero es 3
            b3.effectColor = Color.red;
        }else if (button == 4)
        {
            //Para cuando el entero es 4
            b4.effectColor = Color.red;
        }
    }

    public void OffOutlineRed()
    {
        /*M�todo encargado de restablecer el color original del Outline, cuando pasamos
         a la siguiente pregunta*/
        b1.effectColor = Origin;
        b2.effectColor = Origin;
        b3.effectColor = Origin;
        b4.effectColor = Origin;
    }

    public void OutlineGreen()
    { 
        /*M�todo encargado poner el outline de los botones en verde cuando se ha respondido de 
         una manera correcta*/
        if (ButtonSelection == 1)
        {
            //Para cuando el entero es 1
            b1.effectColor = Color.green;
        }
        else if (ButtonSelection == 2)
        {
            //Para cuando el entero es 12
            b2.effectColor = Color.green;
        }
        else if (ButtonSelection == 3)
        {
            //Para cuando el entero es 3
            b3.effectColor = Color.green;
        }
        else if (ButtonSelection == 4)
        {
            //Para cuando el entero es 4
            b4.effectColor = Color.green;
        }
    }


}

