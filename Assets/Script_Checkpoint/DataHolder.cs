using System.Collections;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;

    // VARIABLES PARA EL CHECKPOINT
    public Vector3 posicionCheckpoint;
    public bool tieneCheckpoint = false;

    public string contrasena;
    public string contrasenaIngresada;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}