using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    AudioClip currentClip;
    bool main = false;

    // Start is called before the first frame update
    void Start()
    {
        BGMManager[] BGMManagers = GameObject.FindObjectsOfType<BGMManager>();

        //Debug.Log(loadSettings.Length);

        if (BGMManagers.Length > 1)
        {
            //Debug.Log("destroying");
            Destroy(this); //There is already one in the scene, delete this one
        }
        else
        {
            main = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public bool CheckMain()
    {
        if (main)
        {
            return true;
        }
        else
        {
            //Debug.Log("destroying");
            return false;
        }
    }

    public void PlayMusic(AudioClip music, float volume)
    {
        if (music != currentClip)
        {
            audioSource.clip = music;
            audioSource.volume = volume;
            audioSource.Play();
            audioSource.loop = true;
        }

        currentClip = music;
    }
}
