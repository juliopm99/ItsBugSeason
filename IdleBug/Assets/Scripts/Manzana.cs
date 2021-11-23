using UnityEngine;
using System.Collections;

public class Manzana : MonoBehaviour {
    public bool targeted = false;
    public float tiempoVida = 20f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        tiempoVida -= Time.deltaTime;
        if (!targeted&&tiempoVida<0)
        {
            Destroy(this.gameObject);
        }
	}
}
