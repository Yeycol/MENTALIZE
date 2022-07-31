using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables del movimiento del personaje
    //public float jumpForce;        // Fuerza de salto
    //public float runningSpeed;     // Velocidad de movimiento horizontal
    public float runSpeed = 0;
    public float runningSpeedHorizontal = 3;
    float horizontalMove = 0;
    [SerializeField] float jumpForce = 3f;
    Rigidbody2D rigidBody;  	        // Variable de física del cuerpo, es una variable 'privada', pero esta palabra se puede omitir
    [SerializeField] Joystick joystickX;
    public LayerMask groundMask;        // Variable de capa del suelo, modificable en interfaz
    Animator animator;
    SpriteRenderer spritePlayer;
    Vector3 startPosition;
    [SerializeField] bool invulnerable;
    [SerializeField] float parpadeoRate = 0.01f;
    

    // Guarda el estado de si está vivo o en el suelo en las variables STATE_ALIVE y STATE_ON_THE_GROUND
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    private int healthPoints;

    public const int INITIAL_HEALTH = 100, MAX_HEALTH = 200, MIN_HEALTH = 10;

    // Obtiene las características físicas antes de inicializar, como la gravedad.
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();    // Otorga las caracteristicas de SpriteRenderer a flipX
      
    }
    // Start is called before the first frame update
    void Start()
    {
        //Indicamos a la música de este modo de juego que se reproduzca en bucle 
        startPosition = this.transform.position;
        Contador.sharecont.text_health.text = Contador.sharecont.vidas.ToString();
        StartGame();
    }

    public void StartGame()
    {
        GameManager.shareInstance.StarGame();//Pasamos al jugador en estado de InGame
        RealTimeAnimation.ShareRealTimeAnimator.RamdomIndex();//Llamamos al método encargadod e habilitar las animaciones de la clase RealTimeAnimation
        animator.SetBool(STATE_ALIVE, true);                // Inicia la variable STATE_ALIVE con true
        animator.SetBool(STATE_ON_THE_GROUND, true);        // Inicia la variable STATE_ON_THE_GROUND con true

        healthPoints = INITIAL_HEALTH;

        Invoke("RestartPosition", 0.2f);
        AudioManager.shareaudio.Efectos[16].Play();
        AudioManager.shareaudio.Efectos[16].loop = true;
    }

    void RestartPosition()
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;

        GameManager.shareInstance.collectedObject = 0;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Si detecta el espacio o click derecho se desencadena el salto
    void Update()
    {
        horizontalMove = joystickX.Horizontal * runningSpeedHorizontal;

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.6f, Color.red);
        Movimiento();
        if (healthPoints <= 0)
        {
            Die();
            gameObject.SetActive(false);
        }
    }
    //Movimiento del personaje en sentido horizontal
    private void Movimiento()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            if (joystickX.Horizontal < 0)
            {
                spritePlayer.flipX = true;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * runSpeed;
            }
            else if (joystickX.Horizontal > 0)
            {
                spritePlayer.flipX = false;
                transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * runSpeed;
            }
        }
    }
    //Método que permite el salto por medio de un Event Trigger en el botón de salto
    public void Saltar()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    // Nos indica si el personaje está o no tocando el suelo
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.6f,
                            groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Método que permite morir al jugador según las condiciones dadas.
    public void Die()
    {
        this.animator.SetBool(STATE_ALIVE, false);

        GameManager.shareInstance.GameOver();
        gameObject.SetActive(false);
    }
    //Condiciones de posicionamiento del jugador, al tocar plataformas con movimiento horizontal, vertical, libre y estático.
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "MovilV":
                transform.position = transform.position;
                break;
            case "MovilH":
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                break;
            case "MovilL":
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                break;
            case "Static":
                transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 1.65f);
                break;
        }
    }
    //Recolección de monedas, cambio de escena a juego de cartas y reducción de vida al tocar objetos peligrosos.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            AudioManager.shareaudio.Efectos[30].Play();
            collision.gameObject.SetActive(false);
            Contador.PointsAdd();
        } else if(collision.CompareTag("Nave"))
        {
            this.gameObject.SetActive(false);
            //Esta condicional evaluará el nombre de la escena activa y dependiendo de la escena cargará el Scene Card correspondiente
            if (Contador.sharecont.scene.name == "YueScene")
            {
                ControlNiveles.shareLvl.CambiarNivel(69);//Le decimos a la clase encargada de cambiar los niveles que cargue la escena de cartas
            } else if (Contador.sharecont.scene.name == "YueScene2")
            {
                ControlNiveles.shareLvl.CambiarNivel(77);
            }
            else if (Contador.sharecont.scene.name == "YueScene3")
            {
                ControlNiveles.shareLvl.CambiarNivel(76);
            }
            else if (Contador.sharecont.scene.name == "YueScene4")
            {
                ControlNiveles.shareLvl.CambiarNivel(75);
            }
            else if (Contador.sharecont.scene.name == "YueScene5")
            {
                ControlNiveles.shareLvl.CambiarNivel(74);
            }
        }
        else if(collision.CompareTag("Peligro"))
        {
            if (invulnerable == true)
            {
                return;
            }
            Contador.ResetHealth();
            invulnerable = true;
            StartCoroutine(HacerVulnerable());
        }
    }
    //Hace vulnerable al personaje tras haber salido de una reducción de vida reciente.
    IEnumerator HacerVulnerable()
    {
        StartCoroutine(Parpadeo());
        yield return new WaitForSeconds(3);
        invulnerable = false;
    }
    //Efecto parpadeo al tocar un objeto peligroso. Activación y desactivación progresiva del sprite para tal efecto.
    IEnumerator Parpadeo()
    {
        int t = 10;
        while(t > 0)
        {
            spritePlayer.enabled = false;
            yield return new WaitForSeconds(t * parpadeoRate);
            spritePlayer.enabled = true;
            yield return new WaitForSeconds(t * parpadeoRate);
            t--;
        }
    }
}
