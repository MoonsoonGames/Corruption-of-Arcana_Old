using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public bool playerTurn = false;
    public CombatManager combatManager;
    public PlayerStats playerStats;

    private string readyAbility;
    public Text cardText;

    public float popupDuration = 5f;

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

        if (cardText != null)
            cardText.text = readyAbility;

        Debug.Log("Readied " + abilityName + " ability.");
    }

    public void ResetAbility()
    {
        readyAbility = null;

        if (cardText != null)
            cardText.text = null;
    }

    private void Slash(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            int damage = Random.Range(8, 12);

            Debug.Log("Cast Slash on " + target.name);

            targetHealth.ChangeHeath(damage, true);
            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = damage.ToString();
            

            ResetAbility();

            StartCoroutine(IEndTurn(0.2f));
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private void CriticalSlash(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            int damage = Random.Range(25, 35);

            Debug.Log("Cast Slash on " + target.name + ". It's a critical hit!");

            targetHealth.ChangeHeath(damage, true);
            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = damage.ToString();

            ResetAbility();

            StartCoroutine(IEndTurn(0.2f));
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
            int cost = 30;
            if (playerStats.CheckMana(cost))
            {
                int damage = Random.Range(10, 14);

                EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

                string message = "Cast Cleave on ";

                foreach (var item in enemies)
                {
                    message += item.gameObject.name + ", ";

                    item.ChangeHeath(damage, true);
                    combatManager.Dmg.SetActive(true);
                    combatManager.DmgValue.text = damage.ToString();

                }

                Debug.Log(message);

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
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
            int cost = 50;
            if (playerStats.CheckMana(cost))
            {
                Debug.Log("Cast Flurry on " + target.name);

                StartCoroutine(IFlurryAttacks(0.05f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.15f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.25f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.35f, targetHealth));
                StartCoroutine(IFlurryAttacks(0.7f, targetHealth));

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(1f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
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

        int damage = Random.Range(3, 5);
        targetHealth.ChangeHeath(damage, true);
        combatManager.Dmg.SetActive(true);
        combatManager.DmgValue.text = damage.ToString();
    }

    private void Firebolt(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            int cost = 40;
            if (playerStats.CheckMana(cost))
            {
                int damage = Random.Range(18, 22);

                Debug.Log("Cast Firebolt on " + target.name);

                targetHealth.ChangeHeath(damage, true);
                combatManager.Dmg.SetActive(true);
                combatManager.DmgValue.text = damage.ToString();

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
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
            int cost = 20;
            if (playerStats.CheckMana(cost))
            {
                int damage = Random.Range(12, 18);

                Debug.Log("Cast Chill Touch on " + target.name);

                targetHealth.ChangeHeath(damage, true);
                combatManager.Dmg.SetActive(true);
                combatManager.DmgValue.text = damage.ToString();

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
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
            int cost = 60;
            if (playerStats.CheckMana(cost))
            {
                EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

                string message = "Cast Chain Lightning on ";

                foreach (var item in enemies)
                {
                    int damage = Random.Range(12, 20);

                    message += item.gameObject.name + ", ";

                    item.ChangeHeath(damage, true);
                    combatManager.Dmg.SetActive(true);
                    combatManager.DmgValue.text = damage.ToString();
                }

                Debug.Log(message);

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
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
            int cost = 40;
            if (playerStats.CheckMana(cost))
            {
                int heal = Random.Range(24, 30);

                Debug.Log("Cast CureWounds on " + target.name);

                targetHealth.ChangeHeath(heal, false);
                combatManager.Healing.SetActive(true);
                combatManager.HealingValue.text = heal.ToString();

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                StartCoroutine(IRemovePopup(popupDuration));
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    public void HealingPotion(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            int cost = 1;
            if (playerStats.CheckPotions(cost))
            {
                int heal = Random.Range(30, 46);

                Debug.Log("Cast CureWounds on " + target.name);

                targetHealth.ChangeHeath(heal, false);
                combatManager.Healing.SetActive(true);
                combatManager.HealingValue.text = heal.ToString();

                playerStats.ChangePotions(cost, true);

                ResetAbility();

                StartCoroutine(IEndTurn(0.2f));
            }
            else
            {
                Debug.Log("Insufficient Potions");
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    private IEnumerator IEndTurn(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (combatManager != null)
        {
            combatManager.EndTurn(true);
        }
    }

    private IEnumerator IRemovePopup(float delay)
    {
        yield return new WaitForSeconds(delay);

        combatManager.noMana.SetActive(false);
    }
}
