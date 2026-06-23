using UnityEngine;
using System.Collections;
using TMPro;

public class TextSave1 : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

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

    public void DispararSecuenciaFinal()
    {
        TextHistory1 textoChimenea = Object.FindAnyObjectByType<TextHistory1>();

        if (textoChimenea != null)
        {
            if (textoChimenea.VerificarSiMotoYaChoco() == false) return;

            
            textoChimenea.ApagarTexto();
        }

        
        StartCoroutine(AparecerTextoSuave());
    }

    
    private IEnumerator AparecerTextoSuave()
    {
        if (textMesh != null)
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
        }
    }
}