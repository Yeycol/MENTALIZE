using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCartasManager : MonoBehaviour
{
    public static menuCartasManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    [SerializeField] Canvas[] menuCanvas = new Canvas[2];

    private void Start()
    {
        menuCanvas[0].enabled = false;
        menuCanvas[1].enabled = false;       
    }

    public void ShowVictory(int score)
    {
        menuCanvas[0].enabled = true;
        EstatusCartas.sharedInstance.WinPoints(score);
    }

    public void ShowDefeat(int score)
    {
        menuCanvas[1].enabled = true;
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
