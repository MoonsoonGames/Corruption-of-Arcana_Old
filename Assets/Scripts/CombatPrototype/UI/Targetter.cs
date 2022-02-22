using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targetter : MonoBehaviour
{
    EnemyStats enemy;

    public GameObject[] corners;
    public GameObject[] arrows;
    public Image resistanceIcon;
    public Sprite positiveResistance, negativeResistance;

    public bool ally = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyStats>();
        SetVisibility(false, null);
    }

    public void SetVisibility(bool visible, CardParent spell)
    {
        foreach (var item in corners)
        {
            if (item != null)
            {
                item.SetActive(visible);
            }
        }

        if (visible && spell != null && enemy != null)
        {
            float resistance = enemy.CheckResistances(spell.damageType);

            if (resistance >= 1.1)
            {
                //vulnerable to damage
                resistanceIcon.sprite = negativeResistance;
                resistanceIcon.enabled = true;
            }
            else if (resistance <= 0.9)
            {
                //resistant to damage
                resistanceIcon.sprite = positiveResistance;
                resistanceIcon.enabled = true;
            }
            else 
            {
                //ambivalent to damage
                resistanceIcon.enabled = false;
            }

        }
        else if (resistanceIcon != null)
        {
            resistanceIcon.enabled = false;
        }
    }
}
