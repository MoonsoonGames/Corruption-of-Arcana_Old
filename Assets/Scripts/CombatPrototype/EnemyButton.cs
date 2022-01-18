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
            Vector2Int restore;
            string selfType;
            Vector2Int dmg;
            string type;
            string cardNameSelf;
            string cardNameTarget;
            bool hitsAll;
            Vector2Int extradmg;

            ability.GetReadyAbilityInfo(out multihit, out restore, out selfType, out dmg, out type, out cardNameSelf, out cardNameTarget, out hitsAll, out extradmg);

            if (hitsAll)
            {
                EnemyButton[] buttons = GameObject.FindObjectsOfType<EnemyButton>();

                foreach (var item in buttons)
                {
                    item.sliderVarScript.ApplyPreview(extradmg);
                }
            }

            sliderVarScript.ApplyPreview(dmg);
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
