using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego 

public class ControlSección : MonoBehaviour
{
    public static ControlSección ShareTienda;
    public GameObject[] GUIControl;// Array de tipo Game OBject que contiene todos los que se necesiten habilitar desahabilitar                                         
    public List<ShopItem> ListaObjetos = null;//Hace referencia a la Lista de la clase propia Llamada ShopItem
    public List<int> IdObjetos;//Lista que alamacenará los id tipo enteros pasados por parámetros
    public Guardado GuardadoListas;//Variable de tipo d ela clase guardado, que sirve apra acceder al guardado por binario
    public int ReferencesIdList;//Variable pública que servirá para almacenar los ids(int), de acuerdo al indice de la Lista IdObjetos
    public Button[] Button_Equipar;//Array de Referencia de los botones que sirven para equipar
    public Text[] Txt_Equipar;// Array de Referencia del texto de los botones de equipar
    public int Ido;//Variable de tipo entero que almacena el Id del objeto equipado en este caso para las cartas
    public int Ido_Perfil;//Variable de tipo entero que almacena el Id del objeto equipado en este caso para los perfiles
    public int Ido_Fondo;//Variable de tipo entero que almacena el Id del objeto  equipado en este caso para los Fondos
    public string Equipado = "No";//Variable de tipo string, cuya finalidad el limitar el cargado de los botones Equipados en las cartas, para evitar en las demás escenas el Error: Null Refrences
    public string Equipado_Perfil = "No";//Variable de tipo string, cuya finalidad es limitar el cargado de los botones Equipados en los Perfiles, con la finalidad de evitar en las demás escenas el Error: Null References
    public string Equipado_Fondo = "No";//Variable de tipo string cuya finalidad es de limitar el cargado de los botones y fondos equipados
    public Scene scene;// Variable privada de tipo escena que se utilizará para controlar y condicionar con las escenas
    public RectTransform[] ObjectsShop;//Arrays que almacenaran la posición, tamaño, anclaje y pivote de los Game Objects.
    public Sprite[] PerfiL_Fondo;//Array de tipo Sprite que almacenará todos los Sprites de los fondos y perfiles
    public Image Perfil;//Variable de tipo image que servirá para cargar la imagen en este objeto
    public Image Fondo;//Variable de tipo image que servirá para cargar la imagen en este objeto
    public  RefPerandBack ReferenPerBack;//Referencia a la clase RefPerandBack, la cual permite cargar los perfiles y fondos en otra escena

