using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oscilador : MonoBehaviour
{
    [SerializeField] Vector2 posInicial;
    [SerializeField] Vector2 dirDesplazamiento;
    [SerializeField] [Range(0,1)] float desplazamiento;
    [SerializeField] float periodo;

    
    
    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        if(periodo >= float.Epsilon)
        {
            float ciclos = Time.time / periodo;
            float tau = Mathf.PI * 2;
            float Seno = Mathf.Sin(tau * ciclos);
            desplazamiento = (Seno / 2) + 0.5f;

            transform.position = posInicial + (dirDesplazamiento * desplazamiento);
        }
        
    }
}
