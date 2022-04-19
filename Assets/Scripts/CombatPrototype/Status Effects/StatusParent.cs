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

    public AudioClip[] applySounds;
    public AudioClip[] turnSounds;
    public AudioClip[] endSounds;

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

    [Header("Damage To Allies")]
    public Vector2Int applyDamageOther;
    public E_DamageTypes applyDamageTypeOther;
    public Vector2Int turnDamageOther;
    public E_DamageTypes turnDamageTypeOther;

    [Header("Healing To Applied Character")]
    public Vector2Int applyHealing;
    public Vector2Int applyArcana;
    public Vector2Int turnHealing;
    public Vector2Int turnArcana;

    [Header("Healing To Allies")]
    public Vector2Int applyHealingOther;
    public Vector2Int applyArcanaOther;
    public Vector2Int turnHealingOther;
    public Vector2Int turnArcanaOther;

    [Header("Healing To Opponents")]
    public Vector2Int applyHealingOpponents;
    public Vector2Int applyArcanaOpponents;
    public Vector2Int turnHealingOpponents;
    public Vector2Int turnArcanaOpponents;

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
    public bool slow;

    [Header("Debuff Others")]
    public bool charmOther;
    public bool revealEntryOther;
    public bool skipTurnOther;
    public bool sleepTurnOther;
    public bool silenceOther;
    public bool slowOther;

    [Header("Debuff Opponents")]
    public bool charmOpponents;
    public bool revealEntryOpponents;
    public bool skipTurnOpponents;
    public bool sleepTurnOpponents;
    public bool silenceOpponents;
    public bool slowOpponents;

    #endregion

    #endregion

    #region Effects

    #region Apply and Remove Status

    public void OnApply(GameObject target, GameObject caster)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        ApplyDamage(target, caster, abilityManager);
        ApplyHealing(target, caster, abilityManager);

        ApplyStatAdjustments(target);
        TurnInhibitors(target, abilityManager, true);

        Enemy stats = target.GetComponent<Enemy>();

        if (revealEntry && stats != null)
        {
            ExposeEnemy(abilityManager, stats);
        }
    }

    public void OnRemove(GameObject target)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        RemoveStatAdjustments(target);
        TurnInhibitors(target, abilityManager, false);

        AudioClip soundEffect = GetSoundEffect(endSounds);
        if (soundEffect != null)
        {
            abilityManager.SoundEffect(soundEffect, 1f);
        }
    }

    #endregion

    #region Turns

    public void OnTurnStart(GameObject target, GameObject caster)
    {
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        TurnStartDamage(target, caster, abilityManager);
        TurnStartHealing(target, caster, abilityManager);
        TurnInhibitors(target, abilityManager, true);
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
                abilityManager.DelayDamage(reflectDamage, reflectDamageType, 0.2f, GetSpawnLocation(abilityManager, caster.transform), attacker, caster, stats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(turnSounds));
            }
        }
    }

    #endregion

    #region Damage and Healing

    void ApplyDamage(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (applyDamage.y > 0f)
        {
            abilityManager.DelayDamage(applyDamage, applyDamageType, 0.2f, null, stats.gameObject, caster, stats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(applySounds));
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
                        abilityManager.DelayDamage(applyDamageOther, applyDamageTypeOther, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(applySounds));
                    }
                }
            }

            if (applyDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, caster, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(applySounds));
            }
        }
        else if (target.GetComponent<PlayerStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (applyDamageOpponents.y > 0f)
                    {
                        abilityManager.DelayDamage(applyDamageOpponents, applyDamageTypeOpponents, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(applySounds));
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
            abilityManager.DelayDamage(turnDamage, turnDamageType, 0.2f, null, stats.gameObject, caster, stats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(turnSounds));
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
                        abilityManager.DelayDamage(turnDamageOther, turnDamageTypeOther, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(turnSounds));
                    }
                }
            }

            if (turnDamageOpponents.y > 0f)
            {
                abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, abilityManager.combatManager.playerStats.gameObject, caster, abilityManager.combatManager.playerStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(turnSounds));
            }
        }
        else if (target.GetComponent<PlayerStats>())
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
                            abilityManager.DelayDamage(turnDamageOpponents, turnDamageTypeOpponents, 0.2f, null, testStats.gameObject, caster, testStats, 0f, new Vector2Int(0, 0), false, damageFX, null, GetSoundEffect(turnSounds));
                        }
                    }
                }
            }
        }
    }

    void ApplyHealing(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        abilityManager.SoundEffect(GetSoundEffect(applySounds), 1f);

        if (applyHealing.y > 0f)
        {
            int heal = Random.Range(applyHealing.x, applyHealing.y);
            stats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
        }

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();
                
                if (testStats != stats)
                {
                    if (applyHealingOther.y > 0f)
                    {
                        int heal = Random.Range(applyHealingOther.x, applyHealingOther.y);
                        testStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
                        Debug.Log("Healed " + testStats.name);
                    }
                }
            }

            if (applyHealingOpponents.y > 0f)
            {
                int heal = Random.Range(applyHealingOpponents.x, applyHealingOpponents.y);
                abilityManager.combatManager.playerStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
            }
        }
        else if (target.GetComponent<PlayerStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (applyHealingOpponents.y > 0f)
                    {
                        int heal = Random.Range(applyHealingOpponents.x, applyHealingOpponents.y);
                        testStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
                    }
                }
            }
        }
    }

    void TurnStartHealing(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        abilityManager.SoundEffect(GetSoundEffect(turnSounds), 1f);

        if (turnHealing.y > 0f)
        {
            int heal = Random.Range(turnHealing.x, turnHealing.y);
            stats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
        }

        if (target.GetComponent<EnemyStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (turnHealingOther.y > 0f)
                    {
                        int heal = Random.Range(turnHealingOther.x, turnHealingOther.y);
                        testStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
                    }
                }
            }

            if (turnHealingOpponents.y > 0f)
            {
                int heal = Random.Range(turnHealingOpponents.x, turnHealingOpponents.y);
                abilityManager.combatManager.playerStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
            }
        }
        else if (target.GetComponent<PlayerStats>())
        {
            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                if (item != null)
                {
                    CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                    if (testStats != stats)
                    {
                        if (turnHealingOpponents.y > 0f)
                        {
                            int heal = Random.Range(turnHealingOpponents.x, turnHealingOpponents.y);
                            testStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healing, null, false, damageFX);
                        }
                    }
                }
            }
        }
    }

    /*

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
    */

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

    void TurnInhibitors(GameObject target, AbilityManager abilityManager, bool apply)
    {
        CharacterStats stats = target.GetComponent<CharacterStats>();

        if (target.GetComponent<EnemyStats>())
        {
            #region Self

            if (charm)
            {
                stats.charm = apply;
            }

            if (silence)
            {
                stats.silence = apply;
            }

            if (skipTurn)
            {
                stats.skipTurn = apply;
            }

            if (sleepTurn)
            {
                stats.sleepTurn = apply;
            }

            if (slow)
            {
                stats.slow = apply;
            }

            #endregion

            #region Allies

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

                    if (slowOther)
                    {
                        testStats.slow = true;
                    }
                }
            }

            #endregion

            #region Opponents

            CharacterStats playerStats = abilityManager.combatManager.playerStats;

            if (playerStats != stats)
            {
                if (charmOpponents)
                {
                    playerStats.charm = apply;
                }

                if (silenceOpponents)
                {
                    playerStats.silence = apply;
                }

                if (skipTurnOpponents)
                {
                    playerStats.skipTurn = apply;
                }

                if (sleepTurnOpponents)
                {
                    playerStats.sleepTurn = apply;
                }

                if (slowOpponents)
                {
                    playerStats.slow = apply;
                }
            }

            #endregion
        }
        else if (target.GetComponent<PlayerStats>())
        {
            #region Self

            if (charm)
            {
                stats.charm = apply;
            }

            if (silence)
            {
                stats.silence = apply;
            }

            if (skipTurn)
            {
                stats.skipTurn = apply;
            }

            if (sleepTurn)
            {
                stats.sleepTurn = apply;
            }

            if (slow)
            {
                stats.slow = apply;
            }

            #endregion

            #region Opponents

            foreach (var item in abilityManager.combatManager.enemyManager.enemies)
            {
                CharacterStats testStats = item.gameObject.GetComponent<CharacterStats>();

                if (testStats != stats)
                {
                    if (charmOpponents)
                    {
                        testStats.charm = apply;
                    }

                    if (silenceOpponents)
                    {
                        Debug.Log("silence worked");
                        testStats.silence = apply;
                    }

                    if (skipTurnOpponents)
                    {
                        testStats.skipTurn = apply;
                    }

                    if (sleepTurnOpponents)
                    {
                        testStats.sleepTurn = apply;
                    }

                    if (slowOpponents)
                    {
                        testStats.slow = apply;
                    }
                }
            }

            #endregion
        }
    }

    #endregion

    #region Helper Functions

    void ExposeEnemy(AbilityManager abilityManager, Enemy target)
    {
        abilityManager.EnemyInfo(target);
    }

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

    Transform GetSpawnLocation(AbilityManager abilityManager, Transform caster)
    {
        switch (reflectDamageSpawner)
        {
            case E_CombatEffectSpawn.Caster:
                return caster;

            case E_CombatEffectSpawn.Sky:
                return abilityManager.playerSpawnPos;

            case E_CombatEffectSpawn.Ground:
                return abilityManager.playerSpawnPos;

            case E_CombatEffectSpawn.Enemies:
                return abilityManager.playerSpawnPos;

            case E_CombatEffectSpawn.Backstab:
                return abilityManager.playerSpawnPos;

            default:
                return caster;
        }
    }

    #endregion

    #endregion

    AudioClip GetSoundEffect(AudioClip[] soundArray)
    {
        if (soundArray != null)
        {
            if (soundArray.Length > 1)
                return soundArray[Random.Range(0, soundArray.Length - 1)];
            else if (soundArray.Length == 1)
                return soundArray[0];
            else
                return null;
        }

        return null;
    }
}