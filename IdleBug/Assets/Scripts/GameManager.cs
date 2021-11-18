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
      
    }
    void Start()
    {

        CalcularComienzo();
    }

    void Update()
    {
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
    public void ComprarHormiguero()
    {
        if (totalManzanas > costeHormigueroActual/* && desbloqueadosHormigueros*/)
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
       
    }
    public float costeManzanoBase = 70;
    public float costeManzanoActual;
    public float ratioCrecimientoManzano = 1.17f;
    public void ComprarManzano()
    {
        if (totalManzanas > costeManzanoActual && desbloqueadosManzanos)
        {
            totalManzanas -= costeManzanoActual;

            costeManzanoActual = costeManzanoBase * Mathf.Pow(ratioCrecimientoManzano, totalManzanos);

            if ((int)totalManzanos - manzanosDesactivados.Length < 0 && manzanosDesactivados[(int)totalManzanos] != null)
            {
                manzanosDesactivados[(int)totalManzanos - 1].gameObject.SetActive(true);
            }
            totalManzanos += 1;
        }
      
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
        if (totalMiel > costePanalActual && desbloqueadosPanales)
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
        if (totalMiel > costeFloresActual && desbloqueadasFlores)
        {
            totalMiel -= costeFloresActual;

            costeFloresActual = costeFloresBase * Mathf.Pow(ratioCrecimientoFlores, totalFlores);

            if ((int)totalFlores - floresDesactivados.Length < 0 && floresDesactivados[(int)totalFlores] != null)
            {
                floresDesactivados[(int)totalFlores ].gameObject.SetActive(true);
            }
            totalFlores += 1;
        }
       
    }
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
    public string nombreMejora1Hormiguero="Anthill";
    public string descripcionMejora1Hormiguero="Max capacity increased";
    public float mejora1HCosteBase = 10;
    public float mejora1HCosteActual;
    public float mejora1HRatio = 1.1f;
    public float mejora1HnivelActual = 0;
    public float cantidadSumadaMejora1H = 15;

    public string nombreMejora2Hormiguero = "Speed";
    public string descripcionMejora2Hormiguero = "Ants speed increased";
    public float mejora2HCosteBase = 10;
    public float mejora2HCosteActual;
    public float mejora2HRatio = 1.1f;
    public float mejora2HnivelActual = 0;
    public float cantidadSumadaMejora2HPorcentaje = 2;

    public string nombreMejora3Hormiguero = "Blocked";
    public string descripcionMejora3Hormiguero = "Blocked";
    public float mejora3HCosteBase = 10;
    public float mejora3HCosteActual;
    public float mejora3HRatio = 1.1f;
    public float mejora3HnivelActual = 0;
    public float cantidadSumadaMejora3H = 0;

    public float mejorastotalesM = 0;
    public string nombreMejora1Manzano = "Apple tree";
    public string descripcionMejora1Manzano = "Apples generated increased";
    public float mejora1MCosteBase = 10;
    public float mejora1MCosteActual;
    public float mejora1MRatio = 1.1f;
    public float mejora1MnivelActual = 0;
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
    public float mejora3MnivelActual = 0;
    public float cantidadSumadaMejora3MPorcentaje = 3;

    public float mejorasTotalesP = 0;
    public string nombreMejora1Panal = "Beehive";
    public string descripcionMejora1Panal = "Max capacity increased";
    public float mejora1PCosteBase = 10;
    public float mejora1PCosteActual;
    public float mejora1PRatio = 1.1f;
    public float mejora1PnivelActual = 0;
    public float cantidadSumadaMejora1P = 15;

    public string nombreMejora2Panal = "Rest";
    public string descripcionMejora2Panal = "Bee rest time decreased";
    public float mejora2PCosteBase = 10;
    public float mejora2PCosteActual;
    public float mejora2PRatio = 1.1f;
    public float mejora2PnivelActual = 0;
    public float cantidadSumadaMejora2PPorcentaje = 5;

    public string nombreMejora3Panal = "Blocked";
    public string descripcionMejora3Panal = "Blocked";
    public float mejora3PCosteBase = 10;
    public float mejora3PCosteActual;
    public float mejora3PRatio = 1.1f;
    public float mejora3PnivelActual = 0;
    public float cantidadSumadaMejora3P = 0;

    public float mejorasTotalesF = 0;
    public string nombreMejora1Flores = "Flowers quality";
    public string descripcionMejora1Flores = "Bees speed increased";
    public float mejora1FCosteBase = 10;
    public float mejora1FCosteActual;
    public float mejora1FRatio = 1.1f;
    public float mejora1FnivelActual = 0;
    public float cantidadSumadaMejora1FPorcentaje = 2;

    public string nombreMejora2Flores = "Pollination";
    public string descripcionMejora2Flores = "Bee pollination time decreased";
    public float mejora2FCosteBase = 10;
    public float mejora2FCosteActual;
    public float mejora2FRatio = 1.1f;
    public float mejora2FnivelActual = 0;
    public float cantidadSumadaMejora2FPorcentaje = 3;

    public string nombreMejora3Flores = "Honey";
    public string descripcionMejora3Flores = "Honey per pollination increased";
    public float mejora3FCosteBase = 10;
    public float mejora3FCosteActual;
    public float mejora3FRatio = 1.1f;
    public float mejora3FnivelActual = 0;
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
            botonBlock.GetComponentInChildren<Text>().text = blockPanal + " " + costeDesbloqueoPanal+" Apples";
        }
        else if (tipo == "Flor")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockFlores + " " + costeDesbloqueoFlores+" Honey";
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
            //PONER STATS CON TEXTOINFO1,2,3,4
           
        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora1Manzano;
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora1Panal;
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora1Flores;
        }
      
    }
    public void SetDescripcionMejora2()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionMejora2Hormiguero;

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora2Manzano;
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora2Panal;
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora2Flores;
        }
    }
    public void SetDescripcionMejora3()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionMejora3Hormiguero;

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionMejora3Manzano;
        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionMejora3Panal;
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionMejora3Flores;
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
            textoInfo3.GetComponent<Text>().text = "Time between generations = "+manzano.TiempoEntreManzanas;
            textoInfo4.GetComponent<Text>().text = "Apples given per unit = "+ manzanasSumadasExpedicion;

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
            textoInfo2.GetComponent<Text>().text = "Speed boost = " + flor.totalSpeedBost;
            textoInfo3.GetComponent<Text>().text = "Pollination speed boost = " + flor.velocidadPolinizacion;
            textoInfo4.GetComponent<Text>().text = "Honey given per pollination = " + flor.mielPorViaje;
        }
    }
    public void ClickBotonBlock()
    {
        print("clickbotonblock"+tipoOpen);
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
            //Gestionar las mejoras
        }
        else if (tipo == "Manzano")
        {

        }
        else if (tipo == "Panal")
        {

        }
        else if (tipo == "Flores")
        {

        }
    }
    public void ClickBotonMejora2()
    {
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {

        }
        else if (tipo == "Manzano")
        {

        }
        else if (tipo == "Panal")
        {

        }
        else if (tipo == "Flores")
        {

        }
    }
    public void ClickBotonMejora3()
    {
        string tipo = tipoOpen;
        if (tipo == "Hormiguero")
        {

        }
        else if (tipo == "Manzano")
        {

        }
        else if (tipo == "Panal")
        {

        }
        else if (tipo == "Flores")
        {

        }
    }
    public void MenuClose()
    {
        menuBlock.SetActive(false);
        menuCompras.SetActive(false);
    }
}
