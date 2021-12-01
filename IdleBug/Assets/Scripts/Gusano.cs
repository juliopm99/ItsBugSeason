using UnityEngine;
using System.Collections;

public class Gusano : MonoBehaviour
{

    public GameObject wanderCenter;
    public int radioWander = 3;
    public bool conDireccion = false;
    public Vector3 destino;
    public float speed;

    public Vector3 origen;
    private bool parado = false;
    // Use this for initialization
    void Start()
    {
        wanderCenter = GameObject.Find("WanderCenterGusanos");
        origen = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Parado)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, origen, speed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, origen) < 0.3f)
            {

                foreach (SkinnedMeshRenderer mr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    mr.enabled = false;

                }

            }
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


        MirarDestino();
    }
    public void MirarDestino()
    {
        if (destino != Vector3.zero)
        {
            transform.LookAt(destino);
        }

    }
    public LayerMask layerMask;

    public bool Parado
    {
        get => parado; set
        {

            parado = value;
            if (parado == false)
            {
                foreach (SkinnedMeshRenderer mr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    mr.enabled = true;
                }
            }
        }
    }

    void InitDirecc()
    {
        int randx = Random.Range(0, radioWander);
        int randz = Random.Range(0, radioWander);
        Ray ray = new Ray(new Vector3(wanderCenter.transform.position.x + randx, wanderCenter.transform.position.y + 10, wanderCenter.transform.position.z + randz), Vector3.down);
        RaycastHit infoHit;
        if (Physics.Raycast(ray, out infoHit, layerMask))
        {
            destino = infoHit.point;
            destino.y = wanderCenter.transform.position.y;

        }
        if (Random.Range(0, 11) == 2)
        {
            destino = GameObject.FindObjectOfType<AlmacenSeda>().gameObject.transform.position;
            destino.y = wanderCenter.transform.position.y;
        }
        if (Random.Range(0, 11) ==1)
        {
            SpawnGus[] posSpawn = FindObjectsOfType<SpawnGus>();
            int random = Random.Range(0, posSpawn.Length);


            destino = posSpawn[random].gameObject.transform.position;
            destino.y = wanderCenter.transform.position.y;
        }
       
    }

}
