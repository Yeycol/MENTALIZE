using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControlador : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridCols = 4;
    public const float offSetX = 2f;
    public const float offSetY = 3f;
    CartasGameYue _firstRevealed;
    CartasGameYue _scondRevealed;
    [SerializeField] float parpadeoRateCard = 0.01f;
    public int score = 0;
    int scoreGame;

    [SerializeField] private CartasGameYue originalCard;
    [SerializeField] private Sprite[] images;

    public static sceneControlador sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this; 
    }

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        numbers = ReorderArrayCard(numbers);

        for(int i = 0; i < gridCols; i++)
        {
            for(int j = 0; j < gridRows; j++)
            {
                CartasGameYue card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard);
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offSetX * i) + startPos.x;
                float posY = -(offSetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    private void Update()
    {
        
        if(GameManager.shareInstance.currentgameState == GameState.InGame) EvaluarVictoria();
    }

    private void EvaluarVictoria()
    {
        if (score == 6 && TimerCartas.sharedInstance.timeLeft > 0)
        {
            scoreGame = 20;
            menuCartasManager.sharedInstance.ShowVictory(scoreGame);

        }
        else if (score >= 0 && score < 6 && TimerCartas.sharedInstance.timeLeft == 0)
        {
            menuCartasManager.sharedInstance.ShowDefeat();
        }
    }

    private int[] ReorderArrayCard(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public bool canReveal
    {
        get { return _scondRevealed == null; }
    }

    public void CardRevealed(CartasGameYue card)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _scondRevealed = card;
            StartCoroutine(CheckedMatch());
        }
    }


    IEnumerator CheckedMatch()
    {
        if (_firstRevealed.id == _scondRevealed.id)
        {
            score++;
        }
        else
        {
            AudioManager.shareaudio.Efectos[32].Play();
            yield return new WaitForSeconds(0.3f);
            int t = 2;
            while(t > 0)
            {
                _firstRevealed.gameObject.SetActive(false);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _scondRevealed.gameObject.SetActive(false);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _firstRevealed.gameObject.SetActive(true);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _scondRevealed.gameObject.SetActive(true);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                t--;
            }
            _firstRevealed.Unreveal();
            _scondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _scondRevealed = null;
    }
}
