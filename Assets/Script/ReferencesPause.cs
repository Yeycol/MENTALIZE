using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencesPause : MonoBehaviour
{
    public GameObject Pause;
    public void ReturnMenu()
    {
  
        ActivarOpciones.shareOp.OffCanvasPause();
        ControlNiveles.shareLvl.CambiarNivel(3);
    }

    public void ResetOpciones()
    {
        Contador.sharecont.resetcont();
        ActivarOpciones.shareOp.DesactivatePause();
    }

    public void DesactivateOpciones()
    {
        ActivarOpciones.shareOp.DesactivatePause();
    }
    
}
