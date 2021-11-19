using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    Flor flor;
    public GameObject[] hormiguerosDesactivados;
    public GameObject[] manzanosDesactivados;


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
        flor = FindObjectOfType<FlorHub>().gameObject.GetComponentInChildren<Flor>();

        mejora1HCosteActual = mejora1HCosteBase;
        mejora1MCosteActual = mejora1MCosteBase;
        mejora1PCosteActual = mejora1PCosteBase;
        mejora1FCosteActual = mejora1FCosteBase;
        mejora2HCosteActual = mejora2HCosteBase;
        mejora2MCosteActual = mejora2MCosteBase;
        mejora2PCosteActual = mejora2PCosteBase;
        mejora2FCosteActual = mejora2FCosteBase;
        mejora3HCosteActual = mejora3HCosteBase;
        mejora3MCosteActual = mejora3MCosteBase;
        mejora3PCosteActual = mejora3PCosteBase;
        mejora3FCosteActual = mejora3FCosteBase;



    }
    void Start()
    {

        CalcularComienzo();
    }
    void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            totalManzanas += 20;

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            totalMiel += 20;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    void Update()
    {
        Cheats();
        //totalManzanas += totalManzanasPorSegundo * Time.deltaTime;
        //if (totalManzanas > 10 && !desbloqueadosManzanos)
        //{
        //    desbloqueadosManzanos = true;
        //    VisiblesLista++;
        //}
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
     
        TextoManzanas.text = (string)("Apples:" + totalManzanas.ToString("0"));
        TextoManzanasPorSegundo.text = (string)("Honey:" + totalMiel.ToString("0"));
        //TextoManzanasGeneradas.text = (string)("Apples every:" + tiempoGeneracionManzana + "s");
        //TextoCantidadHormigas.text = (string)("Max Ant Capacity:" + capacidadTotalHormigas);


    }
    public void CogerHormiga()
    {
        if (hormigasTotal + 1 <= capacidadTotalHormigas)
        {
            hormigasTotal++;
            if (hormigasTotal == capacidadPorHormigueroActual && desbloqueadosHormigueros == false)
            {
                desbloqueadosHormigueros = true;

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
    public float abejasTotal = 0;
    public float abejasFuera = 0;
    public float maxAbejasFuera = 3;
    public GameObject prefabAbeja;
    public Panal panal;
    public void CogerAbeja()
    {
        if (abejasTotal + 1 <= capacidadTotalAbejas)
        {
            abejasTotal++;
            if (abejasTotal == capacidadAbejasPorPanal && desbloqueadosPanales == false)
            {
                desbloqueadosPanales = true;

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
    //public void TextoBotones()
    //{
    //    for (int i = 0; i < listaBotones.Count; i++)
    //    {

    //        if (i > visiblesLista + 2)
    //        {

    //            listaBotones[i].SetActive(false);
    //            print(listaBotones[i].name);
    //            print(listaBotones[i].activeSelf);
    //        }
    //        else if (i > visiblesLista)
    //        {
    //            listaBotones[i].SetActive(true);
    //            listaBotones[i].GetComponentInChildren<Text>().text = textoBloq;
    //        }
    //    }
    //    //if (desbloqueadosHormigueros)
    //    //{
    //    //    GameObject.Find("BuyAnthill").GetComponentInChildren<Text>().text = (string)("Buy Anthill " + costeHormigueroActual);
    //    //}

    //    //if (desbloqueadosManzanos)
    //    //{
    //    //    GameObject.Find("BuyAppleTree").GetComponentInChildren<Text>().text = (string)("Buy Apple tree " + costeManzanoActual);
    //    //}
    //    //if (desbloqueadosPanales)
    //    //{
    //    //    GameObject.Find("BuyPanal").GetComponentInChildren<Text>().text = (string)("Buy beehive " + costePanalActual);
    //    //}
    //    //if (desbloqueadasFlores)
    //    //{
    //    //    GameObject.Find("BuyFlores").GetComponentInChildren<Text>().text = (string)("Buy flowers " + costeFloresBase);
    //    //}

    //}

    // MEJORAS 
    public bool desbloqueadosHormigueros = true;
    public bool desbloqueadosManzanos = false;
    public bool desbloqueadosPanales = false;
    public bool desbloqueadasFlores = false;
    public string textoBloq = "Blocked";
    public float costeHormigueroBase = 50;
    public float costeHormigueroActual;
    public float ratioCrecimientoHormiguero = 1.1f;
    //public void ComprarHormiguero()
    //{
    //    if (totalManzanas > costeHormigueroActual/* && desbloqueadosHormigueros*/)
    //    {
    //        totalManzanas -= costeHormigueroActual;
    //        costeHormigueroActual = costeHormigueroBase * Mathf.Pow(ratioCrecimientoHormiguero, hormiguerosTotal);

    //        if (hormiguerosTotal - hormiguerosDesactivados.Length < 0 && hormiguerosDesactivados[hormiguerosTotal] != null)
    //        {
    //            hormiguerosDesactivados[hormiguerosTotal - 1].gameObject.SetActive(true);
    //        }
    //        hormiguerosTotal += 1;
    //    }
    //    ActualizarTextoHormigas();

    //}
    public float costeManzanoBase = 70;
    public float costeManzanoActual;
    public float ratioCrecimientoManzano = 1.17f;
    //public void ComprarManzano()
    //{
    //    if (totalManzanas > costeManzanoActual && desbloqueadosManzanos)
    //    {
    //        totalManzanas -= costeManzanoActual;

    //        costeManzanoActual = costeManzanoBase * Mathf.Pow(ratioCrecimientoManzano, totalManzanos);

    //        if ((int)totalManzanos - manzanosDesactivados.Length < 0 && manzanosDesactivados[(int)totalManzanos] != null)
    //        {
    //            manzanosDesactivados[(int)totalManzanos - 1].gameObject.SetActive(true);
    //        }
    //        totalManzanos += 1;
    //    }

    //}

    public float totalMiel;
    public float costePanalBase = 50;
    public float costePanalActual;
    public float ratioCrecimientoPanal = 1.15f;
    public int panalesTotal = 1;
    public int capacidadTotalAbejas = 0;
    public int capacidadAbejasPorPanal = 20;
    public GameObject[] panalesDesactivados;
    //public void ComprarPanal()
    //{
    //    if (totalMiel > costePanalActual && desbloqueadosPanales)
    //    {
    //        totalMiel -= costePanalActual;
    //        costePanalActual = costePanalBase * Mathf.Pow(ratioCrecimientoPanal, panalesTotal);

    //        if (panalesTotal - panalesDesactivados.Length < 0 && panalesDesactivados[panalesTotal] != null)
    //        {
    //            panalesDesactivados[panalesTotal ].gameObject.SetActive(true);
    //        }
    //        panalesTotal += 1;
    //    }
    //    ActualizarTextoAbejas();

    //}
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
    //public void ComprarFlores()
    //{
    //    if (totalMiel > costeFloresActual && desbloqueadasFlores)
    //    {
    //        totalMiel -= costeFloresActual;

    //        costeFloresActual = costeFloresBase * Mathf.Pow(ratioCrecimientoFlores, totalFlores);

    //        if ((int)totalFlores - floresDesactivados.Length < 0 && floresDesactivados[(int)totalFlores] != null)
    //        {
    //            floresDesactivados[(int)totalFlores ].gameObject.SetActive(true);
    //        }
    //        totalFlores += 1;
    //    }

    //}
    public bool desbloqueadasAbejas = false;
    public void DesbloquearAbejas()
    {
        desbloqueadasAbejas = true;
    }


    public GameObject menuBlock;
    public GameObject menuCompras;
    public Text descripcion;
    public Text nombre;
    public Text nombreBlocked;


    public string descripcionHormiguero;
    public string descripcionManzano;
    public string descripcionPanal;
    public string descripcionFlores;
    public string nombreBlock = "Blocked";
    public string botonBlockText = "Blocked x money";
    public string blockHormiguero = "Blocked x money";
    public string blockManzano = "Blocked x money";
    public string blockPanal = "Blocked x money";
    public string blockFlores = "Blocked x money";
    public float costeDesbloqueoHormiguero;
    public float costeDesbloqueoManzano;
    public float costeDesbloqueoPanal;
    public float costeDesbloqueoFlores;

    public GameObject botonBlock;
    public GameObject botonMejora1;
    public GameObject botonMejora2;
    public GameObject botonMejora3;
    public GameObject textoInfo1;
    public GameObject textoInfo2;
    public GameObject textoInfo3;
    public GameObject textoInfo4;

    public string tipoOpen;
    public float mejorasTotalesH = 0;
    public string nombreMejora1Hormiguero = "Anthill";
    public string descripcionMejora1Hormiguero = "Max capacity increased";
    public float mejora1HCosteBase = 10;
    public float mejora1HCosteActual;
    public float mejora1HRatio = 1.1f;
    public float mejora1HnivelActual = 1;
    public float cantidadSumadaMejora1H = 15;

    public string nombreMejora2Hormiguero = "Speed";
    public string descripcionMejora2Hormiguero = "Ants speed increased";
    public float mejora2HCosteBase = 10;
    public float mejora2HCosteActual;
    public float mejora2HRatio = 1.1f;
    public float mejora2HnivelActual = 1;
    public float cantidadSumadaMejora2HPorcentaje = 2;

    public string nombreMejora3Hormiguero = "Blocked";
    public string descripcionMejora3Hormiguero = "Blocked";
    public float mejora3HCosteBase = 10;
    public float mejora3HCosteActual;
    public float mejora3HRatio = 1.1f;
    public float mejora3HnivelActual = 1;
    public float cantidadSumadaMejora3H = 0;

    public float mejorastotalesM = 0;
    public string nombreMejora1Manzano = "Apple tree";
    public string descripcionMejora1Manzano = "Apples generated increased";
    public float mejora1MCosteBase = 10;
    public float mejora1MCosteActual;
    public float mejora1MRatio = 1.1f;
    public float mejora1MnivelActual = 1;
    public float cantidadSumadaMejora1M = 1;

    public string nombreMejora2Manzano = "Amount";
    public string descripcionMejora2Manzano = "Apples given per unit increased";
    public float mejora2MCosteBase = 10;
    public float mejora2MCosteActual;
    public float mejora2MRatio = 1.1f;
    public float mejora2MnivelActual = 0;
    public float cantidadSumadaMejora2M = 1;

    public string nombreMejora3Manzano = "Generation";
    public string descripcionMejora3Manzano = "Generating time decreased";
    public float mejora3MCosteBase = 10;
    public float mejora3MCosteActual;
    public float mejora3MRatio = 1.1f;
    public float mejora3MnivelActual = 1;
    public float cantidadSumadaMejora3MPorcentaje = 3;

    public float mejorasTotalesP = 0;
    public string nombreMejora1Panal = "Beehive";
    public string descripcionMejora1Panal = "Max capacity increased";
    public float mejora1PCosteBase = 10;
    public float mejora1PCosteActual;
    public float mejora1PRatio = 1.1f;
    public float mejora1PnivelActual = 1;
    public float cantidadSumadaMejora1P = 15;

    public string nombreMejora2Panal = "Rest";
    public string descripcionMejora2Panal = "Bee rest time decreased";
    public float mejora2PCosteBase = 10;
    public float mejora2PCosteActual;
    public float mejora2PRatio = 1.1f;
    public float mejora2PnivelActual = 1;
    public float cantidadSumadaMejora2PPorcentaje = 5;

    public string nombreMejora3Panal = "Blocked";
    public string descripcionMejora3Panal = "Blocked";
    public float mejora3PCosteBase = 10;
    public float mejora3PCosteActual;
    public float mejora3PRatio = 1.1f;
    public float mejora3PnivelActual = 1;
    public float cantidadSumadaMejora3P = 0;

    public float mejorasTotalesF = 0;
    public string nombreMejora1Flores = "Flowers quality";
    public string descripcionMejora1Flores = "Bees speed increased";
    public float mejora1FCosteBase = 10;
    public float mejora1FCosteActual;
    public float mejora1FRatio = 1.1f;
    public float mejora1FnivelActual = 1;
    public float cantidadSumadaMejora1FPorcentaje = 2;

    public string nombreMejora2Flores = "Pollination";
    public string descripcionMejora2Flores = "Bee pollination time decreased";
    public float mejora2FCosteBase = 10;
    public float mejora2FCosteActual;
    public float mejora2FRatio = 1.1f;
    public float mejora2FnivelActual = 1;
    public float cantidadSumadaMejora2FPorcentaje = 3;

    public string nombreMejora3Flores = "Honey";
    public string descripcionMejora3Flores = "Honey per pollination increased";
    public float mejora3FCosteBase = 10;
    public float mejora3FCosteActual;
    public float mejora3FRatio = 1.1f;
    public float mejora3FnivelActual = 1;
    public float cantidadSumadaMejora3F = 1;

  

    public void MenuOpen(GameObject go)
    {
        MenuClose();
        if (go.GetComponent<Panal>())
        {
            if (GameManager.Instance.desbloqueadosPanales)
            {
                OpenCompras("Panal");
            }
            else
            {
                OpenBlock("Panal");
            }
        }
        else if (go.GetComponent<FlorHub>())
        {
            if (GameManager.Instance.desbloqueadasFlores)
            {
                OpenCompras("Flor");
            }
            else
            {
                OpenBlock("Flor");
            }
        }
        else if (go.GetComponent<Hormiguero>())
        {
            if (GameManager.Instance.desbloqueadosHormigueros)
            {
                OpenCompras("Hormiguero");
            }
            else
            {
                OpenBlock("Hormiguero");
            }
        }
        else if (go.GetComponent<Manzano>())
        {
            if (GameManager.Instance.desbloqueadosManzanos)
            {
                OpenCompras("Manzano");
            }
            else
            {
                OpenBlock("Manzano");
            }
        }

    }
    public void OpenBlock(string tipo)
    {
        menuBlock.SetActive(true);
        tipoOpen = tipo;
        if (tipo == "Hormiguero")
        {
            //nada
        }
        else if (tipo == "Manzano")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockManzano + " " + costeDesbloqueoManzano + " Apples";
        }
        else if (tipo == "Panal")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockPanal + " " + costeDesbloqueoPanal + " Apples";
        }
        else if (tipo == "Flor")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockFlores + " " + costeDesbloqueoFlores + " Honey";
        }
    }
    public void OpenCompras(string tipo)
    {
        menuCompras.SetActive(true);
        tipoOpen = tipo;
        if (tipo == "Hormiguero")
        {
            nombre.text = "Anthill";
            descripcion.text = descripcionHormiguero;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1Hormiguero;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2Hormiguero;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3Hormiguero;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesH;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalHormigas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipo == "Manzano")
        {
            nombre.text = "Apple Tree";
            descripcion.text = descripcionManzano;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1Manzano;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2Manzano;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3Manzano;

            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorastotalesM;
            textoInfo2.GetComponent<Text>().text = "Apples spawned = " + manzano.manzanasSpawned;
            textoInfo3.GetComponent<Text>().text = "Time between generations = " + manzano.TiempoEntreManzanas;
            textoInfo4.GetComponent<Text>().text = "Apples given per unit = " + manzanasSumadasExpedicion;
        }
        else if (tipo == "Panal")
        {
            nombre.text = "Beehive";
            descripcion.text = descripcionPanal;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1Panal;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2Panal;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3Panal;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesP;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalAbejas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipo == "Flor")
        {
            nombre.text = "Flowers";
            descripcion.text = descripcionFlores;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1Flores;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2Flores;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3Flores;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesF;
            textoInfo2.GetComponent<Text>().text = "Speed boost = " + flor.totalSpeedBost;
            textoInfo3.GetComponent<Text>().text = "Pollination speed boost = " + flor.velocidadPolinizacion;
            textoInfo4.GetComponent<Text>().text = "Honey given per pollination = " + flor.mielPorViaje;
        }
    }
    public void SetDescripcionMejora1()
    {

        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionMejora1Hormiguero;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1HCosteActual + " Apples";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1HnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1H;
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora1Manzano;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1MCosteActual + " Apples";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1MnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1M;
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora1Panal;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1PCosteActual + " Honey";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1PnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1P;
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora1Flores;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1FCosteActual + " Honey";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1FnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1FPorcentaje + " %";
            textoInfo4.GetComponent<Text>().text = "";
        }

    }
    public void SetDescripcionMejora2()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionMejora2Hormiguero;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2HCosteActual + " Apples";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2HnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2HPorcentaje + "%";
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora2Manzano;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2MCosteActual + " Apples";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2MnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2M;
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora2Panal;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2PCosteActual + " Honey";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2PnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2PPorcentaje + " %";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora2Flores;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2FCosteActual + " Honey";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2FnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2FPorcentaje + " %";
            textoInfo4.GetComponent<Text>().text = "";
        }
    }
    public void SetDescripcionMejora3()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionMejora3Hormiguero;
            //textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3HCosteActual + " Apples";
            //textoInfo2.GetComponent<Text>().text = "Level = " + mejora3HnivelActual;
            //textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3H;
            //textoInfo4.GetComponent<Text>().text = "";
            textoInfo1.GetComponent<Text>().text = "";
            textoInfo2.GetComponent<Text>().text = "";
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora3Manzano;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3MCosteActual + " Apples";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora3MnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3MPorcentaje + " %";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora3Panal;
            //textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3PCosteActual + " Honey";
            //textoInfo2.GetComponent<Text>().text = "Level = " + mejora3PnivelActual;
            //textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3P;
            //textoInfo4.GetComponent<Text>().text = "";
            textoInfo1.GetComponent<Text>().text = "";
            textoInfo2.GetComponent<Text>().text = "";
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora3Flores;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3FCosteActual + " Honey";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora3FnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3F;
            textoInfo4.GetComponent<Text>().text = "";
        }
    }
    public void DescripcionOriginal()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionHormiguero;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesH;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalHormigas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionManzano;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorastotalesM;
            textoInfo2.GetComponent<Text>().text = "Apples spawned = " + manzano.manzanasSpawned;
            textoInfo3.GetComponent<Text>().text = "Time between generations = " + manzano.TiempoEntreManzanas;
            textoInfo4.GetComponent<Text>().text = "Apples given per unit = " + manzanasSumadasExpedicion;

        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionPanal;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesP;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalAbejas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionFlores;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + mejorasTotalesF;
            textoInfo2.GetComponent<Text>().text = "Speed boost = " + cantidadSumadaMejora1FPorcentaje*mejora1FnivelActual+ "%";
            textoInfo3.GetComponent<Text>().text = "Pollination speed boost = " + cantidadSumadaMejora2FPorcentaje*mejora2FnivelActual+ "%";
           
            textoInfo4.GetComponent<Text>().text = "Honey given per pollination = " + mielSumadaExpedicion;
        }
    }
    public void ClickBotonBlock()
    {
        print("clickbotonblock" + tipoOpen);
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {
            //nada
        }
        else if (tipo == "Manzano")
        {
            if (totalManzanas >= costeDesbloqueoManzano)
            {
                totalManzanas -= costeDesbloqueoManzano;
                desbloqueadosManzanos = true;
                MenuClose();
                OpenCompras("Manzano");
            }


        }
        else if (tipo == "Panal")
        {
            if (totalManzanas >= costeDesbloqueoPanal)
            {
                totalManzanas -= costeDesbloqueoPanal;
                desbloqueadasAbejas = true;
                desbloqueadosPanales = true;
                MenuClose();
                OpenCompras("Panal");
            }
        }
        else if (tipo == "Flor")
        {
            if (totalMiel >= costeDesbloqueoFlores)
            {
                totalMiel -= costeDesbloqueoFlores;
                desbloqueadasFlores = true;

                MenuClose();
                OpenCompras("Flor");
            }
        }
    }
    public void ClickBotonMejora1()
    {
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {
            if (totalManzanas >= mejora1HCosteActual)
            {
                mejora1HnivelActual++;
                mejorasTotalesH++;
                totalManzanas -= mejora1HCosteActual;
                mejora1HCosteActual = (int)mejora1HCosteBase * Mathf.Pow(mejora1HRatio, mejora1HnivelActual);
                hormiguerosTotal++;
                SetDescripcionMejora1();
                ActualizarTextoHormigas();
            }


        }
        else if (tipo == "Manzano")
        {
            if (totalManzanas >= mejora1MCosteActual)
            {
                mejora1MnivelActual++;
                mejorastotalesM++;
                totalManzanas -= mejora1MCosteActual;
                mejora1MCosteActual = (int)(mejora1MCosteBase * Mathf.Pow(mejora1MRatio, mejora1MnivelActual));
               
                SetDescripcionMejora1();

                manzano.manzanasSpawned++;
            }
        }
        else if (tipo == "Panal")
        {
            if (totalMiel >= mejora1PCosteActual)
            {
                mejora1PnivelActual++;
                mejorasTotalesP++;
                totalMiel -= mejora1PCosteActual;
                mejora1PCosteActual = (int)(mejora1PCosteBase * Mathf.Pow(mejora1PRatio, mejora1PnivelActual));
                panalesTotal++;
                SetDescripcionMejora1();
                ActualizarTextoAbejas();
            }
        }
        else if (tipo == "Flor")
        {
            if (totalMiel >= mejora1FCosteActual)
            {
                mejora1FnivelActual++;
                mejorasTotalesF++;
                totalMiel -= mejora1FCosteActual;
                mejora1FCosteActual = (int)mejora1FCosteBase * Mathf.Pow(mejora1FRatio, mejora1FnivelActual);
                panal.speedMission += panal.speedMission * cantidadSumadaMejora1FPorcentaje / 100;
                SetDescripcionMejora1();
            }
        }
    }
    public void ClickBotonMejora2()
    {
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {
            if (totalManzanas >= mejora2HCosteActual)
            {
                mejora2HnivelActual++;
                mejorasTotalesH++;
                totalManzanas -= mejora2HCosteActual;
                mejora2HCosteActual = (int)mejora2HCosteBase * Mathf.Pow(mejora2HRatio, mejora2HnivelActual);
               hormiguero.speedMission+= hormiguero.speedMission * cantidadSumadaMejora2HPorcentaje / 100;
                SetDescripcionMejora2();
                ActualizarTextoHormigas();
            }


        }
        else if (tipo == "Manzano")
        {
            if (totalManzanas >= mejora2MCosteActual)
            {
                mejora2MnivelActual++;
                mejorastotalesM++;
                totalManzanas -= mejora2MCosteActual;
                mejora2MCosteActual = (int)mejora2MCosteBase * Mathf.Pow(mejora2MRatio, mejora2MnivelActual);
                manzanasSumadasExpedicion++;
                SetDescripcionMejora2();

                manzano.manzanasSpawned++;
            }
        }
        else if (tipo == "Panal")
        {
            if (totalMiel >= mejora2PCosteActual)
            {
                mejora2PnivelActual++;
                mejorasTotalesP++;
                totalMiel -= mejora2PCosteActual;
                mejora2PCosteActual = (int)mejora2PCosteBase * Mathf.Pow(mejora2PRatio, mejora2PnivelActual);
                if (panal.tiempoDescanso > 0.5f) panal.tiempoDescanso -= panal.tiempoDescanso * cantidadSumadaMejora2PPorcentaje / 100;
                SetDescripcionMejora2();
                ActualizarTextoAbejas();
            }
        }
        else if (tipo == "Flor")
        {
            if (totalMiel >= mejora2FCosteActual)
            {
                mejora2FnivelActual++;
                mejorasTotalesF++;
                totalMiel -= mejora2FCosteActual;
                mejora2FCosteActual = (int)mejora2FCosteBase * Mathf.Pow(mejora2FRatio, mejora2FnivelActual);
              if(panal.tiempoPolinizando>0.5f)  panal.tiempoPolinizando -= panal.tiempoPolinizando * cantidadSumadaMejora2FPorcentaje / 100;
                SetDescripcionMejora2();
            }
        }
    }
    public void ClickBotonMejora3()
    {
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {
            //if (totalManzanas >= mejora3HCosteActual)
            //{
            //    mejora3HnivelActual++;
            //    mejorasTotalesH++;
            //    totalManzanas -= mejora3HCosteActual;
            //    mejora3HCosteActual = mejora3HCosteBase * Mathf.Pow(mejora3HRatio, mejora3HnivelActual);
            //    hormiguero.speedMission += hormiguero.speedMission * cantidadSumadaMejora3HPorcentaje / 100;
            //    SetDescripcionMejora3();
            //    ActualizarTextoHormigas();
            //}


        }
        else if (tipo == "Manzano")
        {
            if (totalManzanas >= mejora3MCosteActual)
            {
                mejora3MnivelActual++;
                mejorastotalesM++;
                totalManzanas -= mejora3MCosteActual;
                mejora3MCosteActual = (int)mejora3MCosteBase * Mathf.Pow(mejora3MRatio, mejora3MnivelActual);
                manzano.TiempoEntreManzanas -= manzano.TiempoEntreManzanas * cantidadSumadaMejora3MPorcentaje / 100;

                SetDescripcionMejora3();

                manzano.manzanasSpawned++;
            }
        }
        else if (tipo == "Panal")
        {
            //if (totalMiel >= mejora3PCosteActual)
            //{
            //    mejora3PnivelActual++;
            //    mejorasTotalesP++;
            //    totalMiel -= mejora3PCosteActual;
            //    mejora3PCosteActual = mejora3PCosteBase * Mathf.Pow(mejora3PRatio, mejora3PnivelActual);
            //    if (panal.tiempoDescanso > 0.5f) panal.tiempoDescanso -= panal.tiempoDescanso * cantidadSumadaMejora3PPorcentaje / 100;
            //    SetDescripcionMejora3();
            //    ActualizarTextoAbejas();
            //}
        }
        else if (tipo == "Flor")
        {
            if (totalMiel >= mejora3FCosteActual)
            {
                mejora3FnivelActual++;
                mejorasTotalesF++;
                totalMiel -= mejora3FCosteActual;
                mejora3FCosteActual = (int)mejora3FCosteBase * Mathf.Pow(mejora3FRatio, mejora3FnivelActual);
                mielSumadaExpedicion++;
                SetDescripcionMejora3();
            }
        }
    }
    public void MenuClose()
    {
        menuBlock.SetActive(false);
        menuCompras.SetActive(false);
    }
}
