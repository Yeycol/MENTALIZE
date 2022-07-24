using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartasGameYue : MonoBehaviour
{
    [SerializeField] sceneControlador controller;
    [SerializeField] GameObject carta;

    private void Start()
    {
        AudioManager.shareaudio.Efectos[16].Play();
        AudioManager.shareaudio.Efectos[16].loop = true;
    }
    
    public void OnMouseDown()   //Al hacer click, se revela la carta y se emite el respectivo sonido
    {
        if (carta.activeSelf && controller.canReveal && GameManager.shareInstance.currentgameState == GameState.InGame)
        {
            carta.SetActive(false);
            controller.CardRevealed(this);
            AudioManager.shareaudio.Efectos[31].Play();
        }

    }

    public void Unreveal()  //Se desactiva la carta revelada, volviendo al estado inicial.
    {
        carta.SetActive(true);
        this.GetComponent<SpriteRenderer>().color = Color.white;    
    }

    private int _id;
    public int id   //Retorna el identificador(id) de la carta
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)  //Se enlaza al nuevo id la imagen de carta obtenida
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
