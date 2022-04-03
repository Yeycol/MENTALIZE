//Se espera que este script sea global que se pueda accesar desde cualquier escena
//Para ello es de vital importancia indicar al usuario en que estado se encuentra si en partida, pausa, men�
//Para esto usaremos un enumerado, muy utilizado cuando solo queremos tomar una serie de valores y no buscamos hacer nada mas 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Aqui se crear� el enumerado que contendr� los estados del juego
public enum GameState
{
    // Dato: Si establecemos este enumerado fuera de la clase nos permitir� llamarlo desde cualquier otro script 
    menu,
    InGame,
    GameOver,
    Pause,
    Load,
    Win,
    Alert
}

public class GameManager : MonoBehaviour
{
    public GameState currentgameState = GameState.menu;// Variable publica del tipo enumerado inicializada en el men�, es p�blica por lo tanto se podr� visualizar en la Interfaz de Unity
    public static GameManager shareInstance; // Variable que hace referencia a un singleton 
    private bool HabiliteAudioLoad;//Variable de tipo booleano en la que se pretende almacenar un booleano para saber si se debe dar play o no a la canci�n de las trivias
    public Scene scene;//Variable de tipo Scena que pretende guardar la escena actual
    public int collectedObject = 0;// Asignacion de valor inicial de objetos recolectables
    void Awake()
    {
        // El primero que llegue a esta l�nea ser� el �nico que controle el game manager
        if (shareInstance == null)
        {
            shareInstance = this;
            DontDestroyOnLoad(gameObject);//M�todo que evita que las configuraciones y datos almacenados en las variables de la anterior escena no sean borradas al pasar a una nueva
           
        }
        else
        {
         Destroy(gameObject);
        }
        Application.targetFrameRate = 60;//Se indica al videojuego que intente renderizar a una velocidad de fotogramas espec�ficos  
        scene = SceneManager.GetActiveScene();//Obtenemos la escena actual en la que nos encontremos
    }



    void Update()
    {
        EvaluateAlert();//Se llama al m�todo encargado de activar la alerta dependiendo del estado de juego en el que se encuentren
    }

