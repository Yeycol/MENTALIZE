using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.shareInstance.currentgameState==GameState.InGame)
        {
            PlayerController controller = 
                collision.GetComponent<PlayerController>();
            controller.Die();
        }
    }

}
