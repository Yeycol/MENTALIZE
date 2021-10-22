using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego

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
    public Button[] Button_Equipar;
    public int Ido;
    public string Equipado = "No";
    public Scene scene;// Variable privada de tipo escena que se utilizará para controlar y condicionar con las escenas
    public RectTransform [] ObjectsShop;//Arrays que almacenaran la posición, tamaño, anclaje y pivote de los Game Objects.
    [Serializable] public class ShopItem
    {
        //Clase propia que almacena la referencia de los Items, Precio, Botones, Texto de botones,etc
        public Image Items;//Variable tipo imagen que almamcena los items
        public int Price;//Variable tipo entero que almacena el precio de los items
        public Button Botones;//Variable tipo button que almacena los botones
        public Text Txt_Buttons;//Variable tipo texto de botones que servirá para mostrar si se ha comprado
        public Text Precio;//Variable tipo text que mostrará el precio de los items por GUI 
        public Image UI_Monedas;//Variable tipo image que hace referencia a las monedas de interfaz gráfica
        public bool IsPurchased = false;//Variable tipo booleano que servirá para saber si el item ya ha sido comprado 
        public Image Perfil;
        public Image Fondos;
    }
    private void Awake()
    {
        if (ShareTienda == null)
        {
            ShareTienda = this;
        }
        GuardadoListas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Se localiza el objeto según el string establecido y se obtiene la componente
            Ido = PlayerPrefs.GetInt("Id");
        Equipado = PlayerPrefs.GetString("Equipado");
        scene = SceneManager.GetActiveScene();//GetActiveScene es un método que nos permite obtener la escena activa actualmente
        
    }
    private void Start()
    {
        /*PlayerPrefs.DeleteKey("Id");
        PlayerPrefs.DeleteKey("Equipado");*/
     InicializarTienda();//Método encarga de inicializar los precios y el AddEventlistener
     CargadoIdobjetos();//Método que se encarga de cargar los ids comprados
        if (scene.name == "Tienda")
        {
            ButtonNum(1);//Método encargado de mostrarnos las diferentes categorías de los items
        }
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
                    ListaObjetos[ReferencesIdList].UI_Monedas.enabled = false;
                    ListaObjetos[ReferencesIdList].Precio.enabled=false;
                    break;
                case 1://En caso de que el valor de i posición sea igual a 1
                    ListaObjetos[ReferencesIdList].IsPurchased = true;
                    ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";
                    ListaObjetos[ReferencesIdList].Botones.interactable = false;
                    ListaObjetos[ReferencesIdList].UI_Monedas.enabled = false;
                    ListaObjetos[ReferencesIdList].Precio.enabled = false;
                    break;
            }
        }
    }
    public void InicializarTienda()
    {
        //Se muestra por GUI los precios de cada item según su índice
        ListaObjetos[0].Precio.text = ListaObjetos[0].Price.ToString();
        ListaObjetos[1].Precio.text = ListaObjetos[1].Price.ToString();
        for (int i = 0; i < ListaObjetos.Count; i++)
        {
            //Este for esta en bucle hasta la cantidad de elementos que tenga la ListaObjetos
            ListaObjetos[i].Botones.AddEventListener(i, OnShopItemBtnClicked);/*Se añade a cada botón AddListener,
            esperando que se presione alguno de ellos para que este llame al método encargado de establecer que ya 
            se ha comprado un item y pasar por referencia un entero del botón seleccionado*/
        }
        if (Equipado=="Si"&& scene.name=="Tienda")
        {
            LoadEquipament();
        }
    }

    public void OnShopItemBtnClicked(int itemIndex)//Se pasa por parámetro el entero de (param)
    {
        if (Contador.sharecont.moneda > ListaObjetos[itemIndex].Price)
        {
            Contador.sharecont.moneda -= ListaObjetos[itemIndex].Price;
            Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
            GuardadoListas.GuardarMonedas();
            ListaObjetos[itemIndex].IsPurchased = true;//Se ha comprado se pasa a verdadero
            ListaObjetos[itemIndex].Txt_Buttons.text = "Adquirido";//En el texto de los botones se lo cambia por comprado
            ListaObjetos[itemIndex].Botones.interactable = false;//Se desactiva la interacción de los botones de comprar
            ListaObjetos[itemIndex].UI_Monedas.enabled = false;
            ListaObjetos[itemIndex].Precio.enabled = false;
            IdObjetos.Add(itemIndex);//Se añade a la lista el Id entero del objeto comprado
            GuardadoListas.GuardarList();//Se guarda estas compras, llamando a la clase Guardado(GuardadoList)
        }
        else {
            Debug.Log("No hay dinero brou");
        }
    }
    public void ButtonNum(int PressButton)
    {
        //Metodo que resive por parámetro el entero del botón presionado
        if (PressButton == 1)//Si en este caso es uno, habilitara el objeto Cartas y los demas serán deshabilitados
        { 
           /*Siempre que presionemos este boton antes de activar el Game Object reseteara las posiciones del Content Cartas, lo que hace es establecer los valores predeterminados*/ 
                                                   //Left //Bottom
            ObjectsShop[0].offsetMin = new Vector2(0, -10500);
                                               //Right //Top
            ObjectsShop[0].offsetMax = new Vector2(0, 0);
            Cartas.SetActive(true);
            Perfiles.SetActive(false);
            Fondos.SetActive(false);
        } else if (PressButton==2)//Si en este caso es dos, habilitara el objeto Perfiles y los demas serán deshabilitados
        {
            ObjectsShop[1].offsetMin = new Vector2(0, -6340);
            //Right //Top
            ObjectsShop[1].offsetMax = new Vector2(0, 0);
            Perfiles.SetActive(true);
            Cartas.SetActive(false);
            Fondos.SetActive(false);
        }else
        {
            ObjectsShop[2].offsetMin = new Vector2(0, -4351);
            //Right //Top
            ObjectsShop[2].offsetMax = new Vector2(0, 0);
            //Sino es ninguno de los anteriores, se habilitará el objeto Fondos y los demas serán deshabilitados
            Fondos.SetActive(true);
            Cartas.SetActive(false);
            Perfiles.SetActive(false);
        }
    }
    public void EquiparObject(int IdObjectEquip)
    {
        //Método encargado de pasar entero por referencia cuando lso botones de equipar sean presionados
            switch (IdObjectEquip)
            {
            // Con un case se evalua el entero pasado por referencia
                case 0://Si este es cero entrará ente caso
                    if (ListaObjetos[0].IsPurchased == true)//Siempre y cuando el objeto de la lista de la clase propia Shop item tenga en true el Ispurchased entrará a cumplir las acciones establecidas
                    {
                    PlayerPrefs.SetInt("Id", 0);//Se guardara en este caso un cero en el Id del objeto equipado
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[0].interactable = false;//Desahabilitamos el botón de la posición cero del array de botones equipar
                    Contador.sharecont.SaveEquipament(3, 5,"Si",2);//Se llama a un método de la clase contador el cual se encarga de pasar por referencia la vida
                    }
                    else if (ListaObjetos[0].IsPurchased == false)
                    {
                        Debug.Log("Debes comprar primero el Item");
                    }
                    break;

            case 1://Si este es cero entrará ente caso
                if (ListaObjetos[1].IsPurchased == true)//Siempre y cuando el objeto de la lista de la clase propia Shop item tenga en true el Ispurchased entrará a cumplir las acciones establecidas
                {
                    PlayerPrefs.SetInt("Id", 1);//Se guardara en este caso un cero en el Id del objeto equipado
                    PlayerPrefs.SetString("Equipado","Si");
                    Button_Equipar[1].interactable = false;//Desahabilitamos el botón de la posición cero del array de botones equipar
                    Contador.sharecont.SaveEquipament(3, 12, "Si",3);//Se llama a un método de la clase contador el cual se encarga de pasar por referencia la vida
                }
                else if (ListaObjetos[1].IsPurchased == false)
                {
                    Debug.Log("oni oni oni naninnananai oni Debes comprar ");
                }
                break;

        }
    }
    public void LoadEquipament()
    {
            Button_Equipar[Ido].interactable = false; 
    }
}
