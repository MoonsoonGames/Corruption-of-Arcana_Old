using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCard : MonoBehaviour
{
    public Text cardText;
    public Text valueText;
    public Text typeText;
    public Text descriptionText;

    bool cardReadied = false;

    public GameObject[] disable;

    Camera cam;

    private void Start()
    {
        UIElements(false);

        cam = GameObject.FindObjectOfType<Camera>();
    }

    public void ReadyCard(string cardName)
    {
        if (cardText != null)
            cardText.text = cardName;

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
