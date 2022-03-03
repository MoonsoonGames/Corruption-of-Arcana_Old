using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenu : MonoBehaviour
{
    public CardParent spell;

    public Text nameText;
    public Image cardImage;

    SpellMenuManager manager;

    public void Start()
    {
        manager = GetComponentInParent<SpellMenuManager>();

        nameText.text = spell.cardName;
        cardImage.sprite = spell.cardImage;

        //darken card if it has not been unlocked
        bool contains = false;

        LoadSettings loadSettings = LoadSettings.instance;

        if (loadSettings != null)
        {
            if (loadSettings.basicArcana.Contains(spell))
            {
                contains = true;
            }
            if (loadSettings.majourArcana.Contains(spell))
            {
                contains = true;
            }
            if (loadSettings.corruptedArcana.Contains(spell))
            {
                contains = true;
            }
        }

        if (contains == false)
        {
            Debug.Log("setting colour of " + spell.cardName + " to black");
            cardImage.color = new Color(0, 0, 0);

            //disable button
        }
    }

    public void ButtonPressed(bool show)
    {
        if (show)
        {
            manager.ShowCardInfo(spell);
        }
        else
        {
            manager.ShowCardInfo(null);
        }
    }
}
