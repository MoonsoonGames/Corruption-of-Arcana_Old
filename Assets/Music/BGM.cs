using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip audioClip;
    public float volume = 0.22f;

    private void Start()
    {
        Invoke("PlayMusic", 0.5f);
    }

    void PlayMusic()
    {
        BGMManager.instance.PlayMusic(audioClip, volume);
    }
}
