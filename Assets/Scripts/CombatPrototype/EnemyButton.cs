using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    public void ButtonPressed(GameObject target)
    {
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
}
