using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public bool playerTurn = false;
    public CombatManager combatManager;

    private string readyAbility;

    public void CastAbility(GameObject target)
    {
        if (playerTurn)
        {
            if (readyAbility != null)
            {
                gameObject.SendMessage(readyAbility, target);
            }
            else
            {
                Debug.Log("You have not readied an ability.");

                //Could cast basic attack
            }
        }
    }

    public void SetAbility(string abilityName)
    {
        readyAbility = abilityName;

        Debug.Log("Readied " + abilityName + " ability.");
    }

    private void Slash(GameObject target)
    {
        float damage = 0.1f;

        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            Debug.Log("Cast Slash on " + target.name);

            targetHealth.ChangeHeath(damage, true);

            readyAbility = null;

            if (combatManager != null)
            {
                combatManager.EndTurn(true);
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private void Firebolt(GameObject target)
    {
        float damage = 0.2f;

        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            Debug.Log("Cast Firebolt on " + target.name);

            targetHealth.ChangeHeath(damage, true);

            readyAbility = null;

            if (combatManager != null)
            {
                combatManager.EndTurn(true);
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private void CureWounds(GameObject target)
    {
        float heal = 0.2f;

        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            Debug.Log("Cast CureWounds on " + target.name);

            targetHealth.ChangeHeath(heal, false);

            readyAbility = null;

            if (combatManager != null)
            {
                combatManager.EndTurn(true);
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }
}
