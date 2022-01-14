using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Espacio de nombre necesario para controlar los elementos de GUI
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego 

public class RefPerandBack : MonoBehaviour
{
    public GameObject[] PerAndBack; //0: PerfilEquipado //1: PerfilGeneral//Referencia de Game Objects, necesarios para la desahabilitación y habilitación  
    public Image Per,Fon; //Variables de tipo Image, que serán usadas para cambiar el sprite de la componente imagen, dependiendo del Perfil y FondoEquipado
    public int Perfil, Background;//Variables de Tipo Entero utilizadas para almacenar los Id de los objetos Equipados
    public Text Nameuser;//Hace referencia al objeto UI que contiene la componente texto para mostrar por interfaz el nombre de usuario
    public Scene scene;//Variable pública de tipo Scene utilizada para obtener la escena activa
    public Sprite [] ObjectsUser;//Array de Sprites que almacenará las imágenes de los objetos desbloqueados
    private void Awake() 
    {
        scene=SceneManager.GetActiveScene();//Obtenemos la Escena Activa 
    }
    void Start()
    {
            LoadPreverSaveId();//LLamamos al método encargado de cargar los key de los Player Prefs
            RefreshObjectPerfil();//LLamamos al método encargado de cargar el sprite del perfil que se haya equipado
            RefreshObjectFondo();//Llamamos al método encargao de cargar el sprite del fondo que se haya equipado

        /*Borrar los datos almacenados en los Key de los PLayer prefs solo si se requiere

        PlayerPrefs.DeleteKey("ID_PERFIL");
        PlayerPrefs.DeleteKey("ID_FONDO");
        PlayerPrefs.DeleteKey("NameUser");*/
    }

    public void PreverSaveIdPerfil(int IdPerfil, string NameUser)
    {
        //Método que se encargar de guardar el entero IdPerfil pasado por referencia y el nombre de usuario
        PlayerPrefs.SetInt("ID_PERFIL", IdPerfil);
        PlayerPrefs.SetString("NameUser", NameUser);
    }
    public void PreverSaveIdFondo(int IdFondo)
    {
        //Método que se encargar de guardar el entero IdFondo pasado por referencia
        PlayerPrefs.SetInt("ID_FONDO", IdFondo);
    }
    public void LoadPreverSaveId()
    {
        Perfil = PlayerPrefs.GetInt("ID_PERFIL");//Otorgamos el entero del Key del Player Prefs en la variable entera Perfil
        if (scene.name != "Tienda" && scene.name != "SelectModoJuego")
        {
            Nameuser.text = PlayerPrefs.GetString("NameUser"); // Se carga el texto pasado por parámetro a la variable tipo texto que mostrará el nombre de usuario por UI 
        }
            Background = PlayerPrefs.GetInt("ID_FONDO");//Otorgamos el entero del Key del PLayer Prefs en la variable entera Fondo
    }
    public void RefreshObjectPerfil()
    {
           switch (Perfil)
        {
            //Según el entero almacenado en la variable Perfil se cambiará el sprite del objeto que contenga la componente imagen 
            case 23:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[0];
                PerAndBack[0].SetActive(true);
                break;
            case 24:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[1];
                PerAndBack[0].SetActive(true);
                break;
            case 25:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[2];
                PerAndBack[0].SetActive(true);
                break;
            case 26:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[3];
                PerAndBack[0].SetActive(true);
                break;
            case 27:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[4];
                PerAndBack[0].SetActive(true);
                break;
            case 28:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[5];
                PerAndBack[0].SetActive(true);
                break;
            case 29:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[6];
                PerAndBack[0].SetActive(true);
                break;
            case 30:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[7];
                PerAndBack[0].SetActive(true);
                break;
            case 31:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[8];
                PerAndBack[0].SetActive(true);
                break;
            case 32:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[9];
                PerAndBack[0].SetActive(true);
                break;
            case 33:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[10];
                PerAndBack[0].SetActive(true);
                break;
            case 34:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[11];
                PerAndBack[0].SetActive(true);
                break;
            case 35:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[12];
                PerAndBack[0].SetActive(true);
                break;
            case 36:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[13];
                PerAndBack[0].SetActive(true);
                break;
            case 37:
                PerAndBack[1].SetActive(false);
                Per.sprite = ObjectsUser[14];
                PerAndBack[0].SetActive(true);
                break;

        }
    }
    public void RefreshObjectFondo(){

        switch (Background)
        {
            //Segun el entero almacenado en la variable Background se camiará el sprite del game object que contenga la componente Image 
            case 38:
                Fon.sprite = ObjectsUser[15];
                break;
            case 39:
                Fon.sprite = ObjectsUser[16];
                break;
            case 40:
                Fon.sprite = ObjectsUser[17];
                break;
            case 41:
                Fon.sprite = ObjectsUser[18];
                break;
            case 42:
                Fon.sprite = ObjectsUser[19];
                break;
            case 43:
                Fon.sprite = ObjectsUser[19];
                break;
            case 44:
                Fon.sprite = ObjectsUser[20];
                break;
            case 45:
                Fon.sprite = ObjectsUser[21];
                break;
            case 46:
                Fon.sprite = ObjectsUser[22];
                break;
            case 47:
                Fon.sprite = ObjectsUser[23];
                break;
        }

    }
}
