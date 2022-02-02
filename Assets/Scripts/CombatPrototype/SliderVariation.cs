using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVariation : MonoBehaviour
{
    int currentHealth;
    Vector2 previewRange;
    public Slider slider;
    
    bool preview = false;
    float time = 0;
    bool reverse = false;

    public void ApplyPreview(Vector2 newPreviewRange)
    {
        currentHealth = (int)slider.value;
        previewRange = newPreviewRange;

        preview = true;
        InvokeRepeating("Preview", 0f, 0.05f);
    }

    public void StopPreview()
    {
        if (preview)
        {
            CancelInvoke();
            preview = false;
            slider.value = currentHealth;
            previewRange = new Vector2(0, 0);
            time = 0;
        }
    }

    void Preview()
    {
        slider.value = Mathf.Lerp(currentHealth - previewRange.x, currentHealth - previewRange.y, time);

        if (time >= 1)
        {
            reverse = true;
        }
        else if (time <= 0)
        {
            reverse = false;
        }

        if (!reverse)
        {
            time += 0.05f;
        }
        else
        {
            time -= 0.05f;
        }
    }
}
