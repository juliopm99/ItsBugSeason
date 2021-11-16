using UnityEngine;
using System.Collections;

public class Panal : MonoBehaviour
{
    public int capacidadActual;
    public int poblacionActual;
    public int poblacionOcupada;
    public int poblacionMinimaEnBase = 3;
    public int poblacionMinimaMision = 1;
    public GameObject prefabAbeja;
    public bool spawneando = false;
    public GameObject textoabejas;
    public bool lleno = false;
    public GameObject[] spawnPoses;
    public float tiempoEntreAbejasSpawn = 0.5f;
    // Use this for initialization
    void Start()
    {
        textoabejas = GetComponentInChildren<TextMesh>().gameObject;
        poblacionActual = 0;
        poblacionOcupada = 0;
        capacidadActual = GameManager.Instance.capacidadAbejasPorPanal * GameManager.Instance.panalesTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (poblacionActual >= poblacionMinimaEnBase + poblacionMinimaMision && poblacionActual - poblacionOcupada >= poblacionMinimaMision/*&&!spawneando*/)
        {
            //DIFERENCIAR MANZANAS DISPONIBLES
            int random = Random.Range(0, GameObject.FindObjectsOfType<Flor>().Length);
            EmpezarMision(GameObject.FindObjectsOfType<Flor>()[random].transform.gameObject);
            spawneando = true;
            //foreach (Flor go in GameObject.FindObjectsOfType<Flor>())
            //{
            //    if (go.targeted == false)
            //    {
            //        print(go.gameObject.name);
            //        go.targeted = true;
            //        EmpezarMision( go.transform.gameObject);
            //        spawneando = true;

                //    }break;
                //}


        }
    }
    public void OcuparAbeja()
    {
        poblacionOcupada++;
    }
    public void DesocuparAbeja()
    {
        poblacionOcupada--;
    }
    public void EmpezarMision(GameObject flor)
    {
        poblacionOcupada += poblacionMinimaMision;
        int rand=0;
        if (GameManager.Instance.panalesTotal > 1)
        {
             rand = Random.Range(0, GameManager.Instance.panalesTotal-1 );
          
            if (rand > GameManager.Instance.panalesDesactivados.Length)
            {
                rand = 0;
            }
        }
       
        StartCoroutine(SpawnAbejaMision(flor,spawnPoses[rand].transform.position));
        spawneando = false;
    }
    public void TerminarMision()
    {
        //poblacionOcupada -= poblacionMinimaMision;
        //print("TERMINAMISION"+ poblacionOcupada);
    }
    public IEnumerator SpawnAbejaMision(GameObject manz,Vector3 spawnPos)
    {
        
       
        yield return new WaitForSeconds(tiempoEntreAbejasSpawn);
        GameObject abejaInstanciada = (GameObject)Instantiate(prefabAbeja,spawnPos, Quaternion.identity);
        abejaInstanciada.GetComponent<Abeja>().enMision = true;
        abejaInstanciada.GetComponent<Abeja>().destinoFlor = manz;
        abejaInstanciada.GetComponent<Abeja>().ultimaDeLaFila = true;
       
      

    }
}
