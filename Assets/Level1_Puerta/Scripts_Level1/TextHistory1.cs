using UnityEngine;
using System.Collections;
using TMPro;

public class TextHistory1 : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private bool motoYaChoco = false;
    private bool yaSeMostro = false;
    
    
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

    
    public bool VerificarSiPoemaFueLeido()
    {
        return poemaLeidoCompletamente;
    }

    
    public bool VerificarSiMotoYaChoco()
    {
        return motoYaChoco;
    }

    public string ObtenerTextoActual()
    {
    
        if (textMesh != null)
        {
            return textMesh.text.ToLower();
        }
    
        return "";
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

        
        poemaLeidoCompletamente = true;
    }
}