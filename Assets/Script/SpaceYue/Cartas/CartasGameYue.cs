using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartasGameYue : MonoBehaviour
{
    [SerializeField] sceneControlador controller;
    [SerializeField] GameObject carta;
    
    public void OnMouseDown()
    {
        if (carta.activeSelf && controller.canReveal && GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            carta.SetActive(false);
            controller.CardRevealed(this);
            AudioManager.shareaudio.Efectos[11].Play();
        }

    }

    public void Unreveal()
    {
        carta.SetActive(true);
    }

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

}
