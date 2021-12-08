using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip mainMenuMusic;

    public AudioClip battleMusic;

    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic(AudioClip music, float volume)
    {

    }
}
