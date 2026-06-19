using UnityEngine;
using System.Collections;
using TMPro;

public class TextHistory1 : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private bool motoYaChoco = false;
    private bool yaSeMostro = false;
    
    // NUEVO: Candado que asegura que el poema ya fue visto
    private bool poemaLeidoCompletamente = false;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh != null)
        {
            Color c = textMesh.color;
            c.a = 0f;
            textMesh.color = c;
            textMesh.enabled = false;
        }
    }

    public void RegistrarPrimerChoque()
    {
        motoYaChoco = true;
    }

    // NUEVA FUNCIÓN: El segundo texto revisará esto para saber si puede aparecer
    public bool VerificarSiPoemaFueLeido()
    {
        return poemaLeidoCompletamente;
    }

    public void MostrarTextoSuave()
    {
        if (motoYaChoco && !yaSeMostro && textMesh != null)
        {
            yaSeMostro = true;
            StartCoroutine(FadeIn());
        }
    }

    public void ApagarTexto()
    {
        if (textMesh != null) textMesh.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        textMesh.enabled = true;
        float tiempo = 0f;
        Color c = textMesh.color;
        while (tiempo < 1.0f)
        {
            tiempo += Time.deltaTime;
            c.a = Mathf.Clamp01(tiempo / 1.0f);
            textMesh.color = c;
            yield return null;
        }

        // Una vez que el Fade In termina y el texto es 100% visible, abrimos el candado
        poemaLeidoCompletamente = true;
    }
}