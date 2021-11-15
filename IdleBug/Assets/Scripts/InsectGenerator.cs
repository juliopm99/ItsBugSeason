using UnityEngine;
using System.Collections;

public class InsectGenerator : MonoBehaviour
{
    public float porcentajeAbejas = 30;
    public GameObject prefabInsectoGenerado;
    public GameObject[] arrayHijos;
    public int radioHijos;
    public float cooldownEntreInsectos;
    public bool hayInsecto = false;
    public float actualCd = 0;
    public GameObject lastInsectoGenerado;
    public float tiempoMaxInsecto = 5f;
    public float actualTiempoInsecto = 0;
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

    // Use this for initialization
    void Start()
    {

        Hormiguero = GameObject.FindObjectOfType<Hormiguero>();
        Panal = GameObject.FindObjectOfType<Panal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hayInsecto == false)
        {
            actualCd -= Time.deltaTime;
            if (actualCd <= 0)
            {
                SpawnInsecto();
                hayInsecto = true;
                actualTiempoInsecto = tiempoMaxInsecto;
            }
        }
        if (hayInsecto)
        {
            actualTiempoInsecto -= Time.deltaTime;
            if (actualTiempoInsecto < 0)
            {
                PerderInsecto((GameObject)lastInsectoGenerado);
            }
        }
    }
    void SpawnInsecto()
    {
        int rand = Random.Range(0, arrayHijos.Length);
        int randx = Random.Range(0, radioHijos);
        int randz = Random.Range(0, radioHijos);
        Ray ray = new Ray(new Vector3(arrayHijos[rand].gameObject.transform.position.x + randx, arrayHijos[rand].gameObject.transform.position.y + 10, arrayHijos[rand].gameObject.transform.position.z + randz), Vector3.down);
        RaycastHit infoHit;
        if (Physics.Raycast(ray, out infoHit))
        {
            GenerarInsecto(infoHit.point);
        }
    }
    void GenerarInsecto(Vector3 posicion)
    {
        lastInsectoGenerado = (GameObject)Instantiate(prefabInsectoGenerado, posicion, Quaternion.identity);
        if (GameManager.Instance.desbloqueadasAbejas)
        {
            if (Random.Range(0, 100) < porcentajeAbejas)
            {
                lastInsectoGenerado.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Abeja;
            }
            else
            {
                lastInsectoGenerado.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Hormiga;
            }

        }
        else
        {
            lastInsectoGenerado.GetComponent<InsecGenerado>().tipo = InsecGenerado.Tipos.Hormiga;
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
            }
         
        }
        actualCd = cooldownEntreInsectos;
            hayInsecto = false;
            Destroy(objeto);
    }
        public void PerderInsecto(GameObject objeto)
        {//SUMAR ALGO
            actualCd = cooldownEntreInsectos;
            hayInsecto = false;
            Destroy(objeto);
        }
    }
