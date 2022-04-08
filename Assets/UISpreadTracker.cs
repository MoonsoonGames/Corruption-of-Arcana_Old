using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpreadTracker : MonoBehaviour
{
    public CardParent spell;

    [Header("Main Card")]
    public Text card1NameText;
    public Image card1Image;

    [Header("Spread Card")]
    public Text card2NameText;
    public Image card2Image;

    SpellMenuManager manager;

    public void Start()
    {
        manager = GetComponentInParent<SpellMenuManager>();

        card1NameText.text = spell.cardName;
        card1Image.sprite = spell.cardImage;


        card2NameText.text = spell.comboCard.cardName;
        card2Image.sprite = spell.comboCard.cardImage;
    }
}