    [Serializable]
    public class ShopItem
    {
        //Clase propia que almacena la referencia de los Items, Precio, Botones, Texto de botones,etc
        public int Price;//Variable tipo entero que almacena el precio de los items
        public Button Botones;//Variable tipo button que almacena los botones
        public Text Txt_Buttons;//Variable tipo texto de botones que servirá para mostrar si se ha comprado
        public Text Precio;//Variable tipo text que mostrará el precio de los items por GUI 
        public Image UI_Monedas;//Variable tipo image que hace referencia a las monedas de interfaz gráfica
        public bool IsPurchased = false;//Variable tipo booleano que servirá para saber si el item ya ha sido comprado 
    }
    private void Awake()
    {
        if (ShareTienda == null)
        {
            ShareTienda = this;
        }
        GuardadoListas = GameObject.Find("ControlNiveles").GetComponent<Guardado>();//Se localiza el objeto según el string establecido y se obtiene la componente
        CargadoPLayerPrefs(); //Este método se encarga de cargar los Key de los Player Prefs que almacenan el id de los objetos equipados
        scene = SceneManager.GetActiveScene();//GetActiveScene es un método que nos permite obtener la escena activa actualmente
    }
    private void Start()
    {
        /*
        Solo usar cuando se desee resetear los key de los player
        PlayerPrefs.DeleteKey("Id");
        PlayerPrefs.DeleteKey("Equipado");
        PlayerPrefs.DeleteKey("Id_Perfil");
        PlayerPrefs.DeleteKey("Equipado_Perfil");
        PlayerPrefs.DeleteKey("Id_Fondo");
        PlayerPrefs.DeleteKey("Equipado_Fondo");*/

        if (scene.name == "Tienda")//Limitamos a que se llamen a estos métodos solo si la escena se llama Tienda
        {
            InicializarTienda();//Método encarga de inicializar los precios y añadir el AddEventlistener a los botones de compra
            CargadoIdobjetos();//Método que se encarga de cargar los Id comprados 
        }
      
    }
    public void CargadoPLayerPrefs()
    {
        Ido = PlayerPrefs.GetInt("Id");//Inicializamos la variable Ido de acuerdo almacenado en el Key Id del PlayerPrefs
        Ido_Perfil = PlayerPrefs.GetInt("Id_Perfil");//Inicializamos la variable Ido_Perfil de acuerdo a lo almacenado en el Key Id_Perfil del PlayerPrefs
        Ido_Fondo = PlayerPrefs.GetInt("Id_Fondo");//Inicializamos la variable Ido_Fondo de acuerdo a lo almacenado en el key Id_Fondo del Player Prefs
        Equipado = PlayerPrefs.GetString("Equipado");//Inicializamos la variable Equipado de acuerdo a lo almacenado en el key del player prefs
        Equipado_Perfil = PlayerPrefs.GetString("Equipado_Perfil");//Inicializamos la variable Equipado_Perfil de acuerdo almacenado en el key del player prefs
        Equipado_Fondo = PlayerPrefs.GetString("Equipado_Fondo");//Inicializamos la variable Equipado_Fondo de acuerdo almacenado en el key del player prefs
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
            ReferencesIdList = IdObjetos[i];//Segun el valor de i(Posición) este almacenará el valor que contenga dicho índice en ReferencesIdList
            /*Se hace una evaluación en todo los casos posibles por cada posición
             puesto que la compra de un item de la tienda se añadira y comprará 
            de manera aleatoria*/
            ListaObjetos[ReferencesIdList].IsPurchased = true;//Se ha comprado se pasa a verdadero
            ListaObjetos[ReferencesIdList].Txt_Buttons.text = "Adquirido";//En el texto de los botones se lo cambia por comprado
            ListaObjetos[ReferencesIdList].Botones.interactable = false;//Se desactiva la interacción de los botones de comprar
            ListaObjetos[ReferencesIdList].UI_Monedas.enabled = false;//Se desactiva el icono que representa las monedas
            ListaObjetos[ReferencesIdList].Precio.enabled = false;//Se desactiva el precio UI, para que no salga el precio de los items comprados
            if (ReferencesIdList >=23)
            {
                // Si el entero del Id comprado es mayor o igual a 23
                ListaObjetos[ReferencesIdList].Botones.gameObject.SetActive(false);//Se desactiva el objeto del botón de comprado
                Button_Equipar[ReferencesIdList].gameObject.SetActive(true);//En este caso se necesita que se muestre el objeto del botón de equipar
                Txt_Equipar[ReferencesIdList].text = "Equipar";
             /*Nota: Se hace por que en las cartas, existirán canvas para poder leer las descripciones
            de las cartas, donde se mostrará los botones de equipar, en el caso de perfiles y 
            fondos ya no es necesario mostrar un Canvas y por ello solo se requiere que una vez 
            realizado el comprado se muestre el botón para equipar*/
            }
        }

