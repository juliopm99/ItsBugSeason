using UnityEngine;
using System.Collections;

public class InsectGenerator : MonoBehaviour
{

    public GameObject prefabH;
    public GameObject prefabG;
    public GameObject prefabA;
    public GameObject[] arrayHijosH;
    public GameObject[] arrayHijosG;
    public GameObject[] arrayHijosA;
    public int radioHijos;
    public float cooldownEntreHormigas;
    public float cooldownEntreAbejas;
    public float cooldownEntreGusanos;
    public bool hayHormiga = false;
    public bool hayGusano = false;
    public bool hayAbeja = false;
    public float actualCdH = 0;
    public float actualCdG = 0;
    public float actualCdA = 0;
    public float tiempoMaxHormiga = 5f;
    public float tiempoMaxAbeja = 3.75f;
    public float tiempoMaxGusano = 4.25f;
    public GameObject lastH;
    public GameObject lastA;
    public GameObject lastG;

    public float actualTiempoH = 0;
    public float actualTiempoG = 0;
    public float actualTiempoA = 0;
    private Hormiguero hormiguero;


    public Hormiguero Hormiguero
    {
        get
        {
            Hormiguero = GameObject.FindObjectOfType<Hormiguero>();
            return hormiguero;
        }

        set
        {
            hormiguero = value;
        }
    }
    private Panal panal;


    public Panal Panal
    {
        get
        {
            Panal = GameObject.FindObjectOfType<Panal>();
            return panal;
        }

        set
        {
            panal = value;
        }
    }
    private Gusanero gusanero;


    public Gusanero Gusanero
    {
        get
        {
            Gusanero = GameObject.FindObjectOfType<Gusanero>();
            return gusanero;
        }

        set
        {
            gusanero = value;
        }
    }
    // Use this for initialization
    void Start()
    {

        Hormiguero = GameObject.FindObjectOfType<Hormiguero>();
        Panal = GameObject.FindObjectOfType<Panal>();
        arrayHijosG = arrayHijosH;
        arrayHijosA = arrayHijosH;
    }

    // Update is called once per frame
    void Update()
    {
        if (hayHormiga == false)
        {
            actualCdH -= Time.deltaTime;
            if (actualCdH <= 0)
            {
                SpawnInsecto("Hormiga");
                hayHormiga = true;
                actualTiempoH = tiempoMaxHormiga;
            }
        }
        else if (hayHormiga)
        {
            actualTiempoH -= Time.deltaTime;
            if (actualTiempoH < 0)
            {
                actualCdH = cooldownEntreHormigas;
                hayHormiga = false;
                Destroy(lastH);
            }
        }

        if (hayGusano == false && GameManager.Instance.desbloqueadosGusanos)
        {
            actualCdG -= Time.deltaTime;
            if (actualCdG <= 0)
            {
                SpawnInsecto("Gusano");
                hayGusano = true;
                actualTiempoG = tiempoMaxGusano;
            }
        }
        else if (hayGusano)
        {
            actualTiempoG -= Time.deltaTime;
            if (actualTiempoG < 0)
            {
                actualCdG = cooldownEntreGusanos;
                hayGusano = false;
                Destroy(lastG);
            }
        }
        if (hayAbeja == false && GameManager.Instance.desbloqueadosPanales)
        {
            actualCdA -= Time.deltaTime;
            if (actualCdA <= 0)
            {
                SpawnInsecto("Abeja");
                hayAbeja = true;
                actualTiempoA = tiempoMaxAbeja;
            }
        }
        else if (hayAbeja)
        {
            actualTiempoA -= Time.deltaTime;
            if (actualTiempoA < 0)
            {
                actualCdA = cooldownEntreAbejas;
                hayAbeja = false;
                Destroy(lastA);
            }
        }

    }
    void SpawnInsecto(string tipo)
    {
        GameObject[] array;
        if (tipo == "Hormiga")
        {
            array = arrayHijosH;

        }
        else if (tipo == "Gusano")
        {
            array = arrayHijosG;
        }
        else if (tipo == "Abeja")
        {
            array = arrayHijosA;
        }
        else
        {
            array = null;
        }
        int rand = Random.Range(0, array.Length);
        int randx = Random.Range(0, radioHijos);
        int randz = Random.Range(0, radioHijos);
        Ray ray = new Ray(new Vector3(array[rand].gameObject.transform.position.x + randx, array[rand].gameObject.transform.position.y + 10, array[rand].gameObject.transform.position.z + randz), Vector3.down);
        RaycastHit infoHit;
        if (Physics.Raycast(ray, out infoHit))
        {
            GenerarInsecto(infoHit.point, tipo);
        }
    }
    void GenerarInsecto(Vector3 posicion, string tipo)
    {
        if (tipo == "Hormiga")
        {
            lastH = (GameObject)Instantiate(prefabH, posicion, Quaternion.identity);

            lastH.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Hormiga;

        }
        else if (tipo == "Gusano")
        {
            lastG = (GameObject)Instantiate(prefabG, posicion, Quaternion.identity);

            lastG.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Gusano;
        }
        else if (tipo == "Abeja")
        {
            lastA = (GameObject)Instantiate(prefabA, posicion, Quaternion.identity);

            lastA.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Abeja;
        }



    }
    public void CogerInsecto(GameObject objeto)
    {//SUMAR ALGO Y DIFERENCIAR TIPO DE INSECTO

        if (objeto.GetComponent<InsecGenerado>().tipo == InsecGenerado.Tipos.Hormiga)
        {
            if (hormiguero.poblacionActual < hormiguero.capacidadActual)
            {
                for (int i = 0; i < GameManager.Instance.cantidadHormigasCogidas; i++)
                {
                    GameManager.Instance.CogerHormiga();

                }
            }
            else
            {
                //MENSAJE NO CABEN MAS HORMIGAS
            }
            actualCdH = cooldownEntreHormigas;
            hayHormiga = false;
            Destroy(objeto);
        }
        else if (objeto.GetComponent<InsecGenerado>().tipo == InsecGenerado.Tipos.Abeja)
        {
            {
                if (panal.poblacionActual < panal.capacidadActual)
                {
                    for (int i = 0; i < GameManager.Instance.cantidadAbejasCogidas; i++)
                    {
                        GameManager.Instance.CogerAbeja();

                    }
                }
                else
                {

                }
                actualCdA = cooldownEntreAbejas;
                hayAbeja = false;
                Destroy(objeto);
            }

        }
        else if (objeto.GetComponent<InsecGenerado>().tipo == InsecGenerado.Tipos.Gusano)
        {

            if (GameManager.Instance.gusanosTotal < GameManager.Instance.capacidadTotalGusanos)
            {
                for (int i = 0; i < GameManager.Instance.cantidadGusanosCogidos; i++)
                {
                    GameManager.Instance.CogerGusano();

                }
            }
            else
            {

            }
            actualCdG = cooldownEntreGusanos;
            hayGusano = false;
        Destroy(objeto);


        }

    }

}
