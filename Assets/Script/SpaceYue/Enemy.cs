using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    public int enemyDamage = 10;


    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin"){
            return;
        } else if(collision.tag == "Player"){
          Contador.ResetHealth();//Se llama al método encargado de restar la vida 
        }

    }
}
