using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public bool manita = false;
    public Texture2D mano;
    public Texture2D flecha;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.SetCursor(flecha, new Vector2(118/32, 30/32), CursorMode.Auto);
    }
    private static Cursor _instance;

    public static Cursor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Cursor>();
            }

            return _instance;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SonidoManager.Instance.Play("ClickUI");
            }
        }
    }
    public void SetCursor(bool esMano)
    {
        if (esMano)
        {
            manita = true;
            UnityEngine.Cursor.SetCursor(mano, new Vector2(188/32, 46/32), CursorMode.Auto);

        }
        else
        {
            manita = false;
            UnityEngine.Cursor.SetCursor(flecha, new Vector2(118/32, 30/32), CursorMode.Auto);
           
        }
    }
    public void InvertirCursor()
    {
        if (manita)
        {
            UnityEngine.Cursor.SetCursor(flecha, new Vector2(118 / 32, 30 / 32), CursorMode.Auto);
        }
        else
        {
            UnityEngine.Cursor.SetCursor(mano, new Vector2(188 / 32, 46 / 32), CursorMode.Auto);
        }
    }

}
