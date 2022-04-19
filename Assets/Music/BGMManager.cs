using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    AudioClip currentClip;

    float baseVolume;
    float currentVolume;
    public float volumeMultiplier = 0.3f;

    void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static BGMManager instance = null;

    void Singleton()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void PlayMusic(AudioClip music, float volume)
    {
        if (music != currentClip)
        {
            audioSource.clip = music;
            audioSource.volume = volume * volumeMultiplier;
            audioSource.Play();
            audioSource.loop = true;
        }

        baseVolume = volume;
        currentClip = music;
    }

    public void ChangeVolume(float newVolumeMultiplier)
    {
        volumeMultiplier = newVolumeMultiplier;

        currentVolume = volumeMultiplier * baseVolume;

        audioSource.volume = currentVolume;
    }

    public void PlaySoundEffect(AudioClip soundEffect, float volume)
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect, volume);
        }
    }
}