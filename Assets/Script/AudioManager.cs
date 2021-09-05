using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager shareaudio;//Variable que servir� para hacer la clase una instancia compartida
    public AudioSource Partida;//Variable de tipo Audiosource que hace referenica a la m�sica de fondo
    public Slider Musica1,Efectos1;//Variable de tipo Slider que servir� para hacer referencia a los sliders que controlan la regulaci�n de volumen 
    public AudioSource [] Efectos;//Array de efectos que hacen referencia al conjunto de efectos que deseamos regular el volumen
    public Image Mute1;//Refencia  de la imagen que tiene un s�mbolo que da a expresar que se muteo totalmente la m�sica
    public Image Mute2;//Refencia  de la imagen que tiene un s�mbolo que da a expresar que se muteo totalmente los efectos


    private void Awake()
    {
      if (shareaudio == null)
        {
            shareaudio = this;
            DontDestroyOnLoad(gameObject);//M�todo que evita que se destruyan las referencias y datos almacenados en las variables de esta clase 
            Partida = GetComponent<AudioSource>();//Localizamos la componente audio de este objeto
        }
        else
        {
            Destroy(gameObject);
        }
       

    }

    private void Start()
    {
        //M�todo que establece un volum�n inicial
 
            Inicializar();

    }

    // Update is called once per frame
    public void Inicializar()
    {
        //Este m�todo esta encargado de dar play a la m�sica, efectos y de indicar su volum�n inciial
        Partida.Play(); //Permite iniciar la reproducci�n de la componente de sonido
        Partida.loop = true;//Esto permite que la m�sica se reproduzca en bucle 
       
        Musica1.value = PlayerPrefs.GetFloat("Mus", 1f);//Obtenemos el valor para el slider de la m�sica el valor float 1
        Partida.volume = Musica1.value;//El valor que tenga el slider de la m�sica tambien lo tendr� el volumen de la instancia de Audio Source
        Efectos1.value= PlayerPrefs.GetFloat("Efect", 1f);
        Efectos[0].volume = Efectos1.value;//Otorgamos el valor del slider al volumen del efecto de Over 
        Efectos[1].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Answer Good
        Efectos[2].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Answer Bad
        Efectos[3].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de TimeEnd
        Efectos[4].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Alerta o Logro
        Efectos[5].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Back Alert
        Efectos[6].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Disparo
        Efectos[7].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Llegada Volando
        Efectos[8].volume = Efectos1.value;
        Efectos[9].volume = Efectos1.value;
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }



    public void regularVolumen()
    {
        Partida.volume = Musica1.value;
        PlayerPrefs.SetFloat("Mus", Musica1.value);// Le asignamos el valor que tenga el slider en la variable del prefab
        PlayerPrefs.Save();//Guardamos los valores del prefab
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void RegularEfectos()
    {
        Efectos[0].volume = Efectos1.value;
        Efectos[1].volume = Efectos1.value;
        Efectos[2].volume = Efectos1.value;
        Efectos[3].volume = Efectos1.value;
        Efectos[4].volume = Efectos1.value;
        Efectos[5].volume = Efectos1.value;
        Efectos[6].volume = Efectos1.value;
        Efectos[7].volume = Efectos1.value;
        Efectos[8].volume = Efectos1.value;
        Efectos[9].volume = Efectos1.value;
        PlayerPrefs.SetFloat("Efect", Efectos1.value);
        PlayerPrefs.Save();
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarSlider()
    {
        //Se encarga de cargar los valores guardados en la variable de player prefs, es decir le otorgamos el valor almacenado en el player prefs al slider de la m�sica
        Musica1.value = PlayerPrefs.GetFloat("Mus");
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarEfectos()
    {
        //Se encarga de cargar los valores guardados en la variable de player prefs, es decir le otorgamos el valor almacenado en el player prefs al slider de los efectos
        Efectos1.value = PlayerPrefs.GetFloat("Efect");
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void Mute()
    {
        //M�todo encargado de evaluar si se activa el mute o no, dependiendo de los valores del slider
        if (Musica1.value==0 && Efectos1.value == 0)// Si el slider de m�sica y el slider de efectos son iguales a cero
        {
            Mute1.enabled = true;//Habilita la imagen de mute
            Mute2.enabled = true;//Habilita la imagen de mute

        }
       else if(Musica1.value!= 0 && Efectos1.value!=0)
        {
            Mute1.enabled = false;//Deshabilita la imagen de mute
            Mute2.enabled = false;//Deshabilita la imagen de mute
        } else if (Musica1.value>0 && Efectos1.value== 0)//Si el slider de musica es mayor que cero y el slider de efectos es igua� a cero
        {
            Mute1.enabled = false;//Deshabilita la imagen de mute
            Mute2.enabled = true;//Habilita la imagen de mute
        }
        else if (Efectos1.value > 0 && Musica1.value == 0)
        {
            Mute1.enabled = true;
            Mute2.enabled = false;
        }



    } 


}
