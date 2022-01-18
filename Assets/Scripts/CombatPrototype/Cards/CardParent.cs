using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Spells", order = 0)]
public class CardParent : ScriptableObject
{
    #region General

    [Header("General")]
    public string cardName;
    public int cardNumber;
    [TextArea(3, 10)]
    public string flavourDescription;
    public Sprite cardImage;

    public void CastSpell(GameObject target, AbilityManager abilityManager)
    {
        Debug.Log("Self is " + selfInterpretationUnlocked + " || Target is " + target);
        if (target.GetComponent<PlayerStats>() != null && selfInterpretationUnlocked)
        {
            OnSelfCast(target, abilityManager);
        }
        else if (target.GetComponent<EnemyStats>() != null && targetInterpretationUnlocked)
        {
            OnTargetCast(target, abilityManager);
        }
    }

    #endregion

    #region Self

    [Header("Self")]
    public bool selfInterpretationUnlocked = false;

    public string selfName;
    [TextArea(3, 10)]
    public string selfDescription;
    public int selfCost;
    public string selfCostType;
    public Vector2Int selfHeal;
    public Vector2Int selfAP;
    public float selfEndTurnDelay = 0.2f;
    //public Status[] selfStatus;
    //public GameObject selfPrepareEffect;
    //public GameObject selfCastEffect;

