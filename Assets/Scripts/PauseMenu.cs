using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Usar si tienes TextMeshPro
using UnityEngine.UI; // Usar si tienes Text clásico (Legacy)

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Configuración de UI")]
    public TMP_Text volumeText; // Si usas texto Legacy de Unity, cámbialo a 'public Text volumeText;'
    public Slider volumeSlider; // Referencia a la barrita de volumen (Slider)

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        Time.timeScale = 1f;

        // Sincronizamos la barrita (Slider) con el volumen inicial
        if (volumeSlider != null)
        {
            // Hacemos que la barrita vaya del 0 al 10 en números enteros
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 10;
            volumeSlider.wholeNumbers = true;

            // Vinculamos el evento por código para evitar errores en el Inspector de Unity
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(SetVolumeFromSlider);

            volumeSlider.SetValueWithoutNotify(Mathf.Round(AudioListener.volume * 10f));
        }

        UpdateVolumeText(); // Actualizar el texto del volumen al inicio
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HardReset()
    {
        Time.timeScale = 1f; // Restaurar el tiempo normal
        
        // Carga la escena con el índice 0 en los Build Settings (normalmente el nivel 1 o el menú principal).
        // Si tu Nivel 1 tiene otro nombre, puedes cambiar el 0 por el nombre, por ejemplo: SceneManager.LoadScene("nivel1");
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void VolumeUp()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume + 0.1f, 0f, 1f);
        // Usamos SetValueWithoutNotify para no disparar el evento de la barrita y causar un bucle
        if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(Mathf.Round(AudioListener.volume * 10f));
        UpdateVolumeText();
    }

    public void VolumeDown()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume - 0.1f, 0f, 1f);
        if (volumeSlider != null) volumeSlider.SetValueWithoutNotify(Mathf.Round(AudioListener.volume * 10f));
        UpdateVolumeText();
    }

    // Nuevo método para que el Slider controle el volumen al arrastrarlo
    public void SetVolumeFromSlider(float value)
    {
        // Como el slider ahora va de 0 a 10, lo dividimos entre 10 para el AudioListener (que va de 0 a 1)
        AudioListener.volume = value / 10f;
        UpdateVolumeText();
    }

    private void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            // El volumen de AudioListener va de 0 a 1.
            // Lo multiplicamos por 10 para mostrar un valor del 0 al 10 en la UI.
            // (Si prefieres del 0 al 100, cambialo a * 100f)
            int volumeValue = Mathf.RoundToInt(AudioListener.volume * 10f);
            volumeText.text = volumeValue.ToString();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // Si estamos en settings, volver al pause en lugar de reanudar directamente
                if (settingsPanel != null && settingsPanel.activeSelf)
                {
                    CloseSettings();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }
}