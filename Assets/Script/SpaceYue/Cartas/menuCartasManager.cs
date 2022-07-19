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
        GameManager.shareInstance.StarGame();
    }
    [SerializeField] Canvas[] menuCanvas = new Canvas[2];

    public void ShowVictory(int score)
    {
        GameManager.shareInstance.WinGame();
        AnimaCon.ShareAnimation.StartPadlock();
        WinPoints(score);
        Contador.sharecont.GuardadoMonedas.GuardarMonedas();
        PlayerPrefs.DeleteKey("MonedaYue");
        Contador.sharecont.controlCoinsYue = 0;
        PlayerPrefs.SetInt("controlCoinsYue", Contador.sharecont.controlCoinsYue);
        Contador.sharecont.pointsYue += score;
        Contador.sharecont.GuardadoMonedas.GuardarPoints();
    }

    public void ShowDefeat(int score)
    {
        GameManager.shareInstance.GameOver();
        Contador.sharecont.controlCoinsYue = 0;
        PlayerPrefs.SetInt("controlCoinsYue", Contador.sharecont.controlCoinsYue);
        LosePoints(score);
        Contador.sharecont.pointsYue -= score;
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
