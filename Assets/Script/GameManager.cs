//Se espera que este script sea global que se pueda accesar desde cualquier escena
//Para ello es de vital importancia indicar al usuario en que estado se encuentra si en partida, pausa, menú
//Para esto usaremos un enumerado, muy utilizado cuando solo queremos tomar una serie de valores y no buscamos hacer nada mas 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aqui se creará el enumerado que contendrá los estados del juego
public enum GameState
{
    // Dato: Si establecemos este enumerado fuera de la clase nos permitirá llamarlo desde cualquier otro script 
    menu,
    InGame,
    GameOver,
    Pause,
    Load,
    Win
}

public class GameManager : MonoBehaviour
{
    public GameState currentgameState = GameState.menu;// Variable publica del tipo enumerado inicializada en el menú, es pública por lo tanto se podrá visualizar en la Interfaz de Unity
    public static GameManager shareInstance; // Variable que hace referencia a un singleton 
    void Awake()
    {
        // El primero que llegue a esta línea será el único que controle el game manager
        if (shareInstance == null)
        {
            shareInstance = this;
            DontDestroyOnLoad(gameObject);//Método que evita que las configuraciones y datos almacenados en las variables de la anterior escena no sean borradas al pasar a una nueva
           
        }
        else
        {
         Destroy(gameObject);
        }
        Application.targetFrameRate = 60;//Se indica al videojuego que intente renderizar a una velocidad de fotogramas específicos
      
    }



    void Update()
    {
        EvaluateAlert();//Se llama al método encargado de activar la alerta dependiendo del estado de juego en el que se encuentren
    }

    public void EvaluateAlert()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame ||
           GameManager.shareInstance.currentgameState == GameState.menu)
        {
            //Solo si estamos en modo de juego y en menu, podremos activar la alerta de salida
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Si se presiona el boton de escape del Móvil haremos las siguientes instrucciones
                AudioManager.shareaudio.Efectos[4].Play();//Reproducimos el sonido de Alerta o Logro
                AudioManager.shareaudio.Partida.mute = true;//Muteamos el sonido de la música
                ManagerScene.shareMscen.ActiveAlert();//Llamamos al canvas de alerta
                AnimaCon.ShareAnimation.AlertActive();//Activación de la animación de la alerta
            }
        }
    }

    public void StarGame()
    {
        //Método encargado de iniciar la partida 
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        // Método que será llamado para indicar que se ha perdido una partida
        SetGameState(GameState.GameOver);
    }
    public void BackToMenu()
    {
        //Método que nos permite retornar al menu principal
        SetGameState(GameState.menu);
    }


    public void PauseMenu()
    {
        // Método que nos permitirá pausar la partida 
        SetGameState(GameState.Pause);

    }

    public void LoadPartyandGame()
    {
        //Método que nos permitirá mostrar la pantalla de carga
        SetGameState(GameState.Load);
    }

    public void WinGame()
    {
        //Método que nos permitirá mostrar que el jugador ganó la partida
        SetGameState(GameState.Win);
        
    }

    private void SetGameState(GameState newGameState)
    {

        // Método que recibirá como argumento en este caso uno de los estados almacenados en el enumerados
        if (newGameState == GameState.menu)
        {
            //TODO: Colocar la lógica del menú



        }
        else if (newGameState == GameState.InGame)
        {

            AudioManager.shareaudio.Partida.mute = false;//Se desmutea la música del videojuego
            AudioManager.shareaudio.Efectos[3].mute = false;//Se desmutea el efecto TimeEnd
            ManagerScene.shareMscen.OffOver();//Desactiva la interfaz de usuario al perder partida
            ManagerScene.shareMscen.OffWin();//Desactiva la interfaz de usuario al ganar partida
            AnimaCon.ShareAnimation.DesactivateConfeti();//Método encargado de desactivar la animación del confeti
            AnimaCon.ShareAnimation.ActivePizarra();//Activa la animación d ela pizarra al empezar la aprtida

        }
        else if (newGameState == GameState.GameOver)
        {
            AudioManager.shareaudio.Efectos[3].Stop();//Para el sonido llamado Time End
            AudioManager.shareaudio.Efectos[0].Play();//Activa el sonido llamado OverGame
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Game Over
            AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva la animación del evento Time End
            AnimaCon.ShareAnimation.DesactivatePizarra();//Al perder partida se desactiva la animación de la pizarra
            ManagerScene.shareMscen.ActiveOver();//Se activa el Canvas d ela Interfaz de Usuario al perder partida
            AnimaCon.ShareAnimation.ActivateOver();//Se habilita la animación  de la Interfaz de Usuario
            AnimaCon.ShareAnimation.ActiveCandado();//Se activa la animación del candado al perder
        }
        else if (newGameState == GameState.Win)
        {
            //TODO: Indicar al usuario que ha ganado
            AudioManager.shareaudio.Efectos[3].Stop();//Para el sonido llamado Time End
            AudioManager.shareaudio.Efectos[0].Play();//Activa el sonido llamado WinGame
            AnimaCon.ShareAnimation.DesactivateRedTime();//Se desactiva la animación del evento Time End
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Win
            AnimaCon.ShareAnimation.DesactivatePizarra();//Al perder partida se desactiva la animación de la pizarra
            ManagerScene.shareMscen.ActiveWin();//Se activa el Canvas de la Interfaz de Usuario al ganar la Partida
            AnimaCon.ShareAnimation.ActiveWin();//Se habilita la animación  de la Interfaz de Usuario
            AnimaCon.ShareAnimation.StartPadlock();//Se activa la animación del evento Padlock
            AnimaCon.ShareAnimation.AtivateConfeti();
        }
        else if (newGameState == GameState.Load)
        {
            //TODO: Mostrar la pantalla de carga
            
        }
        else if (newGameState==GameState.Pause)
        {
            //TODO: Mostrar al usuario el menú de pausa
            AudioManager.shareaudio.Efectos[3].mute=true;//Mutea el sonido de Time End
            AudioManager.shareaudio.Partida.mute = true;//Mutea el sonido de la Partida
            

        }
        /*Se establece que la variable currentState la cual es mostrada de
        manera pública en el editor de Unity se igual a la pasada por parámetro*/
        this.currentgameState = newGameState;  
    }
   
 

   

  





}
