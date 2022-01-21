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

    [Header("General Damage and Healing")]
    public Object healFX;
    public Object damageFX;

    [Header("Damage To Applied Character")]
    public Vector2Int applyDamage;
    public E_DamageTypes applyDamageType;
    public Vector2Int turnDamage;
    public E_DamageTypes turnDamageType;

    [Header("Damage To Opponents")]
    public Vector2Int applyDamageOpponents;
    public E_DamageTypes applyDamageTypeOpponents;
    public Vector2Int turnDamageOpponents;
    public E_DamageTypes turnDamageTypeOpponents;

    [Header("Damage To Other Characters")]
    public Vector2Int applyDamageOther;
    public E_DamageTypes applyDamageTypeOther;
    public Vector2Int turnDamageOther;
    public E_DamageTypes turnDamageTypeOther;

    [Header("Healing To Applied Character")]
    public Vector2Int applyHealing;
    public Vector2Int applyArcana;
    public Vector2Int turnHealing;
    public Vector2Int turnArcana;

    #endregion

    #region Buffs and Debuffs
    
    [Header("Buffs")]
    public Vector2Int reflectDamage;
    public E_DamageTypes reflectDamageType;
    public E_CombatEffectSpawn reflectDamageSpawner;

    /*
    public bool extraTurn;
    public bool untargettable;
    public bool immune;
    */

    [Header("Stat Adjustments")]
    public float adjustPhysMultiplier;
    public float adjustEmberMultiplier;
    public float adjustStaticMultiplier;
    public float adjustBleakMultiplier;
    public float adjustSepticMultiplier;

    /*
    public float adjustDmgPercent;
    public float adjustDefPercent;
    public float adjustAccPercent;
    public float adjustDodgePercent;
    public float adjustFleePercent;
    */

    /*
    [Header("Debuffs")]
    public bool charm;
    public bool revealEntry;
    public bool skipTurn;
    public bool sleepTurn;
    public bool silence;

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
    */
    #endregion

    #endregion

    #region Effects

    #region Apply and Remove Status

    public void OnApply(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        ApplyDamage(target, abilityManager);
        ApplyHealing(target);

        ApplyStatAdjustments(target);
    }

    public void OnRemove(GameObject target)
    {
        RemoveStatAdjustments(target);
    }

    #endregion

    #region Turns

    public void OnTurnStart(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        TurnStartDamage(target, abilityManager);
        TurnStartHealing(target);
    }

    public void OnTurnEnd(GameObject target)
    {

    }

    #endregion

    #region Take Damage

    public void OnTakeDamage(GameObject attacker, AbilityManager abilityManager)
    {
        if (reflectDamage.y > 0 && attacker != null)
        {
            CharacterStats stats = attacker.GetComponent<CharacterStats>();

            if (stats != null)
            {
                abilityManager.DelayDamage(reflectDamage, reflectDamageType, 0.2f, GetSpawnLocation(abilityManager), attacker, stats, 0f, new Vector2Int(0, 0));
                //SpawnFX(damageFX, attacker.transform);
                SpawnFX(damageFX, GetSpawnLocation(abilityManager));
            }
        }
    }

    #endregion

    #region Helper Functions

    void SpawnFX(Object FX, Transform transform)
    {
        if (FX != null && transform != null)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

            spawnPos.x = transform.position.x;
            spawnPos.y = transform.position.y;
            spawnPos.z = transform.position.z - 5f;

            Instantiate(FX, spawnPos, spawnRot);
        }
    }

    Transform GetSpawnLocation(AbilityManager abilityManager)
    {
        if (reflectDamageSpawner == E_CombatEffectSpawn.Player)
        {
            return abilityManager.playerSpawnPos;
        }
        if (reflectDamageSpawner == E_CombatEffectSpawn.Sky)
        {
            return abilityManager.skySpawnPos;
        }
        else if (reflectDamageSpawner == E_CombatEffectSpawn.Ground)
        {
            return abilityManager.groundSpawnPos;
        }
        else if (reflectDamageSpawner == E_CombatEffectSpawn.Enemies)
        {
            return abilityManager.enemiesSpawnPos;
        }
        else if (reflectDamageSpawner == E_CombatEffectSpawn.Backstab)
        {
            return abilityManager.backstabSpawnPos;
        }
        else
        {
            return null;
        }
    }

    #region Damage and Healing

    void ApplyDamage(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (applyDamage.y > 0f)
        {
            abilityManager.DelayDamage(applyDamage, applyDamageType, 0.2f, null, stats.gameObject, stats, 0f, new Vector2Int(0, 0));

            SpawnFX(damageFX, target.transform);
        }

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (applyDamageOther.y > 0f)
                    {
                        abilityManager.DelayDamage(applyDamageOther, applyDamageTypeOther, 0.2f, null, testStats.gameObject, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }

            if (applyDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0));

                SpawnFX(damageFX, abilityManager.combatManager.playerStats.transform);
            }
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (applyDamageOpponents.y > 0f)
                    {
                        abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, testStats.gameObject, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }
        }
    }

    void TurnStartDamage(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (turnDamage.y > 0f)
        {
            abilityManager.DelayDamage(turnDamage, turnDamageType, 0.2f, null, stats.gameObject, stats, 0f, new Vector2Int(0, 0));

            SpawnFX(damageFX, target.transform);
        }

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (turnDamageOther.y > 0f)
                    {
                        abilityManager.DelayDamage(turnDamageOther, turnDamageTypeOther, 0.2f, null, testStats.gameObject, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }

            if (turnDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0));

                SpawnFX(damageFX, abilityManager.combatManager.playerStats.transform);
            }
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (turnDamageOpponents.y > 0f)
                    {
                        abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, testStats.gameObject, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }
        }
    }

    void ApplyHealing(GameObject target)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        int heal = Random.Range(applyHealing.x, applyHealing.y);

        if (applyHealing.y > 0f)
        {
            stats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null);

            SpawnFX(healFX, target.transform);
        }
    }

    void TurnStartHealing(GameObject target)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        int heal = Random.Range(turnHealing.x, turnHealing.y);

        if (turnHealing.y > 0f)
        {
            stats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null);

            SpawnFX(healFX, target.transform);
        }
    }

    #endregion

    #region Adjust Stats

    void ApplyStatAdjustments(GameObject target)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        stats.AdjustDamageMultipliers(adjustPhysMultiplier, adjustEmberMultiplier, adjustStaticMultiplier, adjustBleakMultiplier, adjustSepticMultiplier);
    }

    void RemoveStatAdjustments(GameObject target)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        stats.AdjustDamageMultipliers(-adjustPhysMultiplier, -adjustEmberMultiplier, -adjustStaticMultiplier, -adjustBleakMultiplier, -adjustSepticMultiplier);
    }

    #endregion

    #endregion

    #endregion
}