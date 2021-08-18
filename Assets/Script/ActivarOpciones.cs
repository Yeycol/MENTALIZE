using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarOpciones : MonoBehaviour
{
    public ReferencesPause Pause;
    public Canvas Pausa;
    public static ActivarOpciones shareOp;

    private void Awake()
    {
        if (shareOp == null)
        {
            shareOp = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Pause = GameObject.FindGameObjectWithTag("Pausa").GetComponent<ReferencesPause>();
        Pausa = GameObject.Find("Pause").GetComponent<Canvas>();
    }

    // Update is called once per frame
    public void ActivePause()
    {
        //Activa el Canvas que muestra las opciones de pausa
        Time.timeScale = 0f;
        Pausa.enabled = true;
        AudioManager.shareaudio.CargarSlider();
        AudioManager.shareaudio.CargarEfectos();
        GameManager.shareInstance.PauseMenu();
    }
    public void DesactivatePause()
    {
        //Desactiva el Canvas que muestra las opciones de pausa
        Time.timeScale = 1f;
        Pausa.enabled = false;
        GameManager.shareInstance.StarGame();
    }
   public void OffCanvasPause()
    {
        Time.timeScale = 1f;
        Pausa.enabled = false;
    }
    
}
