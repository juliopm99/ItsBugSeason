using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    TextMesh textoAlmacen;
    [Header("No se toca")]
    public int cantidadHormigasCogidas = 1;
    public int cantidadAbejasCogidas = 1;
    public int cantidadGusanosCogidos = 1;
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
    Gusanero gusanero;
    public float manzanasXHormiga = 0.1f;
    public float totalManzanasPorSegundo;
    public float tiempoGeneracionManzana;
    public float totalManzanas;
    public float totalManzanos = 1;
    Manzano manzano;

    Flor flor;
    public GameObject[] hormiguerosDesactivados;
    public GameObject[] manzanosDesactivados;
    public GameObject[] panalesDesactivados;
    public GameObject[] floresDesactivados;
    public GameObject[] gusanerosDesactivados;
    public GameObject[] mariposerosDesactivados;
    public GameObject[] floresMariposasDesactivadas;

    [Header("cada cuantas mejoras se activa un obj")]

    public float frecuenciaAparicionHormiguero = 7;
    public float frecuenciaAparicionManzano = 7;
    public float frecuenciaAparicionPanales = 8;
    public float frecuenciaAparicionFlores = 3;
    public float frecuenciaAparicionGusaneros = 6;
    public float frecuenciaAparicionMariposero = 20;
    public float frecuenciaAparicionFloresMariposas = 3;

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

    public bool GusanosParados
    {
        get => gusanosParados; set
        {
            gusanosParados = value;
            if (gusanosParados)
            {
                if (gusanosListaSuelo.Count > 0)
                {
                    foreach (GameObject gusano in gusanosListaSuelo)
                    {
                        gusano.GetComponent<Gusano>().parado = true;
                    }
                }
            }
            else
            {
                if (gusanosListaSuelo.Count > 0)
                {
                    foreach (GameObject gusano in gusanosListaSuelo)
                    {
                        gusano.GetComponent<Gusano>().parado = false;
                    }
                }
            }


        }
    }

   
    public float MejorasTotalesH
    {
        get => mejorasTotalesH;
        set
        {
            mejorasTotalesH = value;
            if (MejorasTotalesH % frecuenciaAparicionHormiguero == 0)
            {
                foreach (GameObject go in hormiguerosDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    public float MejorastotalesM
    {
        get => mejorastotalesM;
        set
        {
            mejorastotalesM = value;
            if (MejorastotalesM % frecuenciaAparicionManzano == 0)
            {
                foreach (GameObject go in manzanosDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    public float MejorasTotalesP
    {
        get => mejorasTotalesP;
        set
        {
            mejorasTotalesP = value;
            if (MejorasTotalesP % frecuenciaAparicionPanales == 0)
            {
                foreach (GameObject go in panalesDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    public float MejorasTotalesF
    {
        get => mejorasTotalesF;
        set
        {
            mejorasTotalesF = value;
            if (MejorasTotalesF % frecuenciaAparicionFlores == 0)
            {
                foreach (GameObject go in floresDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    public float MejorasTotalesG
    {
        get => mejorasTotalesG;
        set
        {
            mejorasTotalesG = value;
            if (MejorasTotalesG % frecuenciaAparicionGusaneros == 0)
            {
                foreach (GameObject go in gusanerosDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    public float MejorasTotalesMA
    {
        get => mejorasTotalesMA;
        set
        {
            mejorasTotalesMA = value;
            if (MejorasTotalesMA % frecuenciaAparicionMariposero == 0)
            {
                foreach (GameObject go in mariposerosDesactivados)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
            if (MejorasTotalesMA % frecuenciaAparicionFloresMariposas == 0)
            {
                foreach (GameObject go in floresMariposasDesactivadas)
                {
                    if (!go.activeSelf)
                    {
                        go.SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    void CalcularComienzo()
    {

        capacidadTotalHormigas = hormiguerosTotal * capacidadPorHormigueroActual;
        capacidadTotalAbejas = panalesTotal * capacidadAbejasPorPanal;
        capacidadTotalGusanos = gusanerosTotal * capacidadPorGusaneroActual;
        capacidadTotalMariposas = mariposerosTotal * capacidadPorMariposeroActual;
        abejasFuera = 0;
        gusanosFuera = 0;
        mariposasFuera = 0;
        hormigasFuera = hormigasTotal;
        hormiguero = GameObject.FindObjectOfType<Hormiguero>();
        panal = GameObject.FindObjectOfType<Panal>();
        ActualizarTextoHormigas();
        TextoHormigas = GameObject.Find("TextoHormigasM").GetComponent<Text>();
        TextoManzanas = GameObject.Find("TextoManzanasM").GetComponent<Text>();
        textoMiel = GameObject.Find("TextoManzanasSegundoM").GetComponent<Text>();
        textoSeda = GameObject.Find("TextoManzanasGeneradasM").GetComponent<Text>();
        textoPetalos = GameObject.Find("TextoCapacidadHormigasM").GetComponent<Text>();
        ActualizarTextoHormigas();
        ActualizarTextoAbejas();
        ActualizarTextoGusanos();
        ActualizarTextoMariposas();
        manzano = FindObjectOfType<Manzano>();
        flor = FindObjectOfType<FlorHub>().gameObject.GetComponentInChildren<Flor>();

        maxSilk = capacidadStorageSilkBase;
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
        mejora1GCosteActual = mejora1GCosteBase;
        mejora2GCosteActual = mejora2GCosteBase;
        mejora3GCosteActual = mejora3GCosteBase;
        mejora1MACosteActual = mejora1MACosteBase;
        mejora2MACosteActual = mejora2MACosteBase;
        mejora3MACosteActual = mejora3MACosteBase;

        textoAlmacen = GameObject.Find("TextoAlmacen").GetComponent<TextMesh>();
        GameObject.Find("TextoCreador").GetComponent<TextMesh>().text = "Cost: " + gusanosNecesarios + " worms";

    }
    void Start()
    {

        CalcularComienzo();
    }
    void Cheats()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                totalManzanas += 20;
               
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                totalMiel += 20;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                totalSeda += 20;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                totalPetalos += 20;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

    }
    void Update()
    {
        Cheats();

        textoAlmacen.text = actualStorageSilk.ToString("0.0") + " / " + maxSilk;
        if (desbloqueadosGusanos)
        {
            sedaPorSegundo = gusanosTotal * sedaPorGusanoSegundo + (gusanosTotal * sedaPorGusanoSegundo * multiplicadorSedaSegundo / 100);
            if (actualStorageSilk < maxSilk)
            {
                actualStorageSilk += sedaPorSegundo * Time.deltaTime;
                if (actualStorageSilk > maxSilk)
                {
                    actualStorageSilk = maxSilk;
                }

            }
            else
            {
                if (gusanosParados == false) gusanosParados = true;
            }
        }
        if (desbloqueadasMariposas)
        {
            petalosPorSegundo = mariposasTotal * petalosPorMariposaSegundo + (mariposasTotal * petalosPorMariposaSegundo * multiplicadorPetalosSegundo / 100);
            totalPetalos += petalosPorSegundo * Time.deltaTime;
        }

        ActualizarTextoMenu();
    }

    public Text TextoHormigas;
    public Text TextoManzanas;
    public Text textoMiel;
    public Text textoSeda;
    public Text textoPetalos;

    public void ActualizarTextoMenu()
    {
        totalManzanasPorSegundo = hormigasTotal * manzanasXHormiga;
        capacidadTotalHormigas = hormiguerosTotal * capacidadPorHormigueroActual;

        TextoManzanas.text = (string)("Apples:" + totalManzanas.ToString("0"));
        textoMiel.text = (string)("Honey:" + totalMiel.ToString("0"));
        textoSeda.text = (string)("Silk :" + totalSeda.ToString("0"));
        textoPetalos.text = (string)("Petals:" + totalPetalos.ToString("0"));


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
                SpawnHor[] posSpawn = FindObjectsOfType<SpawnHor>();
                int random = Random.Range(0, posSpawn.Length);

                Instantiate(prefabHormiga, posSpawn[random].gameObject.transform.position, Quaternion.identity);
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
                SpawnAbe[] posSpawn = FindObjectsOfType<SpawnAbe>();
                int random = Random.Range(0, posSpawn.Length);
              
                Instantiate(prefabAbeja, posSpawn[random].gameObject.transform.position, Quaternion.identity);
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
    public int capacidadTotalGusanos = 0;
    public int capacidadPorGusaneroBase = 10;
    public int capacidadPorGusaneroActual;
    public int gusanerosTotal = 1;
    public int gusanosTotal;
    public int gusanosFuera = 0;
    public int maxGusanosFuera = 20;
    public GameObject prefabGusano;
    public float capacidadStorageSilkBase = 10;
    public float actualStorageSilk;
    public float maxSilk;
    private bool gusanosParados = false;
    public float totalSeda = 0;
    public float sedaPorSegundo = 0;
    public int multiplicadorSedaSegundo = 0;
    public float sedaPorGusanoSegundo = 0.1f;
    public List<GameObject> gusanosListaSuelo = new List<GameObject>();
    public void CogerGusano()
    {
        if (gusanosTotal + 1 <= capacidadTotalGusanos)
        {
            gusanosTotal++;

            print("Tengo" + gusanosTotal + " gusanos");
            if (gusanosFuera < maxGusanosFuera)
            {
                gusanosFuera++;
                SpawnGus[] posSpawn = FindObjectsOfType<SpawnGus>();
                int random = Random.Range(0, posSpawn.Length);
                GameObject gusanoSuelo = Instantiate(prefabGusano, posSpawn[random].gameObject.transform.position, Quaternion.identity);
                gusanosListaSuelo.Add(gusanoSuelo);

            }

            ActualizarTextoGusanos();
        }

    }
    public void VaciarAlmacenSeda()
    {
        totalSeda += actualStorageSilk;
        actualStorageSilk = 0;
        if (gusanosParados == true) gusanosParados = false;
    }
    public void PerderGusano()
    {
        if (gusanosTotal >= maxGusanosFuera)
        {
            gusanosTotal--;
            
        }
        else
        {
            gusanosFuera--;
            gusanosTotal--;
            GameObject destruir = gusanosListaSuelo[0];
            gusanosListaSuelo.Remove(gusanosListaSuelo[0]);
            Destroy(destruir);

        }
    }
    void ActualizarTextoGusanos()
    {
        capacidadTotalGusanos = capacidadPorGusaneroActual * gusanerosTotal;

        GameObject.Find("TextoGusanos").GetComponent<TextMesh>().text = (string)(gusanosTotal + " / " + capacidadTotalGusanos);

    }

    public int capacidadTotalMariposas = 0;
    public int capacidadPorMariposeroBase = 10;
    public int capacidadPorMariposeroActual;
    public int mariposerosTotal = 1;
    public int mariposasTotal;
    public int mariposasFuera = 0;
    public int maxMariposasFuera = 20;
    public GameObject prefabMariposa;
    public float gusanosNecesarios = 10;
    public float totalPetalos = 0;
    public float petalosPorSegundo = 0;
    public int multiplicadorPetalosSegundo = 0;
    public float petalosPorMariposaSegundo = 0.1f;

    public void CrearMariposa()
    {
        if (gusanosTotal - gusanosNecesarios >= 0)
        {
            for (int i = 0; i < gusanosNecesarios; i++)
            {
                PerderGusano();

            }

            if (mariposasTotal + 1 <= capacidadTotalMariposas)
            {
                mariposasTotal++;

                print("Tengo" + mariposasTotal + " gusanos");
                if (mariposasFuera < maxMariposasFuera)
                {
                    mariposasFuera++;

                    GameObject mariposaFuera = Instantiate(prefabMariposa, GameObject.Find("TextoCreador").transform.parent.transform.position, Quaternion.identity);


                }

                ActualizarTextoMariposas();
                ActualizarTextoGusanos();
            }
        }

    }
    void ActualizarTextoMariposas()
    {
        capacidadTotalMariposas = capacidadPorMariposeroActual * mariposerosTotal;
        GameObject.Find("TextoCreador").GetComponent<TextMesh>().text = "Cost: " + gusanosNecesarios + " worms";
        GameObject.Find("TextoMariposas").GetComponent<TextMesh>().text = (string)(mariposasTotal + " / " + capacidadTotalMariposas);

    }

    public bool desbloqueadosHormigueros = true;
    public bool desbloqueadosManzanos = false;
    public bool desbloqueadosPanales = false;
    public bool desbloqueadasFlores = false;
    public bool desbloqueadosGusanos = false;
    public bool desbloqueadasMariposas = false;

    public string textoBloq = "Blocked";


    public float totalMiel;

    public int panalesTotal = 1;
    public int capacidadTotalAbejas = 0;
    public int capacidadAbejasPorPanal = 20;


    void ActualizarTextoAbejas()
    {
        capacidadTotalAbejas = capacidadAbejasPorPanal * panalesTotal;
        panal.poblacionActual = (int)abejasTotal;
        panal.capacidadActual = capacidadTotalAbejas;
        GameObject.Find("TextoAbejas").GetComponent<TextMesh>().text = (string)(abejasTotal + " / " + capacidadTotalAbejas);

    }

    public int totalFlores = 0;

    public float mielSumadaExpedicion = 1;


    public GameObject menuBlock;
    public GameObject menuCompras;
    public Text descripcion;
    public Text nombre;
    public Text nombreBlocked;


    public string descripcionHormiguero;
    public string descripcionManzano;
    public string descripcionPanal;
    public string descripcionFlores;
    public string descripcionGusanos;
    public string descripcionMariposas;
    public string nombreBlock = "Blocked";
    public string botonBlockText = "Blocked x money";
    public string blockHormiguero = "Blocked x money";
    public string blockManzano = "Blocked x money";
    public string blockPanal = "Blocked x money";
    public string blockFlores = "Blocked x money";
    public string blockGusanos = "Blocked x money";
    public string blockMariposas = "Blocked x money";
    public float costeDesbloqueoHormiguero;
    public float costeDesbloqueoManzano;
    public float costeDesbloqueoPanal;
    public float costeDesbloqueoFlores;
    public float costeDesbloqueoGusanos;
    public float costeDesbloqueoMariposas;


    public GameObject botonBlock;
    public GameObject botonMejora1;
    public GameObject botonMejora2;
    public GameObject botonMejora3;
    public GameObject textoInfo1;
    public GameObject textoInfo2;
    public GameObject textoInfo3;
    public GameObject textoInfo4;

    public string tipoOpen;
    private float mejorasTotalesH = 0;
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

    private float mejorastotalesM = 0;
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

    private float mejorasTotalesP = 0;
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

    private float mejorasTotalesF = 0;
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

    private float mejorasTotalesG = 0;
    public string nombreMejora1G = "Worms house";
    public string descripcionMejora1G = "Max capacity increased";
    public float mejora1GCosteBase = 10;
    public float mejora1GCosteActual;
    public float mejora1GRatio = 1.1f;
    public float mejora1GnivelActual = 1;
    public float cantidadSumadaMejora1G = 15;

    public string nombreMejora2G = "Production";
    public string descripcionMejora2G = "Worms production per second increased";
    public float mejora2GCosteBase = 10;
    public float mejora2GCosteActual;
    public float mejora2GRatio = 1.1f;
    public float mejora2GnivelActual = 1;
    public float cantidadSumadaMejora2GPorcentaje = 2;


    public string nombreMejora3G = "Silk storage";
    public string descripcionMejora3G = "Silk storage capacity increased";
    public float mejora3GCosteBase = 10;
    public float mejora3GCosteActual;
    public float mejora3GRatio = 1.1f;
    public float mejora3GnivelActual = 1;
    public float cantidadSumadaMejora3GPorcentaje = 0;

    private float mejorasTotalesMA = 0;
    public string nombreMejora1MA = "Butterfly house";
    public string descripcionMejora1MA = "Max capacity increased";
    public float mejora1MACosteBase = 10;
    public float mejora1MACosteActual;
    public float mejora1MARatio = 1.1f;
    public float mejora1MAnivelActual = 1;
    public float cantidadSumadaMejora1MA = 15;

    public string nombreMejora2MA = "Production";
    public string descripcionMejora2MA = "Butterfly production per second increased";
    public float mejora2MACosteBase = 10;
    public float mejora2MACosteActual;
    public float mejora2MARatio = 1.1f;
    public float mejora2MAnivelActual = 1;
    public float cantidadSumadaMejora2MAPorcentaje = 2;


    public string nombreMejora3MA = "Worms picked";
    public string descripcionMejora3MA = "WormsPicked";
    public float mejora3MACosteBase = 10;
    public float mejora3MACosteActual;
    public float mejora3MARatio = 1.1f;
    public float mejora3MAnivelActual = 1;
    public float cantidadSumadaMejora3MA = 1;

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
        else if (go.GetComponent<Gusanero>())
        {
            if (GameManager.Instance.desbloqueadosGusanos)
            {
                OpenCompras("Gusanero");
            }
            else
            {
                OpenBlock("Gusanero");
            }
        }
        else if (go.GetComponent<Mariposero>())
        {
            if (GameManager.Instance.desbloqueadasMariposas)
            {
                OpenCompras("Mariposero");
            }
            else
            {
                OpenBlock("Mariposero");
            }
        }
        else if (go.GetComponent<AlmacenSeda>())
        {
            VaciarAlmacenSeda();
        }
        else if (go.GetComponent<CreadorMariposas>())
        {
            CrearMariposa();
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
            botonBlock.GetComponentInChildren<Text>().text = blockPanal + " " + costeDesbloqueoPanal + " Silk";
        }
        else if (tipo == "Flor")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockFlores + " " + costeDesbloqueoFlores + " Honey";
        }
        else if (tipo == "Gusanero")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockGusanos + " " + costeDesbloqueoGusanos + " Apples";
        }
        else if (tipo == "Mariposero")
        {
            nombreBlocked.text = nombreBlock;
            botonBlock.GetComponentInChildren<Text>().text = blockMariposas + " " + costeDesbloqueoMariposas + " Honey";
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
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesH;
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

            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorastotalesM;
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
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesP;
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
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesF;
            textoInfo2.GetComponent<Text>().text = "Speed boost = " + cantidadSumadaMejora1FPorcentaje * mejora1FnivelActual + "%";
            textoInfo3.GetComponent<Text>().text = "Pollination speed boost = " + cantidadSumadaMejora2FPorcentaje * mejora2FnivelActual + "%";

            textoInfo4.GetComponent<Text>().text = "Honey given per pollination = " + mielSumadaExpedicion;
        }
        else if (tipo == "Gusanero")
        {
            nombre.text = "Worms house";
            descripcion.text = descripcionGusanos;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1G;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2G;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3G;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesG;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalGusanos;
            textoInfo3.GetComponent<Text>().text = "Production boost = " + cantidadSumadaMejora2GPorcentaje * mejora2GnivelActual + "%";
            textoInfo4.GetComponent<Text>().text = "Max silk storage = " + cantidadSumadaMejora3GPorcentaje*mejora3GnivelActual+"%";
        }
        else if (tipo == "Mariposero")
        {
            nombre.text = "Butterfly house";
            descripcion.text = descripcionMariposas + " Worms consumed =" + " " + gusanosNecesarios;
            botonMejora1.GetComponentInChildren<Text>().text = nombreMejora1MA;
            botonMejora2.GetComponentInChildren<Text>().text = nombreMejora2MA;
            botonMejora3.GetComponentInChildren<Text>().text = nombreMejora3MA;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesMA;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalMariposas;
            textoInfo3.GetComponent<Text>().text = "Production boost = " + cantidadSumadaMejora2MAPorcentaje * mejora2MAnivelActual + "%";
            textoInfo4.GetComponent<Text>().text = "Extra worms picked = " + mejora3MAnivelActual * cantidadSumadaMejora3MA;
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
        else if (tipoOpen == "Gusanero")
        {
            descripcion.text = descripcionMejora1G;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1GCosteActual + " Silk";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1GnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1G;
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Mariposero")
        {
            descripcion.text = descripcionMejora1MA;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora1MACosteActual + " Petals";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora1MAnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora1MA;
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
        else if (tipoOpen == "Gusanero")
        {
            descripcion.text = descripcionMejora2G;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2GCosteActual + " Silk";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2GnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2GPorcentaje + "%";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Mariposero")
        {
            descripcion.text = descripcionMejora2MA;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora2MACosteActual + " Petals";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora2MAnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora2MAPorcentaje;
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
        else if (tipoOpen == "Gusanero")
        {
            descripcion.text = descripcionMejora3G;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3GCosteActual + " Silk";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora3GnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3GPorcentaje+ "%";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Mariposero")
        {
            descripcion.text = descripcionMejora3MA;
            textoInfo1.GetComponent<Text>().text = "Cost = " + mejora3MACosteActual + " Petals";
            textoInfo2.GetComponent<Text>().text = "Level = " + mejora3MAnivelActual;
            textoInfo3.GetComponent<Text>().text = "Amount add = " + cantidadSumadaMejora3MA;
            textoInfo4.GetComponent<Text>().text = "";
        }
    }
    public void DescripcionOriginal()
    {
        if (tipoOpen == "Hormiguero")
        {

            descripcion.text = descripcionHormiguero;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesH;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalHormigas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";

        }
        else if (tipoOpen == "Manzano")
        {
            descripcion.text = descripcionManzano;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorastotalesM;
            textoInfo2.GetComponent<Text>().text = "Apples spawned = " + manzano.manzanasSpawned;
            textoInfo3.GetComponent<Text>().text = "Time between generations = " + manzano.TiempoEntreManzanas;
            textoInfo4.GetComponent<Text>().text = "Apples given per unit = " + manzanasSumadasExpedicion;

        }
        else if (tipoOpen == "Panal")
        {
            descripcion.text = descripcionPanal;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesP;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalAbejas;
            textoInfo3.GetComponent<Text>().text = "";
            textoInfo4.GetComponent<Text>().text = "";
        }
        else if (tipoOpen == "Flor")
        {
            descripcion.text = descripcionFlores;
            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesF;
            textoInfo2.GetComponent<Text>().text = "Speed boost = " + cantidadSumadaMejora1FPorcentaje * mejora1FnivelActual + "%";
            textoInfo3.GetComponent<Text>().text = "Pollination speed boost = " + cantidadSumadaMejora2FPorcentaje * mejora2FnivelActual + "%";

            textoInfo4.GetComponent<Text>().text = "Honey given per pollination = " + mielSumadaExpedicion;
        }
        else if (tipoOpen == "Gusanero")
        {
            nombre.text = "Worms house";
            descripcion.text = descripcionGusanos;

            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesG;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalGusanos;
            textoInfo3.GetComponent<Text>().text = "Production boost = " + cantidadSumadaMejora2GPorcentaje * mejora2GnivelActual + "%";
            textoInfo4.GetComponent<Text>().text = "Max silk storage = " + cantidadSumadaMejora3GPorcentaje * mejora3GnivelActual + "%";
        }
        else if (tipoOpen == "Mariposero")
        {

            nombre.text = "Butterfly house";
            descripcion.text = descripcionMariposas + " Worms consumed =" + " " + gusanosNecesarios;

            textoInfo1.GetComponent<Text>().text = "Total upgrades = " + MejorasTotalesMA;
            textoInfo2.GetComponent<Text>().text = "Max capacity = " + capacidadTotalMariposas;
            textoInfo3.GetComponent<Text>().text = "Production boost = " + cantidadSumadaMejora2MAPorcentaje * mejora2MAnivelActual + "%";
            textoInfo4.GetComponent<Text>().text = "Extra worms picked = " + mejora3MAnivelActual * cantidadSumadaMejora3MA;
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
            if (totalSeda >= costeDesbloqueoPanal)
            {
                totalSeda -= costeDesbloqueoPanal;

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
        else if (tipo == "Gusanero")
        {
            if (totalManzanas >= costeDesbloqueoGusanos)
            {
                totalManzanas -= costeDesbloqueoGusanos;
                desbloqueadosGusanos = true;

                MenuClose();
                OpenCompras("Gusanero");
            }
        }
        else if (tipo == "Mariposero")
        {
            if (totalMiel >= costeDesbloqueoMariposas)
            {
                totalMiel -= costeDesbloqueoMariposas;
                desbloqueadasMariposas = true;

                MenuClose();
                OpenCompras("Mariposero");
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
                MejorasTotalesH++;
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
                MejorastotalesM++;
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
                MejorasTotalesP++;
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
                MejorasTotalesF++;
                totalMiel -= mejora1FCosteActual;
                mejora1FCosteActual = (int)mejora1FCosteBase * Mathf.Pow(mejora1FRatio, mejora1FnivelActual);
                panal.speedMission += panal.speedMission * cantidadSumadaMejora1FPorcentaje / 100;
                SetDescripcionMejora1();
            }
        }
        else if (tipo == "Gusanero")
        {
            if (totalSeda >= mejora1GCosteActual)
            {
                mejora1GnivelActual++;
                MejorasTotalesG++;
                totalSeda -= mejora1GCosteActual;
                mejora1GCosteActual = (int)mejora1GCosteBase * Mathf.Pow(mejora1GRatio, mejora1GnivelActual);
                gusanerosTotal++;
                ActualizarTextoGusanos();
                SetDescripcionMejora1();
            }
        }
        else if (tipo == "Mariposero")
        {
            if (totalPetalos >= mejora1MACosteActual)
            {
                mejora1MAnivelActual++;
                MejorasTotalesMA++;
                totalPetalos -= mejora1MACosteActual;
                mejora1MACosteActual = (int)mejora1MACosteBase * Mathf.Pow(mejora1MARatio, mejora1MAnivelActual);
                mariposerosTotal++;
                SetDescripcionMejora1(); ActualizarTextoMariposas();
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
                MejorasTotalesH++;
                totalManzanas -= mejora2HCosteActual;
                mejora2HCosteActual = (int)mejora2HCosteBase * Mathf.Pow(mejora2HRatio, mejora2HnivelActual);
                hormiguero.speedMission += hormiguero.speedMission * cantidadSumadaMejora2HPorcentaje / 100;
                SetDescripcionMejora2();
                ActualizarTextoHormigas();
            }


        }
        else if (tipo == "Manzano")
        {
            if (totalManzanas >= mejora2MCosteActual)
            {
                mejora2MnivelActual++;
                MejorastotalesM++;
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
                MejorasTotalesP++;
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
                MejorasTotalesF++;
                totalMiel -= mejora2FCosteActual;
                mejora2FCosteActual = (int)mejora2FCosteBase * Mathf.Pow(mejora2FRatio, mejora2FnivelActual);
                if (panal.tiempoPolinizando > 0.5f) panal.tiempoPolinizando -= panal.tiempoPolinizando * cantidadSumadaMejora2FPorcentaje / 100;
                SetDescripcionMejora2();
            }
        }
        else if (tipo == "Gusanero")
        {
            if (totalSeda >= mejora2GCosteActual)
            {
                mejora2GnivelActual++;
                MejorasTotalesG++;
                totalSeda -= mejora2GCosteActual;
                mejora2GCosteActual = (int)mejora2GCosteBase * Mathf.Pow(mejora2GRatio, mejora2GnivelActual);
                multiplicadorSedaSegundo = (int)(cantidadSumadaMejora2GPorcentaje * mejora2GnivelActual);
                SetDescripcionMejora2(); ActualizarTextoGusanos();

            }
        }
        else if (tipo == "Mariposero")
        {
            if (totalPetalos >= mejora2MACosteActual)
            {
                mejora2MAnivelActual++;
                MejorasTotalesMA++;
                totalPetalos -= mejora2MACosteActual;
                mejora2MACosteActual = (int)mejora2MACosteBase * Mathf.Pow(mejora2MARatio, mejora2MAnivelActual);
                multiplicadorPetalosSegundo += (int)(cantidadSumadaMejora2MAPorcentaje * mejora2MAnivelActual);
                SetDescripcionMejora2(); ActualizarTextoMariposas();
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
                MejorastotalesM++;
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
                MejorasTotalesF++;
                totalMiel -= mejora3FCosteActual;
                mejora3FCosteActual = (int)mejora3FCosteBase * Mathf.Pow(mejora3FRatio, mejora3FnivelActual);
                mielSumadaExpedicion++;
                SetDescripcionMejora3();
            }
        }
        else if (tipo == "Gusanero")
        {
            if (totalSeda >= mejora3GCosteActual)
            {
                mejora3GnivelActual++;
                MejorasTotalesG++;
                totalSeda -= mejora3GCosteActual;
                mejora3GCosteActual = (int)mejora3GCosteBase * Mathf.Pow(mejora3GRatio, mejora3GnivelActual);
                maxSilk += (int)(cantidadSumadaMejora3GPorcentaje * maxSilk/100);
                SetDescripcionMejora3();
            }
        }
        else if (tipo == "Mariposero")
        {
            if (totalPetalos >= mejora3MACosteActual)
            {
                mejora3MAnivelActual++;
                MejorasTotalesMA++;
                totalPetalos -= mejora3MACosteActual;
                mejora3MACosteActual = (int)mejora3MACosteBase * Mathf.Pow(mejora3MARatio, mejora3MAnivelActual);
                cantidadGusanosCogidos = (int)(cantidadSumadaMejora3MA * mejora3MAnivelActual);
                SetDescripcionMejora3(); ActualizarTextoMariposas();
            }
        }
    }
    public void MenuClose()
    {
        menuBlock.SetActive(false);
        menuCompras.SetActive(false);
    }
}
