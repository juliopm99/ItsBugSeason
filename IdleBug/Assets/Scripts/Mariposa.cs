using UnityEngine;
using System.Collections;

public class Mariposa : MonoBehaviour
{

    public GameObject wanderCenter;
    public int radioWander = 3;
    public bool conDireccion = false;
    public Vector3 destino;
    public float speed;

    public Vector3 origen;
    public bool parado = false;
    // Use this for initialization
    void Start()
    {
        wanderCenter = GameObject.Find("WanderCenterMariposas");
        origen = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (parado)
        {

        }
        else
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


    }
    public LayerMask layerMask;
    void InitDirecc()
    {
        int randx = Random.Range(0, radioWander);
        int randy = Random.Range(0, radioWander);
        int randz = Random.Range(0, radioWander);
        Vector3 destinyPos = new Vector3(wanderCenter.transform.position.x + randx, wanderCenter.transform.position.y + randy, wanderCenter.transform.position.z + randz);
        int random = Random.Range(0, 20);
        if (random == 0)
        {
            destino = GameObject.Find("TextoMariposas").transform.parent.transform.position;
        }
        else if (random < 5)
        {
            Vector3 destino2Pos = new Vector3(this.transform.position.x + randx, this.transform.position.y + randy, this.transform.position.z + randz);
            destino = destino2Pos;

        }
        else if (random < 10)
        {
            int random1 = 2;
            if (GameObject.FindObjectOfType<FlorB>() != null)
            {
                random1 = Random.Range(0, 2);
            }
            else
            {

            }

            if (random1 == 2)
            {
                int randome = Random.Range(0, GameObject.FindObjectsOfType<Flor>().Length-1);

                destino = GameObject.FindObjectsOfType<Flor>()[randome].transform.position;
            }
            else
            {
                int randome = Random.Range(0, GameObject.FindObjectsOfType<FlorB>().Length-1);

                destino = GameObject.FindObjectsOfType<FlorB>()[randome].transform.position;
            }

        }
        else
        {
            destino = destinyPos;
        }
    }

}
