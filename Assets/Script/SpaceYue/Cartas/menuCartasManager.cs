using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCartasManager : MonoBehaviour
{
    [SerializeField] Text ScoreWin;
    [SerializeField] Text ScoreLose;
    public static menuCartasManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    private void Start()
    {
        Contador.sharecont.GuardadoMonedas.CargarPoints();
        GameManager.shareInstance.StarGame();
    }
    [SerializeField] Canvas[] menuCanvas = new Canvas[2];

    public void ShowVictory(int score)
    {
        //Este método ejecuta sus tareas solo cuando es llamado 
        GameManager.shareInstance.WinGame();//Se pasa el estado al Game Manager para que muestre la interfaz de Ganado
        AnimaCon.ShareAnimation.StartPadlock();//Se llama a la animación para cuando el candado debe desbloquearse
        WinPoints(score);// Llama al método encargado de mostrar los puntos ganados en el nivel
        Contador.sharecont.GuardadoMonedas.GuardarMonedas();//Se guarda las monedas que en ese momento fueron incrementadas, con lo conseguido antes de entrar a scene Card
        Contador.sharecont.pointsYue += score;// En caso de ganar se incrementa el valo de los puntos Yue, de acuerdo al valor llegado por parámetro
        Contador.sharecont.GuardadoMonedas.GuardarPoints();// Se envia a guardar de manera binaria los puntos conseguidos en la actual partida
        ControlNiveles.shareLvl.DesbloquearSpace();//Se llama al método encargado de desbloquear los niveles
    }

    public void ShowDefeat()
    {
        GameManager.shareInstance.GameOver();
        LosePoints(0);
        Contador.sharecont.GuardadoMonedas.GuardarPoints();
    }
    public void NoShowVictory()
    {
        menuCanvas[0].enabled = false;
    }

    public void NoShowDefeat()
    {
        menuCanvas[1].enabled = false;
    }
    public void WinPoints(int score)
    {
        ScoreWin.text = score.ToString() + " Pts.";
    }
    public void LosePoints(int score)
    {
        ScoreLose.text = score.ToString() + " Pts.";
    }
}
