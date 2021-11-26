using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTexto : MonoBehaviour
{
    public float tiempoVida;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.up * speed * Time.deltaTime;
       Color colorcin= GetComponent<TextMesh>().color;
        GetComponent<TextMesh>().color = new Color(colorcin.r, colorcin.g, colorcin.b, colorcin.a /** 0.98f * Time.deltaTime*/);
        tiempoVida -= Time.deltaTime;
        if (tiempoVida < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
