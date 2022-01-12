using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    BGMManager manager;
    public Slider volumeSlider;

    public void Start()
    {
        manager = GameObject.FindObjectOfType<BGMManager>();
        volumeSlider.value = manager.volumeMultiplier;
    }

    public void VolumeChanged(Slider slider)
    {
        Debug.Log(slider.value);
        manager.ChangeVolume(slider.value);
    }
}
