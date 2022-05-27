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
        EvaluarVictoria();
    }

    private void EvaluarVictoria()
    {
        if (score == 6 && TimerCartas.sharedInstance.timeLeft > 0)
        {
            scoreGame = 20;
            menuCartasManager.sharedInstance.ShowVictory(scoreGame);
            score = -1;

        }
        else if (score >= 0 && score < 6 && TimerCartas.sharedInstance.timeLeft == 0)
        {
            scoreGame = -20;
            menuCartasManager.sharedInstance.ShowDefeat(scoreGame);
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
            yield return new WaitForSeconds(0.3f);
            _firstRevealed.Unreveal();
            _scondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _scondRevealed = null;
    }
}
