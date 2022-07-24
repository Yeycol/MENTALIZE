using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControlador : MonoBehaviour
{
    [Header("Color")]
    Color transparente = new Color(1,1,1,0.7f);
    [Header("FilasColumnas")]
    public const int gridRows = 3;
    public const int gridCols = 4;
    [Header("PosicionCartas")]
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
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};  //Array con los 6 pares de cartas según su posición
        numbers = ReorderArrayCard(numbers);    //Reordenamiendo de posición de cartas

        //Se establece la posición de cada carta generada a partir de la original
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
    //Las condiciones para ganar o perder el juego de cartas
    private void Update()
    {
        if(GameManager.shareInstance.currentgameState == GameState.InGame) EvaluarVictoria();
    }
    //Al revelarse todas las cartas y si el tiempo es mayor a 0, se consigue la victoria, caso contrario es derrota.
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
    //Se reordena de forma aleatoria el array que almacena la posición de las cartas
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
    //Indica que puede revelarse la segunda carta.
    public bool canReveal
    {
        get { return _scondRevealed == null; }
    }
    //Si aún no se ha revelado ninguna carta, la carta se añade al 1er objeto, caso contrario se añade al segundo objeto y se evalua si es correcta o no.
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
    //Se evalúa si la 2da carta revelada coincide con la 1ra o no.
    IEnumerator CheckedMatch()
    {
        if (_firstRevealed.id == _scondRevealed.id)
        {
            score++;
            _firstRevealed.GetComponent<SpriteRenderer>().color =_scondRevealed.GetComponent<SpriteRenderer>().color = transparente;
        }
        else
        {
            AudioManager.shareaudio.Efectos[32].Play();
            yield return new WaitForSeconds(0.3f);
            int t = 2;
            while(t > 0)
            {
                //Se añade un efecto de parpadeo a las cartas, al ser incorrectas.
                _firstRevealed.gameObject.SetActive(false); 
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _scondRevealed.gameObject.SetActive(false);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _firstRevealed.GetComponent<SpriteRenderer>().color = Color.red;
                _scondRevealed.GetComponent<SpriteRenderer>().color = Color.red;
                _firstRevealed.gameObject.SetActive(true);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                _scondRevealed.gameObject.SetActive(true);
                yield return new WaitForSeconds(t * parpadeoRateCard);
                t--;
            }
            //Los 2 objetos contenedores de las cartas, vuelven a su estado inicial, sin revelar.
            _firstRevealed.Unreveal();
            _scondRevealed.Unreveal();
        }
        //El valor lógico de los 2 objetos, vuelve a su valor por defecto.
        _firstRevealed = null;
        _scondRevealed = null;
    }
}
