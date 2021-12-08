using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    public SliderVariation sliderVarScript;

    public void ButtonPressed(GameObject target)
    {
        MouseLeft();

        AbilityManager ability = GameObject.Find("Player").GetComponent<AbilityManager>();

        if (ability != null)
        {
            ability.CastAbility(target);
        }
        else
        {
            Debug.Log("Error, no player reference found");
        }
    }

    public void ButtonHovered(GameObject target)
    {
        //Debug.Log("Button hovering");
        AbilityManager ability = GameObject.Find("Player").GetComponent<AbilityManager>();

        if (ability != null)
        {
            bool multihit;
            Vector2 dmg;

            ability.GetReadyAbilityInfo(out multihit, out dmg);

            if (multihit)
            {
                EnemyButton[] buttons = GameObject.FindObjectsOfType<EnemyButton>();

                foreach (var item in buttons)
                {
                    item.sliderVarScript.ApplyPreview(dmg);
                }
            }
            else
            {
                sliderVarScript.ApplyPreview(dmg);
            }
        }
        else
        {
            Debug.Log("Error, no player reference found");
        }
    }

    public void MouseLeft()
    {
        //Debug.Log("Button stop hovering");
        EnemyButton[] buttons = GameObject.FindObjectsOfType<EnemyButton>();

        foreach (var item in buttons)
        {
            item.sliderVarScript.StopPreview();
        }
    }
}
