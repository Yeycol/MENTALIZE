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
       
        Musica1.value = PlayerPrefs.GetFloat("Mus", 1f);//Le asignamos al slider de la m�sica el valor float 1
        Partida.volume = Musica1.value;//El valor que tenga el slider de la m�sica tambien lo tendr� el volumen de la instancia de Audio Source
        Efectos1.value= PlayerPrefs.GetFloat("Efect", 1f);
        Efectos[0].volume = Efectos1.value;
        Efectos[1].volume = Efectos1.value;
        Efectos[2].volume = Efectos1.value;
        Efectos[3].volume = Efectos1.value;
        Efectos[4].volume = Efectos1.value;
        Efectos[5].volume = Efectos1.value;
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }



    public void regularVolumen()
    {
        Partida.volume = Musica1.value;
        PlayerPrefs.SetFloat("Mus", Musica1.value);
        PlayerPrefs.Save();
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
        PlayerPrefs.SetFloat("Efect", Efectos1.value);
        PlayerPrefs.Save();
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarSlider()
    {
        
        Musica1.value = PlayerPrefs.GetFloat("Mus");
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarEfectos()
    {
        Efectos1.value = PlayerPrefs.GetFloat("Efect");
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }
    public void Mute()
    {
        if (Musica1.value==0 && Efectos1.value == 0)
        {
            Mute1.enabled = true;
            Mute2.enabled = true;

        }
       else if(Musica1.value!= 0 && Efectos1.value!=0)
        {
            Mute1.enabled = false;
            Mute2.enabled = false;
        } else if (Musica1.value>0 && Efectos1.value== 0)
        {
            Mute1.enabled = false;
            Mute2.enabled = true;
        }else if (Efectos1.value > 0 && Musica1.value == 0)
        {
            Mute1.enabled = true;
            Mute2.enabled = false;
        }



    } 


}
