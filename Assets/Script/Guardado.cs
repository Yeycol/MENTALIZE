/*Este script est� encargado de guardar los niveles desbloqueados, moendas del videojuego
 Se usa el m�todo binario para ello el cual serializa y deserializa un objeto en formato binario*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;//Libreria que nos permite utilizar las capacidades de serealizaci�n del sistema Operativo en el script
using System.IO;// Librer�a que nos permite crear archivos y leerlos en caulquier momento
using System.Linq;//Librer�a que nos facilitar� la copia de listas
public class Guardado : MonoBehaviour
{
    private string RutaArchivo;//Variable que almacenar� la ruta en la que vamos almacenar los niveles desbloqueados
    private string RutaArchivo1;//Variable que almacenar� la ruta en la que vamos almacenar las monedas
    private string RutaArchivo2;//Variable que almacenar� ruta en donde almacenaremos un array de enteros
    private string RutaArchivo3;//Variable que almacenar� ruta en donde se guardar� el entero de los puntos ganados
    private string RutaArchivo4;//Variable de tipo string que almacena la ruta en donde se alamacenar� los id d elos logros desbloqueados
    static bool PrimeraVez = true;//Booleano utilizado para hacer el cargado de los niveles por �nica vez
    public List<int> Cero = null;//Lista vac�a en caso de que no exista el fichero
    public List<bool> Cero1;//Lista de booleanos que almacenan valores inciiales en false
    private void Awake()
    {
        RutaArchivo = Application.persistentDataPath + "/LvlDate.dat";//Ruta por defecto de Unity donde se almacenar� los archivos del juego, varia segun la plataforma que se exporte el juego 
        RutaArchivo1 = Application.persistentDataPath + "/CoinDate.dat";//Ruta por defecto de Unity donde se almacenar� los archivos del juego, varia segun la plataforma que se exporte el juego 
        RutaArchivo2 = Application.persistentDataPath + "/ObjectBuyDate.dat";//Ruta por defecto de Unity donde se almacenar� los archivos del juego, varia segun la plataforma que se exporte el juego
        RutaArchivo3 = Application.persistentDataPath + "/PointsDate.dat";//Ruta por defecto de Unity donde se almacenar� los archivos del juego, varia segun la plataforma que se exporte el juego
        RutaArchivo4 = Application.persistentDataPath + "/AchievementsDate.dat";
        if (PrimeraVez)//Evalua si es el bolleano es verdad, se carga los niveles desbloqueados, si n es el caso no ingresa a la condicional
        {
            Cargar();//M�todo encargado de cargar los datos almacenados
            PrimeraVez = false;//Se pasa a false cuando ingreso por primera vez para que este no ingrese mas
        }
  
    }
    public void Guardar()
    {
        //M�todo encargado de guardar los niveles desbloqueados
        BinaryFormatter bf = new BinaryFormatter();//Permite crear un formato binario el cual gestionar� el trabajo de serializaci�n
        FileStream file = File.Create(RutaArchivo);//Se crea un puntero en donde crear el archivo, donde se pasa por par�metro la ruta
        GuardadodeDatos datos = new GuardadodeDatos(ControlNiveles.LvlDesbloqueado,ControlNiveles.LvlDesbloqueoSpace);//Se inicializa la clase de gardado de datos y se pasa por par�metro la variable a almacenar 
        bf.Serialize(file, datos);//Guardamos el par�metro enviado al m�todo de la clase GuardadodeDatos en el archivo con ruta que creamos
        file.Close();//Cerrramos el archivo que hemos creado
    }
    public void Cargar()
    {
        if (File.Exists(RutaArchivo))//Se pregunta por la existencia del archivo que creamos al guardar
        {
            //Si existe este lo cargar�
            BinaryFormatter bf = new BinaryFormatter();//Se crea nuevamente un formato binario el cual gestionara el trabajo de deserializaci�n
            FileStream file = File.Open(RutaArchivo, FileMode.Open);//Se crea unpuntero pero esta vez para abrir el archivo en la ruta guardada
            GuardadodeDatos datos = (GuardadodeDatos)bf.Deserialize(file);// Se deserializa el archivo e la ruta que especificamos abrir, no se pueden enviar binarios al Unity y esperar que funcione 
            //Pero si podemos convertir nuestro archivo deserializado a un tipo de dato espec�fico
            ControlNiveles.LvlDesbloqueado = datos.NivelesDesbloqueados;// Se iguala la variable de la clase control niveles a la variable de la clase de GUardadodeNiveles 
            ControlNiveles.LvlDesbloqueoSpace = datos.NivelesDesbloqueadosSpace;
            //Para que reciba e dato almacenado en su interior
        }
        else
        {
            //Si no le otorgar� a la variable niveles desbloqueados un cero
            ControlNiveles.LvlDesbloqueado = 0;
            ControlNiveles.LvlDesbloqueoSpace = 0;
        }
    }
    //Para el guardado de monedas se aplica lo antes mencionado
    public void GuardarMonedas()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file1 = File.Create(RutaArchivo1);
        Monedas datos1 = new Monedas(Contador.sharecont.moneda);
        bf.Serialize(file1, datos1);
        file1.Close();
    }
        public void CargarMonedas()
         {
        if (File.Exists(RutaArchivo1))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file1 = File.Open(RutaArchivo1, FileMode.Open);
            Monedas datos1 = (Monedas)bf.Deserialize(file1);
            Contador.sharecont.moneda = datos1.monedas;
            file1.Close();
        }
        else
        {
            Contador.sharecont.moneda = 0;
        }
    }

    public void GuardarPoints()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file3 = File.Create(RutaArchivo3);
        Puntos datos3 = new Puntos (Contador.sharecont.puntos,Contador.sharecont.pointsYue);
        bf.Serialize(file3, datos3);
        file3.Close();
    }
    public void CargarPoints()
    {
        if (File.Exists(RutaArchivo3))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file3 = File.Open(RutaArchivo3, FileMode.Open);
            Puntos datos3 = (Puntos)bf.Deserialize(file3);
            Contador.sharecont.puntos = datos3.GetPoints();
            Contador.sharecont.pointsYue = datos3.GetPointsYue();
            file3.Close();
        }
        else
        {
            Contador.sharecont.puntos = 0;
            Contador.sharecont.pointsYue = 0;
        }
    }

    public void GuardarList()
    {
        //M�todo encargado de guardar la lista del Id de Items Comprados
        BinaryFormatter bf = new BinaryFormatter();//Permite crear un formato binario el cual gestionar� el trabajo de serializaci�n
        FileStream file2 = File.Create(RutaArchivo2);//Se crea un puntero en donde crear el archivo, donde se pasa por par�metro la ruta
        IdObjetos datos2 = new IdObjetos(ControlSecci�n.ShareTienda.IdObjetos);//Se inicializa la clase de IdObjetos y se pasa por par�metro la lista a almacenar 
        bf.Serialize(file2, datos2);//Guardamos el par�metro enviado al m�todo de la clase IdObjetos en el archivo con ruta que creamos
        file2.Close();//Cerrramos el archivo que hemos creado
    }
    public void CargarList()
    {
        if (File.Exists(RutaArchivo2))//Se pregunta por la existencia del archivo que creamos al guardar
        {
            //Si existe este lo cargar�
            BinaryFormatter bf = new BinaryFormatter();//Se crea nuevamente un formato binario el cual gestionara el trabajo de deserializaci�n
            FileStream file2 = File.Open(RutaArchivo2, FileMode.Open);//Se crea un puntero pero esta vez para abrir el archivo en la ruta guardada
            IdObjetos datos2 = (IdObjetos)bf.Deserialize(file2);// Se deserializa el archivo en la ruta que especificamos abrir, no se pueden enviar binarios al Unity y esperar que funcione 
            //Pero si podemos convertir nuestro archivo deserializado a un tipo de dato espec�fico
            ControlSecci�n.ShareTienda.IdObjetos = datos2.GetList().ToList(); // Se iguala la variable de la clase control Selecci�n a la variable de la clase de IdObjetos
            //Para que reciba el dato almacenado en su interior, para ello copeamos la lista de la clase IdObjetos y la asignamos a la variable d ela clase control selecci�n
            file2.Close();
        }
        else
        {
            //Sino existe el fichero se copear� una lista vac�a 
            ControlSecci�n.ShareTienda.IdObjetos = Cero.ToList();
        }
    }

    public void GuardarLogros()
    {
        //M�todo encargado de guardar la lista del Id de Logros Desbloqueados
        BinaryFormatter bf = new BinaryFormatter();//Permite crear un formato binario el cual gestionar� el trabajo de serializaci�n
        FileStream file4 = File.Create(RutaArchivo4);//Se crea un puntero en donde crear el archivo, donde se pasa por par�metro la ruta
        IdLogrosObject datos4 = new IdLogrosObject (ControlLogro.ShareLogro.IdLogros, ControlLogro.ShareLogro.Recolected);//Se crea una instancia de la clase IdLogrosObject Y llamamos al constructor el cual le pasamos la lista que vamos a serializar como par�metro
        bf.Serialize(file4, datos4);//Guardamos el par�metro enviado al m�todo de la clase IdLogrosObject en el archivo con ruta que creamos
        file4.Close();//Cerrramos el archivo que hemos creado
    }
    public void CargarLogros()
    {
        if (File.Exists(RutaArchivo4))//Se pregunta por la existencia del archivo que creamos al guardar
        {
            //Si existe este lo cargar�
            BinaryFormatter bf = new BinaryFormatter();//Se crea nuevamente un formato binario el cual gestionara el trabajo de deserializaci�n
            FileStream file4 = File.Open(RutaArchivo4, FileMode.Open);//Se crea un puntero pero esta vez para abrir el archivo en la ruta guardada
            IdLogrosObject datos4 = (IdLogrosObject)bf.Deserialize(file4);// Se deserializa el archivo en la ruta que especificamos abrir, no se pueden enviar binarios al Unity y esperar que funcione, para ello se convierte en el formato que deseamos (Casting)
            //Pero si podemos convertir nuestro archivo deserializado a un tipo de dato espec�fico
            ControlLogro.ShareLogro.IdLogros = datos4.GetListIdLogros().ToList(); // Se incializa los datos de las dem�s variables de acuerdo a los datos almacenados
            ControlLogro.ShareLogro.Recolected = datos4.GetListRecolected().ToList();
            //Para que reciba el dato almacenado en su interior, para ello copeamos la lista de la clase IdObjetos y la asignamos a la variable d ela clase control selecci�n
            file4.Close();
        }
        else
        {
            ControlLogro.ShareLogro.IdLogros=Cero.ToList();
            ControlLogro.ShareLogro.Recolected = Cero1.ToList();
        }

    }
}
[System.Serializable]
class GuardadodeDatos{
    public int NivelesDesbloqueados;// Variable de tipo entero que recibir� el nivel desbloqueado pasado por par�metro 
    public int NivelesDesbloqueadosSpace;//Variable de tipo entero que almacenar� los niveles desbloqueados de Space
    public GuardadodeDatos(int NivelesDesbloqueados_, int NivelDesSpace)//M�todo que recibe por par�metro  un entero al crearse la clase
    {
        NivelesDesbloqueados = NivelesDesbloqueados_;// Se le otorga el dato pasado por par�metro a la variable entero
        NivelesDesbloqueadosSpace = NivelDesSpace;//Se establece el valor de la variable de clase al valor pasado por referencia en este caso el nivel desbloqueado para modo de juego Space Yue 
    }
}
[System.Serializable]
class Monedas
{
    public int monedas;

    public Monedas(int MonedasGanadas)
    {
        monedas = MonedasGanadas;
    }
}

[System.Serializable]
class Puntos
{
    private int PointsSave;
    private int PointsYue;

    public Puntos(int PuntosGanados, int PuntosYue)
    {
        PointsSave = PuntosGanados;
        PointsYue=PuntosYue;
    }
    public int GetPoints()
    {
        return PointsSave;  
    }
    public int GetPointsYue()
    {
        return PointsYue;
    }


}
[System.Serializable]
class IdObjetos
{
    private List<int> Idonjectos=null;//Lista que almacernar� la copia de la lista pasada por referencia

    public IdObjetos(List<int>ObjeRef)
    {
        Idonjectos = ObjeRef.ToList();//Se copia la lista pasada por referencia
    }
    public List<int> GetList()
    {
        return Idonjectos;
    }
}
[System.Serializable]
class IdLogrosObject
{

    private List<int> IdLogrosId;//Lista de tipo entero que almacenar� el id del logro desbloqueado
    private List<bool> Recolecteds;// Lista de tipo booleano que almacena si se ha recogido una recompensa
    public IdLogrosObject(List<int> ReferencesLogrosId, List<bool> ReferencesRecoled)
    {
        IdLogrosId = ReferencesLogrosId;//Se copia la lista pasada por referencia
        Recolecteds = ReferencesRecoled;
    }

    public List<int> GetListIdLogros()
    {
        //M�todo encargado de retornar la lista cuando se inicialice la lista de esta clase en el constructor
        return IdLogrosId;
    }
    public List<bool>GetListRecolected(){
        return Recolecteds;
    }
}
