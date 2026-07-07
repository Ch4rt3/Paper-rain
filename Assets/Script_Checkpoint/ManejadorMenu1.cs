using UnityEngine;
using UnityEngine.SceneManagement; // 💡 Obligatorio para cambiar de escena

public class ManejadorMenu1 : MonoBehaviour
{
    // 🎬 Esta función cargará la escena de la historieta
    public void IrAlCutscene()
    {
        Debug.Log("Cargando la historieta...");
        SceneManager.LoadScene("Cutscene"); // ⚠️ Pon el nombre EXACTO de tu escena de la historieta
    }
}