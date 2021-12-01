using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAscension : MonoBehaviour
{
    private static DataAscension _instance;

    public static DataAscension Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DataAscension>();
            }

            return _instance;
        }
    }
    void Awake()
    {


        DontDestroyOnLoad(this.gameObject);
    }

    public bool hayDatos = false;

    private void Start()
    {
        GuardarDatos();
    }
    public void GuardarDatos()
    {
        dActualTokens = GameManager.Instance.actualTokens;
        dCurrentYear = GameManager.Instance.currentYear;
        dChanceShiny = GameManager.Instance.chanceShiny;
        dCosteMejora1Actual = GameManager.Instance.costeMejora1Actual;
        dNivelMejora1 = GameManager.Instance.nivelMejora1;
        dTotalMejora1Tokens = GameManager.Instance.totalMejora1Tokens;
        dCosteMejora2Actual = GameManager.Instance.costeMejora2Actual;
        dNivelMejora2 = GameManager.Instance.nivelMejora2;
        dTotalMejora2Tokens = GameManager.Instance.totalMejora2Tokens;
        dCosteMejora3Actual = GameManager.Instance.costeMejora3Actual;
        dNivelMejora3 = GameManager.Instance.nivelMejora3;
        dTotalMejora3Tokens = GameManager.Instance.totalMejora3Tokens;
        dCosteMejora4Actual = GameManager.Instance.costeMejora4Actual;
        dNivelMejora4 = GameManager.Instance.nivelMejora4;
        dTotalMejora4Tokens = GameManager.Instance.totalMejora4Tokens;
        dCosteMejora5Actual = GameManager.Instance.costeMejora5Actual;
        dNivelMejora5 = GameManager.Instance.nivelMejora5;
        dTotalMejora5Tokens = GameManager.Instance.totalMejora5Tokens;
        listaCheck = GameManager.Instance.propiedadesAñosCheck;
    }
    public void DevolverDatos()
    {
        GameManager.Instance.actualTokens = (int)dActualTokens;
        GameManager.Instance.currentYear = (int)dCurrentYear;
        GameManager.Instance.chanceShiny = (int)dChanceShiny;
        GameManager.Instance.costeMejora1Actual = (int)dCosteMejora1Actual;
        GameManager.Instance.nivelMejora1 = dNivelMejora1;
        GameManager.Instance.totalMejora1Tokens = dTotalMejora1Tokens;
        GameManager.Instance.costeMejora2Actual = (int)dCosteMejora2Actual;
        GameManager.Instance.nivelMejora2 = dNivelMejora2;
        GameManager.Instance.totalMejora2Tokens = dTotalMejora2Tokens;
        GameManager.Instance.costeMejora3Actual = (int)dCosteMejora3Actual;
        GameManager.Instance.nivelMejora3 = dNivelMejora3;
        GameManager.Instance.totalMejora3Tokens = dTotalMejora3Tokens;
        GameManager.Instance.costeMejora4Actual = (int)dCosteMejora4Actual;
        GameManager.Instance.nivelMejora4 = dNivelMejora4;
        GameManager.Instance.totalMejora4Tokens = dTotalMejora4Tokens;
        GameManager.Instance.costeMejora5Actual = (int)dCosteMejora5Actual;
        GameManager.Instance.nivelMejora5 = dNivelMejora5;
        GameManager.Instance.totalMejora5Tokens = dTotalMejora5Tokens;
        GameManager.Instance.capacidadPorHormigueroActual = GameManager.Instance.capacidadPorHormigueroBase;
        GameManager.Instance.capacidadPorMariposeroActual = GameManager.Instance.capacidadPorMariposeroBase;
        GameManager.Instance.capacidadPorGusaneroActual = GameManager.Instance.capacidadPorGusaneroBase;
        GameManager.Instance.capacidadAbejasActual = GameManager.Instance.capacidadAbejasBase;
        GameManager.Instance.capacidadAbejasActual += (int)dTotalMejora2Tokens;
        GameManager.Instance.capacidadPorGusaneroActual += (int)dTotalMejora2Tokens;
        GameManager.Instance.capacidadPorHormigueroActual += (int)dTotalMejora2Tokens;
        GameManager.Instance.capacidadPorMariposeroActual += (int)dTotalMejora2Tokens;
        GameManager.Instance.cantidadAbejasCogidas = (int)(dNivelMejora1 * GameManager.Instance.cantidadMejora1);
        GameManager.Instance.cantidadGusanosCogidos = (int)(dNivelMejora1 * GameManager.Instance.cantidadMejora1);
        GameManager.Instance.cantidadHormigasCogidas = (int)(dNivelMejora1 * GameManager.Instance.cantidadMejora1);
        GameManager.Instance.costeDesbloqueoFlores -= GameManager.Instance.costeDesbloqueoFlores * dTotalMejora3Tokens / 100;
        GameManager.Instance.costeDesbloqueoGusanos -= GameManager.Instance.costeDesbloqueoGusanos * dTotalMejora3Tokens / 100;
        GameManager.Instance.costeDesbloqueoManzano -= GameManager.Instance.costeDesbloqueoManzano * dTotalMejora3Tokens / 100;
        GameManager.Instance.costeDesbloqueoMariposas -= GameManager.Instance.costeDesbloqueoMariposas * dTotalMejora3Tokens / 100;
        GameManager.Instance.costeDesbloqueoPanal -= GameManager.Instance.costeDesbloqueoPanal * dTotalMejora3Tokens / 100;
        GameManager.Instance.propiedadesAñosCheck = listaCheck;
        Destroy(this.gameObject);

    }
    public float dActualTokens;
    public float dCurrentYear;
    public float dCosteMejora1Actual;
    public int dNivelMejora1;
    public float dTotalMejora1Tokens;
    public float dCosteMejora2Actual;
    public int dNivelMejora2;
    public float dTotalMejora2Tokens;
    public float dCosteMejora3Actual;
    public int dNivelMejora3;
    public float dTotalMejora3Tokens;
    public float dCosteMejora4Actual;
    public int dNivelMejora4;
    public float dTotalMejora4Tokens;
    public float dChanceShiny;
    public float dCosteMejora5Actual;
    public int dNivelMejora5;
    public float dTotalMejora5Tokens;
    public List<CheckProperties> listaCheck;

    // Update is called once per frame
    void Update()
    {

    }
}
