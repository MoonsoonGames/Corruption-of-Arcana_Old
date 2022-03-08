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

    public Transform belowAnchor, aboveAnchor;

    public void Setup(StatusParent status, int duration, bool appearAbove)
    {
        if (status != null)
        {
            button.image.sprite = status.effectIcon;
            nameText.text = status.effectName;
            desciptionText.text = status.effectDescription;
            durationText.text = duration.ToString();
        }

        if (appearAbove)
        {
            panel.transform.position = aboveAnchor.position;
        }
        else
        {
            panel.transform.position = belowAnchor.position;
        }

        PanelDisplay(false);
    }

    public void PanelDisplay(bool show)
    {
        panel.SetActive(show);
    }
}
