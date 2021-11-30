using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonPantallaInicio : MonoBehaviour
{
    public float tiempoTotal;
    float tiempoActual;
    bool adelante =  true;
    // Start is called before the first frame update
    void Start()
    {
        adelante = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SeasonVisuales>().fuerzaCalor = 0;
        tiempoActual += Time.realtimeSinceStartup;
        if (adelante)
        {
            GetComponent<SeasonVisuales>().seasonValue = Mathf.Lerp(0, 1, tiempoActual / tiempoTotal);
        }
        else
        {
            GetComponent<SeasonVisuales>().seasonValue = Mathf.Lerp(1, 0, tiempoActual / tiempoTotal);
        }

        if(tiempoActual >= tiempoTotal)
        {
            tiempoActual = 0;
            if (adelante)
            {

                adelante = false;
            }
            else
            {
                adelante = true;
            }
        }
    }
}
