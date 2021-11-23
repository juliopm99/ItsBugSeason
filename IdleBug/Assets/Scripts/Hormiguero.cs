﻿using UnityEngine;
using System.Collections;

public class Hormiguero : MonoBehaviour
{
    public int capacidadActual;
    public int poblacionActual;
    public int poblacionOcupada;
    public int poblacionMinimaEnBase = 5;
    public int poblacionMinimaMision = 5;
    public GameObject prefabHormiga;
    public bool spawneando = false;
    public GameObject textoHormigas;
    public bool lleno = false;
    public GameObject[] spawnPoses;
    public float tiempoEntreHormigasSpawn = 0.5f;
    public float speedMission = 5;
    // Use this for initialization
    void Start()
    {
        textoHormigas = GetComponentInChildren<TextMesh>().gameObject;
        poblacionActual = 2;
        poblacionOcupada = 2;
        capacidadActual = GameManager.Instance.capacidadPorHormigueroActual * GameManager.Instance.hormiguerosTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (poblacionActual >= poblacionMinimaEnBase + poblacionMinimaMision && poblacionActual - poblacionOcupada >= poblacionMinimaMision/*&&!spawneando*/)
        {
            print("SE CUMPE");
            //DIFERENCIAR MANZANAS DISPONIBLES

            foreach (Manzana go in GameObject.FindObjectsOfType<Manzana>())
            {
                if (go.targeted == false)
                {
                    go.targeted = true;
                    EmpezarMision(go.transform.gameObject);

                    break;
                }
            }


        }
    }
    public void OcuparHormiga()
    {
        poblacionOcupada++;
    }
    public void DesocuparHormiga()
    {
        poblacionOcupada--;
    }
    public void EmpezarMision(GameObject manz)
    {
        poblacionOcupada += poblacionMinimaMision;
        int rand = 0;
        if (GameManager.Instance.hormiguerosTotal > 1)
        {
            rand = Random.Range(0, GameManager.Instance.hormiguerosTotal - 1);
            print(rand + "randomgeeeeee");
            if (rand > GameManager.Instance.hormiguerosDesactivados.Length)
            {
                rand = 0;
            }
        }
        print(rand + "randomg");
        StartCoroutine(SpawnHormigasMision(manz, this.transform.position));//CAMBIAR
        spawneando = false;
    }
    public void TerminarMision()
    {
        //poblacionOcupada -= poblacionMinimaMision;
        //print("TERMINAMISION"+ poblacionOcupada);
    }
    public IEnumerator SpawnHormigasMision(GameObject manz, Vector3 spawnPos)
    {
        SpawnHor[] posSpawn = FindObjectsOfType<SpawnHor>();
        int random = Random.Range(0, posSpawn.Length);

        spawnPos = posSpawn[random].gameObject.transform.position;

        yield return new WaitForSeconds(tiempoEntreHormigasSpawn);

        GameObject hormigaInstanciada = (GameObject)Instantiate(prefabHormiga, spawnPos, Quaternion.identity);
        print("primerahormiga" + spawnPos);
        hormigaInstanciada.GetComponent<Hormiga>().enMision = true;
        hormigaInstanciada.GetComponent<Hormiga>().destinoManzana = manz;
        hormigaInstanciada.GetComponent<Hormiga>().speedMission = speedMission;
        yield return new WaitForSeconds(tiempoEntreHormigasSpawn);
        GameObject hormigaInstanciada2 = (GameObject)Instantiate(prefabHormiga, spawnPos, Quaternion.identity);
        hormigaInstanciada2.GetComponent<Hormiga>().enMision = true;
        hormigaInstanciada2.GetComponent<Hormiga>().destinoManzana = manz;
        hormigaInstanciada2.GetComponent<Hormiga>().speedMission = speedMission;

        yield return new WaitForSeconds(tiempoEntreHormigasSpawn);
        GameObject hormigaInstanciada3 = (GameObject)Instantiate(prefabHormiga, spawnPos, Quaternion.identity);
        hormigaInstanciada3.GetComponent<Hormiga>().enMision = true;
        hormigaInstanciada3.GetComponent<Hormiga>().destinoManzana = manz;
        hormigaInstanciada3.GetComponent<Hormiga>().speedMission = speedMission;

        yield return new WaitForSeconds(tiempoEntreHormigasSpawn);

        GameObject hormigaInstanciada4 = (GameObject)Instantiate(prefabHormiga, spawnPos, Quaternion.identity);
        hormigaInstanciada4.GetComponent<Hormiga>().enMision = true;
        hormigaInstanciada4.GetComponent<Hormiga>().destinoManzana = manz;
        hormigaInstanciada4.GetComponent<Hormiga>().speedMission = speedMission;

        yield return new WaitForSeconds(tiempoEntreHormigasSpawn);
        GameObject hormigaInstanciada5 = (GameObject)Instantiate(prefabHormiga, spawnPos, Quaternion.identity);
        hormigaInstanciada5.GetComponent<Hormiga>().enMision = true;
        hormigaInstanciada5.GetComponent<Hormiga>().destinoManzana = manz;
        hormigaInstanciada5.GetComponent<Hormiga>().ultimaDeLaFila = true;
        hormigaInstanciada5.GetComponent<Hormiga>().speedMission = speedMission;




    }
}
