using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText, coinsTextGO, scoreTextGO;
    private PlayerController controller;
    public float score;
    public int coins;

    public static GameView sharedInstance;
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("YuePlayer").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(GameManager.shareInstance.currentgameState == GameState.InGame){
            coins = GameManager.shareInstance.collectedObject;
            score = controller.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0f);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("0");
            maxScoreText.text = "MaxScore: " + maxScore.ToString("0");
        }else if(GameManager.shareInstance.currentgameState == GameState.GameOver)
        {
            coinsTextGO.text = coins.ToString();
            scoreTextGO.text = "Score: " + score.ToString("0");
        }
    }*/
}
