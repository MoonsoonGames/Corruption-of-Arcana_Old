using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDescription : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text dmgText;
    public Image image;
    public Text descriptionText;

    bool cardReadied = false;

    public GameObject[] disable;

    public void ReadyCard(string cardName, string attack, Vector2 dmg, E_DamageTypes damageType, string description, Sprite sprite)
    {
        if (nameText != null)
            nameText.text = cardName;

        if (attackText != null)
            attackText.text = attack;

        if (dmgText != null)
        {
            if (dmg.y > 0)
            {
                dmgText.text = (dmg.x + "-" + dmg.y + " " + damageType.ToString() + " dmg");
            }
            else
            {
                dmgText.text = "";
            }
        }

        if (descriptionText != null)
            descriptionText.text = description;

        if (image != null)
            image.sprite = sprite;

        cardReadied = true;

        UIElements(true);
    }

    public void RemoveCard()
    {
        if (nameText != null)
            nameText.text = "Active Card";

        cardReadied = false;

        UIElements(false);
    }

    public bool GetCardReadied()
    {
        return cardReadied;
    }

    public void UIElements(bool enabled)
    {
        foreach (var item in disable)
        {
            item.SetActive(enabled);
        }
    }
}
