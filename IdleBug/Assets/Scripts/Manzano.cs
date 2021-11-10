using UnityEngine;
using System.Collections;

public class Manzano : MonoBehaviour
{
    private float tiempoEntreManzanas = 10f;

    float actualCd = 0;
    public GameObject prefabManzana;
    public float radioAparicion = 6;
    public float radioMin = 0.5f;
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
                for (int i = 0; i < GameManager.Instance.totalManzanos; i++)
                {
                    SpawnManzana();
                }
            }
        }
    }
    void SpawnManzana()
    {
        float randx = Random.Range(radioMin, radioAparicion);
        float randz = Random.Range(radioMin, radioAparicion);
        Instantiate(prefabManzana, new Vector3(centro.x + randx, centro.y, centro.z + randz), Quaternion.identity);
    }
}
