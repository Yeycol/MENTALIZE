using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlSección : MonoBehaviour
{
    public static ControlSección ShareTienda;
    public GameObject Cartas;//Variable que almacena el objeto llamado ViewPort
    public GameObject Perfiles;//Variable que almacena el objeto llamado Perfiles
    public GameObject Fondos;//Variable que almacena el objeto llamado Fondos
    public List<ShopItem> ListaObjetos = null;//Hace referencia a la Lista de la clase propia Llamada ShopItem
    public List<int> IdObjetos;//Lista que alamacenará los id tipo enteros pasados por parámetros
    public Guardado GuardadoListas;//Variable de tipo d ela clase guardado, que sirve apra acceder al guardado por binario
    public int ReferencesIdList;//Variable pública que servirá para almacenar los ids(int), de acuerdo al indice de la Lista IdObjetos

    [Serializable] public class ShopItem
    {
        //Clase propia que almacena la referencia de los Items, Precio, Botones, Texto de botones,etc
        public Image Items;//Variable tipo imagen que almamcena los items
        public int Price;//Variable tipo entero que almacena el precio de los items
        public Button Botones;//Variable tipo button que almacena los botones
        public Text Txt_Buttons;//Variable tipo texto de botones que servirá para mostrar si se ha comprado
        public Text Precio;//Variable tipo text que mostrará el precio de los items por GUI 
        public bool IsPurchased = false;//Variable tipo booleano que servirá para saber si el item ya ha sido comprado 
    }
    private void Awake()
    {
        if (ShareTienda == null)
        {
            ShareTienda = this;
        }
        GuardadoListas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Se localiza el objeto según el string establecido y se obtiene la componente
    }
    private void Start()
    {
        InicializarTienda();//Método encarga de inicializar los precios y el AddEventlistener
        CargadoIdobjetos();//Método que se encarga de cargar los ids comprados
        ButtonNum(1);//Método encargado de mostrarnos las diferentes categorías de los items
     

    }
    public void CargadoIdobjetos()
    {
        GuardadoListas.CargarList();//Llamamos al método de la clase Guardado para que cargue la lista
        EvaluaciónDeComprado();//Método encargado de evaluar la lista de ids desbloqueados
    }

    public void EvaluaciónDeComprado()
    {
        for (int i = 0; i < IdObjetos.Count; i++)
        {
            //Este for esta en bucle hasta la cantidad de objetos almacenados en la lista IdObjetos
             ReferencesIdList = IdObjetos[i] ;//Segun el valor de i(Posición) este almacenará el valor que contenga dicho índice en ReferencesIdList
            /*Se hace una evaluación en todo los casos posibles por cada posición
             puesto que la compra de un item de la tienda se añadira y comprará 
            de manera aleatoria*/
            switch (ReferencesIdList)
            {
                case 0://En caso de que el valor de i posición sea igual a 0
                    ListaObjetos[ReferencesIdList].IsPurchased = true;//Se ha comprado se pasa a verdadero
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";//En el texto de los botones se lo cambia por comprado
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;//Se desactiva la interacción de los botones de comprar
                    break;
                case 1://En caso de que el valor de i posición sea igual a 1
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 2://En caso de que el valor de i posición sea igual a 2
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 3://En caso de que el valor de i posición sea igual a 3
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 4://En caso de que el valor de i posición sea igual a 4
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 5://En caso de que el valor de i posición sea igual a 5
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 6://En caso de que el valor de i posición sea igual a 6
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 7://En caso de que el valor de i posición sea igual a 7
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 8://En caso de que el valor de i posición sea igual a 8
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 9://En caso de que el valor de i posición sea igual a 9
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 10://En caso de que el valor de i posición sea igual a 10
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 11://En caso de que el valor de i posición sea igual a 11
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 12://En caso de que el valor de i posición sea igual a 12
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 13://En caso de que el valor de i posición sea igual a 13
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 14://En caso de que el valor de i posición sea igual a 14
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 15://En caso de que el valor de i posición sea igual a 15
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 16://En caso de que el valor de i posición sea igual a 16
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 17://En caso de que el valor de i posición sea igual a 17
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 18://En caso de que el valor de i posición sea igual a 18
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 19://En caso de que el valor de i posición sea igual a 19
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
                case 20://En caso de que el valor de i posición sea igual a 20
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    break;
            }
        }
    }
    public void InicializarTienda()
    {
        //Se muestra por GUI los precios de cada item según su índice
        ListaObjetos[0].Precio.text = ListaObjetos[0].Price.ToString();
        ListaObjetos[1].Precio.text = ListaObjetos[1].Price.ToString();
        ListaObjetos[2].Precio.text = ListaObjetos[2].Price.ToString();
        ListaObjetos[3].Precio.text = ListaObjetos[3].Price.ToString();
        ListaObjetos[4].Precio.text = ListaObjetos[4].Price.ToString();
        ListaObjetos[5].Precio.text = ListaObjetos[5].Price.ToString();
        ListaObjetos[6].Precio.text = ListaObjetos[6].Price.ToString();
        ListaObjetos[7].Precio.text = ListaObjetos[7].Price.ToString();
        ListaObjetos[8].Precio.text = ListaObjetos[8].Price.ToString();
        ListaObjetos[9].Precio.text = ListaObjetos[9].Price.ToString();
        for (int i = 0; i < ListaObjetos.Count; i++)
        {
            //Este for esta en bucle hasta la cantidad de elementos que tenga la ListaObjetos
            ListaObjetos[i].Botones.AddEventListener(i, OnShopItemBtnClicked);/*Se añade a cada botón AddListener,
            esperando que se presione alguno de ellos para que este llame al método encargado de establecer que ya 
            se ha comprado un item y pasar por referencia un entero del botón seleccionado*/
        }
                 
    }

    public void OnShopItemBtnClicked(int itemIndex)//Se pasa por parámetro el entero de (param)
    {
        ListaObjetos[itemIndex].IsPurchased = true;//Se ha comprado se pasa a verdadero
        ListaObjetos[itemIndex].Txt_Buttons.text = "Adquirido";//En el texto de los botones se lo cambia por comprado
        ListaObjetos[itemIndex].Botones.interactable = false;//Se desactiva la interacción de los botones de comprar
        IdObjetos.Add(itemIndex);//Se añade a la lista el Id entero del objeto comprado
        GuardadoListas.GuardarList();//Se guarda estas compras, llamando a la clase Guardado(GuardadoList)
    }
    public void ButtonNum(int PressButton)
    {
        //Metodo que resive por parámetro el entero del botón presionado
        if (PressButton == 1)//Si en este caso es uno, habilitara el objeto Cartas y los demas serán deshabilitados
        {
            Cartas.SetActive(true);
            Perfiles.SetActive(false);
            Fondos.SetActive(false);
        } else if (PressButton==2)//Si en este caso es dos, habilitara el objeto Perfiles y los demas serán deshabilitados
        {
            Perfiles.SetActive(true);
            Cartas.SetActive(false);
            Fondos.SetActive(false);
        }else
        {
            //Sino es ninguno de los anteriores, se habilitará el objeto Fondos y los demas serán deshabilitados
            Fondos.SetActive(true);
            Cartas.SetActive(false);
            Perfiles.SetActive(false);
        }
    }
}
