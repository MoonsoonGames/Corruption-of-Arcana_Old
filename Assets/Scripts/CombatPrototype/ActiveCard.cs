using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCard : MonoBehaviour
{
    public Text cardText;
    public Text spreadText;
    public Text valueText;
    public Text typeText;
    public Text costText;
    public Text costTypeText;
    public Text descriptionText;

    public Image image;

    bool cardReadied = false;

    public GameObject[] disable;

    Camera cam;

    private void Start()
    {
        UIElements(false);

        cam = GameObject.FindObjectOfType<Camera>();
    }

    public void ReadyCard(string cardName, string spreadCard, Vector2Int dmg, string type, int cost, string description, string costType)
    {
        if (cardText != null)
            cardText.text = cardName;

        if (spreadText != null)
            spreadText.text = spreadCard;

        if (valueText != null)
            valueText.text = (dmg.x + "-" + dmg.y);

        if (typeText != null)
            typeText.text = type;

        if (costText != null)
            costText.text = cost.ToString();

        if (descriptionText != null)
            descriptionText.text = description;

        if (costTypeText != null)
            costTypeText.text = costType;

        cardReadied = true;

        UIElements(true);
    }

    public void CastCard()
    {
        if (cardText != null)
            cardText.text = "Active Card";

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

    /* //Not working
    private void Update()
    {
        if (cardReadied && cam != null)
        {
            this.gameObject.GetComponent<RectTransform>().position = cam.ScreenToViewportPoint(Input.mousePosition) + new Vector3(6500f, 7200f, 1f);
        }
    }
    */
}
