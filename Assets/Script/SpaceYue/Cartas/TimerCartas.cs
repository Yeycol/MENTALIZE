using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCartas : MonoBehaviour
{
    Image timeBar;
    public float maxTime = 40f;
    public float timeLeft;

    public static TimerCartas sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
    }

    private void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {
        if(timeLeft > 0f && sceneControlador.sharedInstance.score < 6)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft/maxTime;
        }
        else if(timeLeft <= 0f)
        {
            timeLeft = 0f;
        }
    }
}
