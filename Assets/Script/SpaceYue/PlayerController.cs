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
    public float runningSpeedVertical = 3;  //DESACTIVAR
    float horizontalMove = 0;
    float verticalMove = 0; //DESACTIVAR
    [SerializeField] float jumpForce = 3f;
    Rigidbody2D rigidBody;  	        // Variable de física del cuerpo, es una variable 'privada', pero esta palabra se puede omitir
    [SerializeField] Joystick joystickX;
    [SerializeField] Joystick joystickY;
    public LayerMask groundMask;        // Variable de capa del suelo, modificable en interfaz
    Animator animator;
    SpriteRenderer spritePlayer;
    Vector3 startPosition;
    float travelledDistance;
    [SerializeField] bool invulnerable;
    [SerializeField] float parpadeoRate = 0.01f;
    

    // Guarda el estado de si está vivo o en el suelo en las variables STATE_ALIVE y STATE_ON_THE_GROUND
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    private int healthPoints; //manaPoints

    //POner esto en public const int
    //INITIAL_MANA = 30, MAX_MANA = 30, MIN_MANA = 0;
    //---
    //public const int SUPERJUMP_COST = 2;
    //public const float SUPERJUMP_FORCE = 1.3f;

    public const int INITIAL_HEALTH = 100, MAX_HEALTH = 200, MIN_HEALTH = 10;

    // Obtiene las características físicas antes de inicializar, como la gravedad.
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();    // Otorga las caracteristicas de SpriteRenderer a flipX
        GameManager.shareInstance.StarGame();//Pasamos al jugador en estado de InGame
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
        RealTimeAnimation.ShareRealTimeAnimator.RamdomIndex();//Llamamos al método encargadod e habilitar las animaciones de la clase RealTimeAnimation
        animator.SetBool(STATE_ALIVE, true);                // Inicia la variable STATE_ALIVE con true
        animator.SetBool(STATE_ON_THE_GROUND, true);        // Inicia la variable STATE_ON_THE_GROUND con true

        healthPoints = INITIAL_HEALTH;
        //manaPoints = INITIAL_MANA;

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
        verticalMove = joystickX.Vertical * runningSpeedVertical;    //DESACTIVAR
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

    private void Movimiento()
    {
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            if (joystickX.Vertical >= 0.5 && IsTouchingTheGround()) //joystickY DESACTIVAR

            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                transform.position += new Vector3(0, verticalMove, 0) * Time.deltaTime * runSpeed;  //DESACTIVAR
            }

            //transform.position += new Vector3(horizontalMove, verticalMove, 0) * Time.deltaTime * runSpeed;
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

    /*private void MoveTeclas()
    {
        //GetButtonDown("Jump")
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(false);
            rigidBody.gravityScale = 1f;

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Jump(true);
            rigidBody.gravityScale = 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {  // Al presionar D o Flecha dere  // se da vel. de mov. positiva
            move(runningSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {   // Al presionar A o Flecha izq   // se da vel. de mov. negativa
            move(-runningSpeed);
        }
        else
        {
            move(0);
        }
    }*/
    
    /*void FixedUpdate()
    {
        if (rigidBody.velocity.x != 0)
        {      // Si el personaje esta quieto, se cancela la animacion
            animator.enabled = true;
        }
        else if (IsTouchingTheGround() == false)
        {
            animator.enabled = true;        // Si esta en mov, la animacion se reanuda 
        }*/
        //animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());       // En cada frame, ubica si el jugador está o no el suelo y lo ubica en su sitio
        //Debug.DrawRay(this.transform.position, Vector2.down * 1.6f, Color.red);      // Debug permite probar cosas, en este caso dibuja un rayo rojo desde
                                                                                     // el centro del jugador hacia el suelo
        /*if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            MoveTeclas();
        }
        if (healthPoints <= 0)
        {
            Die();
        }
    }*/

    /*void move(float direction)
    {
        rigidBody.velocity = new Vector2(direction, rigidBody.velocity.y);  // Da la velocidad de mov en X y Y
        if (Input.GetKey(KeyCode.A))
        {        // Si la direccion cambia de sentido, el sprite se da la vuelta al renderizarse
            flipX.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {                              // Sino, la renderizacion se mantiene normal
            flipX.flipX = false;
        }

    }*/

    // La forma en que salta, tomando la dirección, la fuerza y el modo de la fuerza
    /*void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;
    //-----------
        /*if (superjump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
    //-----------
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }*/
    /*void Jump(bool superjump)
    {
        float jumpForceFactor = runningSpeedVertical;

        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }*/


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
    public void Die()
    {
        travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }

        this.animator.SetBool(STATE_ALIVE, false);

        GameManager.shareInstance.GameOver();
        gameObject.SetActive(false);
    }

    public void CollectHealth(int points)
    {
        healthPoints += points;
        if (healthPoints >= MAX_HEALTH)
        {
            healthPoints = MAX_HEALTH;
        }
    }

    /*public void CollectMana(int points)
    {
        manaPoints += points;
        if (manaPoints >= MAX_MANA)
        {
            manaPoints = MAX_MANA;
        }
    }*/

    public int GetHealth()
    {
        return healthPoints;
    }

    /*public int GetMana()
    {
        return manaPoints;
    }*/

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "MovilV":
                //rigidBody.gravityScale = 17f;

                if (joystickX.Vertical > 0) //joystickY DESACTIVAR

                {
                    transform.position = transform.position;
                }
                else
                {
                    transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 1.65f);
                }
                break;
            case "MovilH":
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                Debug.Log("Posición alterada");
                break;
            case "MovilL":
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                Debug.Log("Posición alterada");
                break;
            /*default:
                transform.position = transform.position;
                break;*/
        }
        /*if (collision.gameObject.tag == "Movil")
        {
            //rigidBody.gravityScale = 17f;
            //Debug.Log("Posición alterada");
        }*/
    }
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
            ControlNiveles.shareLvl.CambiarNivel(69);
        }else if(collision.CompareTag("Peligro"))
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
    IEnumerator HacerVulnerable()
    {
        StartCoroutine(Parpadeo());
        yield return new WaitForSeconds(3);
        invulnerable = false;
    }
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
