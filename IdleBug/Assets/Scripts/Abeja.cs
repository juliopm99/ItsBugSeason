using UnityEngine;
using System.Collections;

public class Abeja : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
        wanderCenter = GameObject.Find("WanderCenterAbejas");
        origen = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
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
                if (Vector3.Distance(this.transform.position, destino) < 0.6f)
                {
                    if (volviendo == false)
                    {
                        destino = VolverManzana();
                        volviendo = true;
                        //if (ultimaDeLaFila)
                        //{
                        //    Destroy(destinoFlor);
                        //}
                    }
                    else
                    {
                        if (ultimaDeLaFila)
                        {
                            GameObject.FindObjectOfType<Panal>().TerminarMision();
                            GameManager.Instance.SumarMiel((int)GameManager.Instance.mielSumadaExpedicion);
                           
                        }
                        GameObject.FindObjectOfType<Panal>().DesocuparAbeja();
                        Destroy(this.gameObject);
                    }
                  
                }
            }

        }
    }
    void InitDirecc()
    { 
        int randx = Random.Range(0, radioWander);
        int randy = Random.Range(0, radioWander);
        int randz = Random.Range(0, radioWander);
       Vector3 destinyPos=new Vector3(wanderCenter.transform.position.x + randx, wanderCenter.transform.position.y + randy, wanderCenter.transform.position.z + randz);
     
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
