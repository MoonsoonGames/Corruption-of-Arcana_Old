using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    public StatusParent status;

    public Button button;
    public GameObject panel;
    public Text nameText;
    public Text desciptionText;
    public Text durationText;

    public void Setup(StatusParent status, int duration)
    {
        if (status != null)
        {
            button.image.sprite = status.effectIcon;
            nameText.text = status.effectName;
            desciptionText.text = status.effectDescription;
            durationText.text = duration.ToString();
        }

        PanelDisplay(false);
    }

    public void PanelDisplay(bool show)
    {
        panel.SetActive(show);
    }
}
