using UnityEngine;
using System.Collections;

public class Manzano : MonoBehaviour
{
    public float tiempoEntreManzanas = 10f;

    float actualCd = 0;
    public GameObject prefabManzana;
    public float radioAparicion = 6;
    public float radioMin = 0.5f;
    public float manzanasSpawned = 1;
    public GameObject centroManzanas;
    
    Vector3 centro;

    public float TiempoEntreManzanas
    {
        get
        {
            return tiempoEntreManzanas;
        }

        set
        {
            GameManager.Instance.tiempoGeneracionManzana = tiempoEntreManzanas;
            tiempoEntreManzanas = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        centro = gameObject.transform.GetChild(0).transform.position;
        actualCd = tiempoEntreManzanas;
        GameManager.Instance.tiempoGeneracionManzana = tiempoEntreManzanas;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualCd > 0)
        {
            actualCd -= Time.deltaTime;
            if (actualCd < 0)
            {if (CamaraChange.Instance.activeCam == 0) SonidoManager.Instance.Play("ManzanasCaer");
                actualCd = tiempoEntreManzanas+(tiempoEntreManzanas*GameManager.Instance.tGenManzPrimaveraPorcentaje/100)-(tiempoEntreManzanas*GameManager.Instance.tMenosGenManzOtonoPorcentaje/100);
                for (int i = 0; i < manzanasSpawned; i++)
                {
                    SpawnManzana();
                }
            }
        }
    }
  
    void SpawnManzana()
    {

        ManzanoF[] manzanosF = GameObject.FindObjectsOfType<ManzanoF>();
        float randx = Random.Range(-radioAparicion, radioAparicion);
        float randz = Random.Range(-radioAparicion, radioAparicion);
        int random = Random.Range(0, manzanosF.Length);
       
        Instantiate(prefabManzana, new Vector3(manzanosF[random].gameObject.transform.position.x+ randx, manzanosF[random].gameObject.transform.position.y, manzanosF[random].gameObject.transform.position.z+ randz), Quaternion.identity);
    }
}
