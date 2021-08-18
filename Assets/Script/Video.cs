using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    public float wite = 6f;
    // Start is called before the first frame update

void Awake()
    {
        AudioManager.shareaudio.Partida.mute=true;
    }
    void Start()
    {
        StartCoroutine(Wait_Intro()); 
    }
     IEnumerator Wait_Intro()
    {
        GameManager.shareInstance.LoadPartyandGame();
        yield return new WaitForSeconds(wite);
        ControlNiveles.shareLvl.CambiarNivel(3);
        AudioManager.shareaudio.Partida.Play();
        AudioManager.shareaudio.Partida.mute = false;
    }
  
   

}
