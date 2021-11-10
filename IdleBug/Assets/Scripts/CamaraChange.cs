using UnityEngine;
using System.Collections;

public class CamaraChange : MonoBehaviour {
    public GameObject[] Camaras;
    public int activeCam = 0;
    int maxCam;
	// Use this for initialization
	void Start () {
        maxCam = Camaras.Length;
        print(maxCam);
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
                }
            }
        }
        
    }
    void ChangeCam()
    {
        foreach (GameObject go in Camaras)
        {
            go.SetActive(false);
            if (go == Camaras[activeCam].gameObject)
            {
                go.SetActive(true);
            }
        }
    }
}