        if (Equipado == "Si" && scene.name == "Tienda")//Si la variable string Equipado es igual a Si y si estamos en la tienda
        {
            LoadEquipament();//Cargará el Id del botón que se equipo en este caso para las cartas
        }
        if (Equipado_Perfil == "Si" && scene.name == "Tienda" || scene.name == "SelectModoJuego")//Si la variable string Equipado_Perfil es igual a Si y si estamos en la tienda
        {
            LoadPerfiles();//Cargará el Id del botón Equipado en este caso para los perfiles
        }
        if (Equipado_Fondo == "Si" && scene.name == "Tienda")
        {
            LoadFondos();//Cargará el Id del botón Equipado en este caso para los fondos
        }
    }
    public void InicializarTienda()
    {
        //Se muestra por GUI los precios de cada item según su índice hasta la cantidad de Objetos presentes en la lista
        for (int i = 0; i < ListaObjetos.Count; i++)
        {
            ListaObjetos[i].Precio.text = ListaObjetos[i].Price.ToString();// Se muestra en UI el valor entero de los precios enteros
            if (i == 22 || i >= 38)// Si los Id de las posiciones de la ListaObjetos  es igual 22 o mayores a 38
            {
                ListaObjetos[i].Precio.gameObject.SetActive(false);//Se desactiva el game Object que tiene la componente texto que muestra los precios por UI
                ListaObjetos[i].UI_Monedas.gameObject.SetActive(false);//Se desactiva el Game Object que contiene la componente imagen que muestra el icon de monedas
                ListaObjetos[i].Txt_Buttons.text = "CONSEGUIR";//Se cambia el texto de los botones de comprar a conseguir
            }
        }
        for (int i = 0; i < ListaObjetos.Count; i++)
        {
            //Este for esta en bucle hasta la cantidad de elementos que tenga la ListaObjetos
            ListaObjetos[i].Botones.AddEventListener(i, OnShopItemBtnClicked);/*Se añade a cada botón AddListener,
            esperando que se presione alguno de ellos para que este llame al método encargado de establecer que ya 
            se ha comprado un item y pasar por referencia un entero del botón seleccionado*/
        }
        //En esta sección limitamos el cargado de los objetos equipados
    
    }

    public void OnShopItemBtnClicked(int itemIndex)//Se pasa por parámetro el entero de (param) del botón presionado 
    {
        //Esta condicional limita los objetos con id 22 y mayores por que estos serán conseguidos solo de forma desbloqueable
        //Es decir que si aun no se ha desbloqueado el logro estos no podran ser desbloqueados ni comprados.
        if (itemIndex == 22 || itemIndex >= 38 )
        {
            StartCoroutine(Nologro());//Se llama a la corrutina encargada de mostrar la animación cuando aun no se ha desbloqueado un logro
        }

        if (Contador.sharecont.moneda >= ListaObjetos[itemIndex].Price && itemIndex!=22&&itemIndex<=37)//Si el valor de la variable d ela clase contador moneda es maypr que el precio establecido en la posición del Objeto a comprar 
        {
            Contador.sharecont.moneda -= ListaObjetos[itemIndex].Price;//Se decrementa el valor de la variable moneda de acuerdo al precio que tenga el obejto de la lista 
            Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();//Imprimimos el valor que tenga la moneda en ese momento ya decrementado
            StartCoroutine(WaitObjectConseguido());//Se llama a la corrutina encargada de  activar la animación cuando se obtiene un objeto nuevo 
            GuardadoListas.GuardarMonedas();//Se guarda la cantidad total de las monedas después de este proceso
            ListaObjetos[itemIndex].IsPurchased = true;//Se ha comprado se pasa a verdadero
            ListaObjetos[itemIndex].Txt_Buttons.text = "Adquirido";//En el texto de los botones se lo cambia por comprado
            ListaObjetos[itemIndex].Botones.interactable = false;//Se desactiva la interacción de los botones de comprar
            ListaObjetos[itemIndex].UI_Monedas.enabled = false;//Se deja de mostrar el Sprite de las monedas
            ListaObjetos[itemIndex].Precio.enabled = false;//Se deja de mostrar el Text del precio
            IdObjetos.Add(itemIndex);//Se añade a la lista el Id entero del objeto comprado
            GuardadoListas.GuardarList();//Se guarda estas compras, llamando a la clase Guardado(GuardadoList)
            if (itemIndex >= 23)//Si en este caso el entero pasado por referencia es mayor o igual a 23 
            {
                ListaObjetos[itemIndex].Botones.gameObject.SetActive(false);//Se desactiva el objeto del botón de comprar
                Button_Equipar[itemIndex].gameObject.SetActive(true);//Se activa el objeto del botón de equipar

            }
        }
        else if(itemIndex!=22 && itemIndex <= 37)
        {
            StartCoroutine(TimeCeroMoney());//Llamamos a la corrutina encargada de dar tiempo para ver los efectos cuando no se tiene el suficiente dinero
        }
    }
    public void ButtonNum(int PressButton)
    {
        //Metodo que resive por parámetro el entero del botón presionado
        //0= ScrollBar Cartas
        //1= ScrollBar Perfiles
        //2= ScrollBar Fondos
        //3= Animación No Logrado
        //4= Animación No Compra
        //5= Perfil General
        //6= Animación Item Conseguido
        if (PressButton == 1)//Si en este caso es uno, habilitara el objeto Cartas y los demas serán deshabilitados
        {
            /*Siempre que presionemos este boton antes de activar el Game Object reseteara las posiciones del Content Cartas, lo que hace es establecer los valores predeterminados*/
            //Left //Bottom
            ObjectsShop[0].offsetMin = new Vector2(0, -10789);
            //Right //Top
            ObjectsShop[0].offsetMax = new Vector2(0, 0);
            GUIControl[0].SetActive(true);
            GUIControl[1].SetActive(false);
            GUIControl[2].SetActive(false);
        }
        else if (PressButton == 2)//Si en este caso es dos, habilitara el objeto Perfiles y los demas serán deshabilitados
        {
            ObjectsShop[1].offsetMin = new Vector2(0, -6720);
            //Right //Top
            ObjectsShop[1].offsetMax = new Vector2(0, 0);
            GUIControl[0].SetActive(false);
            GUIControl[1].SetActive(true);
            GUIControl[2].SetActive(false);
        }
        else
        {
            ObjectsShop[2].offsetMin = new Vector2(0, -4351);
            //Right //Top
            ObjectsShop[2].offsetMax = new Vector2(0, 0);
            //Sino es ninguno de los anteriores, se habilitará el objeto Fondos y los demas serán deshabilitados
            GUIControl[0].SetActive(false);
            GUIControl[1].SetActive(false);
            GUIControl[2].SetActive(true);
        }
    }
    public void EquiparObject(int IdObjectEquip)
    {
        //Método encargado de pasar entero por referencia cuando lso botones de equipar sean presionados
        //Esta parte del código se encarga de resetear la interacción de los botones de equipar en tiempo real
        //Importante cargar las variables de acuerdo al Key de los PlayerPrefs que corresponda los Id de los objetos equipados
        Ido=PlayerPrefs.GetInt("Id");
        Ido_Perfil = PlayerPrefs.GetInt("Id_Perfil");
        Ido_Fondo = PlayerPrefs.GetInt("Id_Fondo");
        if ( IdObjectEquip !=Ido && IdObjectEquip<=22)
        {
            Button_Equipar[Ido].interactable = true;
            Txt_Equipar[Ido].text = "Equipar";
        }
        if (IdObjectEquip!=Ido_Perfil && IdObjectEquip<=37)
        {
            Button_Equipar[Ido_Perfil].interactable = true;
            Txt_Equipar[Ido_Perfil].text = "Equipar";
        }
        if (IdObjectEquip != Ido_Fondo && IdObjectEquip>=38)
        {
            Button_Equipar[Ido_Fondo].interactable = true;
            Txt_Equipar[Ido_Fondo].text = "Equipar";
        }
        switch (IdObjectEquip)
        {

            // Con un case se evalua el entero pasado por referencia
            case 0://Si este es cero entrará ente caso
                if (ListaObjetos[0].IsPurchased == true)//Siempre y cuando el objeto de la lista de la clase propia Shop item tenga en true el Ispurchased entrará a cumplir las acciones establecidas
                {
                    PlayerPrefs.SetInt("Id", 0);//Se guardara en este caso un cero en el Id del objeto equipado
                    PlayerPrefs.SetString("Equipado", "Si");//Se guardara el id del elemento equipado
                    Button_Equipar[0].interactable = false;//Desahabilitamos el botón de la posición cero del array de botones equipar
                    Txt_Equipar[0].text = "Equipado";//Se cambia el texto del botón a equipado
                    Contador.sharecont.SaveEquipament(1, 20, "Si", 1);//Se llama a un método de la clase contador el cual se encarga de pasar por referencia la vidas, tiempo, monedas extras y si se debe aplicar lo equipado
                }
                else if (ListaObjetos[0].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());//En caso de que el objeto no este comprado se llamará a una corrutina que controla la animación de Aun no comprado
                }
                break;

            case 1://Si este es uno entrará ente caso
                if (ListaObjetos[1].IsPurchased == true) { 
                    PlayerPrefs.SetInt("Id", 1);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[1].interactable = false;
                    Txt_Equipar[1].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 10, "Si", 3);
                }
                else if (ListaObjetos[1].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 2://Si este es dos entrará ente caso
                if (ListaObjetos[2].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 2);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[2].interactable = false;
                    Txt_Equipar[2].text = "Equipado";
                    Contador.sharecont.SaveEquipament(5, 20, "Si", 10);
                }
                else if (ListaObjetos[2].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 3://Si este es tres entrará ente caso
                if (ListaObjetos[3].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 3);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[3].interactable = false;
                    Txt_Equipar[3].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[3].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 4://Si este es cuatro entrará ente caso
                if (ListaObjetos[4].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 4);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[4].interactable = false;
                    Txt_Equipar[4].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[4].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra()); ;
                }
                break;

            case 5://Si este es cinco entrará ente caso
                if (ListaObjetos[5].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 5);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[5].interactable = false;
                    Txt_Equipar[5].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[5].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 6://Si este es seis entrará ente caso
                if (ListaObjetos[6].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 6);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[6].interactable = false;
                    Txt_Equipar[6].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[6].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 7://Si este es siete entrará ente caso
                if (ListaObjetos[7].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 7);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[7].interactable = false;
                    Txt_Equipar[7].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[7].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 8://Si este es ocho entrará ente caso
                if (ListaObjetos[8].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 8);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[8].interactable = false;
                    Txt_Equipar[8].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[8].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 9://Si este es nueve entrará ente caso
                if (ListaObjetos[9].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 9);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[9].interactable = false;
                    Txt_Equipar[9].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[9].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 10://Si este es diez entrará ente caso
                if (ListaObjetos[10].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 10);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[10].interactable = false;
                    Txt_Equipar[10].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[10].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 11://Si este es once entrará ente caso
                if (ListaObjetos[11].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 11);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[11].interactable = false;
                    Txt_Equipar[11].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[11].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 12://Si este es doce entrará ente caso
                if (ListaObjetos[12].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 12);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[12].interactable = false;
                    Txt_Equipar[12].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[12].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 13://Si este es trece entrará ente caso
                if (ListaObjetos[13].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 13);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[13].interactable = false;
                    Txt_Equipar[13].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[13].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 14://Si este es Catorce entrará ente caso
                if (ListaObjetos[14].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 14);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[14].interactable = false;
                    Txt_Equipar[14].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[14].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 15://Si este es Quince entrará ente caso
                if (ListaObjetos[15].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 15);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[15].interactable = false;
                    Txt_Equipar[15].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[15].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 16://Si este es Dieciseis entrará ente caso
                if (ListaObjetos[16].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 16);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[16].interactable = false;
                    Txt_Equipar[16].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[16].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 17://Si este es Diecisiete entrará ente caso
                if (ListaObjetos[17].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 17);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[17].interactable = false;
                    Txt_Equipar[17].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[17].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 18://Si este es Diciocho entrará ente caso
                if (ListaObjetos[18].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 18);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[18].interactable = false;
                    Txt_Equipar[18].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[18].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 19://Si este es Diecinueve entrará ente caso
                if (ListaObjetos[19].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 19);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[19].interactable = false;
                    Txt_Equipar[19].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[19].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 20://Si este es Veinte entrará ente caso
                if (ListaObjetos[20].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 20);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[20].interactable = false;
                    Txt_Equipar[20].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[20].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 21://Si este es Veintiuno entrará ente caso
                if (ListaObjetos[21].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id", 21);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[21].interactable = false;
                    Txt_Equipar[21].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[21].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;

            case 22://Si este es Veintidos entrará ente caso
                if (ListaObjetos[22].IsPurchased == true) { 
                    PlayerPrefs.SetInt("Id",22);
                    PlayerPrefs.SetString("Equipado", "Si");
                    Button_Equipar[22].interactable = false;
                    Txt_Equipar[22].text = "Equipado";
                    Contador.sharecont.SaveEquipament(3, 12, "Si", 3);
                }
                else if (ListaObjetos[22].IsPurchased == false)
                {
                    StartCoroutine(WaitNoCompra());
                }
                break;
            //Empiezan los objetos de los Perfiles
            case 23://Si este es Veintitres entrará ente caso
                if (ListaObjetos[23].IsPurchased == true)//En caso de que IsPurchased este verdadero 
                {
                    PlayerPrefs.SetInt("Id_Perfil", 23);//Se guardara el Id 23 en el Player Prefs Id_Perfil  
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");//Se guarda un "Si " en el PLayer Prefs Equipado_Perfil con la finalidad de controlar el cargado de los elementos equipados
                    ReferenPerBack.PreverSaveIdPerfil(23,"Sr.Buck");// Se envía al método de la clase ReferenPerandBak, para que estos guarden el id y puedan ser cargados en otra escena, en este caso para los perfiles 
                    Button_Equipar[23].interactable = false;//Se desactiva la interacción del botón equipar en la posición del caso
                    Txt_Equipar[23].text = "Equipado";//Se pone el texto del boton del equipado en Equipado
                    GUIControl[5].SetActive(false);//Se desactiva el Game Object que contiene la componente imagen en la que se encuentra el Perfil General 
                    Perfil.sprite = PerfiL_Fondo[0];//Se pasa le asigna la imagen del array de sprites a la variable de tipo Image 
                    Perfil.gameObject.SetActive(true);// Se activa el Game Object que contiene la componente Image del Perifl equipado
                }
                break;

            case 24://Si este es VeintiCuatro entrará ente caso
                if (ListaObjetos[24].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 24);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(24,"Yuyo");
                    Button_Equipar[24].interactable = false;
                    Txt_Equipar[24].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[1];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 25://Si este es Veinticinco entrará ente caso
                if (ListaObjetos[25].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 25);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(25,"Blue");
                    Button_Equipar[25].interactable = false;
                    Txt_Equipar[25].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[2];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 26://Si este es Veintiseis entrará ente caso
                if (ListaObjetos[26].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 26);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(26,"Oreo");
                    Button_Equipar[26].interactable = false;
                    Txt_Equipar[26].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[3];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 27://Si este es Veintisiete entrará ente caso
                if (ListaObjetos[27].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 27);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(27,"Andrew");
                    Button_Equipar[27].interactable = false;
                    Txt_Equipar[27].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[4];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 28://Si este es Venintiochos entrará ente caso
                if (ListaObjetos[28].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 28);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(28,"Xoana");
                    Button_Equipar[28].interactable = false;
                    Txt_Equipar[28].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[5];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 29://Si este es Ventinueve entrará ente caso
                if (ListaObjetos[29].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 29);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(29,"Coco");
                    Button_Equipar[29].interactable = false;
                    Txt_Equipar[29].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[6];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 30://Si este es Treinta entrará ente caso
                if (ListaObjetos[30].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 30);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(30,"Celeste");
                    Button_Equipar[30].interactable = false;
                    Txt_Equipar[30].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[7];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 31://Si este es Treinta y uno entrará ente caso
                if (ListaObjetos[31].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 31);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(31,"Nick");
                    Button_Equipar[31].interactable = false;
                    Txt_Equipar[31].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[8];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 32://Si este es treinta y dos entrará ente caso
                if (ListaObjetos[32].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 32);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(32,"Juan");
                    Button_Equipar[32].interactable = false;
                    Txt_Equipar[32].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[9];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 33://Si este es treinta y tres entrará ente caso
                if (ListaObjetos[33].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 33);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(33,"Shock");
                    Button_Equipar[33].interactable = false;
                    Txt_Equipar[33].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[10];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 34://Si este es treinta y cuatro entrará ente caso
                if (ListaObjetos[34].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 34);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(34,"Yume");
                    Button_Equipar[34].interactable = false;
                    Txt_Equipar[34].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[11];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 35://Si este es treinta y cinco entrará ente caso
                if (ListaObjetos[35].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 35);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(35,"Napoleón");
                    Button_Equipar[35].interactable = false;
                    Txt_Equipar[35].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[12];
                    Perfil.gameObject.SetActive(true);
                }
                break;

            case 36://Si este es Treinta y Seis entrará ente caso
                if (ListaObjetos[36].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 36);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(36,"Nube");
                    Button_Equipar[36].interactable = false;
                    Txt_Equipar[36].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[13];
                    Perfil.gameObject.SetActive(true);
                }

                break;

            case 37://Si este es Treinta y Siete entrará ente caso
                if (ListaObjetos[37].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Perfil", 37);
                    PlayerPrefs.SetString("Equipado_Perfil", "Si");
                    ReferenPerBack.PreverSaveIdPerfil(37,"Manchas");
                    Button_Equipar[37].interactable = false;
                    Txt_Equipar[37].text = "Equipado";
                    GUIControl[5].SetActive(false);
                    Perfil.sprite = PerfiL_Fondo[14];
                    Perfil.gameObject.SetActive(true);
                }
                break;
            //Se pasa a la sección de fondos
            case 38://Si este es Treinta y Ocho entrará ente caso
                if (ListaObjetos[38].IsPurchased == true)//Si la variable Booleana llamada IsPurchased es verdadera 
                {
                    PlayerPrefs.SetInt("Id_Fondo", 38);//Se guarda en el key del Player Prefs llamado Id_Fondo la posición del boton equipar presionado 
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");//Se pasa un Si al Key del Player Prefs llamado Equipado_Fondo para controlar el cargado de los botones
                    ReferenPerBack.PreverSaveIdFondo(38);//Se pasa al método de la clase ReferenPerandBack un entero por referencia para que este cargue el fondo en otra escena 
                    Button_Equipar[38].interactable = false;//Se desahabilita la interacción del botón equipar en la posición del caso establecido
                    Txt_Equipar[38].text = "Equipado";//Se establece el texto del Botón equipar en "Equipado" en la posición del caso actual
                    Fondo.sprite = PerfiL_Fondo[15];//Se establece el sprite del array en la posición establecida a la variable de tipo Image Fondo
                }
                break;

            case 39://Si este es Treinta y Nueve entrará ente caso
                if (ListaObjetos[39].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 39);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(39);
                    Button_Equipar[39].interactable = false;
                    Txt_Equipar[39].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[16];
                }
                break;

            case 40://Si este es Cuarenta entrará ente caso
                if (ListaObjetos[40].IsPurchased == true)
                { 
                    PlayerPrefs.SetInt("Id_Fondo", 40);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(40);
                    Button_Equipar[40].interactable = false;
                    Txt_Equipar[40].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[17];
                }
                break;

            case 41://Si este es Cuarenta y Uno entrará ente caso
                if (ListaObjetos[41].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 41);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(41);
                    Button_Equipar[41].interactable = false;
                    Txt_Equipar[41].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[18];
                }
                break;

            case 42://Si este es Cuarenta y Dos entrará ente caso
                if (ListaObjetos[42].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 42);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(42);
                    Button_Equipar[42].interactable = false;
                    Txt_Equipar[42].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[19];
                }
                break;

            case 43://Si este es Cuarenta y Tres entrará ente caso
                if (ListaObjetos[43].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 43);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(43);
                    Button_Equipar[43].interactable = false;
                    Txt_Equipar[43].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[20];
                }
                break;

            case 44://Si este es Cuarenta y Cuatro entrará ente caso
                if (ListaObjetos[44].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 44);//Se guardara en este caso un cero en el Id del objeto equipado
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(44);
                    Button_Equipar[44].interactable = false;
                    Txt_Equipar[44].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[21];
                }
                break;

            case 45://Si este es Cuarenta y Cinco entrará ente caso
                if (ListaObjetos[45].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 45);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(45);
                    Button_Equipar[45].interactable = false;
                    Txt_Equipar[45].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[22];
                }
                break;

            case 46://Si este es Cuarenta y Seis entrará ente caso
                if (ListaObjetos[46].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 46);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(46);
                    Button_Equipar[46].interactable = false;
                    Txt_Equipar[46].text = "Euipado";
                    Fondo.sprite = PerfiL_Fondo[23];
                }
                break;

            case 47://Si este es Cuarenta y Siete entrará ente caso
                if (ListaObjetos[47].IsPurchased == true)
                {
                    PlayerPrefs.SetInt("Id_Fondo", 47);
                    PlayerPrefs.SetString("Equipado_Fondo", "Si");
                    ReferenPerBack.PreverSaveIdFondo(47);
                    Button_Equipar[47].interactable = false;
                    Txt_Equipar[47].text = "Equipado";
                    Fondo.sprite = PerfiL_Fondo[24];
                }
                break;
        }
    }
    IEnumerator TimeCeroMoney()
    {
        //Corrutina encargada de dar un tiempo para mostrar la animación de que hace falta dinero
        AudioManager.shareaudio.Efectos[2].Play();//Activa el sonido para cuando algo esta erroneo
        AnimaCon.ShareAnimation.ActiveEventCeroMoney();//Activa la animación cuando no hay dinero suficiente
        yield return new WaitForSeconds(1f);
        AnimaCon.ShareAnimation.DesactiveEventCeroMoney();//Se desahabilita la animación luego del tiempo establecido
    }
    IEnumerator Nologro()
    {
        //Corrutina encargada de activar la animación cuando aun no se ha desbloqeuado determinado objeto
        GUIControl[3].SetActive(true);//Se activa el Objeto Panel que contiene los elementos necesarios para la animación 
        AnimaCon.ShareAnimation.ActivateNologro();//Se llama al metodo que activa la animación cuando no se ha desbloqueado Logro
        yield return new WaitForSeconds(3f);
        StartCoroutine(SalidaNoLogro());//Después del  tiempo establecido en la corrutina se llama a otra que permite dar un tiempo a la otra animación de salida
    }
    IEnumerator SalidaNoLogro()
    {
        AnimaCon.ShareAnimation.DesactivateNologro();//LLama al método encargado de desactivar la animación de salida cuando un logro aun no esta desbloqueado
        yield return new WaitForSeconds(1f);
        GUIControl[3].SetActive(false);//Desactivamos el Objeto Panel que contiene todos los elementos necesarios para la animación
    }
    IEnumerator WaitNoCompra()
    {
        GUIControl[4].SetActive(true);//Activa el Objeto panel de No compra
        AnimaCon.ShareAnimation.ActiveNocompra();//Llama al método encargado de activar la animación de aun no comprado
        yield return new WaitForSeconds(3f);
        StartCoroutine(NoCompraSalida());//Se llama a la siguiente corrutina para dar un tiempo a la salida de la animación 
    }

    IEnumerator NoCompraSalida()
    {
        AnimaCon.ShareAnimation.DesactiveNocompra();//LLama al método encargado de desahabilitar la animación de aun no comprado
        yield return new WaitForSeconds(1f);
        GUIControl[4].SetActive(false);//Luego del tiempo pasado a la corrutina desahabilatamos el objeto panel de No Compra
    }

    IEnumerator WaitObjectConseguido()
    {
        GUIControl[6].SetActive(true);//Activa el objeto que contiene los demás elementos para la animación al obtener un objeto
        AnimaCon.ShareAnimation.ActiveAnimationObjectGet();//Se le indica a la clase Animacon que llame a su comportamiento encargado de pasar un booleano para que se habilite la animación
        yield return new WaitForSeconds(5f);
        StartCoroutine(EndWaitObjectConseguido());//Se llama a la corrutina encargada de dar tiempo para la salia de la animación
    }
    IEnumerator EndWaitObjectConseguido()
    {
        AnimaCon.ShareAnimation.DesactivateAnimationObjectGet();//Se le indica a la clase AnimaCon que llame a su comportamiento encargado de activar la animación de salida al obtener un objeto
        yield return new WaitForSeconds(3f);
        GUIControl[6].SetActive(false);//Se desactiva el obejeto que contiene los elementos para la animación de objeto obtenido
    }
    public void LoadEquipament()
    {
        //Método encargado de cargar el botón que se presionó al Equipar una Carta
        Button_Equipar[Ido].interactable = false;//Se desahabilita la interacción de acuerdo al valor entero almacenado en la variable Ido
        Txt_Equipar[Ido].text = "Equipado";//Se cambia el texto del botón de acuerdo al valor almacenado en la variable entera Ido
    }
    public void LoadPerfiles()
    {
        //Método encargado de cargar el botón que se presionó al Equipar un Perfil 
        Button_Equipar[Ido_Perfil].interactable = false;//Se desahabilita la interacción de acuerdo al valor entero almacenado en la variable Ido_Perfil
        Txt_Equipar[Ido_Perfil].text = "Equipado";//Se cambia el texto del botón de acuerdo al valor almacenado en la variable entera Ido_Perfil
        switch (Ido_Perfil) {
            case 23:
                GUIControl[5].SetActive(false);//Se desactiva el Game Object que contiene la componente Image del PerfilGeneral
                Perfil.sprite = PerfiL_Fondo[0];//Se le asigna un spray en n posición de la lista de sprites a la variable Perfil de tipo Image
                Perfil.gameObject.SetActive(true);//Se habilita el Game Object que contiene la componente Image del Perfil
                break;
            case 24:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[1];
                Perfil.gameObject.SetActive(true);
                break;
            case 25:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[2];
                Perfil.gameObject.SetActive(true);
                break;
            case 26:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[3];
                Perfil.gameObject.SetActive(true);
                break;
            case 27:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[4];
                Perfil.gameObject.SetActive(true);
                break;
            case 28:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[5];
                Perfil.gameObject.SetActive(true);
                break;
            case 29:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[6];
                Perfil.gameObject.SetActive(true);
                break;
            case 30:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[7];
                Perfil.gameObject.SetActive(true);
                break;
            case 31:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[8];
                Perfil.gameObject.SetActive(true);
                break;
            case 32:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[9];
                Perfil.gameObject.SetActive(true);
                break;
            case 33:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[10];
                Perfil.gameObject.SetActive(true);
                break;
            case 34:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[11];
                Perfil.gameObject.SetActive(true);
                break;
            case 35:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[12];
                Perfil.gameObject.SetActive(true);
                break;
            case 36:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[13];
                Perfil.gameObject.SetActive(true);
                break;
            case 37:
                GUIControl[5].SetActive(false);
                Perfil.sprite = PerfiL_Fondo[14];
                Perfil.gameObject.SetActive(true);
                break;
        }
        
    }

    public void LoadFondos()
    {
        Button_Equipar[Ido_Fondo].interactable = false;
        Txt_Equipar[Ido_Fondo].text = "Equipado";
        switch (Ido_Fondo)
        {
            case 38:
                Fondo.sprite = PerfiL_Fondo[15];
                break;
            case 39:
                Fondo.sprite = PerfiL_Fondo[16];
                break;
            case 40:
                Fondo.sprite = PerfiL_Fondo[17];
                break;
            case 41:
                Fondo.sprite = PerfiL_Fondo[18];
                break;
            case 42:
                Fondo.sprite = PerfiL_Fondo[19];
                break;
            case 43:
                Fondo.sprite = PerfiL_Fondo[20];
                break;
            case 44:
                Fondo.sprite = PerfiL_Fondo[21];
                break;
            case 45:
                Fondo.sprite = PerfiL_Fondo[22];
                break;
            case 46:
                Fondo.sprite = PerfiL_Fondo[23];
                break;
            case 47:
                Fondo.sprite = PerfiL_Fondo[24];
                break;
        }
    }
    public void UnlockedGoals(int CaseDesblock)
    {
        IdObjetos.Add(CaseDesblock);
        GuardadoListas.GuardarList();
    }
}
