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

    [Header("Buffs")]
    public Vector2Int reflectDamage;
    public E_DamageTypes reflectDamageType;
    public E_CombatEffectSpawn reflectDamageSpawner;

    /*
    public bool extraTurn;
    public bool untargettable;
    public bool immune;
    */

    [Header("Debuffs")]
    public bool charm;
    public bool revealEntry;
    public bool skipTurn;
    public bool sleepTurn;
    public bool silence;

    [Header("Debuff Others")]
    public bool charmOther;
    public bool revealEntryOther;
    public bool skipTurnOther;
    public bool sleepTurnOther;
    public bool silenceOther;

    [Header("Debuff Opponents")]
    public bool charmOpponents;
    public bool revealEntryOpponents;
    public bool skipTurnOpponents;
    public bool sleepTurnOpponents;
    public bool silenceOpponents;

    #endregion

    #endregion

    #region Effects

    #region Apply and Remove Status

    public void OnApply(GameObject target, GameObject caster)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        ApplyDamage(target, caster, abilityManager);
        ApplyHealing(target);

        ApplyStatAdjustments(target);
        ApplyTurnInhibitors(target, abilityManager);
    }

    public void OnRemove(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        RemoveStatAdjustments(target);
        RemoveTurnInhibitors(target, abilityManager);
    }

    #endregion

    #region Turns

    public void OnTurnStart(GameObject target, GameObject caster)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        TurnStartDamage(target, caster, abilityManager);
        TurnStartHealing(target);
        ApplyTurnInhibitors(target, abilityManager);
    }

    public void OnTurnEnd(GameObject target)
    {

    }

    #endregion

    #region Take Damage

    public void OnTakeDamage(GameObject attacker, GameObject caster, AbilityManager abilityManager)
    {
        if (reflectDamage.y > 0 && attacker != null && attacker != caster)
        {
            CharacterStats stats = attacker.GetComponent<CharacterStats>();

            if (stats != null)
            {
                abilityManager.DelayDamage(reflectDamage, reflectDamageType, 0.2f, GetSpawnLocation(abilityManager), attacker, caster, stats, 0f, new Vector2Int(0, 0));
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

    void ApplyDamage(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (applyDamage.y > 0f)
        {
            abilityManager.DelayDamage(applyDamage, applyDamageType, 0.2f, null, stats.gameObject, caster, stats, 0f, new Vector2Int(0, 0));

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
                        abilityManager.DelayDamage(applyDamageOther, applyDamageTypeOther, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }

            if (applyDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, caster, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0));

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
                        abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }
        }
    }

    void TurnStartDamage(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (turnDamage.y > 0f)
        {
            abilityManager.DelayDamage(turnDamage, turnDamageType, 0.2f, null, stats.gameObject, caster, stats, 0f, new Vector2Int(0, 0));

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
                        abilityManager.DelayDamage(turnDamageOther, turnDamageTypeOther, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0));

                        SpawnFX(damageFX, item.gameObject.transform);
                    }
                }
            }

            if (turnDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, caster, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0));

                SpawnFX(damageFX, abilityManager.combatManager.playerStats.transform);
            }
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                if (item != null)
                {
                    CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                    if (testStats != stats)
                    {
                        if (turnDamageOpponents.y > 0f)
                        {
                            abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0));

                            SpawnFX(damageFX, item.gameObject.transform);
                        }
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

    #region Turn Inhibitors

    void ApplyTurnInhibitors(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (charmOther)
                    {
                        testStats.charm = true;
                    }

                    if (silenceOther)
                    {
                        testStats.silence = true;
                    }

                    if (skipTurnOther)
                    {
                        testStats.skipTurn = true;
                    }

                    if (sleepTurnOther)
                    {
                        testStats.sleepTurn = true;
                    }
                }
            }

            if (applyDamageOpponents.y > 0f)
            {
                CharacterStats playerStats = abilityManager.combatManager.playerStats;

                if (playerStats != stats)
                {
                    if (charmOpponents)
                    {
                        playerStats.charm = true;
                    }

                    if (silenceOpponents)
                    {
                        playerStats.silence = true;
                    }

                    if (skipTurnOpponents)
                    {
                        playerStats.skipTurn = true;
                    }

                    if (sleepTurnOpponents)
                    {
                        playerStats.sleepTurn = true;
                    }
                }
            }
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (testStats != stats)
                    {
                        if (charmOpponents)
                        {
                            testStats.charm = true;
                        }

                        if (silenceOpponents)
                        {
                            testStats.silence = true;
                        }

                        if (skipTurnOpponents)
                        {
                            testStats.skipTurn = true;
                        }

                        if (sleepTurnOpponents)
                        {
                            testStats.sleepTurn = true;
                        }
                    }
                }
            }
        }
    }

    void RemoveTurnInhibitors(GameObject target, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (charmOther)
                    {
                        testStats.charm = false;
                    }

                    if (silenceOther)
                    {
                        testStats.silence = false;
                    }

                    if (skipTurnOther)
                    {
                        testStats.skipTurn = false;
                    }

                    if (sleepTurnOther)
                    {
                        testStats.sleepTurn = false;
                    }
                }
            }

            if (applyDamageOpponents.y > 0f)
            {
                CharacterStats playerStats = abilityManager.combatManager.playerStats;

                if (playerStats != stats)
                {
                    if (charmOpponents)
                    {
                        playerStats.charm = false;
                    }

                    if (silenceOpponents)
                    {
                        playerStats.silence = false;
                    }

                    if (skipTurnOpponents)
                    {
                        playerStats.skipTurn = false;
                    }

                    if (sleepTurnOpponents)
                    {
                        playerStats.sleepTurn = false;
                    }
                }
            }
        }
        else
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (testStats != stats)
                    {
                        if (charmOpponents)
                        {
                            testStats.charm = false;
                        }

                        if (silenceOpponents)
                        {
                            testStats.silence = false;
                        }

                        if (skipTurnOpponents)
                        {
                            testStats.skipTurn = false;
                        }

                        if (sleepTurnOpponents)
                        {
                            testStats.sleepTurn = false;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #endregion

    #endregion
}