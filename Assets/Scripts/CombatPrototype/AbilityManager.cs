using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public bool playerTurn = false;
    public CombatManager combatManager;
    public PlayerStats playerStats;

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
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float damage = Random.Range(0.8f, 1.2f);

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

    private void Cleave(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();
        
        if (targetHealth != null)
        {
            float cost = 0.3f;
            if (playerStats.CheckMana(cost))
            {
                float damage = Random.Range(1f, 1.4f);

                EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

                string message = "Cast Cleave on ";

                foreach (var item in enemies)
                {
                    message += item.gameObject.name + ", ";

                    item.ChangeHeath(damage, true);
                }

                Debug.Log(message);

                playerStats.ChangeMana(cost, true);

                readyAbility = null;

                if (combatManager != null)
                {
                    combatManager.EndTurn(true);
                }
            }
            else
            {
                Debug.Log("Insufficient Mana");
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private void Firebolt(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float cost = 0.4f;
            if (playerStats.CheckMana(cost))
            {
                float damage = Random.Range(1.8f, 2.2f);

                Debug.Log("Cast Firebolt on " + target.name);

                targetHealth.ChangeHeath(damage, true);

                playerStats.ChangeMana(cost, true);

                readyAbility = null;

                if (combatManager != null)
                {
                    combatManager.EndTurn(true);
                }
            }
            else
            {
                Debug.Log("Insufficient Mana");
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private void CureWounds(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            float cost = 0.4f;
            if (playerStats.CheckMana(cost))
            {
                float heal = Random.Range(2.3f, 2.7f);

                Debug.Log("Cast CureWounds on " + target.name);

                targetHealth.ChangeHeath(heal, false);

                playerStats.ChangeMana(cost, true);

                readyAbility = null;

                if (combatManager != null)
                {
                    combatManager.EndTurn(true);
                }
            }
            else
            {
                Debug.Log("Insufficient Mana");
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }
}
