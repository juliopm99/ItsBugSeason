using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CamaraChange : MonoBehaviour
{
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
    public LayerMask mask;
    // Use this for initialization
    void Start()
    {
        maxCam = ubicaciones.Length;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        speed = speedBase;
    }
    private static CamaraChange _instance;

    public static CamaraChange Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CamaraChange>();
            }

            return _instance;
        }
    }
    public void MoverDerecha()
    {
        GameManager.Instance.MenuClose();
        activeCam++;
        if (activeCam > maxCam - 1)
        {
            activeCam = 0;
        }
        ChangeCam();
    }
    public void MoverIzquierda()
    {
        GameManager.Instance.MenuClose();

        activeCam--;
        if (activeCam == 0)
        {
            SonidoManager.Instance.Play("HormigasFondo");
            SonidoManager.Instance.Stop("GusanosFondo");
            SonidoManager.Instance.Stop("MariposasFondo");
            SonidoManager.Instance.Stop("AbejaFondo");
        }
        else if (activeCam == 1)
        {
            SonidoManager.Instance.Play("GusanosFondo");
            SonidoManager.Instance.Stop("HormigasFondo");
            SonidoManager.Instance.Stop("MariposasFondo");
            SonidoManager.Instance.Stop("AbejaFondo");

        }
        else if (activeCam == 2)
        {
            SonidoManager.Instance.Play("MariposasFondo");
            SonidoManager.Instance.Stop("HormigasFondo");
            SonidoManager.Instance.Stop("GusanosFondo");
            SonidoManager.Instance.Stop("AbejaFondo");


        }
        else if (activeCam == 3)
        {
            SonidoManager.Instance.Stop("HormigasFondo");
            SonidoManager.Instance.Stop("GusanosFondo");
            SonidoManager.Instance.Stop("MariposasFondo");
            SonidoManager.Instance.Stop("AbejaFondo");

        }
        else if (activeCam == 4)
        {
            SonidoManager.Instance.Play("AbejaFondo");
            SonidoManager.Instance.Stop("HormigasFondo");
            SonidoManager.Instance.Stop("GusanosFondo");
            SonidoManager.Instance.Stop("MariposasFondo");
        }
        print(activeCam);
        if (activeCam < 0)
        {
            activeCam = maxCam - 1;
            print(activeCam);
        }
        ChangeCam();
    }
    public void VisionGeneral()
    {
        if (activeCam != 3)
        {
            activeCam = 3;
            ChangeCam();
        }
    }
    GameObject lastEffect;
    GameObject lastMenuBordes;
    public void SetScale(Transform obj)
    {
        obj.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void UnSetScale(Transform obj)
    {
        obj.localScale =  Vector3.one;
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray2 = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo2;
        if (Physics.Raycast(ray2, out hitInfo2, Mathf.Infinity, mask))
        {

            if (hitInfo2.transform.tag == "Edificio")
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                  
                    foreach (Transform g in hitInfo2.collider.gameObject.GetComponentsInChildren<Transform>(includeInactive: true))
                    {
                        if (g.tag == "Efecto")
                        {
                            if (lastEffect != null) lastEffect.SetActive(false);
                            g.gameObject.SetActive(true);
                            lastEffect = g.gameObject;
                        }
                    }
                }

            }
            else
            {





                if (lastMenuBordes != null) lastMenuBordes.transform.localScale = Vector3.one;

                if (lastEffect != null)
                {
                    lastEffect.SetActive(false);
                }
            }



        }
      


        if (Input.GetKeyUp(KeyCode.D))
        {
            MoverDerecha();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            MoverIzquierda();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
            {
                if (hitInfo.collider.tag == "InsectoSuelo")
                {
                    if (activeCam != 3)
                    {
                        GameObject.FindObjectOfType<InsectGenerator>().CogerInsecto(hitInfo.collider.gameObject);
                        GameManager.Instance.MenuClose();
                    }
                    else
                    {
                        GameManager.Instance.SetFeedBack("Can´t pick up bugs from here");
                    }

                }
                else if (hitInfo.collider.tag == "Edificio")
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        GameManager.Instance.MenuOpen(hitInfo.collider.gameObject);
                    }
                }
                else
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        if (GameManager.Instance.menuBlock.activeSelf || GameManager.Instance.menuCompras.activeSelf) SonidoManager.Instance.Play("BotonesUI");
                        GameManager.Instance.MenuClose();
                        print("Its over UI elements" + EventSystem.current.currentSelectedGameObject.name);
                    }
                }

            }
        }
        if (cambiando == true)
        {
            GameManager.Instance.ActVolumen();
            speed += 100 * Time.deltaTime;
            float actualD = Vector3.Distance(cam.transform.position, destino);
            float actualT = actualD / speed;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, destino, speed * Time.deltaTime);
            //cam.transform.rotation =Quaternion.Euler((actualD / distEntreCams) * diferenciaRotaciones * this.transform.rotation.eulerAngles);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, destinoR, speedRot * Time.deltaTime);
            if (Vector3.Distance(cam.transform.position, destino) < 0.3f * distEntreCams)
            {
                if (speed - Time.deltaTime * 250 > 200) speed -= Time.deltaTime * 250;

            }
            if (cam.transform.position == destino && cam.transform.rotation == destinoR)
            {
                cambiando = false;
                if (activeCam != 3)
                {
                    GameManager.Instance.DesactVolumen();
                }
                speed = speedBase;
            }
        }
    }
    public void ChangeCam()
    {
        cambiando = true;
        destino = ubicaciones[activeCam].gameObject.transform.position;
        destinoR = ubicaciones[activeCam].gameObject.transform.rotation;
        distEntreCams = Vector3.Distance(cam.transform.position, destino);
        diferenciaRotaciones = Quaternion.Angle(this.transform.rotation, destinoR);
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
