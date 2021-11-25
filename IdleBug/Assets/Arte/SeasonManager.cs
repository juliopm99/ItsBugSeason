using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public bool firstYear = true;
    public bool spring =  false;
    public bool summer = false;
    public bool autumn = false;
    public bool calor = false;
    public bool frio = false;
    public float tiempoPartida = 360;
    public float probBaseEvento = 10f;
    public float probActualEvento;
    public float tiempoComprobarEvento = 5f;
    float baseLluviaEvento;
    float baseSequiaEvento;
    public bool lluvia;
    public bool sequia;
    float tmp;
    public float tiempoEventoDuracion;
    public bool eventoActivo;
    // Start is called before the first frame update
    void Awake()
    {
        //firstYear = true;
        DecidirCondiciones();
        tmp = tiempoComprobarEvento;
        probActualEvento = probBaseEvento;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SeasonVisuales>().seasonValue = Mathf.Lerp(0, 1, Time.realtimeSinceStartup / tiempoPartida);
        if(Time.realtimeSinceStartup < tiempoPartida / 3) { spring = true; summer = false; autumn = false; }
        if(Time.realtimeSinceStartup >= tiempoPartida / 3 && Time.realtimeSinceStartup < (tiempoPartida * 2) / 3) { spring = false; summer = true; autumn = false; }
        if(Time.realtimeSinceStartup >= (tiempoPartida * 2) / 3 && Time.realtimeSinceStartup < tiempoPartida) { spring = false; summer = false; autumn = true; }
        if(Time.realtimeSinceStartup >= tiempoPartida) { print("Sacabo");  Time.timeScale = 0;  }
        if (spring)
        {
            //manzanos + 20%
            if (calor)
            {
                baseSequiaEvento = 7;
                baseLluviaEvento = 8;
                print("primaveracalor");
            }
            else if (frio)
            {
                baseSequiaEvento = 3;
                baseLluviaEvento = 4;
                print("primaverafrio");
            }
            else
            {
                baseSequiaEvento = 5f;
                baseLluviaEvento = 6f;
                print("primaveranormal");
            }
        }
        else if (summer)
        {
            //flores + 20%
            if (calor)
            {
                baseSequiaEvento = 9;
                baseLluviaEvento = 10;
                print("veranocalor");
            }
            else if (frio)
            {
                baseSequiaEvento = 6;
                baseLluviaEvento = 7;
                print("veranofrio");
            }
            else
            {
                baseSequiaEvento = 7;
                baseLluviaEvento = 8;
                print("veranoanormal");
            }
        }
        else if (autumn)
        {
            //flores y arboles -10%
            if (calor)
            {
                baseSequiaEvento = 4;
                baseLluviaEvento = 5;
                print("otonocalor");
            }
            else if (frio)
            {
                baseSequiaEvento = 9;
                baseLluviaEvento = 10;
                print("otonofrio");
            }
            else
            {
                baseSequiaEvento = 3;
                baseLluviaEvento = 4;
                print("otononormal");
            }
        }


        tmp -= Time.deltaTime;

        if (eventoActivo == false)
        {
            if (tmp <= 0)
            {
                int randomEvento = Random.Range(0, 100);

                if (randomEvento <= probActualEvento)
                {
                    probActualEvento = probBaseEvento;
                    int randomLluviaSequia = Random.Range(1, 10);
                    if (randomLluviaSequia <= baseSequiaEvento)
                    {
                        sequia = true;

                        GetComponent<SeasonVisuales>().tmp = 0;
                        GetComponent<SeasonVisuales>().sequia = true;
                        eventoActivo = true;
                        tmp = tiempoEventoDuracion;
                        print("sequia");
                    }
                    if (randomLluviaSequia >= baseLluviaEvento)
                    {
                        lluvia = true;
                        GetComponent<SeasonVisuales>().tmp = 0;
                        GetComponent<SeasonVisuales>().lluvia = true;
                        
                        eventoActivo = true;
                        tmp = tiempoEventoDuracion;
                        print("lluvia");
                    }
                }
                else
                {
                    probActualEvento += 5;
                    tmp = tiempoComprobarEvento;
                }
            }
        }
        else
        {
            if(tmp <= 0)
            {
                lluvia = false;
                GetComponent<SeasonVisuales>().tmp = 0;
                GetComponent<SeasonVisuales>().lluvia = false;
                GetComponent<SeasonVisuales>().sequia = false;
                sequia = false;
                tmp = tiempoComprobarEvento * 2;
                eventoActivo = false;
                
            }
        }
       

        if (lluvia)
        {

            //Datos
        }

        if (sequia)
        {
            //Datos
        }

    }

    void DecidirCondiciones()
    {
        if (firstYear)
        {
            calor = false;
            frio = true;
        }
        else
        {
            int decision = Random.Range(0,2);
            if (decision == 0) { calor = false; frio = false; }
            if (decision == 1) { calor = true; frio = false; }
            if(decision == 2) { calor = false; frio = true; }
        }
    }
}
