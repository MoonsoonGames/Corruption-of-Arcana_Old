using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatus", menuName = "Combat/Status Effect", order = 1)]
public class StatusParent : ScriptableObject
{
    #region Status Effect Stats

    #region General

    [Header("General")]
    public string effectName;
    [TextArea(3, 10)]
    public string effectDescription;
    public Sprite effectIcon;
    public StatusParent[] alsoApply;
    public StatusParent[] alsoApplyOthers;
    public StatusParent[] alsoApplyOthersOnHit;

    #endregion

    #region Damage and Healing

    [Header("Damage To Applied Character")]
    public int applyDamage;
    public E_DamageTypes applyDamageType;
    public int turnDamage;
    public E_DamageTypes turnDamageType;

    [Header("Damage To Opponents")]
    public int applyDamageOpponents;
    public E_DamageTypes applyDamageTypeOpponents;
    public int turnDamageOpponents;
    public E_DamageTypes turnDamageTypeOpponents;

    [Header("Damage To Other Characters")]
    public int applyDamageOther;
    public E_DamageTypes applyDamageTypeOther;
    public int turnDamageOther;
    public E_DamageTypes turnDamageTypeOther;

    [Header("Healing To Applied Character")]
    public int applyHealing;
    public int applyArcana;
    public int turnHealing;
    public int turnArcana;

    #endregion

    #region Buffs and Debuffs

    [Header("Buffs")]
    public float reflectDamagePercent;
    public E_DamageTypes reflectDamageType;
    public float increaseDmgPercent;
    public float increaseDefPercent;
    public float increaseAccPercent;
    public float increaseDodgePercent;
    public float increaseFleePercent;
    public bool extraTurn;
    public bool untargettable;
    public bool immune;
    public float increasePhysResist;
    public float increaseEmberResist;
    public float increaseStaticResist;
    public float increaseBleakResist;
    public float increaseSepticResist;

    [Header("Debuffs")]
    public bool charm;
    public bool revealEntry;
    public float shareDamagePercent;
    public float reduceDmgPercent;
    public float reduceDefPercent;
    public float reduceAccPercent;
    public float reduceDodgePercent;
    public bool skipTurn;
    public bool sleepTurn;
    public bool silence;
    public float reducePhysResist;
    public float reduceEmberResist;
    public float reduceStaticResist;
    public float reduceBleakResist;
    public float reduceSepticResist;

    [Header("Debuff Others")]
    public bool charmOther;
    public bool revealEntryOther;
    public float shareDamagePercentOther;
    public float reduceDmgPercentOther;
    public float reduceDefPercentOther;
    public float reduceAccPercentOther;
    public float reduceDodgePercentOther;
    public bool skipTurnOther;
    public bool sleepTurnOther;
    public bool silenceOther;
    public float reducePhysResistOther;
    public float reduceEmberResistOther;
    public float reduceStaticResistOther;
    public float reduceBleakResistOther;
    public float reduceSepticResistOther;

    [Header("Debuff Others on Hit")]
    public bool charmOtherOnHit;
    public bool revealEntryOtherOnHit;
    public float shareDamagePercentOtherOnHit;
    public float reduceDmgPercentOtherOnHit;
    public float reduceDefPercentOtherOnHit;
    public float reduceAccPercentOtherOnHit;
    public float reduceDodgePercentOtherOnHit;
    public bool skipTurnOtherOnHit;
    public bool sleepTurnOtherOnHit;
    public bool silenceOtherOnHit;
    public float reducePhysResistOtherOnHit;
    public float reduceEmberResistOtherOnHit;
    public float reduceStaticResistOtherOnHit;
    public float reduceBleakResistOtherOnHit;
    public float reduceSepticResistOtherOnHit;

    #endregion

    #endregion

    public void OnApply(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        ApplyDamage(target, abilityManager);
    }

    public void OnTurnStart(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        TurnStartDamage(target, abilityManager);
    }

    public void OnTurnEnd(GameObject target)
    {

    }

    void ApplyDamage(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        stats.ChangeHealth(applyDamage, true, applyDamageType, out int damageTaken);

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    testStats.ChangeHealth(applyDamageOther, true, applyDamageTypeOther, out int damageTakenOther);
                }
            }

            abilityManager.combatManager.playerStats.ChangeHealth(applyDamageOpponents, true, applyDamageTypeOpponents, out int damageTakenOpponents);
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    testStats.ChangeHealth(applyDamageOpponents, true, applyDamageTypeOpponents, out int damageTakenOpponents);
                }
            }
        }
    }

    void TurnStartDamage(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        stats.ChangeHealth(turnDamage, true, turnDamageType, out int damageTaken);

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    testStats.ChangeHealth(turnDamageOther, true, turnDamageTypeOther, out int damageTakenOther);
                }
            }

            abilityManager.combatManager.playerStats.ChangeHealth(turnDamageOpponents, true, turnDamageTypeOpponents, out int damageTakenOpponents);
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    testStats.ChangeHealth(turnDamageOpponents, true, turnDamageTypeOpponents, out int damageTakenOpponents);
                }
            }
        }
    }
}
