using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//Librería que nos permitira controlar las scenas del videojuego

/*Este script esta encargado de conrolar el tamaño y ajuste de la interfaz de acuerdo al apect ratio 
  que no es mas que la proporción del alto y ancho de las pantallas*/
public class ControlResolution : MonoBehaviour
{
    public float aspect;//Variable de tipo flotante que almacena el apect ratio
    public float rounded;//Variable de tipo flotante que pretende almacenar el valor de aspec ratio con dos decimales
    public CanvasScaler[] ReferencesScalerScene;//Array de tipo canvas scaler que servirá para adaptar las GUI
    public Scene ScenaAdaptable;//Variable que almacenará las escenas detectadas 
    /*Lista  de rectransforms(Utilizado en GUI para manipular la posición,tamaño y anclaje de un rectangulo)*/
    public RectTransform []ObjectRecTransformGeneral;
    public GridLayoutGroup OrdSons;// Variable de tipo Grid Layout, que pretende controlar el orden de los objetos hijos de un padre
    public Text[] TextButonShop;// Array que pretende almacenar los texto de los botones de la tienda en la sección de fondos 
    public Transform FondYueScene;// Variable de tipo transform que pretende almacenar referencia del fondo de la escena Yue
    public int Numr=5;// Número entero que prentende almacenar la cantidad de veces que se debe llamar al método
    void Awake()
    {
        ScenaAdaptable = SceneManager.GetActiveScene();//Se devuelve y almacena en la variable la escena activa    
    }
    void FixedUpdate()
    {
        if(Time.frameCount%Numr==0)
        DetecResolution();//Método encargado de obtener la relación de aspecto de la cámara y según su ratio establecer las configuraciones para los gráficos de la Interfaz   
     }

