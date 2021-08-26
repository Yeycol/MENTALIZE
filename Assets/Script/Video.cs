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
        GameManager.shareInstance.LoadPartyandGame();//Llamamos al método que se encarga de pasar al estado de menú
        StartCoroutine(Wait_Intro());
    }
    IEnumerator Wait_Intro()
    {
    
        yield return new WaitForSeconds(wite);
        ControlNiveles.shareLvl.CambiarNivel(3);
        AudioManager.shareaudio.Partida.Play();
    }


}
