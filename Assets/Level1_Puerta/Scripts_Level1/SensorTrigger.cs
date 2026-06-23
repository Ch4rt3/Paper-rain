using System.Collections;
using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    [Header("Referencias de Historia")]
    public TextHistory1 textoChimenea; 
    public TextSave1 scriptTextoObstaculo; 

    [Header("Referencias de Objetos")]
    public Manija scriptManija; 
    public Puerta scriptPuerta; 
    public PuertaMovimiento scriptPuertaAnimada; // 

    [Header("Configuración de Tiempos")]
    public float tiempoVisibleTxt = 3f; 

    private bool yaPaso = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaPaso)
        {
            if (textoChimenea == null) return;

            if (textoChimenea.VerificarSiMotoYaChoco()) 
            {
                float posicionXMoto = other.transform.position.x;
                float posicionXSensor = transform.position.x;

                if (posicionXMoto < posicionXSensor)
                {
                    yaPaso = true; 
                    StartCoroutine(SecuenciaCinematicaPuerta());
                }
            }
        }
    }

    private IEnumerator SecuenciaCinematicaPuerta()
    {
        
        if (scriptTextoObstaculo != null)
        {
            scriptTextoObstaculo.DispararSecuenciaFinal();
        }

        // 2. La manija de la puerta se cae
        if (scriptManija != null)
        {
            scriptManija.ActivarCaidaDeManija();
        }

        yield return new WaitForSeconds(1f);

        
        if (scriptPuertaAnimada != null)
        {
            scriptPuertaAnimada.EjecutarApertura(); 
        }

        if (scriptPuerta != null)
        {
            scriptPuerta.AbrirPuertaDirecto(); 
        }

        
        yield return new WaitForSeconds(tiempoVisibleTxt);

        
        if (scriptTextoObstaculo != null)
        {
            TMPro.TextMeshProUGUI textMesh = scriptTextoObstaculo.GetComponent<TMPro.TextMeshProUGUI>();
            if (textMesh != null)
            {
                float tiempoEfecto = 0f;
                Color c = textMesh.color;
                while (tiempoEfecto < 1.0f)
                {
                    tiempoEfecto += Time.deltaTime;
                    c.a = Mathf.Clamp01(1.0f - (tiempoEfecto / 1.0f));
                    textMesh.color = c;
                    yield return null;
                }
                textMesh.enabled = false;
            }
        }

        
        if (scriptManija != null)
        {
            scriptManija.gameObject.SetActive(false);
        }
    }
}