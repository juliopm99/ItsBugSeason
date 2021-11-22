using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CamaraChange : MonoBehaviour {
    public GameObject[] ubicaciones;
    GameObject cam;
    public int activeCam = 0;
    int maxCam;
    public float speedMax;
    public float speedBase;
    public float speed;
    public float speedRot;
    public bool cambiando = false;
    public Vector3 destino;
    public Quaternion destinoR;
    float distEntreCams;
    float diferenciaRotaciones;
	// Use this for initialization
	void Start () {
        maxCam = ubicaciones.Length;
        cam = FindObjectOfType<Camera>().gameObject;
        speed = speedBase;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.D))
        {
            activeCam++;
            if (activeCam > maxCam-1)
            {
                activeCam = 0;
            }
            ChangeCam();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            print(activeCam);
            activeCam--;
            print(activeCam);
            if (activeCam < 0)
            {
                activeCam = maxCam-1;
                print(activeCam);
            }
            ChangeCam();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.tag == "InsectoSuelo")
                {
                    GameObject.FindObjectOfType<InsectGenerator>().CogerInsecto(hitInfo.collider.gameObject);
                    GameManager.Instance.MenuClose();
                }
                else if (hitInfo.collider.tag == "Edificio")
                {
                    print("hola");
                    GameManager.Instance.MenuOpen(hitInfo.collider.gameObject);

                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {  GameManager.Instance.MenuClose();
                        Debug.Log("Its over UI elements");
                        print(hitInfo.collider.name);
                    }
                }
              
            }
        }
        if (cambiando == true)
        {
            speed += 100*Time.deltaTime;
            float actualD= Vector3.Distance(cam.transform.position, destino);
            float actualT = actualD / speed;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, destino, speed * Time.deltaTime);
            //cam.transform.rotation =Quaternion.Euler((actualD / distEntreCams) * diferenciaRotaciones * this.transform.rotation.eulerAngles);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, destinoR, speedRot * Time.deltaTime);
            if (Vector3.Distance(cam.transform.position, destino) < 0.3f * distEntreCams)
            {
                speed -= Time.deltaTime * 250;
            }
            if (cam.transform.position == destino && cam.transform.rotation == destinoR)
            {
                cambiando = false;
                speed = speedBase;
            }
        }
    }
    void ChangeCam()
    {
        cambiando = true;
        destino = ubicaciones[activeCam].gameObject.transform.position;
        destinoR = ubicaciones[activeCam].gameObject.transform.rotation;
        distEntreCams = Vector3.Distance(cam.transform.position, destino);
        diferenciaRotaciones = Quaternion.Angle(this.transform.rotation , destinoR);
        //foreach (GameObject go in Camaras)
        //{
        //    go.SetActive(false);
        //    if (go == Camaras[activeCam].gameObject)
        //    {
        //        go.SetActive(true);
        //    }
        //}
    }
}
