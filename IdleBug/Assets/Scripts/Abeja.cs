using UnityEngine;
using System.Collections;

public class Abeja : MonoBehaviour
{
    public bool enMision = false;
    public bool ultimaDeLaFila = false;
    public bool volviendo;
    public GameObject wanderCenter;
    public int radioWander = 3;
    public bool conDireccion = false;
    public Vector3 destino;
    public float speed;
    public float speedMission;
    public GameObject destinoFlor;
    public Vector3 origen;
    public float tiempoPolinizando;
    public float tiempoDescanso;
    // Use this for initialization
    void Start()
    {
        wanderCenter = GameObject.Find("WanderCenterAbejas");
        origen = this.transform.position;
    }
 
    // Update is called once per frame
    void Update()
    {
      
        if (enMision == false)
        {
            if (conDireccion == false)
            {
                conDireccion = true;
                InitDirecc();
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, destino, speed * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, destino) < 0.3f)
                {
                    InitDirecc();
                }
            }
        }
        else
        {
            if (conDireccion == false)
            {
                conDireccion = true;
                destino = BuscarManzana();
            }
            else
            {
                int randx = Random.Range(0, radioWander);
                int randy = Random.Range(0, radioWander);
                int randz = Random.Range(0, radioWander);
                Vector3 desviacion = new Vector3(randx, randy, randz);
                this.transform.position = Vector3.MoveTowards(this.transform.position, destino, speedMission * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, destino) < 0.2f)
                {
                    if (volviendo == false)
                    {
                        if (tiempoPolinizando > 0)
                        {
                            tiempoPolinizando -= Time.deltaTime;
                            if (tiempoPolinizando <= 0)
                            {
                                if (CamaraChange.Instance.activeCam == 4) SonidoManager.Instance.Play("AbejaAccion");
                                transform.GetChild(0).gameObject.SetActive(true);
                                destino = VolverManzana();
                                volviendo = true;
                            }
                        }

                      
                    }

                    else
                    {
                        if (GetComponentInChildren<MeshRenderer>().enabled)
                        {
                           foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                            {
                                mr.enabled = false;
                            }
                            GameManager.Instance.SumarMiel((int)GameManager.Instance.mielSumadaExpedicion);
                        }
                        tiempoDescanso -= Time.deltaTime;
                        if (ultimaDeLaFila && tiempoDescanso <= 0)
                        {
                          
                            GameObject.FindObjectOfType<Panal>().DesocuparAbeja();
                            Destroy(this.gameObject);
                        }


                    }

                }
            }

        }
        MirarDestino();
    }
    public void MirarDestino()
    {
        if (destino != Vector3.zero)
        {
            Vector3 destinazo = new Vector3(destino.x, this.transform.position.y, destino.z);
            transform.LookAt(destinazo);
        }

    }
    void InitDirecc()
    {
        int randx = Random.Range(0, radioWander);
        int randy = Random.Range(0, radioWander);
        int randz = Random.Range(0, radioWander);
        Vector3 destinyPos = new Vector3(wanderCenter.transform.position.x + randx, wanderCenter.transform.position.y + randy, wanderCenter.transform.position.z + randz);

        if (Random.Range(0, 11) == 2)
        {
            destino = GameObject.Find("TextoAbejas").transform.parent.transform.position;
        }
        else
        {
            destino = destinyPos;
        }
    }
    Vector3 BuscarManzana()
    {
        Vector3 posManzana = destinoFlor.transform.position;

        return posManzana;
    }
    Vector3 VolverManzana()
    {
        Vector3 posHormiguero = origen;
        return posHormiguero;
    }
}
