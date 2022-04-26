using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlLogro : MonoBehaviour
{
    public List<int> IdLogros;//Lista de tipo entero que almacenará los enteros id de los objetos desbloqueados
    public List<bool> Recolected;//Lista de tipo bool que servirá para verificar si se recogieron las recompensas
    public Text InformaciónLogros;//Variable de tipo texto que sirve para mostrar por UI la cantidad de logros conseguidos
    public int ContadorBronce;//Variable de tipo entero que almacena la cantidad total de carta de bronces compradas
    public int ContadorOro;//Variable de tipo entero que almacena la cantidad total de cartas de oro compradas
    public int ContadorPlata;//Variable de tipo entero que alamacena la cantidad total de cartas de Plata compradas
    public string ControlBronce;//Variable de tipo string que pretende controlar el desbloque de logros para carta bronce
    public string ControlPlata;//Variable de tipo string que pretende controlar el desbloqueo del logro por comprar de cartas de Plata
    public string ControlPointsLvl20;//Variable de tipo string que pretende controlar el desbloquedo del logro Alpinista de Niveles
    public string ControlPointsLvl60;//Variable de tipo string que pretende controlar el desbloqueo del Logro Experto en Trivias
    public string ControlSinCard;//Variable de tipo string que pretende controlar el desbloqueo del Logro Solo de Dioses
    public string ControlOro;//Variable de tipo string que pretende controlar el desbloqueo del logro por comprar cartas de Oro                 
    public ObjecGui [] ElementsInterface;//Array de tipo de clase ObjectGui, el cual contiene todos los elementos necesarios a manipular
    public Guardado GuardadoListas;//Variable de tipo guardado que hace referencia a la clase que guarda los datos del juego
    public static ControlLogro ShareLogro;

    private void Awake()
    {
        if (ShareLogro == null)
        {
            ShareLogro = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CargarPlayerPrefs();//Método encargado de cargar los PlayerPrefs
    }

    private void Start()
    {
        /*Solo cuando se desee borrar los PLayerPrefs
        PlayerPrefs.DeleteKey("BronceCard");
        PlayerPrefs.DeleteKey("OroCard");
        PlayerPrefs.DeleteKey("PlataCard");
        PlayerPrefs.DeleteKey("CtrlBronce");
        PlayerPrefs.DeleteKey("CtrlPlata");
        PlayerPrefs.DeleteKey("CtrlOro");
        PlayerPrefs.DeleteKey("CtrlLvl20");
        PlayerPrefs.DeleteKey("CtrlLvl60");
        PlayerPrefs.DeleteKey("CtrlSinCard");*/
        if (Contador.sharecont.scene.name == "Inicio")
        {
            CargarList();//Método encargado de cargar las listas
            EvaluateLogros();//Se evalua en cada Start si se ha cumplido alguna condicional de los logros
            CargarRecompensas();//Método encargado de habilitar las recompensas obtenidas
        }
    }
    public void CargarPlayerPrefs()
    {
        //Método encargado de cargar los Players Prefs de los contadores
        ContadorBronce= PlayerPrefs.GetInt("BronceCard");
        ContadorOro = PlayerPrefs.GetInt("OroCard");
        ContadorPlata = PlayerPrefs.GetInt("PlataCard");
        ControlBronce = PlayerPrefs.GetString("CtrlBronce");
        ControlPlata = PlayerPrefs.GetString("CtrlPlata");
        ControlOro = PlayerPrefs.GetString("CtrlOro");
        ControlPointsLvl20 = PlayerPrefs.GetString("CtrlLvl20");
        ControlPointsLvl60 = PlayerPrefs.GetString("CtrlLvl60");
        ControlSinCard = PlayerPrefs.GetString("CtrlSinCard");
    }
    public void CargarList()
    {
        //Método encargado de cargar la lista de objetos guardados(IdLogros)
        GuardadoListas.CargarLogros();//Se llama al método encargado de cargar los Id Logros y boleanos
    }
    public void CargarRecompensas()
    {
        //Método encargado de habilitar y desahabilitar la recolección de recompensas
        for (int i = 0; i <IdLogros.Count; i++)
        {
            int id = IdLogros[i];//Se asigna el entero almecenado en la posición recorrida
            if (Recolected[id] == false)//Si la posición de la variable Id tiene el Recolected en false
            {
                ElementsInterface[id].ButtonsInterface.interactable = true;//Hablitará los botones del GUI de los Logros
            } else if (Recolected[id] == true)//En caso de que el Recolección se haya realizado entonces 
            {
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
            }
        }
    }
    public void DesbloquearLogro(int LogroDesbloqueado)
    {
        //Método encargado de habilitar las recompensas de los logros desbloqueados
        ElementsInterface[LogroDesbloqueado].ButtonsInterface.interactable = true;//Se habilita el botón de recompensa para que pueda recogerse
        IdLogros.Add(LogroDesbloqueado);//Se añade a la lista el entero del objeto desbloqueado 
        GuardadoListas.GuardarLogros();
    }
    public void PassCaseCompra(string NameCase)
    {

        //Método encargado de recibir el caso de las cartas que se han comprado, estos casos se evaluan y aumentan los valores de los contadores
        switch (NameCase)
        {
            case "Bronce"://Para el caso que se haya comprado una carta de bronce
                if (ContadorBronce<5)
                {
                    ContadorBronce++;
                    PlayerPrefs.SetInt("BronceCard", ContadorBronce);
                }
                break;
            case "Oro"://Para el caso que se haya comprado carta de oro
                if (ContadorBronce <4)
                {
                    ContadorOro++;
                    PlayerPrefs.SetInt("OroCard", ContadorOro);
                }
                break;
            case "Plata"://Para el caso que se haya comprado carta de Plata 
                if (ContadorPlata <5)
                {
                    ContadorPlata++;
                    PlayerPrefs.SetInt("PlataCard", ContadorPlata);
                }
            break;
        }
    }

    public void EvaluateLogros()
    {
        //Evaluando compras realizadas en la tienda
        if (ContadorBronce == 5 && ControlBronce=="")//Este caso solo es considerado cuando se han comprado un total de 10 cartas
        {
            DesbloquearLogro(0);//Se envía al método encargado de desbloquear las recompensas, el entero del logro ha desbloquear
            ControlBronce = "S";
            PlayerPrefs.SetString("CtrlBronce", ControlBronce);
        }

        if (ContadorPlata == 5 && ControlPlata == "")//Es parecido a Bronce solo que son compras por cartas de PLata
        {
            DesbloquearLogro(1);
            ControlPlata = "S";
            PlayerPrefs.SetString("CtrlPlata", ControlPlata);
        } else if(ContadorOro==4 &&ControlOro=="" && ControlSección.ShareTienda.ListaObjetos[22].IsPurchased == true )//Este evalua si se han comprado 4 cartas de oro y si ya se ha desbloqueado la carta de diamante
        {
            DesbloquearLogro(2);
            ControlOro = "S";
            PlayerPrefs.SetString("CtrlOro", ControlOro);
        }


        //Evaluando los niveles alcanzados a partir de los puntos
        if (Contador.sharecont.puntos == 145 && ControlPointsLvl20=="")//Si se consigue 145 puntos equivalente al Lvl 20
        {
            DesbloquearLogro(3);//Desbloqueamos el logro Alpinista de niveles
            ControlPointsLvl20 = "S";
            PlayerPrefs.SetString("CtrlLvl20", ControlPointsLvl20);
        } else if (Contador.sharecont.puntos == 555 && ControlPointsLvl60=="")// Si se consigue 555 puntos equivalentes al Lvl 60
        {
            DesbloquearLogro(4);//Desbloqueamos el logro Experto en Trivias 
            ControlPointsLvl60 = "S";
            PlayerPrefs.SetString("CtrlLvl60",ControlPointsLvl60);
        }//TODO:Aun falta establecer las conidcionales para el modo de juego Space Yue


        //Evaluando cantidad alamacenados en la Lista IdLogros
        if (IdLogros.Count == 1)//Si es igual a un elemento
        {
            //Se coloca el siguiente mensaje
            InformaciónLogros.text = "┏(＾0＾)┛ ¡Kaboom! has conseguido tu primer Logro";
        } else if (IdLogros.Count >= 2)// Si es igual a 2
        {
            InformaciónLogros.text = $"Tienes un total de {IdLogros.Count} logros ･ω･";
        } else if (IdLogros.Count == 0) //EN casa de que no haya logros
        {
            //Sale este mensaje predeterminado
            InformaciónLogros.text = "Aun no tienes logros ●～●";
        }

        //Evaluando si no se ha equipado ninguna carta
        if (ControlSección.ShareTienda.Equipado == ""&& Contador.sharecont.puntos == 345&&ControlSinCard=="")//Está condicional ejecuta sus acciones siempre y cuando no s ehaya equipado ninguna carta y se haya alcanzado el nivel 40
        {
            DesbloquearLogro(6);//Desbloqueamos el logro Experto en Trivias 
            ControlSinCard = "S";
            PlayerPrefs.SetString("CtrlSinCard", ControlSinCard);
        }
    }

    public void DesblockGift(int i)
    {
        GuardadoListas.CargarList();//Se llama al método encargado de cargar la lista de objetos comprados

        switch (i)
        {
            case 0:
                ControlSección.ShareTienda.UnlockedGoals(38);
                Contador.sharecont.moneda += 4000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
            case 1:
                ControlSección.ShareTienda.UnlockedGoals(39);
                Contador.sharecont.moneda += 6000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
            case 2:
                ControlSección.ShareTienda.UnlockedGoals(40);
                Contador.sharecont.moneda += 80000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
            case 3:
                ControlSección.ShareTienda.UnlockedGoals(41);
                Contador.sharecont.moneda += 20000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
            case 4:
                ControlSección.ShareTienda.UnlockedGoals(42);
                Contador.sharecont.moneda += 30000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
            case 6:
                ControlSección.ShareTienda.UnlockedGoals(43);
                ControlSección.ShareTienda.UnlockedGoals(21);
                Contador.sharecont.moneda += 50000;
                Contador.sharecont.moneda_ui.text = Contador.sharecont.moneda.ToString();
                GuardadoListas.GuardarMonedas();
                Recolected[i] = true;
                ElementsInterface[i].ButtonsInterface.interactable = false;//Se deshabilita la interacción de los botones de la interfaz Logros
                ElementsInterface[i].Moneda.enabled = false;//Se deshabilita los iconos de moneda y fondo en la posición de id 
                ElementsInterface[i].Fondo.enabled = false;
                ElementsInterface[i].TextInterface.enabled = false;//Se deshabilita la componente texto de Inteerfaz Logros
                GuardadoListas.GuardarLogros();
                break;
        }
    }
    [System.Serializable]
    public class ObjecGui
    {
        public Image Moneda;
        public Image Fondo;
        public Text TextInterface;
        public Button ButtonsInterface;
    }
}