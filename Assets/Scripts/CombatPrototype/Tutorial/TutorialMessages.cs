using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessages : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;

    public Image image;

    public void ShowMessage(string title, string description)
    {
        image.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);

        if (title != null)
        {
            titleText.text = title;
        }
        descriptionText.text = description;
    }
}
