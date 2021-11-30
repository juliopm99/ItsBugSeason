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
    public static Vector3 WorldToScreenSpace(Vector3 worldPos, Camera cam, RectTransform area)
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
        screenPoint.z = 0;

        Vector2 screenPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(area, screenPoint, cam, out screenPos))
        {
            return screenPos;
        }

        return screenPoint;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        this.transform.rotation = mainCamera.transform.localRotation;
        //    Vector3 a = WorldToScreenSpace(this.transform.position, mainCamera, GameObject.Find("Canvas").GetComponent<RectTransform>());
        //    a.z = mainCamera.forward;
        //    transform.LookAt(-a-, Vector3.up);
        //transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
    }
}
