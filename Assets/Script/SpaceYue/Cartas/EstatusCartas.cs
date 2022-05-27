using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EstatusCartas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreWin;
    [SerializeField] TextMeshProUGUI ScoreLose;
    public static EstatusCartas sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
    }
    public void WinPoints(int score)
    {
        ScoreWin.SetText(score.ToString() + " Pts.");
    }

    public void LosePoints(int score)
    {
        ScoreLose.SetText(score.ToString() + " Pts.");
    }
}
