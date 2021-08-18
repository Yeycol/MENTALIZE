using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager shareaudio;//Variable que servirá para hacer la clase una instancia compartida
    public AudioSource Partida;//Variable de tipo Audiosource que hace referenica a la música de fondo
    public Slider Musica1,Efectos1;//Variable de tipo Slider que servirá para hacer referencia a los sliders que controlan la regulación de volumen 
    public AudioSource [] Efectos;//Array de efectos que hacen referencia al conjunto de efectos que deseamos regular el volumen
    public Image Mute1;//Refencia  de la imagen que tiene un símbolo que da a expresar que se muteo totalmente la música
    public Image Mute2;//Refencia  de la imagen que tiene un símbolo que da a expresar que se muteo totalmente los efectos


    private void Awake()
    {
      if (shareaudio == null)
        {
            shareaudio = this;
            DontDestroyOnLoad(gameObject);//Método que evita que se destruyan las referencias y datos almacenados en las variables de esta clase 
            Partida = GetComponent<AudioSource>();//Localizamos la componente audio de este objeto
        }
        else
        {
            Destroy(gameObject);
        }
       

    }

    private void Start()
    {
        //Método que establece un volumén inicial
 
            Inicializar();

    }

    // Update is called once per frame
    public void Inicializar()
    {
        //Este método esta encargado de dar play a la música, efectos y de indicar su volumén inciial
        Partida.Play(); //Permite iniciar la reproducción de la componente de sonido
        Partida.loop = true;//Esto permite que la música se reproduzca en bucle 
       
        Musica1.value = PlayerPrefs.GetFloat("Mus", 1f);//Le asignamos al slider de la música el valor float 1
        Partida.volume = Musica1.value;//El valor que tenga el slider de la música tambien lo tendrá el volumen de la instancia de Audio Source
        Efectos1.value= PlayerPrefs.GetFloat("Efect", 1f);
        Efectos[0].volume = Efectos1.value;
        Efectos[1].volume = Efectos1.value;
        Efectos[2].volume = Efectos1.value;
        Efectos[3].volume = Efectos1.value;
        Efectos[4].volume = Efectos1.value;
        Efectos[5].volume = Efectos1.value;
        Mute();//Llamado a un método que evalua cuando debe mostrarse el icono de mute
    }



    public void regularVolumen()
    {
        Partida.volume = Musica1.value;
        PlayerPrefs.SetFloat("Mus", Musica1.value);
        PlayerPrefs.Save();
        Mute();//Llamado a un método que evalua cuando debe mostrarse el icono de mute
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
        Mute();//Llamado a un método que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarSlider()
    {
        
        Musica1.value = PlayerPrefs.GetFloat("Mus");
        Mute();//Llamado a un método que evalua cuando debe mostrarse el icono de mute
    }
    public void CargarEfectos()
    {
        Efectos1.value = PlayerPrefs.GetFloat("Efect");
        Mute();//Llamado a un método que evalua cuando debe mostrarse el icono de mute
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
