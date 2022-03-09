using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Combat/Spells", order = 0)]
public class CardParent : ScriptableObject
{
    #region General

    [Header("General")]
    public string cardName;
    public int cardNumber;
    [TextArea(3, 10)]
    public string flavourDescription;
    public Sprite cardImage;

    public bool enemySpell;

    public void CastSpell(GameObject target, GameObject caster, AbilityManager abilityManager, out bool canCast)
    {
        canCast = false;

        if (QuerySelf(target, caster, abilityManager))
        {
            OnSelfCast(target, caster, abilityManager, out canCast);
        }
        else if (QueryTarget(target, caster, abilityManager))
        {
            OnTargetCast(target, caster, abilityManager, out canCast);
        }
        else
        {
            Debug.Log("Too many spells cast");
        }
        
    }

    #endregion

    #region Self

    [Header("Self")]
    public bool selfInterpretationUnlocked = false;

    public string selfName;
    [TextArea(3, 10)]
    public string selfDescription;
    public bool selfEndTurn = false;
    public bool selfUsesAction = true;
    public int selfCost;
    public int selfPotionCost;
    public string selfCostType;
    public Vector2Int selfHeal;
    public Vector2Int selfAP;
    public E_DamageTypes restoreType;
    public float selfEndTurnDelay = 0.2f;
    public StatusParent[] selfStatus;
    public int[] selfStatusDuration;
    //public GameObject selfPrepareEffect;
    //public GameObject selfCastEffect;

    public int selfDrawCards;

