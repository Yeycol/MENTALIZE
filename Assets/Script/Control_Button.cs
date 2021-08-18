/* Este script está encargado de resolver un bug en la trivias, cuando sucedía el evento de presionar los botones
 y llamar a la courrutina este permitia presionar los demás botones aunque ya e haya seleccionado uno, provocando 
que el contadr se corrompa, no cumpliendo con lo establecido en las instrucciones*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Button : MonoBehaviour
{
    public Button buton1, buton2, buton3, buton4;
    public Color Origin = Color.black;
    public Outline b1, b2, b3, b4;
    public OptionButton optionbutton;
    public int ButtonSelection;

    // Start is called before the first frame update
    void Awake()
    {
        // Se lo caliza los hijos del objeto al cual le otorguemos este script
        buton1 = transform.GetChild(0).GetComponent<Button>();
        buton2 = transform.GetChild(1).GetComponent<Button>();
        buton3 = transform.GetChild(2).GetComponent<Button>();
        buton4 = transform.GetChild(3).GetComponent<Button>();
        // Se localizan los cuatros botones de las opciones
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Al ejecutarse este método a cada frame evalua si los eventos suceden
        buton1.onClick.AddListener(button1Off);// Si se presiona el uno de los botones llamrá a su método corrrespondiente
        buton2.onClick.AddListener(button2Off);
        buton3.onClick.AddListener(button3Off);
        buton4.onClick.AddListener(button4Off);
    }


    void button1Off()
    {
        //Este método se encarga de desahabilitar la interacción con los botones 2,3,4
        buton2.interactable = false;
        buton3.interactable = false;
        buton4.interactable = false;
    }
    void button2Off()
    {
        //Este método se encarga de desahabilitar la interacción con los botones 1,3,4
        buton1.interactable = false;
        buton3.interactable = false;
        buton4.interactable = false;
    }
    void button3Off()
    {
        //Este método se encarga de desahabilitar la interacción con los botones 1,2,4
        buton1.interactable = false;
        buton2.interactable = false;
        buton4.interactable = false;
    }

    void button4Off()
    {
        //Este método se encarga de desahabilitar la interacción con los botones 1,2,3
        buton1.interactable = false;
        buton2.interactable = false;
        buton3.interactable = false;
    }

    public void activebutton()
    {
        //Este método activa todos los botones
        buton1.interactable = true;
        buton2.interactable = true;
        buton3.interactable = true;
        buton4.interactable = true;
    }
  
    public void OutlineRed(int button)
    {
        ButtonSelection = button;
        if (button==1)
        {
            b1.effectColor = Color.red;
        }else if (button == 2)
        {
            b2.effectColor = Color.red;
        }else if (button == 3)
        {
            b3.effectColor = Color.red;
        }else if (button == 4)
        {
            b4.effectColor = Color.red;
        }
    }

    public void OffOutlineRed()
    {
        b1.effectColor = Origin;
        b2.effectColor = Origin;
        b3.effectColor = Origin;
        b4.effectColor = Origin;
    }

    public void OutlineGreen()
    {
        if (ButtonSelection == 1)
        {
            b1.effectColor = Color.green;
        }
        else if (ButtonSelection == 2)
        {
            b2.effectColor = Color.green;
        }
        else if (ButtonSelection == 3)
        {
            b3.effectColor = Color.green;
        }
        else if (ButtonSelection == 4)
        {
            b4.effectColor = Color.green;
        }
    }


}

