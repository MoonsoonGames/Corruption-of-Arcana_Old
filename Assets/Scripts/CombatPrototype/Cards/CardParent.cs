using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Spells", order = 0)]
public class CardParent : ScriptableObject
{
    [Header("General")]
    public string cardName;
    public int cardNumber;
    public string flavourDescription;
    public Sprite cardImage;

    public void CastSpell(GameObject target, AbilityManager abilityManager)
    {
        if (target.GetComponent<PlayerStats>() != null)
        {
            OnSelfCast(target, abilityManager);
        }
        else if (target.GetComponent<EnemyStats>() != null)
        {
            OnTargetCast(target, abilityManager);
        }
    }

    [Header("Self")]
    public bool self = false;

    public string selfName;
    public string selfDescription;
    public int selfCost;
    public string selfCostType;
    public Vector2Int selfHeal;
    public float selfEndTurnDelay;
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

                Debug.Log("Cast" + cardName + "on Taro");

                playerStats.ChangeHealth(heal, false);
                abilityManager.combatManager.Healing.SetActive(true);
                abilityManager.combatManager.HealingValue.text = heal.ToString();

                playerStats.ChangeMana(cost, true);
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

    [Header("Target")]
    public bool target = false;

    public string targetName;
    public string targetDescription;
    public int targetCost;
    public string targetCostType;
    public string damageType;
    public Vector2Int targetDmg;
    public Vector2Int lifeLeach;
    public int hits;
    public float hitInterval;
    public float finalHitInterval;
    public Vector2Int targetFinalDmg;
    public float targetEndTurnDelay;

    public bool targetCleave;
    public bool targetChain;
    public Vector2Int extraDmg;
    //public Status[] targetStatus;
    //public GameObject targetPrepareEffect;
    //public GameObject targetCastEffect;

    public void OnTargetCast(GameObject target, AbilityManager abilityManager)
    {
        EnemyStats enemyStats = target.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            abilityManager.MouseLeft();

            int cost = selfCost;
            if (abilityManager.playerStats.CheckMana(cost))
            {
                int dmg = (int)Random.Range(targetDmg.x, targetDmg.y);
                abilityManager.SpawnAttackEffect(abilityManager.spawnPos.position, target);

                Debug.Log("Cast" + cardName + "on " + target.name);

                if (targetChain) // chain target attack
                {
                    enemyStats.ChangeHealth(dmg, true);
                    abilityManager.combatManager.Healing.SetActive(true);
                    abilityManager.combatManager.HealingValue.text = dmg.ToString();

                    //chain code
                }
                else if (targetCleave) // cleave target attack
                {
                    enemyStats.ChangeHealth(dmg, true);
                    abilityManager.combatManager.Healing.SetActive(true);
                    abilityManager.combatManager.HealingValue.text = dmg.ToString();

                    //cleave code
                }
                else
                {
                    enemyStats.ChangeHealth(dmg, true);
                    abilityManager.combatManager.Healing.SetActive(true);
                    abilityManager.combatManager.HealingValue.text = dmg.ToString();

                    if (hits > 1)
                    {
                        for (int i = 1; i <= hits; i++)
                        {
                            Vector2 dmgVector = targetDmg;

                            if (i == hits)
                            {
                                dmgVector = targetFinalDmg;
                            }

                            abilityManager.DelayDamage(dmgVector, 0f, abilityManager.spawnPos, target, enemyStats);
                        }
                    }
                } //single target attack

                abilityManager.playerStats.ChangeMana(cost, true);
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

    public void GetReadyAbilityInfo(out bool multihit, out Vector2Int heal, out Vector2Int dmg, out string type, out string cardNameSelf, out string cardNameTarget, out bool hitsAll, out Vector2Int extradmg)
    {

        multihit = false;
        heal = new Vector2Int(0, 0);
        dmg = new Vector2Int(0, 0);
        type = "none";
        cardNameSelf = "none";
        cardNameTarget = "none";
        hitsAll = false;
        extradmg = new Vector2Int(0, 0);

        if (self)
        {
            heal = selfHeal;
            cardNameSelf = selfName;
        }
        if (target)
        {
            multihit = hits > 1;
            dmg = targetDmg * hits;
            type = damageType;
            cardNameTarget = targetName;
            hitsAll = (targetCleave || targetChain);
            extradmg = extraDmg;
        }
    }
}