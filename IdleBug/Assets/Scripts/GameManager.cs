using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public int cantidadHormigasCogidas = 1;
    public int cantidadAbejasCogidas = 1;

    public int manzanasSumadasExpedicion = 1;
    public int hormigasTotal = 2;
    public int hormiguerosTotal = 1;
    public int capacidadPorHormigueroBase = 15;
    public int capacidadPorHormigueroActual = 15;
    public int capacidadTotalHormigas;
    public int maxHormigasFuera = 5;
    public int hormigasFuera = 2;
    public GameObject prefabHormiga;
    Hormiguero hormiguero;

    public float manzanasXHormiga = 0.1f;
    public float totalManzanasPorSegundo;
    public float tiempoGeneracionManzana;
    public float totalManzanas;
    public float totalManzanos = 1;
    Manzano manzano;

    public GameObject[] hormiguerosDesactivados;
    public GameObject[] manzanosDesactivados;

    public List<GameObject> listaBotones=new List<GameObject>();
    public int visiblesLista = 0;
    public int VisiblesLista { get => visiblesLista; set { TextoBotones(); visiblesLista = value; } }
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }



    void CalcularComienzo()
    {
        costeHormigueroActual = costeHormigueroBase;
        costeFloresActual = costeFloresBase;
        costePanalActual = costePanalBase;
        costeManzanoActual = costeManzanoBase;
        capacidadTotalHormigas = hormiguerosTotal * capacidadPorHormigueroActual;
        capacidadTotalAbejas = panalesTotal * capacidadAbejasPorPanal;
        abejasFuera = 0;
        hormigasFuera = hormigasTotal;
        hormiguero = GameObject.FindObjectOfType<Hormiguero>();
        panal = GameObject.FindObjectOfType<Panal>();
        ActualizarTextoHormigas();
        TextoHormigas = GameObject.Find("TextoHormigasM").GetComponent<Text>();
        TextoManzanas = GameObject.Find("TextoManzanasM").GetComponent<Text>();
        TextoManzanasPorSegundo = GameObject.Find("TextoManzanasSegundoM").GetComponent<Text>();
        TextoManzanasGeneradas = GameObject.Find("TextoManzanasGeneradasM").GetComponent<Text>();
        TextoCantidadHormigas = GameObject.Find("TextoCapacidadHormigasM").GetComponent<Text>();
        ActualizarTextoHormigas();
        ActualizarTextoAbejas();
        manzano = FindObjectOfType<Manzano>();
        TextoBotones();
    }
    void Start()
    {

        CalcularComienzo();
    }

    void Update()
    {
        totalManzanas += totalManzanasPorSegundo * Time.deltaTime;
        if (totalManzanas > 10 && !desbloqueadoCompraManzano)
        {
            desbloqueadoCompraManzano = true;
            VisiblesLista++;
        }
        ActualizarTextoMenu();
    }

    public Text TextoHormigas;
    public Text TextoManzanas;
    public Text TextoManzanasPorSegundo;
    public Text TextoManzanasGeneradas;
    public Text TextoCantidadHormigas;

    public void ActualizarTextoMenu()
    {
        totalManzanasPorSegundo = hormigasTotal * manzanasXHormiga;
        capacidadTotalHormigas = hormiguerosTotal * capacidadPorHormigueroActual;
        TextoHormigas.text = (string)("Ants:" + hormigasTotal);
        TextoManzanas.text = (string)("Apples:" + totalManzanas);
        TextoManzanasPorSegundo.text = (string)("Apples per sec:" + totalManzanasPorSegundo.ToString("0.00"));
        TextoManzanasGeneradas.text = (string)("Apples every:" + tiempoGeneracionManzana + "s");
        TextoCantidadHormigas.text = (string)("Max Ant Capacity:" + capacidadTotalHormigas);


    }
    public void CogerHormiga()
    {
        if (hormigasTotal + 1 <= capacidadTotalHormigas)
        {
            hormigasTotal++;
            if (hormigasTotal == capacidadPorHormigueroActual && desbloqueadoCompraHormiguero == false)
            {
                desbloqueadoCompraHormiguero = true;
                VisiblesLista++;
            }
            print("Tengo" + hormigasTotal + " hormigas");
            if (hormigasFuera < maxHormigasFuera)
            {
                hormigasFuera++;
                Instantiate(prefabHormiga, GameObject.Find("TextoHormigas").transform.parent.transform.position, Quaternion.identity);
                hormiguero.OcuparHormiga();
            }
            hormiguero.poblacionActual++;
            ActualizarTextoHormigas();
        }

    }
    public float abejasTotal=0;
    public float abejasFuera = 0;
    public float maxAbejasFuera = 3;
    public GameObject prefabAbeja;
    public Panal panal;
    public void CogerAbeja()
    {
        if (abejasTotal + 1 <= capacidadTotalAbejas)
        {
            abejasTotal++;
            if (abejasTotal == capacidadAbejasPorPanal && desbloqueadoCompraPanal == false)
            {
                desbloqueadoCompraPanal = true;
                VisiblesLista++;
            }
            print("Tengo" + abejasTotal + " abejas");
            if (abejasFuera < maxAbejasFuera)
            {
                abejasFuera++;
                Instantiate(prefabAbeja, GameObject.Find("TextoAbejas").transform.parent.transform.position, Quaternion.identity);
                panal.OcuparAbeja();
            }
            panal.poblacionActual++;
            ActualizarTextoAbejas();
        }

    }
    public void SumarManzana(int cantidad)
    {
        totalManzanas += cantidad;
    }
    public void SumarMiel(int cantidad)
    {
        totalMiel += cantidad;
    }
    void ActualizarTextoHormigas()
    {
        capacidadTotalHormigas = capacidadPorHormigueroActual * hormiguerosTotal;
        hormiguero.poblacionActual = hormigasTotal;
        hormiguero.capacidadActual = capacidadTotalHormigas;
        GameObject.Find("TextoHormigas").GetComponent<TextMesh>().text = (string)(hormigasTotal + " / " + capacidadTotalHormigas);

    }
    public void TextoBotones()
    {
        for (int i = 0; i < listaBotones.Count; i++)
        {

            if (i > visiblesLista + 2)
            {

                listaBotones[i].SetActive(false);
                print(listaBotones[i].name);
                print(listaBotones[i].activeSelf);
            }
            else if (i > visiblesLista)
            {
                listaBotones[i].SetActive(true);
                listaBotones[i].GetComponentInChildren<Text>().text = textoBloq;
            }
        }
        if (desbloqueadoCompraHormiguero)
        {
            GameObject.Find("BuyAnthill").GetComponentInChildren<Text>().text = (string)("Buy Anthill " + costeHormigueroActual);
        }

        if (desbloqueadoCompraManzano)
        {
            GameObject.Find("BuyAppleTree").GetComponentInChildren<Text>().text = (string)("Buy Apple tree " + costeManzanoActual);
        }
        if (desbloqueadoCompraPanal)
        {
            GameObject.Find("BuyPanal").GetComponentInChildren<Text>().text = (string)("Buy beehive " + costePanalActual);
        }
        if (desbloqueadoCompraFlores)
        {
            GameObject.Find("BuyFlores").GetComponentInChildren<Text>().text = (string)("Buy flowers " + costeFloresBase);
        }

    }

    // MEJORAS 
    public bool desbloqueadoCompraHormiguero = false;
    public bool desbloqueadoCompraManzano = false;
    public bool desbloqueadoCompraPanal = false;
    public bool desbloqueadoCompraFlores = false;
    public string textoBloq = "Blocked";
    public float costeHormigueroBase = 50;
    public float costeHormigueroActual;
    public float ratioCrecimientoHormiguero = 1.1f;
    public void ComprarHormiguero()
    {
        if (totalManzanas > costeHormigueroActual && desbloqueadoCompraHormiguero)
        {
            totalManzanas -= costeHormigueroActual;
            costeHormigueroActual = costeHormigueroBase * Mathf.Pow(ratioCrecimientoHormiguero, hormiguerosTotal);

            if (hormiguerosTotal - hormiguerosDesactivados.Length < 0 && hormiguerosDesactivados[hormiguerosTotal] != null)
            {
                hormiguerosDesactivados[hormiguerosTotal - 1].gameObject.SetActive(true);
            }
            hormiguerosTotal += 1;
        }
        ActualizarTextoHormigas();
        TextoBotones();
    }
    public float costeManzanoBase = 70;
    public float costeManzanoActual;
    public float ratioCrecimientoManzano = 1.17f;
    public void ComprarManzano()
    {
        if (totalManzanas > costeManzanoActual && desbloqueadoCompraManzano)
        {
            totalManzanas -= costeManzanoActual;

            costeManzanoActual = costeManzanoBase * Mathf.Pow(ratioCrecimientoManzano, totalManzanos);

            if ((int)totalManzanos - manzanosDesactivados.Length < 0 && manzanosDesactivados[(int)totalManzanos] != null)
            {
                manzanosDesactivados[(int)totalManzanos - 1].gameObject.SetActive(true);
            }
            totalManzanos += 1;
        }
        TextoBotones();
    }

    public float totalMiel;
    public float costePanalBase = 50;
    public float costePanalActual;
    public float ratioCrecimientoPanal = 1.15f;
    public int panalesTotal = 1;
    public int capacidadTotalAbejas = 0;
    public int capacidadAbejasPorPanal = 20;
    public GameObject[] panalesDesactivados;
    public void ComprarPanal()
    {
        if (totalMiel > costePanalActual && desbloqueadoCompraPanal)
        {
            totalMiel -= costePanalActual;
            costePanalActual = costePanalBase * Mathf.Pow(ratioCrecimientoPanal, panalesTotal);

            if (panalesTotal - panalesDesactivados.Length < 0 && panalesDesactivados[panalesTotal] != null)
            {
                panalesDesactivados[panalesTotal ].gameObject.SetActive(true);
            }
            panalesTotal += 1;
        }
        ActualizarTextoAbejas();
        TextoBotones();
    }
    void ActualizarTextoAbejas()
    {
        capacidadTotalAbejas = capacidadAbejasPorPanal * panalesTotal;
        panal.poblacionActual = (int)abejasTotal;
        panal.capacidadActual = capacidadTotalAbejas;
        GameObject.Find("TextoAbejas").GetComponent<TextMesh>().text = (string)(abejasTotal + " / " + capacidadTotalAbejas);

    }
    public float costeFloresBase = 70;
    public float costeFloresActual;
    public float ratioCrecimientoFlores = 1.17f;
    public int totalFlores = 0;
    public GameObject[] floresDesactivados;
    public float mielSumadaExpedicion = 1;
    public void ComprarFlores()
    {
        if (totalMiel > costeFloresActual && desbloqueadoCompraFlores)
        {
            totalMiel -= costeFloresActual;

            costeFloresActual = costeFloresBase * Mathf.Pow(ratioCrecimientoFlores, totalFlores);

            if ((int)totalFlores - floresDesactivados.Length < 0 && floresDesactivados[(int)totalFlores] != null)
            {
                floresDesactivados[(int)totalFlores ].gameObject.SetActive(true);
            }
            totalFlores += 1;
        }
        TextoBotones();
    }
    public bool desbloqueadasAbejas = false;
    public void DesbloquearAbejas()
    {
        desbloqueadasAbejas = true;
    }
}
