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
    public Transform spawnPos;

    public SliderVariation sliderVarScript;

    #region Ability Values

    [HideInInspector]
    public int multihitMax = 0;
    [HideInInspector]
    public int multihitTally = 0;
    [HideInInspector]
    public int multihitCount = 0;

    #region Basic Attacks
    [Header("Basic Attacks")]
    public Vector2 slashDamage = new Vector2(6, 15);
    public Vector2 criticalSlashDamage = new Vector2(22, 35);
    public Vector2 cleaveDamage = new Vector2(8, 16);
    public Vector2 flurryDamage = new Vector2(4, 8);
    public Vector2 flurryFinalHitDamage = new Vector2(6, 10);

    #endregion

    #region Spells
    [Header("Spells")]

    public Vector2 stormBarrageDamage = new Vector2(14, 32);
    public int stormBarrageCost = 55;

    public Vector2 fireboltDamage = new Vector2(26, 40);
    public int fireboltCost = 40;

    public Vector2 chillTouchDamage = new Vector2(16, 30);
    public int chillTouchCost = 20;

    public Vector2 chainLightningDamage = new Vector2(18, 32);
    public int chainLightningCost = 60;

    public Vector2 cureWoundsHealing = new Vector2(28, 42);
    public int cureWoundsCost = 40;

    public Vector2 healthPotionHealing = new Vector2(34, 50);

    #endregion

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
        string type;
        string cardNameSelf;
        string cardNameTarget;
        bool hitsAll;
        Vector2Int extradmg;

        GetReadyAbilityInfo(out multihit, out restore, out selfType, out dmg, out type, out cardNameSelf, out cardNameTarget, out hitsAll, out extradmg);
        Debug.Log(selfType);
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
                readyAbility.CastSpell(target, this);
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
                activeCard.ReadyCard(ability.targetName, ability.TotalDmgRange(), ability.damageType, ability.targetCost, ability.targetDescription, ability.targetCostType);
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

    #region Abilities

    #region Basic Attacks

    private void Slash(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int damage = (int)Random.Range(slashDamage.x, slashDamage.y);

            Debug.Log("Cast Slash on " + target.name);

            targetHealth.ChangeHealth(damage, true);
            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = damage.ToString();

            SpawnAttackEffect(spawnPos.position, target);

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
            MouseLeft();
            SpawnAttackEffect(spawnPos.position, target);

            int damage = (int)Random.Range(criticalSlashDamage.x, criticalSlashDamage.y);

            Debug.Log("Cast Slash on " + target.name + ". It's a critical hit!");

            targetHealth.ChangeHealth(damage, true);
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
            MouseLeft();
            foreach (var item in combatManager.enemyManager.enemies)
            {
                SpawnAttackEffect(spawnPos.position, item.gameObject);
            }

            int damage = (int)Random.Range(cleaveDamage.x, cleaveDamage.y);

            EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

            string message = "Cast Cleave on ";

            foreach (var item in enemies)
            {
                message += item.gameObject.name + ", ";

                item.ChangeHealth(damage, true);

                multihitTally += damage;
            }

            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = multihitTally.ToString();

            multihitTally = 0;

            Debug.Log(message);

            ResetAbility();

            StartCoroutine(IEndTurn(0.2f));
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
            MouseLeft();

            Debug.Log("Cast Flurry on " + target.name);

            multihitMax = 5;
            StartCoroutine(IDelayDamage(flurryDamage, 0.05f, spawnPos, target, targetHealth));
            StartCoroutine(IDelayDamage(flurryDamage, 0.15f, spawnPos, target, targetHealth));
            StartCoroutine(IDelayDamage(flurryDamage, 0.25f, spawnPos, target, targetHealth));
            StartCoroutine(IDelayDamage(flurryDamage, 0.35f, spawnPos, target, targetHealth));
            StartCoroutine(IDelayDamage(flurryFinalHitDamage, 0.7f, spawnPos, target, targetHealth));

            ResetAbility();

            StartCoroutine(IEndTurn(1f));
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }

    }

    #endregion

    #region Spells

    private void StormBarrage(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int cost = stormBarrageCost;
            if (playerStats.CheckMana(cost))
            {
                Debug.Log("Cast Flurry on " + target.name);

                multihitMax = 3;
                StartCoroutine(IDelayDamage(stormBarrageDamage, 0.05f, spawnPos, target, targetHealth));
                StartCoroutine(IDelayDamage(stormBarrageDamage, 0.2f, spawnPos, target, targetHealth));
                StartCoroutine(IDelayDamage(stormBarrageDamage, 0.35f, spawnPos, target, targetHealth));

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

    private void Firebolt(GameObject target)
    {
        EnemyStats targetHealth = target.GetComponent<EnemyStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int cost = fireboltCost;
            if (playerStats.CheckMana(cost))
            {
                SpawnAttackEffect(spawnPos.position, target);
                int damage = (int)Random.Range(fireboltDamage.x, fireboltDamage.y);

                Debug.Log("Cast Firebolt on " + target.name);

                targetHealth.ChangeHealth(damage, true);
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
            MouseLeft();

            int cost = chillTouchCost;
            if (playerStats.CheckMana(cost))
            {
                SpawnAttackEffect(spawnPos.position, target);

                int damage = (int)Random.Range(chillTouchDamage.x, chillTouchDamage.y);

                Debug.Log("Cast Chill Touch on " + target.name);

                targetHealth.ChangeHealth(damage, true);
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
            MouseLeft();

            int cost = chainLightningCost;
            if (playerStats.CheckMana(cost))
            {
                string message = "Cast Chain Lightning on ";
                multihitMax = combatManager.enemyManager.enemies.Count;

                foreach (var item in combatManager.enemyManager.enemies)
                {
                    EnemyStats itemHealth = item.gameObject.GetComponent<EnemyStats>();
                    

                    if (item.gameObject == target)
                    {
                        StartCoroutine(IDelayDamage(chainLightningDamage, 0f, spawnPos, item.gameObject, itemHealth));
                    }
                    else
                    {
                        StartCoroutine(IDelayDamage(chainLightningDamage, 0.25f, target.transform, item.gameObject, itemHealth));
                    }
                }

                Debug.Log(message);

                playerStats.ChangeMana(cost, true);
                combatManager.Ap.SetActive(true);
                combatManager.ApValue.text = cost.ToString();

                ResetAbility();

                StartCoroutine(IEndTurn(0.6f));
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
            MouseLeft();

            int cost = cureWoundsCost;
            if (playerStats.CheckMana(cost))
            {
                int heal = (int)Random.Range(cureWoundsHealing.x, cureWoundsHealing.y);

                Debug.Log("Cast CureWounds on " + target.name);

                targetHealth.ChangeHealth(heal, false);
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

    private void HealingPotion(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int cost = 1;
            if (playerStats.CheckPotions(cost))
            {
                int heal = (int)Random.Range(healthPotionHealing.x, healthPotionHealing.y);

                Debug.Log(target.name + " used a healing potion");

                targetHealth.ChangeHealth(heal, false);
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
            Debug.Log("You cannot use a healing potion on that character");
        }
    }

    private void ArcanaPotion(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int cost = 1;
            if (false /*playerStats.CheckArcanaPotions(cost)*/)
            {
                int restore = Random.Range(1 /*healthPotion.x, healthPotion.y*/, 1);

                Debug.Log(target.name + " used an arcana potion");

                targetHealth.ChangeMana(restore, false);
                combatManager.Healing.SetActive(true);
                combatManager.HealingValue.text = restore.ToString();

                //playerStats.ChangePotions(cost, true);

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
            Debug.Log("You cannot use an arcana potion on that character");
        }
    }

    #endregion

    #endregion

    #region Helper Functions

    public void DelayDamage(Vector2 damageRange, float delay, Transform origin, GameObject target, EnemyStats targetHealth)
    {
        StartCoroutine(IDelayDamage(damageRange, delay, origin, target, targetHealth));
    }

    private IEnumerator IDelayDamage(Vector2 damageRange, float delay, Transform origin, GameObject target, EnemyStats targetHealth)
    {
        Vector3 originRef = origin.position;

        yield return new WaitForSeconds(delay);

        if (targetHealth != null)
        {
            SpawnAttackEffect(originRef, target);

            int damage = (int)Random.Range(damageRange.x, damageRange.y);
            targetHealth.ChangeHealth(damage, true);

            multihitTally += damage;
            multihitCount++;

            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = multihitTally.ToString();

            Debug.Log(multihitCount + " hits for " + multihitTally + " points of damage");

            if (multihitCount >= multihitMax)
            {
                Debug.Log("Finish multihit");

                multihitCount = 0;
                multihitTally = 0;
            }
        }
    }

    public void SpawnAttackEffect(Vector3 origin, GameObject target)
    {
        if (attackFX != null)
        {
            GameObject attackRef = Instantiate(attackFX, origin, new Quaternion(0, 0, 0, 0)) as GameObject;

            ProjectileMovement projScript = attackRef.GetComponent<ProjectileMovement>();

            if (projScript != null)
            {
                projScript.target = target;
                projScript.moving = true;
            }
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