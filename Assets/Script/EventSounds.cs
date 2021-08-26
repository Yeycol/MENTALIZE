using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
