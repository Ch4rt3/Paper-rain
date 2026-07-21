using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, lastingsfxSounds;
    public AudioSource musicSource, sfxSource, lastingsfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x=> x != null && x.name==name);

        if (s==null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip=s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x=> x != null && x.name==name);

        if (s==null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);

        }
    }
     public void PlaylastingSFX(string name)
    {
        Sound s = Array.Find(lastingsfxSounds, x=> x != null && x.name==name);

        if (s==null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            lastingsfxSource.PlayOneShot(s.clip);

        }
    }
    

}
