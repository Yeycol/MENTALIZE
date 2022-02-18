using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager shareaudio;//Variable que servir� para hacer la clase una instancia compartida
    public Slider Musica1,Efectos1;//Variable de tipo Slider que servir� para hacer referencia a los sliders que controlan la regulaci�n de volumen 
    public AudioSource [] Efectos;//Array de efectos que hacen referencia al conjunto de efectos que deseamos regular el volumen
    public Image Mute1;//Refencia  de la imagen que tiene un s�mbolo que da a expresar que se muteo totalmente la m�sica
    public Image Mute2;//Refencia  de la imagen que tiene un s�mbolo que da a expresar que se muteo totalmente los efectos
    public Canvas ReferAudio;//Referencia al canvas del audio

    private void Awake()
    {
      if (shareaudio == null)
        {
            shareaudio = this;
            DontDestroyOnLoad(gameObject);//M�todo que evita que se destruyan las referencias y datos almacenados en las variables de esta clase 
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
        Musica1.value = PlayerPrefs.GetFloat("Mus", 1f);//Obtenemos el valor para el slider de la m�sica el valor float 1
        Efectos[14].volume = Musica1.value;//El valor que tenga el slider de la m�sica tambien lo tendr� el volumen de la instancia de Audio Source
        Efectos[15].volume = Musica1.value;//Efecto 15 hace referencia a la m�sica de las trivias
        Efectos[16].volume = Musica1.value;//Efecto 16 hace referencia a la m�sica de Space Yue
        Efectos1.value= PlayerPrefs.GetFloat("Efect", 1f);
        Efectos[0].volume = Efectos1.value;//Otorgamos el valor del slider al volumen del efecto de Over 
        Efectos[1].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Answer Good
        Efectos[2].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Answer Bad
        Efectos[3].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de TimeEnd
        Efectos[4].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Alerta o Logro
        Efectos[5].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Back Alert
        Efectos[6].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Disparo
        Efectos[7].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Llegada Volando
        Efectos[8].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Salida Nave
        Efectos[9].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Roto
        Efectos[10].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de Nave Abducir
        Efectos[11].volume = Efectos1.value;// Otorgamos el valor del slider al volumen del efecto de ButtonSelect
        Efectos[12].volume = Efectos1.value;//Otorgamos el valor del slider al volumen del efecto de FocosDa�ados
        Efectos[13].volume = Efectos1.value;//Otorgamos el valor del slider al volumen del efecto de Win
        Efectos[17].volume = Efectos1.value;//Otorgamos el valor del slider al volumen de la voz de la frase A toda M�quina Go Go
        Efectos[18].volume = Efectos1.value;//Otorgmaos el valor del slider al volumen del objeto que contiene la componete audio source de la frase Se te acaba el tiempo   
        Efectos[19].volume = Efectos1.value;//Otrogamos el valor del slider a la propiedad volumen del objeto encargado de reproducir la frase Mira el Reloj no te queda tiempo
        Efectos[20].volume=Efectos1.value;//Otorgamos el valor del slider a la propieda volumen del objeto que contiene el sonido de la frase Concentrate tu puedes hacerlo mejor
        Efectos[21].volume = Efectos1.value;//Otrogamos el valor del slider a la propiedad volumen del objeto que contiene el sonido d ela frase Ey no te distraigas te queda una vida
        Efectos[22].volume=Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del objeto que contiene el sonido de la frase Mira en donde presionas tienes una vida menos
        Efectos[23].volume = Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del componente Source que contine el sonido de la frase Hit Hit Hurra
        Efectos[24].volume = Efectos1.value;//Ortorgamos el valor del slider a la propieda volumen del objeto que contiene el sonido de la frase JIJIJI Jugamos de nuevo eso fue divertido
        Efectos[25].volume = Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del objeto que contiene el sonido de la frase KABOM Vamos por otra
        Efectos[26].volume = Efectos1.value;//Otorgamos el valor de slider a la propiedad volume del objeto que contiene el sonido  de la frase Eso fue excelente, quieres ir por muffins
        Efectos[27].volume = Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del objeto que contiene la frase Cuanto m�s dificil es la victoria mayor es la felicidad al ganar
        Efectos[28].volume = Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del objeto que contiene el sonido de la frase Nos equivocamos es momento de nutrir nuestras mentes
        Efectos[29].volume = Efectos1.value;//Otorgamos el valor del slider a la propiedad volumen del objeto que contiene el sonido de la frase No te sientas mal sigue intentandolo
        Mute();//Llamado a un m�todo que evalua cuando debe mostrarse el icono de mute
    }



    public void regularVolumen()
    {
        Efectos[14].volume = Musica1.value;//El valor del Slider ser� igual a la propiedad volumen de la m�sica para el Men�
        Efectos[15].volume = Musica1.value;
        Efectos[16].volume = Musica1.value;
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
        Efectos[10].volume = Efectos1.value;
        Efectos[11].volume = Efectos1.value;
        Efectos[12].volume = Efectos1.value;
        Efectos[13].volume = Efectos1.value;
        Efectos[17].volume = Efectos1.value;
        Efectos[18].volume = Efectos1.value;
        Efectos[19].volume = Efectos1.value;
        Efectos[20].volume = Efectos1.value;
        Efectos[21].volume = Efectos1.value;
        Efectos[22].volume = Efectos1.value;
        Efectos[23].volume = Efectos1.value;
        Efectos[24].volume = Efectos1.value;
        Efectos[25].volume = Efectos1.value;
        Efectos[26].volume = Efectos1.value;
        Efectos[27].volume = Efectos1.value;
        Efectos[28].volume = Efectos1.value;
        Efectos[29].volume = Efectos1.value;
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
            Mute1.enabled = true;//Habilita la imagen de mute
            Mute2.enabled = false;//Deshabilita la imagen de mute
        }
    } 
}
