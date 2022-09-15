using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproCredito : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        AudioManager.shareaudio.Efectos[14].Pause();
    }
    private void Start()
    {
        StartCoroutine(WaitForE());
    }

   IEnumerator WaitForE() {


        yield return new WaitForSeconds(80f);
        ControlNiveles.shareLvl.CambiarNivel(7);
    
   }

}
