using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour
{
    public bool firstYear = true;
    public bool spring = false;
    public bool summer = false;
    public bool autumn = false;
    public bool calor = false;
    public bool frio = false;
    public Text mensajeYear;
    public Text mensajeSeason;
    public Text mensajeSeason2;
    public Text mensajeEventos;
    public Text mensajeEventos2;

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

    public float tiempo = 0;
    public GameObject flecha;
    public float rotInicial = -10;
    public float rotacionFinal = -165;
    public float rotacionActual;
    public bool started = false;

    // Start is called before the first frame update
    void Awake()
    {  mensajeYear = GameObject.Find("MensajeYear").GetComponent<Text>();
        mensajeSeason = GameObject.Find("MensajeSeason").GetComponent<Text>();
        mensajeSeason2 = GameObject.Find("MensajeSeason2").GetComponent<Text>();
        mensajeEventos = GameObject.Find("MensajeEvento").GetComponent<Text>();
        mensajeEventos2 = GameObject.Find("MensajeEvento2").GetComponent<Text>();
        mensajeEventos.text = " ";

        mensajeEventos2.text = "";
        mensajeSeason.text = " ";
        mensajeYear.text = "";
        mensajeSeason.text = "";
        //firstYear = true;
        DecidirCondiciones();
        tmp = tiempoAseguradoSinEventosAlPrincipio;
        actualTiempoEntreSonidos = tiempoAseguradoSinEventosAlPrincipio;
        probActualEvento = probBaseEvento;
    }
    private void Start()
    {
      

        flecha = GameObject.Find("FlechaRotar").gameObject;
        rotacionActual = rotInicial;
        flecha.transform.rotation = Quaternion.Euler(new Vector3(flecha.transform.rotation.x, flecha.transform.rotation.y, rotInicial));

        tiempo = 0;
    }
    public float tiempoEntreSonidosFondoBase = 3;
    public float actualTiempoEntreSonidos;
    public void DecidirSonido()
    {
        int rand = Random.Range(0, 4);
        if (rand == 0)
        {
            SonidoManager.Instance.Play("SonidoAleatorio1");
        }
        if (rand == 1)
        {
            SonidoManager.Instance.Play("SonidoAleatorio1");
        }
        if (rand == 2)
        {
            SonidoManager.Instance.Play("SonidoAleatorio3");
        }
        if (rand == 3)
        {
            SonidoManager.Instance.Play("SonidoAleatorio4");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            rotacionActual = Mathf.Lerp(rotInicial, rotacionFinal, tiempo / tiempoPartida);
            flecha.transform.rotation = Quaternion.Euler(new Vector3(flecha.transform.rotation.x, flecha.transform.rotation.y, rotacionActual));
            if (SonidoManager.Instance.IsPlaying("SonidoAleatorio1") || SonidoManager.Instance.IsPlaying("SonidoAleatorio2") || SonidoManager.Instance.IsPlaying("SonidoAleatorio3") || SonidoManager.Instance.IsPlaying("SonidoAleatorio4"))
            {

            }
            else
            {
                if (actualTiempoEntreSonidos == 0)
                {
                    actualTiempoEntreSonidos = tiempoEntreSonidosFondoBase;
                }
                if (actualTiempoEntreSonidos > 0)
                {
                    actualTiempoEntreSonidos -= Time.deltaTime;
                    if (actualTiempoEntreSonidos <= 0)
                    {
                        DecidirSonido();
                        actualTiempoEntreSonidos = 0;
                    }
                }
            }
            tiempo += Time.deltaTime;
            bool sequiainicio = sequia;
            bool lluviainicio = lluvia;
            GetComponent<SeasonVisuales>().seasonValue = Mathf.Lerp(0, 1, tiempo / tiempoPartida);
            if (tiempo < tiempoPartida / 3)
            {
                if (spring == false)
                {
                    mensajeSeason2.text = "";
                    spring = true;
                    GameManager.Instance.SetSeason("spring");
                    summer = false;
                    autumn = false;
                    GameManager.Instance.SetFeedBack("Spring started", 2f);

                    mensajeSeason.text = "Spring: +" + GameManager.Instance.tGenManzPrimaveraPorcentaje + "% apple generation";



                }

            }
            if (tiempo >= tiempoPartida / 3 && tiempo < (tiempoPartida * 2) / 3)
            {
                if (summer == false)
                {
                    spring = false;
                    summer = true;
                    GameManager.Instance.SetSeason("summer");

                    autumn = false;
                    GameManager.Instance.SetFeedBack("Summer started", "");
                    if (GameManager.Instance.desbloqueadosPanales)
                    {
                        mensajeSeason.text = "Summer: -" + GameManager.Instance.tPolinVeranoPorcentaje + "% pollination time";
                    }
                    else
                    {
                        mensajeSeason.text = "";
                    }

                    mensajeSeason2.text = "";

                }
            }
            if (tiempo >= (tiempoPartida * 2) / 3 && tiempo < tiempoPartida)
            {
                if (autumn == false)
                {
                    spring = false; summer = false;
                    GameManager.Instance.SetSeason("autumn");
                    autumn = true;
                    GameManager.Instance.SetFeedBack("Autumn started", "");

                    mensajeSeason.text = "Autumn: -" + GameManager.Instance.tMenosGenManzOtonoPorcentaje + "% apple generation";

                    if (GameManager.Instance.desbloqueadosPanales)
                    {
                        mensajeSeason2.text = "Autumn: +" + GameManager.Instance.tMenosPolinOtonoPorcentaje + "% pollination time";
                    }
                    else
                    {
                        mensajeSeason2.text = "";
                    }

                }

            }
            if (tiempo >= (tiempoPartida * 2.75f) / 3 && tiempo < tiempoPartida)
            {
                GameManager.Instance.SetFeedBack("Winter is coming soon", "");
            }
            if (Time.timeScale == 1) if (tiempo >= tiempoPartida) { Time.timeScale = 0; GameManager.Instance.OpenCloseInviernoMenu(); }
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
                SonidoManager.Instance.Play("LLuvia");
                mensajeEventos.text = "Rain: -" + GameManager.Instance.reduccionVelocidadHormLLuvia + "% ant speed";
                if (GameManager.Instance.desbloqueadosGusanos)
                {
                    mensajeEventos2.text = "Rain: -" + GameManager.Instance.reduccionProdGusanLLuvia + "% worm production";
                }
                else
                {
                    mensajeEventos2.text = "";
                }

                //Datos
            }
            else if (lluvia == false && lluviainicio == true)
            {
                GameManager.Instance.UnSetEvento();
                GameManager.Instance.SetFeedBack("The rain is ending...", "");
                SonidoManager.Instance.Stop("LLuvia");
                mensajeEventos.text = " ";

                mensajeEventos2.text = "";
            }

            if (sequia == true && sequiainicio == false)
            {
                GameManager.Instance.SetEvento("sequia");
                GameManager.Instance.SetFeedBack("A drought has started", "");
                SonidoManager.Instance.Play("Sequia");

                if (GameManager.Instance.desbloqueadosPanales)
                {
                    mensajeEventos.text = "Drought: -" + GameManager.Instance.aumentoDescansoAbejasSequia + "% bee rest time";
                }
                else
                {
                    mensajeEventos.text = "";
                }
                if (GameManager.Instance.desbloqueadasMariposas)
                {
                    mensajeEventos2.text = "Drought: -" + GameManager.Instance.reduccionProdMaripSequia + "% butterfly production";
                }
                else
                {
                    mensajeEventos2.text = "";
                }
                //Datos
            }
            else if (sequia == false && sequiainicio == true)
            {
                GameManager.Instance.UnSetEvento();
                GameManager.Instance.SetFeedBack("The drought is ending", "");
                SonidoManager.Instance.Stop("Sequia");
                mensajeEventos.text = "";
                mensajeEventos2.text = "";
            }
        }
       

    }

    void DecidirCondiciones()
    {
        if (GameManager.Instance.currentYear == 0 && FindObjectOfType<DataAscension>() == null)
        {
            calor = false;
            frio = false;

            GameManager.Instance.SetFeedBack("This year will have normal climate", "");
            mensajeYear.text = "Standard year";
        }
        else
        {
            int decision = Random.Range(0, 2);
            if (decision == 0)
            {
                calor = false; frio = false;
                GameManager.Instance.SetFeedBack("This year will have normal climate", ""); mensajeYear.text = "Standard year";

            }
            if (decision == 1)
            {
                calor = true; frio = false;
                GameManager.Instance.SetFeedBack("This year will be hotter than usual", ""); mensajeYear.text = "Hot year";

            }
            if (decision == 2)
            {
                calor = false; frio = true;
                GameManager.Instance.SetFeedBack("This year it will be rainier than usual", ""); mensajeYear.text = "Cold year";

            }
        }
    }
}
