using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EstatusCartas : MonoBehaviour
{
    [SerializeField] Text ScoreWin;
    [SerializeField] Text ScoreLose;
    public static EstatusCartas sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
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