    public void OnSelfCast(GameObject target, GameObject caster, AbilityManager abilityManager, out bool canCast)
    {
        canCast = false;
        CharacterStats stats = target.GetComponent<CharacterStats>();
        if (stats != null && abilityManager != null)
        {
            if (abilityManager.playerStats.CheckMana(selfCost) && abilityManager.playerStats.CheckPotions(selfPotionCost))
            {
                canCast = true;

                int heal = (int)Random.Range(selfHeal.x, selfHeal.y);
                int mana = (int)Random.Range(selfAP.x, selfAP.y);

                //Debug.Log("Cast" + selfName + "on " + target.name);

                stats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int damageTaken, stats.gameObject, false);
                if (heal > 0)
                {
                    abilityManager.combatManager.Healing.SetActive(true);
                    abilityManager.combatManager.HealingValue.text = heal.ToString();
                    abilityManager.RemoveHpPopup(2f);
                }

                PlayerStats playerStats = target.GetComponent<PlayerStats>();

                SelfApplyStatus(stats, caster);

                if (enemySpell)
                {
                    Summon(caster);
                }
                else
                {
                    stats.ChangeMana(selfCost - mana, true);
                    abilityManager.combatManager.Ap.SetActive(true);
                    abilityManager.combatManager.ApValue.text = selfCost.ToString();
                    abilityManager.RemoveApPopup(2f);

                    playerStats.ChangePotions(selfPotionCost, true);

                    abilityManager.ResetAbility();

                    if (selfEndTurn)
                    {
                        abilityManager.EndTurn(selfEndTurnDelay);
                    }

                    DrawCards(abilityManager, selfDrawCards);
                }
            }
            else
            {
                abilityManager.combatManager.noMana.SetActive(true);
                Debug.Log("Insufficient Mana");
                abilityManager.RemoveArcanaPopup(selfEndTurnDelay + 5f);
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
    public bool targetEndTurn = false;
    public bool targetUsesAction = true;
    public int targetCost;
    public string targetCostType;
    public E_DamageTypes damageType;
    public bool targetCanBeCountered = true;
    public Vector2Int targetDmg;
    public int hits;
    public float hitInterval;
    public float finalHitInterval;
    public Vector2Int targetFinalDmg;
    public float targetEndTurnDelay = 0.2f;

    public bool targetCleave;
    public bool targetChain;
    public Vector2Int extraDmg;

    public bool randomTargets;
    public Vector2Int lifeLeach;
    public Vector2Int healOnKill;
    public float executeThreshold;
    public StatusParent[] targetStatus;
    public int[] targetStatusDuration;
    public float[] targetStatusChance;
    //public GameObject targetPrepareEffect;
    //public GameObject targetCastEffect;

    public int targetDrawCards;

    public E_CombatEffectSpawn spawnPosition;

    public void OnTargetCast(GameObject target, GameObject caster, AbilityManager abilityManager, out bool canCast)
    {
        canCast = false;
        CharacterStats stats = target.GetComponent<CharacterStats>();
        if (stats != null)
        {
            Transform spawnPos = GetSpawnLocation(abilityManager, caster);

            int cost = targetCost;
            if (abilityManager.playerStats.CheckMana(cost))
            {
                canCast = true;

                if (targetChain)
                {
                    string message = "Cast " + targetName + " on ";
                    abilityManager.multihitMax = abilityManager.combatManager.enemyManager.enemies.Count;

                    foreach (var item in abilityManager.combatManager.enemyManager.enemies)
                    {
                        CharacterStats itemHealth = item.gameObject.GetComponent<CharacterStats>();
                        message += item.gameObject.name + ", ";

                        if (item.gameObject == target)
                        {
                            abilityManager.DelayDamage(targetDmg, damageType, 0f, spawnPos, target, caster, itemHealth, executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(itemHealth, caster);
                        }
                        else
                        {
                            abilityManager.DelayDamage(extraDmg, damageType, 0.25f, target.transform, item.gameObject, caster, itemHealth, executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(itemHealth, caster);
                        }
                    }

                    abilityManager.combatManager.Dmg.SetActive(true);
                    abilityManager.combatManager.DmgValue.text = abilityManager.multihitTally.ToString();

                    abilityManager.multihitTally = 0;

                    //Debug.Log(message);
                } //chain target attack
                else if (targetCleave)
                {
                    string message = "Cast " + targetName + " on ";
                    abilityManager.multihitMax = abilityManager.combatManager.enemyManager.enemies.Count;

                    foreach (var item in abilityManager.combatManager.enemyManager.enemies)
                    {
                        CharacterStats itemHealth = item.gameObject.GetComponent<CharacterStats>();
                        message += item.gameObject.name + ", ";

                        if (item.gameObject == target)
                        {
                            abilityManager.DelayDamage(targetDmg, damageType, 0f, spawnPos, item.gameObject, caster, itemHealth, executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(itemHealth, caster);
                        }
                        else
                        {
                            abilityManager.DelayDamage(extraDmg, damageType, 0.1f, spawnPos, item.gameObject, caster, itemHealth, executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(itemHealth, caster);
                        }
                    }

                    abilityManager.combatManager.Dmg.SetActive(true);
                    abilityManager.combatManager.DmgValue.text = abilityManager.multihitTally.ToString();

                    abilityManager.multihitTally = 0;

                    //Debug.Log(message);
                }  //cleave target attack
                else if (randomTargets)
                {
                    abilityManager.multihitMax = hits;
                    abilityManager.combatManager.Dmg.SetActive(true);

                    if (hits > 1)
                    {
                        EnemyStats[] enemyStatsArray = GameObject.FindObjectsOfType<EnemyStats>();

                        int randTarget = Random.Range(0, enemyStatsArray.Length);

                        for (int i = 1; i < hits; i++)
                        {
                            enemyStatsArray = GameObject.FindObjectsOfType<EnemyStats>();

                            Vector2 dmgVector = targetDmg;
                            float hitTime = i * hitInterval;

                            abilityManager.DelayDamage(dmgVector, damageType, hitTime, spawnPos, enemyStatsArray[randTarget].gameObject, caster, enemyStatsArray[randTarget], executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(enemyStatsArray[randTarget], caster);

                            int nextRandTarget = Random.Range(0, enemyStatsArray.Length);

                            if (enemyStatsArray.Length != 1)
                            {
                                while (nextRandTarget == randTarget)
                                {
                                    nextRandTarget = Random.Range(0, enemyStatsArray.Length);
                                }
                            }

                            randTarget = nextRandTarget;
                        }

                        Vector2 dmgVectorFinal = targetFinalDmg;
                        float hitTimeFinal = (hits * hitInterval) + finalHitInterval;

                        abilityManager.DelayDamage(dmgVectorFinal, damageType, hitTimeFinal, spawnPos, target, caster, stats, executeThreshold, healOnKill, targetCanBeCountered);
                        TargetApplyStatus(stats, caster);
                    }
                    else
                    {
                        Vector2 dmgVector = targetDmg;

                        abilityManager.DelayDamage(dmgVector, damageType, 0f, spawnPos, target, caster, stats, executeThreshold, healOnKill, targetCanBeCountered);
                        TargetApplyStatus(stats, caster);
                    }
                } //random targets
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

                            abilityManager.DelayDamage(dmgVector, damageType, hitTime, spawnPos, target, caster, stats, executeThreshold, healOnKill, targetCanBeCountered);
                            TargetApplyStatus(stats, caster);
                        }

                        Vector2 dmgVectorFinal = targetFinalDmg;
                        float hitTimeFinal = (hits * hitInterval) + finalHitInterval;

                        abilityManager.DelayDamage(dmgVectorFinal, damageType, hitTimeFinal, spawnPos, target, caster, stats, executeThreshold, healOnKill, targetCanBeCountered);
                        TargetApplyStatus(stats, caster);
                    }
                    else
                    {
                        Vector2 dmgVector = targetDmg;

                        abilityManager.DelayDamage(dmgVector, damageType, 0f, spawnPos, target, caster, stats, executeThreshold, healOnKill, targetCanBeCountered);
                        TargetApplyStatus(stats, caster);
                    }
                } //single target attack

                if (!enemySpell)
                {
                    abilityManager.playerStats.ChangeMana(cost, true);
                    abilityManager.combatManager.Ap.SetActive(true);
                    abilityManager.combatManager.ApValue.text = cost.ToString();

                    abilityManager.ResetAbility();

                    if (targetEndTurn)
                    {
                        abilityManager.EndTurn(selfEndTurnDelay);
                    }
                }

                CharacterStats casterStats = caster.GetComponent<CharacterStats>();

                if (casterStats != null)
                {
                    int heal = Random.Range(lifeLeach.x, lifeLeach.y);
                    casterStats.ChangeHealth(heal, false, E_DamageTypes.Physical, out int healNull, caster, false);
                }

                DrawCards(abilityManager, targetDrawCards);
            }
            else
            {
                abilityManager.combatManager.noMana.SetActive(true);
                //Debug.Log("Insufficient Mana");
                abilityManager.RemoveArcanaPopup(selfEndTurnDelay + 5f);
            }
        }
        else
        {
            //Debug.Log("You cannot target that character with this spell");
        }
    }

    #endregion

    #region Enemy

    [Header("Enemy Only")]
    public Object summonAlly;
    public int summonCount;
    public Object summonFX;

    void Summon(GameObject caster)
    {
        if (summonAlly != null && summonCount > 0)
        {
            int currentSpawnNumber = caster.GetComponentInParent<CombatEnemySpawner>().enemyNumber;

            CombatEnemySpawner[] spawners = GameObject.FindObjectsOfType<CombatEnemySpawner>();

            int spawned = 0;

            foreach (var item in spawners)
            {
                if (item.enemyNumber != currentSpawnNumber)
                {
                    if (item.GetComponentInChildren<Enemy>() == null)
                    {
                        item.Spawn(summonAlly);
                        SpawnFX(summonFX, item.transform);

                        spawned++;
                    }
                }

                if (spawned >= summonCount)
                    break;
            }

            GameObject.FindObjectOfType<EnemyManager>().SetupLists();
        }
    }

    #endregion

    #region Helper Functions

    void DrawCards(AbilityManager abilityManager, int count)
    {
        abilityManager.combatDeckManager.DrawCards(count);
    }

    public bool QuerySelf(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        return (caster == GameObject.Find("Player") || caster != GameObject.Find("Player")) && (target == caster && selfInterpretationUnlocked && target.GetComponent<CharacterStats>() != null);
    }

    public bool QueryTarget(GameObject target, GameObject caster, AbilityManager abilityManager)
    {
        return (caster == GameObject.Find("Player") || caster != GameObject.Find("Player")) && (target != caster && targetInterpretationUnlocked && target.GetComponent<CharacterStats>() != null);
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

    public void GetReadyAbilityInfo(out bool multihit, out Vector2Int restore, out string selfType, out Vector2Int dmg, out E_DamageTypes type, out string cardNameSelf, out string cardNameTarget, out bool hitsAll, out Vector2Int extradmg)
    {
        multihit = false;
        restore = new Vector2Int(0, 0);
        selfType = "none";
        dmg = new Vector2Int(0, 0);
        type = E_DamageTypes.Physical;
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

    Transform GetSpawnLocation(AbilityManager abilityManager, GameObject caster)
    {
        if (!enemySpell)
        {
            if (spawnPosition == E_CombatEffectSpawn.Sky)
            {
                return abilityManager.skySpawnPos;
            }
            else if (spawnPosition == E_CombatEffectSpawn.Ground)
            {
                return abilityManager.groundSpawnPos;
            }
            else if (spawnPosition == E_CombatEffectSpawn.Enemies)
            {
                return abilityManager.enemiesSpawnPos;
            }
            else if (spawnPosition == E_CombatEffectSpawn.Backstab)
            {
                return abilityManager.backstabSpawnPos;
            }
            else
            {
                return abilityManager.playerSpawnPos;
            }
        }
        else
        {
            return caster.transform;
        }
    }

    void SelfApplyStatus(CharacterStats target, GameObject caster)
    {
        if (selfStatus != null)
        {
            if (selfStatus.Length > 0)
            {
                for (int i = 0; i < selfStatus.Length; i++)
                {
                    target.ApplyStatus(selfStatus[i], caster, selfStatusDuration[i]);
                }
            }
        }
    }

    void TargetApplyStatus(CharacterStats target, GameObject caster)
    {
        if (targetStatus != null)
        {
            if (targetStatus.Length > 0)
            {
                for (int i = 0; i < targetStatus.Length; i++)
                {
                    float randomNumber = Random.Range(0f, 1f);

                    //Debug.Log("Attempt to apply " + targetStatus[i].effectName);

                    if (randomNumber < targetStatusChance[i])
                    {
                        //apply status
                        target.ApplyStatus(targetStatus[i], caster, targetStatusDuration[i]);
                    }
                }
            }
        }
    }

    #endregion
}