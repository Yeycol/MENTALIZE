using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables del movimiento del personaje
    public float jumpForce;        // Fuerza de salto
    public float runningSpeed;     // Velocidad de movimiento horizontal
    Rigidbody2D rigidBody;  	        // Variable de física del cuerpo, es una variable 'privada', pero esta palabra se puede omitir
    public LayerMask groundMask;        // Variable de capa del suelo, modificable en interfaz
    Animator animator;
    SpriteRenderer flipX;
    Vector3 startPosition;
    float travelledDistance;
    Rigidbody2D platformMovil;

    // Guarda el estado de si está vivo o en el suelo en las variables STATE_ALIVE y STATE_ON_THE_GROUND
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 30,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0;
    public const int SUPERJUMP_COST = 2;
    public const float SUPERJUMP_FORCE = 1.5f;

    // Obtiene las características físicas antes de inicializar, como la gravedad.
    void Awake()
    {
        platformMovil = GameObject.FindWithTag("Movil").GetComponent<Rigidbody2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flipX = GetComponent<SpriteRenderer>();    // Otorga las caracteristicas de SpriteRenderer a flipX
        GameManager.shareInstance.StarGame();//Pasamos al jugador en estado de InGame


    }
    // Start is called before the first frame update
    void Start()
    {
        //Indicamos a la música de este modo de juego que se reproduzca en bucle 
        startPosition = this.transform.position;
        PlayerPrefs.SetFloat("maxscore", 0);
        StartGame();
    }

    public void StartGame()
    {
        RealTimeAnimation.ShareRealTimeAnimator.RamdomIndex();//Llamamos al método encargadod e habilitar las animaciones de la clase RealTimeAnimation
        animator.SetBool(STATE_ALIVE, true);                // Inicia la variable STATE_ALIVE con true
        animator.SetBool(STATE_ON_THE_GROUND, true);        // Inicia la variable STATE_ON_THE_GROUND con true

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

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
    {   // Si estamos en partida, podemos movernos
        if (GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            MoveTeclas();
        }
        /*else
        {      // Si no estamos en partida, no hay velocidad en X
            rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
        }*/
    }

    private void MoveTeclas()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Jump(true);
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
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
    }

    void FixedUpdate()
    {
        if (rigidBody.velocity.x != 0)
        {      // Si el personaje esta quieto, se cancela la animacion
            animator.enabled = true;
        }
        else if (IsTouchingTheGround() == false)
        {
            animator.enabled = true;        // Si esta en mov, la animacion se reanuda 
        }/*else{
            animator.enabled = false;
        }*/
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());       // En cada frame, ubica si el jugador está o no el suelo y lo ubica en su sitio
        Debug.DrawRay(this.transform.position, Vector2.down * 1.8f, Color.red);      // Debug permite probar cosas, en este caso dibuja un rayo rojo desde
                                                                                     // el centro del jugador hacia el suelo
        if (healthPoints <= 0)
        {
            Die();
        }
    }

    void move(float direction)
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

    }

    // La forma en que salta, tomando la dirección, la fuerza y el modo de la fuerza
    void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;
        if (superjump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }

    // Nos indica si el personaje está o no tocando el suelo
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.8f,
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
    }

    public void CollectHealth(int points)
    {
        healthPoints += points;
        if (healthPoints >= MAX_HEALTH)
        {
            healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        manaPoints += points;
        if (manaPoints >= MAX_MANA)
        {
            manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Movil")
        {
            float velPlatform = platformMovil.velocity.y;
            rigidBody.velocity = new Vector2(0, velPlatform);
            //this.GetComponent<Rigidbody2D>().gravityScale = 100f;
            Debug.Log(rigidBody.velocity.y);
        }
    }
}
