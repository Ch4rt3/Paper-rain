using UnityEngine;
using UnityEngine.SceneManagement; 

public class CambiarEscenasTeclado : MonoBehaviour
{
    void Update()
    {
        // Si presionas el número 1, carga escena con índice 0 (la primera)
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(0);
        }

        // Si presionas el número 2, carga escena con índice 1 (la segunda)
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(1);
        }
    }
}