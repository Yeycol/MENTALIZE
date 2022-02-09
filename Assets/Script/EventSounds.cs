using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSounds : MonoBehaviour
{
    public Scene scene;
    void Awake()
    {
     scene = SceneManager.GetActiveScene();
        EvaluateMusicIngame();//Llamamos al método  encargado de establecer la música de Ingame
    }
  
    public void Disparo()
    {
        AudioManager.shareaudio.Efectos[6].Play();
    }

    public void LLegadaVolando()
    {
        AudioManager.shareaudio.Efectos[7].Play();
    }

    public void SalidaVolando()
    {
        AudioManager.shareaudio.Efectos[8].Play();
    }

    public void Roto()
    {
        AudioManager.shareaudio.Efectos[9].Play();
    }
    public void NaveCorrectEntrada()
    {
        AudioManager.shareaudio.Efectos[10].Play();
    }

    public void ButtonSelect() {
        AudioManager.shareaudio.Efectos[11].Play();
    }
   public void FocosDañados()
    {
        AudioManager.shareaudio.Efectos[12].Play();
    }
   public void PlayMusicTrivias()
    {
            AudioManager.shareaudio.Efectos[15].Play();
        AudioManager.shareaudio.Efectos[15].loop = true;
    }
   public void PlayMusicSpaceYue()
    {
        AudioManager.shareaudio.Efectos[16].Play();
        AudioManager.shareaudio.Efectos[16].loop = true;
    }
   public void PlayAtodaMáquinaGo()
    {
        AudioManager.shareaudio.Efectos[17].Play();
    }
    public void PLaySeTeAcabaElTiempo()
    {
        AudioManager.shareaudio.Efectos[18].Play();
    }

    public void PLayMiraElRelojNoTeQuedaTiempo()
    {
        AudioManager.shareaudio.Efectos[19].Play();
    }
    public void PLayConcentrateTuPuedesHacerloMejor()
    {
        AudioManager.shareaudio.Efectos[20].Play();
    }
    public void EvaluateMusicIngame()
    {
        //Método encargado de evaluar que música debe sonar en Ingame
        //TODO: Aun falta establecer las demás condicionales cuando existan mas niveles
        if (scene.name == "Level 1" || scene.name == "Level 2")
        {
            PlayMusicTrivias();
        }
        else if (scene.name == "SelectLevelSpace")
        {
            PlayMusicSpaceYue();
        }
    }
}
