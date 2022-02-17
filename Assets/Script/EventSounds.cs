using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSounds : MonoBehaviour
{

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
   public void FocosDa�ados()
    {
        AudioManager.shareaudio.Efectos[12].Play();
    }
   public void PlayMusicSpaceYue()
    {
        //TODO: Establecer el sonido de Space Yue en un objeto que no este en ninguna otra escena
        AudioManager.shareaudio.Efectos[16].Play();
        AudioManager.shareaudio.Efectos[16].loop = true;
    }
   public void PlayAtodaM�quinaGo()
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
    public void PLayEyNoTeDistraigasTienesUnaVidaMenos()
    {
        AudioManager.shareaudio.Efectos[21].Play();
    }
    public void PLayMiraEnDondePresionasTienesUnaVidaMenos()
    {
        AudioManager.shareaudio.Efectos[22].Play();
    }
    public void PlayHitHitHurra()
    {
        AudioManager.shareaudio.Efectos[23].Play();
    }
    public void PlayJiJiJi()
    {
        AudioManager.shareaudio.Efectos[24].Play();
    }
    public void PlayKaboomVamosPorOtra()
    {
        AudioManager.shareaudio.Efectos[25].Play();
    }
    public void PlayEsoFueExcelenteQuieresIrPorMuffins()
    {
        AudioManager.shareaudio.Efectos[26].Play();
    }
    public void PlayCuantoMasdificilEsLaVictoria()
    {
        AudioManager.shareaudio.Efectos[27].Play();
    }
   
}
