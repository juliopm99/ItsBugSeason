using UnityEngine;
using System.Collections;

public class Flor : MonoBehaviour {
    public bool targeted = false;
    public float cd = 3f;
    public float actualCd = 0;
    public float totalSpeedBost = 0;
    public float mielPorViaje = 1;
    public float velocidadPolinizacion = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (targeted == true)
        {
            if (actualCd == 0)
            {
                actualCd = cd;
            }
            if (actualCd > 0)
            {
                actualCd -= Time.deltaTime;
                if (actualCd < 0)
                {
                    actualCd = 0;
                    targeted = false;
                }
            }
        }
	}
}
