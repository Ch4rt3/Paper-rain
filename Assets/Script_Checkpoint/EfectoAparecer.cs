using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EfectoAparecer : MonoBehaviour
{
    // Aquí arrastraremos la imagen negra en el nuevo nivel
    public Image imagenNegra; 
    public float tiempoDeTransicion = 1f; 

    void Start()
    {
        // Esto se ejecuta AUTOMÁTICAMENTE apenas carga el nivel
        if (imagenNegra != null)
        {
            StartCoroutine(QuitarPantallaNegra());
        }
    }

    IEnumerator QuitarPantallaNegra()
    {
        // Forzamos a que la imagen empiece totalmente negra (opacidad 1)
        Color colorInicio = imagenNegra.color;
        colorInicio.a = 1f;
        imagenNegra.color = colorInicio;

        float tiempo = tiempoDeTransicion;

        // Poco a poco restamos opacidad al cuadro negro
        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            
            Color colorActual = imagenNegra.color;
            colorActual.a = tiempo / tiempoDeTransicion; // Baja la opacidad gradualmente
            imagenNegra.color = colorActual;

            yield return null; 
        }

        // Al terminar, desactivamos el cuadro negro para que no estorbe ni bloquee clics
        imagenNegra.gameObject.SetActive(false);
    }
}