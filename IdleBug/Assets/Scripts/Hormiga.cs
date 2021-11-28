using UnityEngine;
using System.Collections;

public class Hormiga : MonoBehaviour {
    public bool enMision = false;
    public bool ultimaDeLaFila = false;
    public bool volviendo;
    public GameObject wanderCenter;
    public int radioWander = 3;
    public bool conDireccion = false;
    public Vector3 destino;
    public float speed;
    public float speedMission;
    public GameObject destinoManzana;
    public Vector3 origen;
	// Use this for initialization
	void Start () {
        wanderCenter = GameObject.Find("WanderCenterHormigas");
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
                this.transform.position = Vector3.MoveTowards(this.transform.position, destino, speedMission * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, destino) < 0.2f)
                {
                    if (volviendo == false)
                    {
                        destino = VolverManzana();
                        volviendo = true;
                        transform.GetChild(0).gameObject.SetActive(true);
                        if (ultimaDeLaFila)
                        {
                            Destroy(destinoManzana);
                        }
                    }
                    else
                    {
                        if (ultimaDeLaFila)
                        {
                            GameObject.FindObjectOfType<Hormiguero>().TerminarMision();
                            GameManager.Instance.SumarManzana(GameManager.Instance.manzanasSumadasExpedicion);
                           
                        }
                        GameObject.FindObjectOfType<Hormiguero>().DesocuparHormiga();
                        Destroy(this.gameObject);
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
            transform.LookAt(destino);
        }

    }
    void InitDirecc()
    { 
        int randx = Random.Range(0, radioWander);
        int randz = Random.Range(0, radioWander);
        Ray ray = new Ray(new Vector3(wanderCenter.transform.position.x + randx, wanderCenter.transform.position.y + 10, wanderCenter.transform.position.z + randz), Vector3.down);
        RaycastHit infoHit;
        if (Physics.Raycast(ray, out infoHit))
        {
            destino = infoHit.point;
            destino.y = this.transform.position.y;
        }
        if (Random.Range(0, 11) == 2)
        {
            destino = GameObject.Find("TextoHormigas").transform.parent.transform.position;
            destino.y = this.transform.position.y;
        }
    }
    Vector3 BuscarManzana()
    {
        Vector3 posManzana = destinoManzana.transform.position;
    
        return posManzana;
    }
    Vector3 VolverManzana()
    {
        Vector3 posHormiguero = origen;
        return posHormiguero;
    }
}
