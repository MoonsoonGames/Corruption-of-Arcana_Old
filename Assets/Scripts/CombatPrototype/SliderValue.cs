using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider slider;

    public Color healthBarNormalColour;
    public Color healthBarHalfColour;
    public Color healthBarLowColour;
    Image sliderImage;

    private void Start()
    {
        if (slider != null)
        {
            sliderImage = slider.fillRect.GetComponent<Image>();
        }
    }

    public void HealthFlash(float healthPercentage)
    {
        if (healthPercentage < 0.35)
        {
            sliderImage.color = healthBarLowColour;
        }
        else if (healthPercentage < 0.75)
        {
            sliderImage.color = healthBarHalfColour;
        }
        else
        {
            sliderImage.color = healthBarNormalColour;
        }
    }
}
