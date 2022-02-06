/*Este script está encargado de guardar los niveles desbloqueados, moendas del videojuego
 Se usa el método binario para ello el cual serializa y deserializa un objeto en formato binario*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;//Libreria que nos permite utilizar las capacidades de serealización del sistema Operativo en el script
using System.IO;// Librería que nos permite crear archivos y leerlos en caulquier momento
using System.Linq;//Librería que nos facilitará la copia de listas
public class Guardado : MonoBehaviour
{
    private string RutaArchivo;//Variable que almacenará la ruta en la que vamos almacenar los niveles desbloqueados
    private string RutaArchivo1;//Variable que almacenará la ruta en la que vamos almacenar las monedas
    private string RutaArchivo2;
    static bool PrimeraVez = true;//Booleano utilizado para hacer el cargado de los niveles por única vez
    public List<int> Cero = null;//Lista vacía en caso de que no exista el fichero
    private void Awake()
    {
        RutaArchivo = Application.persistentDataPath + "/datos.dat";//Ruta por defecto de Unity donde se almacenará los archivos del juego, varia segun la plataforma que se exporte del juego 
        RutaArchivo1 = Application.persistentDataPath + "/datos1.dat";//Ruta por defecto de Unity donde se almacenará los archivos del juego, varia segun la plataforma que se exporte del juego 
        RutaArchivo2 = Application.persistentDataPath + "/ObjectBuy.dat";
        if (PrimeraVez)//Evalua si es el bolleano es verdad, se carga los niveles desbloqueados, si n es el caso no ingresa a la condicional
        {
            Cargar();//Método encargado de cargar los datos almacenados
            PrimeraVez = false;//Se pasa a false cuando ingreso por primera vez para que este no ingrese mas
        }
  
    }
    public void Guardar()
    {
        //Método encargado de guardar los niveles desbloqueados
        BinaryFormatter bf = new BinaryFormatter();//Permite crear un formato binario el cual gestionará el trabajo de serialización
        FileStream file = File.Create(RutaArchivo);//Se crea un puntero en donde crear el archivo, donde se pasa por parámetro la ruta
        GuardadodeDatos datos = new GuardadodeDatos(ControlNiveles.LvlDesbloqueado);//Se inicializa la clase de gardado de datos y se pasa por parámetro la variable a almacenar 
        bf.Serialize(file, datos);//Guardamos el parámetro enviado al método de la clase GuardadodeDatos en el archivo con ruta que creamos
        file.Close();//Cerrramos el archivo que hemos creado
    }
    public void Cargar()
    {
        if (File.Exists(RutaArchivo))//Se pregunta por la existencia del archivo que creamos al guardar
        {
            //Si existe este lo cargará
            BinaryFormatter bf = new BinaryFormatter();//Se crea nuevamente un formato binario el cual gestionara el trabajo de deserialización
            FileStream file = File.Open(RutaArchivo, FileMode.Open);//Se crea unpuntero pero esta vez para abrir el archivo en la ruta guardada
            GuardadodeDatos datos = (GuardadodeDatos)bf.Deserialize(file);// Se deserializa el archivo e la ruta que especificamos abrir, no se pueden enviar binarios al Unity y esperar que funcione 
            //Pero si podemos convertir nuestro archivo deserializado a un tipo de dato específico
            ControlNiveles.LvlDesbloqueado = datos.NivelesDesbloqueados;// Se iguala la variable de la clase control niveles a la variable de la clase de GUardadodeNiveles 
            //Para que reciba e dato almacenado en su interior
        }
        else
        {
            //Si no le otorgará a la variable niveles desbloqueados un cero
            ControlNiveles.LvlDesbloqueado = 0;
        }
    }
    //Para el guardado de monedas se aplica lo antes mencionado
    public void GuardarMonedas()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file1 = File.Create(RutaArchivo1);
        Monedas datos1 = new Monedas(Contador.sharecont.moneda,Contador.sharecont.puntos);
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
            Contador.sharecont.puntos = datos1.points;
            file1.Close();
        }
        else
        {
            Contador.sharecont.moneda = 0;
            Contador.sharecont.puntos = 0;
        }
    }

    public void GuardarList()
    {
        //Método encargado de guardar la lista del Id de Items Comprados
        BinaryFormatter bf = new BinaryFormatter();//Permite crear un formato binario el cual gestionará el trabajo de serialización
        FileStream file2 = File.Create(RutaArchivo2);//Se crea un puntero en donde crear el archivo, donde se pasa por parámetro la ruta
        IdObjetos datos2 = new IdObjetos(ControlSección.ShareTienda.IdObjetos);//Se inicializa la clase de IdObjetos y se pasa por parámetro la lista a almacenar 
        bf.Serialize(file2, datos2);//Guardamos el parámetro enviado al método de la clase IdObjetos en el archivo con ruta que creamos
        file2.Close();//Cerrramos el archivo que hemos creado
    }
    public void CargarList()
    {
        if (File.Exists(RutaArchivo2))//Se pregunta por la existencia del archivo que creamos al guardar
        {
            //Si existe este lo cargará
            BinaryFormatter bf = new BinaryFormatter();//Se crea nuevamente un formato binario el cual gestionara el trabajo de deserialización
            FileStream file2 = File.Open(RutaArchivo2, FileMode.Open);//Se crea un puntero pero esta vez para abrir el archivo en la ruta guardada
            IdObjetos datos2 = (IdObjetos)bf.Deserialize(file2);// Se deserializa el archivo en la ruta que especificamos abrir, no se pueden enviar binarios al Unity y esperar que funcione 
            //Pero si podemos convertir nuestro archivo deserializado a un tipo de dato específico
            ControlSección.ShareTienda.IdObjetos = datos2.Idonjectos.ToList(); ;// Se iguala la variable de la clase control Selección a la variable de la clase de IdObjetos
            //Para que reciba el dato almacenado en su interior, para ello copeamos la lista de la clase IdObjetos y la asignamos a la variable d ela clase control selección
            file2.Close();
        }
        else
        {
            //Sino existe el fichero se copeará una lista vacía 
            ControlSección.ShareTienda.IdObjetos = Cero.ToList();
        }
    }
}
[System.Serializable]
class GuardadodeDatos{
    public int NivelesDesbloqueados;// Variable de tipo entero que recibirá el nivel desbloqueado pasado por parámetro 
    public GuardadodeDatos(int NivelesDesbloqueados_)//Método que recibe por parámetro  un entero al crearse la clase
    {
        NivelesDesbloqueados = NivelesDesbloqueados_;// Se le otorga el dato pasado por parámetro a la variable entero
    }
}
[System.Serializable]
class Monedas
{
    public int monedas;
    public int points;
    public Monedas(int MonedasGanadas, int puntosganados)
    {
        monedas = MonedasGanadas;
        points = puntosganados;
    }
}

[System.Serializable]
class IdObjetos
{
    public List<int> Idonjectos=null;//Lista que almacernará la copia de la lista pasada por referencia
    public IdObjetos(List<int>ObjeRef)
    {
        Idonjectos = ObjeRef.ToList();//Se copia la lista pasada por referencia
    }
}