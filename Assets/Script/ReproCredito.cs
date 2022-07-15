using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproCredito : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private void Start()
    {
        StartCoroutine(WaitForE());
    }

   IEnumerator WaitForE() {


        yield return new WaitForSeconds(100f);
        ControlNiveles.shareLvl.CambiarNivel(7);
    
   }

}
