using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVariation : MonoBehaviour
{
    int currentHealth;
    public Slider slider;

    public void ApplyPreview(int maxDamage)
    {
        currentHealth = (int)slider.value;
        slider.value = currentHealth - maxDamage;
    }

    public void StopPreview(int health)
    {
        slider.value = health;
    }
}
