using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;

    public int enemyDamage = 10;
    Rigidbody2D rigidBody;
    public bool facingRight = false;
    private Vector3 startPosition;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentRunnigSpeed = runningSpeed;

        if(facingRight){
            //Vista hacia la derecha
            currentRunnigSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        } else{
            //Vista hacia la izquierda
            currentRunnigSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if(GameManager.shareInstance.currentgameState == GameState.InGame){
            rigidBody.velocity = new Vector2(currentRunnigSpeed, rigidBody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin"){
            return;
        }

        if(collision.tag == "Player"){
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }
        //Si no chocamos con otro elemento, entonces hay otro enemigo
        //ROtación de enemigo
        facingRight = !facingRight;
    }
}