    public void DetecResolution()
    {
               
            aspect = Camera.main.aspect; //Almacenamos el ratio detectado por la cámara en nuestra variable flotante
            rounded = (int)(aspect * 100.0f) / 100.0f;//Rounded variable flotante que almacena la relación de aspecto con un máximo de 2 decimales

  

        //Nota: Lo que hace es multiplicar el ratio por 100 y este flotante resultante se lo pasa a entero por una conversión explicita(casting), y de ahi se lo divide dando como resultado un flotante con dos decimales
        switch (rounded)
        {
            /*-|RecTransformList|-*/
            /* |Inicio|
             |0=FondoHorizontal
             |1=MarcoProtafolio

             */
            /*-|CanvasScaler|-*/
            /* |Inicio|
             |0=Inicio,SelectModo de juego, Tienda
             |1=Portafolioversiones
             |2= Canvas de Logros
             |3= Canvas de Carga
             */
            case 0.46f://Sansung Galaxy S22+
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.88f;
                }else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.85f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.85f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.88f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.94f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1f, 1.1f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    OrdSons.cellSize = new Vector2(320f, 320f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                    for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                    {
                        TextButonShop[i].fontSize = 25;//Establecemos este tamaño para esta resolución
                    }
                } else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.3f, 0f);
                    OrdSons.cellSize = new Vector2(450f, 200f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;
        

            case 0.47f://Sansung Galaxy Note 10 +
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.89f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.86f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.87f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.88f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.94f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1f, 1.1f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    OrdSons.cellSize = new Vector2(325f, 325f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                    for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                    {
                        TextButonShop[i].fontSize = 27;//Establecemos este tamaño para esta resolución
                    }
                }
                else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.2f, 1.23f, 0f);
                    OrdSons.cellSize = new Vector2(400f, 175f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;


            case 0.48f://Samsung Galaxy J6+
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.90f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.89f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.89f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.98f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.92f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.94f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.94f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1f, 1.1f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    OrdSons.cellSize = new Vector2(340f, 340f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                    for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                    {
                        TextButonShop[i].fontSize = 28;//Establecemos este tamaño para esta resolución
                    }
                }
                else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.89f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.2f, 1.2f, 0f);
                    OrdSons.cellSize = new Vector2(400f, 175f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;




            case 0.4f://Samsung Galaxy Z Flip3 5G
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.78f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.66f, 1.66f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.80f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.66f, 1.66f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.80f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.81f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.90f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.86f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.40f, 1.40f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    OrdSons.cellSize = new Vector2(330f, 330f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                    for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                    {
                        TextButonShop[i].fontSize = 26;//Establecemos este tamaño para esta resolución
                    } 
                }
                else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.47f, 0f);
                    OrdSons.cellSize = new Vector2(440f, 220f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;


            case 0.36f://Samsung W22 5G
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.77f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.73f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.75f;
                }else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.74f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.83f, 1.83f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.74f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.76f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.82f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 0.81f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.7f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    OrdSons.cellSize = new Vector2(330f, 330f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                    for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                    {
                        TextButonShop[i].fontSize = 26;//Establecemos este tamaño para esta resolución
                    }
                } else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.81f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.47f, 0f);
                    OrdSons.cellSize = new Vector2(440f, 220f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;

            case 0.5f://Samsung Galaxy A6s
                if (ScenaAdaptable.name == "Inicio")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ReferencesScalerScene[2].matchWidthOrHeight = 0.91f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.6f, 1.4f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
                }
                else if (ScenaAdaptable.name == "Carga")
                {
                    ReferencesScalerScene[3].matchWidthOrHeight = 0.92f;
                }
                else if (ScenaAdaptable.name == "SelectModoJuego")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.4f, 1.2f, 0f);
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                }
                else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
                } else if (ScenaAdaptable.name == "Tienda")
                {
                    for (int i = 0; i < ReferencesScalerScene.Length; i++)
                    {
                        ReferencesScalerScene[i].matchWidthOrHeight = 0.93f;//Establecemos la relación de altura y anchura para para las descripciones
                    }
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ReferencesScalerScene[1].matchWidthOrHeight = 1f;
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.4f, 1.2f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                    ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                    
                }
                else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.92f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.2f, 1.16f, 0f);
                    OrdSons.cellSize = new Vector2(400f, 160f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                }
                break;

            case 0.56f:
                if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                    || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                    || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                    || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                    || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                    || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                    || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                    || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                    || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                    || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                    || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                    || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
                {
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.1f, 1.1f, 0f);
                }
                break;

            //Aspec Ratio para Landscape

            case 2.22f://Sansung Galaxy S20,S21 Ultra, Samsung Galaxy M23, Xiaomi RedmiK40 Gaming
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.2f, 0f);
                }
                else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.7f, 1.7f);
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);//Establecemos la escala de la interfaz de pausa no mas empieza el juego
                }
                break;

            case 2.16f://Sansung Galaxy S22+
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.2f, 0f);
                } else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.7f, 1.7f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);//Establecemos la escala de la interfaz de pausa no mas empieza el juego
                }
                break;

            case 2.11f://Sansung Galaxy Note 10 +
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.3f, 0f);
                }else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.7f, 1.7f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);
                }
                break;

            case 2.05f://Samsung Galaxy J6+
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.3f, 1.3f, 0f);
                }else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.7f, 1.7f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);
                }
                break;

            case 2.44f://Samsung Galaxy Z Flip3 5G
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 1f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.4f, 1.3f, 0f);
                }else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.8f, 1.6f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                }
                break;

            case 2.72f://Samsung W22 5G
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 1f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.6f, 1.5f, 0f);
                } else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(2f, 1.75f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.40f, 0.40f, 0.40f);
                }
                break;
      

            case 2.00f://Samsung Galaxy A6s
                if (ScenaAdaptable.name == "SelectLevelSpace")
                {
                    ReferencesScalerScene[0].matchWidthOrHeight = 1f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                    ObjectRecTransformGeneral[0].localScale = new Vector3(1.2f, 1.2f, 0f);
                }
                else if (ScenaAdaptable.name == "YueScene" || ScenaAdaptable.name == "YueScene2" || ScenaAdaptable.name == "YueScene3" || ScenaAdaptable.name == "YueScene4" || ScenaAdaptable.name == "YueScene5"
                || ScenaAdaptable.name == "SceneCard" || ScenaAdaptable.name == "SceneCard 1" || ScenaAdaptable.name == "SceneCard 2" || ScenaAdaptable.name == "SceneCard 3" || ScenaAdaptable.name == "SceneCard 4")
                {
                    FondYueScene.localScale = new Vector3(1.5f, 1.6f);// Establecemos el tamaño del fondo del modo de juego Space Yue
                    AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);
                }
                break;

            case 1.77f:
                AudioManager.shareaudio.InterfacePause.localScale = new Vector3(0.50f, 0.50f, 0.50f);
                break;
        }



        if (aspect == 0.45f || rounded==0.44f)//Sansung Galaxy S20,S21 Ultra, Samsung Galaxy M23
        {
            //Xiaomi Redmi K40 Gaming
            if (ScenaAdaptable.name == "Inicio")
            {
                ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                ReferencesScalerScene[2].matchWidthOrHeight = 0.84f;
                ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Por si se necesitará mover la posición de algun objeto utiliamos la posición local
            }
            else if (ScenaAdaptable.name == "Carga")
            {
                ReferencesScalerScene[3].matchWidthOrHeight = 0.85f;
            }
            else if (ScenaAdaptable.name == "SelectModoJuego")
            {
                ReferencesScalerScene[0].matchWidthOrHeight = 0.84f;
                ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                ObjectRecTransformGeneral[0].localScale = new Vector3(1.5f, 1.5f, 0f);
                ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
            }
            else if (ScenaAdaptable.name == "SelectLevel (Trivias)")
            {
                ReferencesScalerScene[0].matchWidthOrHeight = 0.84f;
                ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);
            }
            else if (ScenaAdaptable.name == "Tienda")
            {
                for (int i = 0; i < ReferencesScalerScene.Length; i++)
                {
                    ReferencesScalerScene[i].matchWidthOrHeight = 0.87f;//Establecemos la relación de altura y anchura para para las descripciones
                }
                ReferencesScalerScene[0].matchWidthOrHeight = 0.94f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                ReferencesScalerScene[1].matchWidthOrHeight = 0.92f;
                ObjectRecTransformGeneral[0].localScale = new Vector3(1f, 1.1f, 0f);//Establecemos la escala en la que queremos que este nuestro objeto
                ObjectRecTransformGeneral[1].localPosition = new Vector2(1f, 0f);//Po|r si se necesitará mover la posición de algun objeto utiliamos la posición local
                OrdSons.cellSize = new Vector2(330f, 330f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
                for (int i = 0; i < TextButonShop.Length; i++)// Recorremos hasta la cantidad de elementos que tenga la lsita de texto de la tienda de botones especificamente en fondos
                {
                    TextButonShop[i].fontSize = 27;//Establecemos este tamaño para esta resolución
                }
            }
            else if (ScenaAdaptable.name == "Level 1" || ScenaAdaptable.name == "Level 2" || ScenaAdaptable.name == "Level 3" || ScenaAdaptable.name == "Level 4" || ScenaAdaptable.name == "Level 5"
                || ScenaAdaptable.name == "Level 6" || ScenaAdaptable.name == "Level 7" || ScenaAdaptable.name == "Level 8" || ScenaAdaptable.name == "Level 9" || ScenaAdaptable.name == "Level 10"
                || ScenaAdaptable.name == "Level 11" || ScenaAdaptable.name == "Level 12" || ScenaAdaptable.name == "Level 13" || ScenaAdaptable.name == "Level 14" || ScenaAdaptable.name == "Level 15"
                || ScenaAdaptable.name == "Level 16" || ScenaAdaptable.name == "Level 17" || ScenaAdaptable.name == "Level 18" || ScenaAdaptable.name == "Level 19" || ScenaAdaptable.name == "Level 20"
                || ScenaAdaptable.name == "Level 21" || ScenaAdaptable.name == "Level 22" || ScenaAdaptable.name == "Level 23" || ScenaAdaptable.name == "Level 24" || ScenaAdaptable.name == "Level 25"
                || ScenaAdaptable.name == "Level 26" || ScenaAdaptable.name == "Level 27" || ScenaAdaptable.name == "Level 28" || ScenaAdaptable.name == "Level 29" || ScenaAdaptable.name == "Level 30"
                || ScenaAdaptable.name == "Level 31" || ScenaAdaptable.name == "Level 32" || ScenaAdaptable.name == "Level 33" || ScenaAdaptable.name == "Level 34" || ScenaAdaptable.name == "Level 35"
                || ScenaAdaptable.name == "Level 36" || ScenaAdaptable.name == "Level 37" || ScenaAdaptable.name == "Level 38" || ScenaAdaptable.name == "Level 39" || ScenaAdaptable.name == "Level 40"
                || ScenaAdaptable.name == "Level 41" || ScenaAdaptable.name == "Level 42" || ScenaAdaptable.name == "Level 43" || ScenaAdaptable.name == "Level 44" || ScenaAdaptable.name == "Level 45"
                || ScenaAdaptable.name == "Level 46" || ScenaAdaptable.name == "Level 47" || ScenaAdaptable.name == "Level 48" || ScenaAdaptable.name == "Level 49" || ScenaAdaptable.name == "Level 50"
                || ScenaAdaptable.name == "Level 51" || ScenaAdaptable.name == "Level 52" || ScenaAdaptable.name == "Level 53" || ScenaAdaptable.name == "Level 54" || ScenaAdaptable.name == "Level 55"
                || ScenaAdaptable.name == "Level 56" || ScenaAdaptable.name == "Level 57" || ScenaAdaptable.name == "Level 58" || ScenaAdaptable.name == "Level 59" || ScenaAdaptable.name == "Level 60")
            {
                ReferencesScalerScene[0].matchWidthOrHeight = 0.83f;//Controlamos la relación de altura de las interfaces Gráficas para que estas se adapten
                ObjectRecTransformGeneral[0].localScale = new Vector3(1.2f, 1.32f, 0f);
                OrdSons.cellSize = new Vector2(450f, 200f);// Aqui controlamos el tamaño de los hijos de los botones de los fondos
            }
        }


    }
}