using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int cantidadHormigasCogidas = 1;

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
        costeManzanoActual = costeManzanoBase;
        capacidadTotalHormigas = hormiguerosTotal * capacidadPorHormigueroActual;
        hormigasFuera = hormigasTotal;
        hormiguero = GameObject.FindObjectOfType<Hormiguero>();
        ActualizarTextoHormigas();
        TextoHormigas = GameObject.Find("TextoHormigasM").GetComponent<Text>();
        TextoManzanas = GameObject.Find("TextoManzanasM").GetComponent<Text>();
        TextoManzanasPorSegundo = GameObject.Find("TextoManzanasSegundoM").GetComponent<Text>();
        TextoManzanasGeneradas = GameObject.Find("TextoManzanasGeneradasM").GetComponent<Text>();
        TextoCantidadHormigas = GameObject.Find("TextoCapacidadHormigasM").GetComponent<Text>();
        ActualizarTextoHormigas();
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
    public void SumarManzana(int cantidad)
    {
        totalManzanas += cantidad;
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
        GameObject.Find("BuyAnthill").GetComponentInChildren<Text>().text = (string)("Buy Anthill " + costeHormigueroActual);
        GameObject.Find("BuyAppleTree").GetComponentInChildren<Text>().text = (string)("Buy Apple tree " + costeManzanoActual);
    }
    public float costeHormigueroBase = 50;
    public float costeHormigueroActual;
    public float ratioCrecimientoHormiguero = 1.1f;
    public void ComprarHormiguero()
    {
        if (totalManzanas > costeHormigueroActual)
        {
            totalManzanas -= costeHormigueroActual;
            costeHormigueroActual = costeHormigueroBase * Mathf.Pow(ratioCrecimientoHormiguero, hormiguerosTotal);
         
            if (hormiguerosTotal - hormiguerosDesactivados.Length < 0 && hormiguerosDesactivados[hormiguerosTotal] != null)
            {
                hormiguerosDesactivados[hormiguerosTotal-1].gameObject.SetActive(true);
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
        if (totalManzanas > costeManzanoActual)
        {
            totalManzanas -= costeManzanoActual;

            costeManzanoActual = costeManzanoBase * Mathf.Pow(ratioCrecimientoManzano, totalManzanos);
          
            if ((int)totalManzanos - manzanosDesactivados.Length < 0 && manzanosDesactivados[(int)totalManzanos ] != null)
            {
                manzanosDesactivados[(int)totalManzanos-1 ].gameObject.SetActive(true);
            }
            totalManzanos += 1;
        }
        TextoBotones();
    }
}
