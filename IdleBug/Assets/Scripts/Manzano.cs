using UnityEngine;
using System.Collections;

public class Manzano : MonoBehaviour
{
    private float tiempoEntreManzanas = 10f;

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
            {
                actualCd = tiempoEntreManzanas;
                for (int i = 0; i < manzanasSpawned; i++)
                {
                    SpawnManzana();
                }
            }
        }
    }
    void SpawnManzana()
    {
      

        float randx = Random.Range(-radioAparicion, radioAparicion);
        float randz = Random.Range(-radioAparicion, radioAparicion);
        Instantiate(prefabManzana, new Vector3( centroManzanas.transform.position.x+ randx, centroManzanas.transform.position.y,  centroManzanas.transform.position.z+ randz), Quaternion.identity);
    }
}