    public void EvaluateAlert()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame ||
           GameManager.shareInstance.currentgameState == GameState.menu)
        {
            //Solo si estamos en modo de juego y en menu y el estado de juego es diferente de GameOver Y Win 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Si se presiona el boton de escape del M�vil haremos las siguientes instrucciones
                AlertActive();//Se llama al m�todo encargado de pasar al m�todo SetGameState el estado de Alerta
            }
        }
    }

    public void StarGame()
    {
        //M�todo encargado de iniciar la partida 
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        // M�todo que ser� llamado para indicar que se ha perdido una partida
        SetGameState(GameState.GameOver);
    }
    public void BackToMenu()
    {
        //M�todo que nos permite retornar al menu principal
        SetGameState(GameState.menu);
    }


    public void PauseMenu()
    {
        // M�todo que nos permitir� pausar la partida 
        SetGameState(GameState.Pause);

    }

    public void LoadPartyandGame(bool active_audio=false)
    {
        HabiliteAudioLoad = active_audio;//Se almacena el valor booleano pasado por referencia a la variable booleana
        //M�todo que nos permitir� mostrar la pantalla de carga
        SetGameState(GameState.Load);
    }

    public void WinGame()
    {
        //M�todo que nos permitir� mostrar que el jugador gan� la partida
        SetGameState(GameState.Win);
        
    }
    public void AlertActive()
    {
        //M�todo encargado de pasar estado de juego Alerta Activa
        SetGameState(GameState.Alert);
    }

    private void SetGameState(GameState newGameState)
    {

        // M�todo que recibir� como argumento en este caso uno de los estados almacenados en el enumerados
        if (newGameState == GameState.menu)
        {
            AudioManager.shareaudio.Efectos[14].UnPause();//Quitamos la pausa a la M�sica del Men� para que se vuelva a escuchar
            AudioManager.shareaudio.Efectos[15].Pause();//Colocamos en Pausa a la m�sica de la Trivia
            AudioManager.shareaudio.Efectos[16].Pause();//Colocamos En Pausa a la m�sica de Space Yue
            AudioManager.shareaudio.Efectos[3].Stop();       
        }
        else if (newGameState == GameState.InGame)
        {
            if (scene.name == "GameScene")
            {
                LevelManager.sharedInstance.RemoveAllLevelBlocks();
                LevelManager.sharedInstance.GenerateInitialBlocks();
            }
            else 
            {
                AudioManager.shareaudio.Efectos[14].Pause();//Al llegar ha In game es necesario pausar la canci�n del men�
                AudioManager.shareaudio.Efectos[15].UnPause();//Quitamos la pausa de las canciones de la partida siempre y cuando tengan la orden play establecida
                AudioManager.shareaudio.Efectos[16].UnPause();
               // ManagerScene.shareMscen.OffOver();//Desactiva la interfaz de usuario al perder partida
                //ManagerScene.shareMscen.OffWin();//Desactiva la interfaz de usuario al ganar partida
            }

        }
        else if (newGameState == GameState.GameOver)
        {
            AudioManager.shareaudio.Efectos[3].Stop();//Paramos el sonido del TimeEnd
            AudioManager.shareaudio.Efectos[15].Stop();//De igual forma en el Win paramos la m�sica de la escena para posterior escuchar el sonido de Game Over 
            AudioManager.shareaudio.Efectos[16].Stop();
            AudioManager.shareaudio.Efectos[17].Stop();//Paramos el sonido de la frase A toda la m�quina Go Go Go
            AudioManager.shareaudio.Efectos[18].Stop();//Paramos el sonido de Se te caba Tiempo Tic Tac
            AudioManager.shareaudio.Efectos[19].Stop();//Paramos el sonido de la frase Mira el reloj no te queda tiempo 
            AudioManager.shareaudio.Efectos[20].Stop();//Paramos el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio.Efectos[21].Stop();//Paramos el sonido de la frase Ey no te distraigas tienes una vida menos
            AudioManager.shareaudio.Efectos[22].Stop();//Paramos el sonido de la frase Mira en donde presionas tienes una vida menos
            AudioManager.shareaudio.Efectos[0].Play();//Activa el sonido llamado OverGame
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Game Over
            AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva la animaci�n del evento Time End
            //AnimaCon.ShareAnimation.DesactivatePizarra();//Al perder partida se desactiva la animaci�n de la pizarra
            ManagerScene.shareMscen.ActiveOver();//Se activa el Canvas d ela Interfaz de Usuario al perder partida
            AnimaCon.ShareAnimation.ActivateOver();//Se habilita la animaci�n  de la Interfaz de Usuario
            AnimaCon.ShareAnimation.ActiveCandado();//Se activa la animaci�n del candado al perder
            RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = true;//Activamos el canvas de las animaciones
            RealTimeAnimation.ShareRealTimeAnimator.EventInGame("Over");//Pasamos el caso al m�todo para activar una animaci�n de manera aleatoria
        }
        else if (newGameState == GameState.Win)
        {
            //TODO: Indicar al usuario que ha ganado
            AudioManager.shareaudio.Efectos[15].Stop();//Al ganar una partida es necesario apagar la m�sica  para escuchar el sonido de win
            AudioManager.shareaudio.Efectos[16].Stop();
            AudioManager.shareaudio.Efectos[3].Stop();//Para el sonido llamado Time End
            AudioManager.shareaudio.Efectos[13].Play();//Activa el sonido llamado WinGame
            AudioManager.shareaudio.Efectos[17].Stop();//Paramos el sonido de la frase A toda la m�quina Go Go Go
            AudioManager.shareaudio.Efectos[18].Stop();//Paramos el sonido de Se te caba Tiempo Tic Tac
            AudioManager.shareaudio.Efectos[19].Stop();//Paramos el sonido de la frase Mira el reloj no te queda tiempo 
            AudioManager.shareaudio.Efectos[20].Stop();//Paramos el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio.Efectos[21].Stop();//Paramos el sonido de la frase Ey no te distraigas tienes una vida menos
            AudioManager.shareaudio.Efectos[22].Stop();//Paramos el sonido de la frase Mira en donde presionas tienes una vida menos
            AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva la animaci�n del evento Time End
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Win
            //AnimaCon.ShareAnimation.DesactivatePizarra();//Al perder partida se desactiva la animaci�n de la pizarra
            ManagerScene.shareMscen.ActiveWin();//Se activa el Canvas de la Interfaz de Usuario al ganar la Partida
            AnimaCon.ShareAnimation.ActiveWin();//Se habilita la animaci�n  de la Interfaz de Usuario
            RealTimeAnimation.ShareRealTimeAnimator.Refer.enabled = true;//Activamos el canvas de las animaciones
            StartCoroutine(WaitForConfeti());//Llamamos a la corrutina encargada de habilitar la animaci�n del Confeti
        }
        else if (newGameState == GameState.Load)
        {   
            if (HabiliteAudioLoad)//Esta condicional evalua si la variable booleana es true para poder un pausar el sonido de menu, reiniciarlo y repetirlo en loop
            {
                AudioManager.shareaudio.Efectos[14].UnPause();//Desmuteamos la m�sica del men� del juego
                AudioManager.shareaudio.Efectos[14].Play();//Se vuelve a reproducir el audio desde un inicio para que no se escuche entrecortado al desmutear
                AudioManager.shareaudio.Efectos[14].loop = true;// Se establece en bucle para que la m�sica del men� se reproduzca   
            }
        }
        else if (newGameState==GameState.Pause)
        {
            //TODO: Recuerda que al agregar un sonido nuevo debes Pausarlo si esta en pausa 
            AudioManager.shareaudio.Efectos[3].Pause();//Pausa el sonido de Time End
            AudioManager.shareaudio.Efectos[6].Pause();//Pausamos el Efecto Disparo
            AudioManager.shareaudio.Efectos[7].Pause();//Pausamos el Efecto Llegada Nave
            AudioManager.shareaudio.Efectos[8].Pause();//Pausamos el Efeco Salida Nave
            AudioManager.shareaudio.Efectos[9].Pause();//Pausamos el Efeco Roto
            AudioManager.shareaudio.Efectos[10].Pause();//Pausamos el Efeco Abducir Nave
            AudioManager.shareaudio.Efectos[14].Pause();//Al pasar en esta pausa se deben deshabilitar las canciones en determinada escena para proceder con su configuraci�n*/
            AudioManager.shareaudio.Efectos[15].Pause();// Al pasar a pausa se para un momento la m�sica de Trivias
            AudioManager.shareaudio.Efectos[16].Pause();//Se pausa la m�sica de Space Yue
            AudioManager.shareaudio.Efectos[17].Pause();//Se pausa la Frase A toda M�quina gogo
            AudioManager.shareaudio.Efectos[18].Pause();//Se pausa la Frase Se te acaba el Tiempo
            AudioManager.shareaudio.Efectos[19].Pause();//Se pausa el sonido de la frase Mira el Reloj no te queda tiempo
            AudioManager.shareaudio.Efectos[20].Pause();//Se pausa el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio .Efectos[21].Pause();//Se pausa el sonido de la frase Ey no te distraigas te queda una vida menos 
            AudioManager.shareaudio.Efectos[22].Pause();//Se pausa el sonido de la frase Mira en donde presionas tienes una vida menos
     
        } else if (newGameState == GameState.Alert)
        { 
            //TODO: Recuerda que cada que agregues algun Efecto de sonido nuevo debes asignarle su estado cuando pase a Alert
            //Este modo de encarga de desahabilitar los sonidos para cuando sale la alerta
            AudioManager.shareaudio.Efectos[4].Play();//Reproducimos el sonido de Alerta o Logro
            AudioManager.shareaudio.Efectos[3].Pause();//Pausa el sonido de Time End
            AudioManager.shareaudio.Efectos[6].mute = true;//Pausamos el Efecto Disparo
            AudioManager.shareaudio.Efectos[7].mute = true;//Pausamos el Efecto Llegada Nave
            AudioManager.shareaudio.Efectos[8].mute = true;//Pausamos el Efeco Salida Nave
            AudioManager.shareaudio.Efectos[9].mute = true;//Pausamos el Efeco Roto
            AudioManager.shareaudio.Efectos[10].mute = true;//Pausamos el Efeco Abducir Nave
            AudioManager.shareaudio.Efectos[12].mute = true;//Muteamos el sonido de los Focos Da�ados
            AudioManager.shareaudio.Efectos[14].Pause();//Muteamos el sonido de la m�sica
            AudioManager.shareaudio.Efectos[15].Pause();//Muteamos la m�sica para Trivias
            AudioManager.shareaudio.Efectos[16].Pause();//Muteamos m�sica para Space Yue
            AudioManager.shareaudio.Efectos[17].mute = true;//Muteamos la frase de A toda m�quina Go Go
            AudioManager.shareaudio.Efectos[18].mute = true;//Muteamos la frase Se te aca el Tiempo Tic Tac
            AudioManager.shareaudio.Efectos [19].mute = true;//Muteamos el sonido de la frase Mira el Reloj
            AudioManager.shareaudio.Efectos[20].mute = true;//Mutemaos el sonido de la frase Concentrate tu puedes hacerlo mejor
            AudioManager.shareaudio.Efectos[21].mute = true;//Muteamos el sonido de la frase Ey no te distraigas tienes una vida menos
            AudioManager.shareaudio.Efectos[22].mute=true;//Muteamos el sonido de la frase Mira donde presionas tienes una vida menos         
            ManagerScene.shareMscen.ActiveAlert();//Llamamos al canvas de alerta
        }
        /*Se establece que la variable currentState la cual es mostrada de
        manera p�blica en el editor de Unity se igual a la pasada por par�metro*/
        currentgameState = newGameState;  
    }
   
    IEnumerator WaitForConfeti()
    {

        //Esta corrutina permite darle tiempo a la animaci�n dle Active Over 
        //De esa forma evitar perdida de FPS Al ejecutar simultaneas animaciones
        //AnimaCon.ShareAnimation.EventInGame("Win");//LLamamos al m�todo encargado de habilitar las animaciones por eventualidad
        RealTimeAnimation.ShareRealTimeAnimator.EventCorrectAndError[4].gameObject.SetActive(true);//Habilitamos el objeto del confeti para poder activar la animaci�n del Confeti
        RealTimeAnimation.ShareRealTimeAnimator.StartAnimationConfeti();//Activamos la animaci�n del confeti para cuando sale la interfaz de win
        yield return new WaitForSeconds(1f);//Tiempo que se le asigna a la corrutina para que ejecute las acciones establecidas
        StartCoroutine(WaitForWin());//Llamamos a la corrutina encargada de dar tiempo para llamar a la animaci�n de holograma
    }
    IEnumerator WaitForWin()
    {
       //Corrutina que espera un segundo para poder llmar a la animaci�n de Win Game
        yield return new WaitForSeconds(0.5f);
        RealTimeAnimation.ShareRealTimeAnimator.EventInGame("Win");//Pasamos el caso al m�todo para activar una animaci�n de manera aleatoria


    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }

}
