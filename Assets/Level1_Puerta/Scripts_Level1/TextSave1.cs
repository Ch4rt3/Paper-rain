using UnityEngine;
using System.Collections;
using TMPro;

public class TextSave1 : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Rigidbody2D rb;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();

        if (textMesh != null) textMesh.enabled = false;
        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void DispararSecuenciaFinal()
    {
        TextHistory1 textoChimenea = Object.FindFirstObjectByType<TextHistory1>();

        if (textoChimenea != null)
        {
            if (textoChimenea.VerificarSiPoemaFueLeido() == false) return;
            textoChimenea.ApagarTexto();
        }

        StartCoroutine(RutinaInicial());
    }

    private IEnumerator RutinaInicial()
    {
        if (textMesh != null) textMesh.enabled = true;

        yield return new WaitForSeconds(1f);

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Cae el texto
        }
    }
}