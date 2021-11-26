using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public bool firstYear = true;
    public bool spring = false;
    public bool summer = false;
    public bool autumn = false;
    public bool calor = false;
    public bool frio = false;

    public float tiempoPartida = 360;
    public float probBaseEvento = 10f;
    public float probActualEvento;
    public float tiempoComprobarEvento = 5f;
    public float tiempoAseguradoSinEventosAlPrincipio = 15f;
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
        tmp = tiempoAseguradoSinEventosAlPrincipio;
        probActualEvento = probBaseEvento;
    }

    // Update is called once per frame
    void Update()
    {
        bool sequiainicio = sequia;
        bool lluviainicio = lluvia;
        GetComponent<SeasonVisuales>().seasonValue = Mathf.Lerp(0, 1, Time.realtimeSinceStartup / tiempoPartida);
        if (Time.realtimeSinceStartup < tiempoPartida / 3)
        {
            if (spring == false)
            {
                spring = true;
                GameManager.Instance.SetSeason("spring");
                summer = false;
                autumn = false;
                GameManager.Instance.SetFeedBack("Spring started", 2f);
            }

        }
        if (Time.realtimeSinceStartup >= tiempoPartida / 3 && Time.realtimeSinceStartup < (tiempoPartida * 2) / 3)
        {
            if (summer == false)
            {
                spring = false;
                summer = true;
                GameManager.Instance.SetSeason("summer");

                autumn = false;
                GameManager.Instance.SetFeedBack("Summer started", "");
            }
        }
        if (Time.realtimeSinceStartup >= (tiempoPartida * 2) / 3 && Time.realtimeSinceStartup < tiempoPartida)
        {
            if (autumn == false)
            {
                spring = false; summer = false;
                GameManager.Instance.SetSeason("autumn");
                autumn = true;
                GameManager.Instance.SetFeedBack("Autumn started", "");
            }

        }
        if (Time.realtimeSinceStartup >= (tiempoPartida * 2.75f) / 3 && Time.realtimeSinceStartup < tiempoPartida)
        {
            GameManager.Instance.SetFeedBack("Winter is coming soon", "");
        }
        if (Time.timeScale == 1) if (Time.realtimeSinceStartup >= tiempoPartida) { Time.timeScale = 0; GameManager.Instance.OpenCloseInviernoMenu(); }
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
            if (tmp <= 0)
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


        if (lluvia == true && lluviainicio == false)
        {
            GameManager.Instance.SetEvento("lluvia");
            GameManager.Instance.SetFeedBack("It started to rain...", "");
            //Datos
        }
        else if (lluvia == false && lluviainicio == true)
        {
            GameManager.Instance.UnSetEvento();
            GameManager.Instance.SetFeedBack("The rain is ending...", "");
        }

        if (sequia == true && sequiainicio == false)
        {
            GameManager.Instance.SetEvento("sequia");
            GameManager.Instance.SetFeedBack("A drought has started", "");
            //Datos
        }
        else if (sequia == false && sequiainicio == true)
        {
            GameManager.Instance.UnSetEvento();
            GameManager.Instance.SetFeedBack("The drought is ending", "");
        }

    }

    void DecidirCondiciones()
    {
        if (GameManager.Instance.currentYear == 0 && FindObjectOfType<DataAscension>() == null)
        {
            calor = false;
            frio = false;
            
            GameManager.Instance.SetFeedBack("This year will have normal climate", "");
        }
        else
        {
            int decision = Random.Range(0, 2);
            if (decision == 0) { calor = false; frio = false;
                GameManager.Instance.SetFeedBack("This year will have normal climate", "");
            }
            if (decision == 1) { calor = true; frio = false;
                GameManager.Instance.SetFeedBack("This year will be hotter than usual", "");
            }
            if (decision == 2) { calor = false; frio = true;
                GameManager.Instance.SetFeedBack("This year it will be rainier than usual", "");
            }
        }
    }
}
