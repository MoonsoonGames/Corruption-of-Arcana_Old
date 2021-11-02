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
            float damage = Random.Range(0.08f, 0.12f);

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
                float damage = Random.Range(0.1f, 0.14f);

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

    private void Flurry(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float cost = 0.5f;
            if (playerStats.CheckMana(cost))
            {
                Debug.Log("Cast Flurry on " + target.name);

                StartCoroutine(IFlurryAttacks(0.05f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.15f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.25f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.35f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.7f, targetHealth));

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

    private IEnumerator IFlurryAttacks(float delay, EnemyStats targetHealth)
    {
        yield return new WaitForSeconds(delay);

        float damage = Random.Range(0.03f, 0.05f);
        targetHealth.ChangeHeath(damage, true);
    }

    private void Firebolt(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float cost = 0.4f;
            if (playerStats.CheckMana(cost))
            {
                float damage = Random.Range(0.18f, 0.22f);

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

    private void ChillTouch(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float cost = 0.2f;
            if (playerStats.CheckMana(cost))
            {
                float damage = Random.Range(0.12f, 0.18f);

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

    private void ChainLightning(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            float cost = 0.6f;
            if (playerStats.CheckMana(cost))
            {
                EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

                string message = "Cast Chain Lightning on ";

                foreach (var item in enemies)
                {
                    float damage = Random.Range(0.14f, 0.2f);

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

    private void CureWounds(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            float cost = 0.4f;
            if (playerStats.CheckMana(cost))
            {
                float heal = Random.Range(0.23f, 0.27f);

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
