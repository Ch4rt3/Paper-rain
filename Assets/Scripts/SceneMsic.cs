using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(musicName);
    }
}