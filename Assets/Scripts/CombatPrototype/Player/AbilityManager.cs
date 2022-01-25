using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    #region Setup

    public bool playerTurn = false;
    public CombatManager combatManager;
    public PlayerStats playerStats;

    private CardParent readyAbility;
    private ActiveCard activeCard;

    public float popupDuration = 5f;

    Targetter targetter;

    public GameObject attackFX;
    public Transform playerSpawnPos, skySpawnPos, groundSpawnPos, enemiesSpawnPos, backstabSpawnPos;

    public SliderVariation sliderVarScript;

    #region Ability Values

    [HideInInspector]
    public int multihitMax = 0;
    [HideInInspector]
    public int multihitTally = 0;
    [HideInInspector]
    public int multihitCount = 0;

    #endregion

    #region Damage Type Colours
    [Header("Damage Type Colours - Image")]
    public Color physicalColour;
    public Color emberColour;
    public Color staticColour;
    public Color bleakColour;
    public Color septicColour;
    public Color perforationColour;

    [Header("Damage Type Colours - Trail")]
    public Gradient physicalTrailColour;
    public Gradient emberTrailColour;
    public Gradient staticTrailColour;
    public Gradient bleakTrailColour;
    public Gradient septicTrailColour;
    public Gradient perforationTrailColour;
    #endregion

    private void Start()
    {
        activeCard = GameObject.FindObjectOfType<ActiveCard>();
        targetter = GetComponentInChildren<Targetter>();
    }

    #endregion

    #region Selecting Abilities

    public void ButtonHovered(GameObject target)
    {
        //Debug.Log("Button hovering");

        bool multihit;
        Vector2Int restore;
        string selfType;
        Vector2Int dmg;
        E_DamageTypes type;
        string cardNameSelf;
        string cardNameTarget;
        bool hitsAll;
        Vector2Int extradmg;

        GetReadyAbilityInfo(out multihit, out restore, out selfType, out dmg, out type, out cardNameSelf, out cardNameTarget, out hitsAll, out extradmg);
        //Debug.Log(selfType);
        if (selfType == "Healing & Arcana" || selfType == "Healing")
        {
            sliderVarScript.ApplyPreview(-restore);
        }
        if (selfType == "Healing & Arcana" || selfType == "Arcana")
        {
            //newSliderVarScript for mana preview
        }
    }

    public void MouseLeft()
    {
        //Debug.Log("Button stop hovering");
        sliderVarScript.StopPreview();
        //stop preview for mana slider too
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

        if (readyAbility != null)
        {
            readyAbility.GetReadyAbilityInfo(out multihit, out restore, out selfType, out dmg, out type, out cardNameSelf, out cardNameTarget, out hitsAll, out extradmg);
        }
    }

    public void CastAbility(GameObject target)
    {
        if (playerTurn)
        {
            if (readyAbility != null)
            {
                readyAbility.CastSpell(target, this.gameObject, this);
            }
            else
            {
                //Debug.Log("You have not readied an ability.");
                
                EnemyInfo(target.GetComponent<Enemy>());
            }
        }
    }

    private void EnemyInfo(Enemy target)
    {
        if (target != null)
            combatManager.enemyManager.EnemyInfo(target);
        else
            combatManager.enemyManager.EnemyInfo(null);
    }

    public void SetAbility(CardParent ability)
    {
        EnemyInfo(null);

        readyAbility = ability;

        if (activeCard != null)
        {
            if (ability.selfInterpretationUnlocked && ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.cardName, ability.selfHeal, "Unknown", ability.selfCost, "Two interpretations active, UI issue", ability.selfCostType);
                combatManager.TargetEnemies(true);
                targetter.SetVisibility(true);
            }
            else if (ability.selfInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.selfName, ability.RestoreValue(), ability.RestoreType(), ability.selfCost, ability.selfDescription, ability.selfCostType); ;
                combatManager.TargetEnemies(false);
                targetter.SetVisibility(true);
            }
            else if (ability.targetInterpretationUnlocked)
            {
                activeCard.ReadyCard(ability.targetName, ability.TotalDmgRange(), ability.damageType.ToString(), ability.targetCost, ability.targetDescription, ability.targetCostType);
                combatManager.TargetEnemies(true);
                targetter.SetVisibility(false);
            }
        }
    }

    public void ResetAbility()
    {
        readyAbility = null;

        if (activeCard != null)
            activeCard.CastCard();

        combatManager.TargetEnemies(false);
        targetter.SetVisibility(false);
    }

    #endregion

    #region Helper Functions

    public void DelayDamage(Vector2 damageRange, E_DamageTypes damageType, float delay, Transform origin, GameObject target, GameObject caster, CharacterStats targetHealth, float executeThreshold, Vector2Int healOnKill)
    {
        if (damageType == E_DamageTypes.Random)
        {
            int randDamage = Random.Range(0, System.Enum.GetValues(typeof(E_DamageTypes)).Length - 1);

            switch (randDamage)
            {
                case 1:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Physical, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
                case 2:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Ember, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
                case 3:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Static, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
                case 4:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Bleak, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
                case 5:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Septic, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
                default:
                    StartCoroutine(IDelayDamage(damageRange, E_DamageTypes.Physical, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
                    break;
            }
        }
        else
        {
            StartCoroutine(IDelayDamage(damageRange, damageType, delay, origin, target, caster, targetHealth, executeThreshold, healOnKill));
        }
    }

    private IEnumerator IDelayDamage(Vector2 damageRange, E_DamageTypes damageType, float delay, Transform origin, GameObject target, GameObject caster, CharacterStats targetHealth, float executeThreshold, Vector2Int healOnKill)
    {
        Vector3 originRef = new Vector3(999999, 999999, 999999);

        if (origin != null)
        {
            originRef = origin.position;
        }

        yield return new WaitForSeconds(delay);

        if (targetHealth != null)
        {
            if (originRef != new Vector3(999999, 999999, 999999))
            {
                SpawnAttackEffect(originRef, target, damageType);
            }

            int damage = (int)Random.Range(damageRange.x, damageRange.y);

            int damageTaken;

            targetHealth.ChangeHealth(damage, true, damageType, out damageTaken, caster);

            //execute enemy
            if (targetHealth.HealthPercentage() < executeThreshold)
            {
                //execute anim and delay
                targetHealth.ChangeHealth(999999999, true, damageType, out int nullDamageTaken, caster);
                //Debug.Log("Executed");
            }

            if (targetHealth == null || targetHealth.GetHealth() == 0)
            {
                //killed enemy
                playerStats.ChangeHealth(Random.Range(healOnKill.x, healOnKill.y), false, E_DamageTypes.Physical, out int damageTakenNull, caster);
                //Debug.Log("Heal on Kill");
            }

            multihitTally += damageTaken;
            multihitCount++;

            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = multihitTally.ToString();

            //Debug.Log(multihitCount + " hits for " + multihitTally + " points of damage (not final damage as enemy mey be vulnerable or resistant to the damage)");

            if (multihitCount >= multihitMax)
            {
                //Debug.Log("Finish multihit");

                multihitCount = 0;
                multihitTally = 0;
            }
        }
    }

    public void SpawnAttackEffect(Vector3 origin, GameObject target, E_DamageTypes damageType)
    {
        if (attackFX != null)
        {
            GameObject attackRef = Instantiate(attackFX, origin, new Quaternion(0, 0, 0, 0)) as GameObject;

            #region Colour

            SpriteRenderer image = attackRef.GetComponent<SpriteRenderer>();

            if (image != null)
            {
                if (damageType == E_DamageTypes.Physical)
                {
                    image.color = physicalColour;
                }
                if (damageType == E_DamageTypes.Ember)
                {
                    image.color = emberColour;
                }
                if (damageType == E_DamageTypes.Static)
                {
                    image.color = staticColour;
                }
                if (damageType == E_DamageTypes.Bleak)
                {
                    image.color = bleakColour;
                }
                if (damageType == E_DamageTypes.Septic)
                {
                    image.color = septicColour;
                }
                if (damageType == E_DamageTypes.Perforation)
                {
                    image.color = perforationColour;
                }
            }

            TrailRenderer trail = attackRef.GetComponent<TrailRenderer>();

            if (trail != null)
            {
                if (damageType == E_DamageTypes.Physical)
                {
                    trail.colorGradient = physicalTrailColour;
                }
                if (damageType == E_DamageTypes.Ember)
                {
                    trail.colorGradient = emberTrailColour;
                }
                if (damageType == E_DamageTypes.Static)
                {
                    trail.colorGradient = staticTrailColour;
                }
                if (damageType == E_DamageTypes.Bleak)
                {
                    trail.colorGradient = bleakTrailColour;
                }
                if (damageType == E_DamageTypes.Septic)
                {
                    trail.colorGradient = septicTrailColour;
                }
                if (damageType == E_DamageTypes.Perforation)
                {
                    trail.colorGradient = perforationTrailColour;
                }
            }

            #endregion

            ProjectileMovement projScript = attackRef.GetComponent<ProjectileMovement>();

            if (projScript != null)
            {
                projScript.target = target;
                projScript.moving = true;
            }

            //Debug.Break();
        }
    }

    public void EndTurn(float delay)
    {
        StartCoroutine(IEndTurn(delay));
    }

    private IEnumerator IEndTurn(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (combatManager != null)
        {
            combatManager.EndTurn(true);
        }
    }

    public void RemovePopup(float delay)
    {
        StartCoroutine(IRemovePopup(delay));
    }

    private IEnumerator IRemovePopup(float delay)
    {
        yield return new WaitForSeconds(delay);

        combatManager.noMana.SetActive(false);
    }

    #endregion
}