    public void OnSelfCast(GameObject target, AbilityManager abilityManager)
    {
        PlayerStats playerStats = target.GetComponent<PlayerStats>();
        if (playerStats != null && abilityManager != null)
        {
            abilityManager.MouseLeft();

            int cost = selfCost;
            if (abilityManager.playerStats.CheckMana(cost))
            {
                int heal = (int)Random.Range(selfHeal.x, selfHeal.y);
                int mana = (int)Random.Range(selfAP.x, selfAP.y);

                Debug.Log("Cast" + cardName + "on Taro");

                playerStats.ChangeHealth(heal, false);
                abilityManager.combatManager.Healing.SetActive(true);
                abilityManager.combatManager.HealingValue.text = heal.ToString();

                playerStats.ChangeMana(cost - mana, true);
                abilityManager.combatManager.Ap.SetActive(true);
                abilityManager.combatManager.ApValue.text = cost.ToString();

                abilityManager.ResetAbility();

                abilityManager.EndTurn(selfEndTurnDelay);
            }
            else
            {
                abilityManager.combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                abilityManager.RemovePopup(selfEndTurnDelay + 5f);
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    #endregion

    #region Target

    [Header("Target")]
    public bool targetInterpretationUnlocked = false;

    public string targetName;
    [TextArea(3, 10)]
    public string targetDescription;
    public int targetCost;
    public string targetCostType;
    public string damageType;
    public Vector2Int targetDmg;
    public Vector2Int lifeLeach;
    public bool execute;
    public int hits;
    public float hitInterval;
    public float finalHitInterval;
    public Vector2Int targetFinalDmg;
    public float targetEndTurnDelay = 0.2f;

    public bool targetCleave;
    public bool targetChain;
    public Vector2Int extraDmg;
    //public Status[] targetStatus;
    //public float statusChance;
    //public GameObject targetPrepareEffect;
    //public GameObject targetCastEffect;

    public void OnTargetCast(GameObject target, AbilityManager abilityManager)
    {
        EnemyStats enemyStats = target.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            int cost = targetCost;
            if (abilityManager.playerStats.CheckMana(cost))
            {
                abilityManager.MouseLeft();

                Debug.Log("Cast" + cardName + "on " + target.name);

                if (targetChain)
                {
                    string message = "Cast Chain Lightning on ";
                    abilityManager.multihitMax = abilityManager.combatManager.enemyManager.enemies.Count;

                    foreach (var item in abilityManager.combatManager.enemyManager.enemies)
                    {
                        EnemyStats itemHealth = item.gameObject.GetComponent<EnemyStats>();
                        message += item.gameObject.name + ", ";

                        if (item.gameObject == target)
                        {
                            abilityManager.DelayDamage(targetDmg, 0f, abilityManager.spawnPos, target, itemHealth);
                        }
                        else
                        {
                            abilityManager.DelayDamage(extraDmg, 0.25f, target.transform, item.gameObject, itemHealth);
                        }
                    }

                    abilityManager.combatManager.Dmg.SetActive(true);
                    abilityManager.combatManager.DmgValue.text = abilityManager.multihitTally.ToString();

                    abilityManager.multihitTally = 0;

                    Debug.Log(message);
                } //chain target attack
                else if (targetCleave)
                {
                    string message = "Cast Chain Lightning on ";
                    abilityManager.multihitMax = abilityManager.combatManager.enemyManager.enemies.Count;

                    foreach (var item in abilityManager.combatManager.enemyManager.enemies)
                    {
                        EnemyStats itemHealth = item.gameObject.GetComponent<EnemyStats>();
                        message += item.gameObject.name + ", ";

                        if (item.gameObject == target)
                        {
                            abilityManager.DelayDamage(targetDmg, 0f, abilityManager.spawnPos, item.gameObject, itemHealth);
                        }
                        else
                        {
                            abilityManager.DelayDamage(extraDmg, 0.1f, abilityManager.spawnPos, item.gameObject, itemHealth);
                        }
                    }

                    abilityManager.combatManager.Dmg.SetActive(true);
                    abilityManager.combatManager.DmgValue.text = abilityManager.multihitTally.ToString();

                    abilityManager.multihitTally = 0;

                    Debug.Log(message);
                }  //cleave target attack
                else
                {
                    abilityManager.multihitMax = hits;
                    abilityManager.combatManager.Dmg.SetActive(true);

                    if (hits > 1)
                    {
                        for (int i = 1; i < hits; i++)
                        {
                            Vector2 dmgVector = targetDmg;
                            float hitTime = i * hitInterval;

                            abilityManager.DelayDamage(dmgVector, hitTime, abilityManager.spawnPos, target, enemyStats);
                        }

                        Vector2 dmgVectorFinal = targetFinalDmg;
                        float hitTimeFinal = (hits * hitInterval) + finalHitInterval;

                        abilityManager.DelayDamage(dmgVectorFinal, hitTimeFinal, abilityManager.spawnPos, target, enemyStats);
                    }
                    else
                    {
                        Vector2 dmgVector = targetDmg;

                        abilityManager.DelayDamage(dmgVector, 0f, abilityManager.spawnPos, target, enemyStats);
                    }
                } //single target attack

                abilityManager.playerStats.ChangeMana(cost, true);
                abilityManager.combatManager.Ap.SetActive(true);
                abilityManager.combatManager.ApValue.text = cost.ToString();

                abilityManager.ResetAbility();

                abilityManager.EndTurn(targetEndTurnDelay);
            }
            else
            {
                abilityManager.combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                abilityManager.RemovePopup(selfEndTurnDelay + 5f);
            }
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }
    }

    #endregion

    #region Helper Functions

    public Vector2Int TotalDmgRange()
    {
        if (hits > 1)
        {
            return (targetDmg * (hits - 1)) + (targetFinalDmg);
        }
        else
        {
            return targetDmg;
        }
    }

    public string RestoreType()
    {
        if (selfHeal != new Vector2Int(0, 0) && selfAP != new Vector2Int(0, 0))
        {
            return "Healing & Arcana";
        }
        else if (selfHeal != new Vector2Int(0, 0))
        {
            return "Healing";
        }
        else if (selfAP != new Vector2Int(0, 0))
        {
            return "Arcana";
        }
        else
        {
            return " ";
        }
    }

    public Vector2Int RestoreValue()
    {
        return selfHeal + selfAP;
    }

    public void GetReadyAbilityInfo(out bool multihit, out Vector2Int restore, out string selfType, out Vector2Int dmg, out string type, out string cardNameSelf, out string cardNameTarget, out bool hitsAll, out Vector2Int extradmg)
    {

        multihit = false;
        restore = new Vector2Int(0, 0);
        selfType = "none";
        dmg = new Vector2Int(0, 0);
        type = "none";
        cardNameSelf = "none";
        cardNameTarget = "none";
        hitsAll = false;
        extradmg = new Vector2Int(0, 0);

        if (selfInterpretationUnlocked)
        {
            if (selfHeal != new Vector2Int(0, 0) && selfAP != new Vector2Int(0, 0))
            {
                restore = selfHeal + selfAP;
                selfType = "Healing & Arcana";
            }
            else if (selfHeal != new Vector2Int(0, 0))
            {
                restore = selfHeal;
                selfType = "Healing";
            }
            else if (selfAP != new Vector2Int(0, 0))
            {
                restore = selfAP;
                selfType = "Arcana";
            }
            cardNameSelf = selfName;
        }
        if (targetInterpretationUnlocked)
        {
            multihit = hits > 1;
            if (multihit)
            {
                dmg = (targetDmg * (hits - 1)) + (targetFinalDmg);
            }
            else
            {
                dmg = targetDmg;
            }

            type = damageType;
            cardNameTarget = targetName;
            hitsAll = (targetCleave || targetChain);
            extradmg = extraDmg;
        }
    }

    #endregion
}