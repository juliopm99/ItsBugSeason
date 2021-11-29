using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBillboard : MonoBehaviour {

    Camera mainCamera;
    private Quaternion originalRotation;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;  originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.rotation = mainCamera.transform.rotation; ;
       
    }
}
