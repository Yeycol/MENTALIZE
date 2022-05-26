using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCartasManager : MonoBehaviour
{
    public static menuCartasManager sharedInstance;

    private void Awake()
    {
        GameManager.shareInstance.StarGame();
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    [SerializeField] Canvas[] menuCanvas = new Canvas[2];

    public void ShowVictory(int score)
    {
        GameManager.shareInstance.WinGame();
        EstatusCartas.sharedInstance.WinPoints(score);
    }

    public void ShowDefeat(int score)
    {
        GameManager.shareInstance.GameOver();
        EstatusCartas.sharedInstance.LosePoints(score);
    }
    public void NoShowVictory()
    {
        menuCanvas[0].enabled = false;
    }

    public void NoShowDefeat()
    {
        menuCanvas[1].enabled = false;
    }
}
