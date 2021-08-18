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

    private void Awake()
    {
        // El primero que llegue a esta línea será el único que controle el game manager
        if (shareInstance == null)
        {
            shareInstance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else
        {
         Destroy(gameObject);
        }
        
    }



    void Update()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame ||
            GameManager.shareInstance.currentgameState == GameState.menu)
        {
            //Solo si estamos en modo de juego y en menu, podremos activar la alerta de salida
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Si se presiona el boton de escape del Móvil haremos las siguientes instrucciones
                AudioManager.shareaudio.Efectos[4].Play();
                AudioManager.shareaudio.Partida.mute=true;
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
            AudioManager.shareaudio.Partida.mute = false;
            AudioManager.shareaudio.Efectos[3].mute = false;
            ManagerScene.shareMscen.OffAlert();
            AnimaCon.ShareAnimation.DesactivateCandado();
            AnimaCon.ShareAnimation.DesactivateOver();
            Invoke("WaitOver", 0.70f);// Desactiva el canvas del Game Over en un tiempo determinado dando tiempo a que se muestre la animación 
            AnimaCon.ShareAnimation.DesactivateWin();
            AnimaCon.ShareAnimation.StopPadlock();
            AnimaCon.ShareAnimation.DesactivateConfeti();
            AnimaCon.ShareAnimation.ActivePizarra();
            Invoke("WaitWin", 1f);//Desactiva el canvas del Win en un tiempo determinado dando tiempo a que se muestre la animación
        }
        else if (newGameState == GameState.GameOver)
        {
            //TODO: Indicar al usuario que ha perdido la partida
            AudioManager.shareaudio.Efectos[3].Stop();
            AudioManager.shareaudio.Efectos[0].Play();
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Game Over
            ManagerScene.shareMscen.ResetLifeOff();
            AnimaCon.ShareAnimation.DesactivateRedTime();
            AnimaCon.ShareAnimation.DesactivatePizarra();
            ManagerScene.shareMscen.ActiveOver();
            AnimaCon.ShareAnimation.ActivateOver();
            AnimaCon.ShareAnimation.ActiveCandado();
        }
        else if (newGameState == GameState.Win)
        {
            //TODO: Indicar al usuario que ha ganado
            AudioManager.shareaudio.Efectos[3].Stop();
            AudioManager.shareaudio.Efectos[0].Play();
            AnimaCon.ShareAnimation.DesactivateRedTime();
            ManagerScene.shareMscen.OffAlert();//Se desactiva la alerta en caso de que este activa al pasar a Win
            AnimaCon.ShareAnimation.DesactivatePizarra();
            ManagerScene.shareMscen.ActiveWin();
            AnimaCon.ShareAnimation.ActiveWin();
            AnimaCon.ShareAnimation.StartPadlock();
            Invoke("Wait_Confeti",2f);   
        }
        else if (newGameState == GameState.Load)
        {
            //TODO: Mostrar la pantalla de carga
        }
        else if (newGameState==GameState.Pause)
        {
            //TODO: Mostrar al usuario el menú de pausa
            AudioManager.shareaudio.Efectos[3].mute=true;
            AudioManager.shareaudio.Partida.mute = true;
            

        }

       this.currentgameState = newGameState;
    }
    public void WaitOver()
    {
        
        ManagerScene.shareMscen.OffOver();
    }
    public void WaitWin()
    {
        ManagerScene.shareMscen.OffWin();
    }
    public void Wait_Confeti()
    {
        AnimaCon.ShareAnimation.AtivateConfeti();
    }

    
   



}
