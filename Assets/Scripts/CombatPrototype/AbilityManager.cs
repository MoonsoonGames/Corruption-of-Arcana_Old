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

    private string readyAbility;
    private ActiveCard activeCard;

    public float popupDuration = 5f;

    Targetter targetter;

    public GameObject attackFX;
    public Transform spawnPos;

    public SliderVariation sliderVarScript;

    #region Ability Values

    int multihitTally = 0;
    int multihitCount = 0;

    #region Basic Attacks
    [Header("Basic Attacks")]
    public int slashDamageMin = 9;
    public int slashDamageMax = 12;

    public int criticalSlashDamageMin = 28, criticalSlashDamageMax = 38;

    public int cleaveDamageMin = 10, cleaveDamageMax = 14;

    public int flurryIndividualDamageMin = 6, flurryIndividualDamageMax = 8;

    #endregion

    #region Spells
    [Header("Spells")]
    public int stormBarrageIndividualDamageMin = 12;
    public int stormBarrageIndividualDamageMax = 15;

    public int stormBarrageCost = 55;

    public int fireboltDamageMin = 30, fireboltDamageMax = 35;

    public int fireboltCost = 40;

    public int chillTouchDamageMin = 20, chillTouchDamageMax = 25;

    public int chillTouchCost = 20;

    public int chainLightningDamageMin = 22, chainLightningDamageMax = 28;

    public int chainLightningCost = 60;

    public int cureWoundsHealMin = 32, cureWoundsHealMax = 38;

    public int cureWoundsCost = 40;

    public int healthPotionMin = 38, healthPotionMax = 46;

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
        Vector2 dmg;

        GetReadyAbilityInfo(out multihit, out dmg);

        sliderVarScript.ApplyPreview(dmg);
    }

    public void MouseLeft()
    {
        //Debug.Log("Button stop hovering");
        sliderVarScript.StopPreview();
    }

    public void GetReadyAbilityInfo(out bool multihit, out Vector2 dmg)
    {
        //set caller values
        multihit = false;
        dmg = new Vector2(0, 0);

        if (readyAbility == "Slash")
        {
            multihit = false;
            dmg = new Vector2(slashDamageMin, slashDamageMax);
        }
        else if (readyAbility == "CriticalSlash")
        {
            multihit = false;
            dmg = new Vector2(criticalSlashDamageMin, criticalSlashDamageMax);
        }
        else if (readyAbility == "Cleave")
        {
            multihit = true;
            dmg = new Vector2(cleaveDamageMin, cleaveDamageMax);
        }
        else if (readyAbility == "Flurry")
        {
            multihit = false;
            dmg = new Vector2(flurryIndividualDamageMin * 5, flurryIndividualDamageMax * 5);
        }
        else if (readyAbility == "StormBarrage")
        {
            multihit = false;
            dmg = new Vector2(stormBarrageIndividualDamageMin * 5, stormBarrageIndividualDamageMax * 5);
        }
        else if (readyAbility == "Firebolt")
        {
            multihit = false;
            dmg = new Vector2(fireboltDamageMin, fireboltDamageMax);
        }
        else if (readyAbility == "ChillTouch")
        {
            multihit = false;
            dmg = new Vector2(chillTouchDamageMin, chillTouchDamageMax);
        }
        else if (readyAbility == "ChainLightning")
        {
            multihit = true;
            dmg = new Vector2(chainLightningDamageMin, chainLightningDamageMax);
        }
        else if (readyAbility == "CureWounds")
        {
            multihit = false;
            dmg = new Vector2(-cureWoundsHealMin, -cureWoundsHealMax);
        }
        else if (readyAbility == "HealingPotion")
        {
            multihit = false;
            dmg = new Vector2(-healthPotionMin, -healthPotionMax);
        }
    }

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

    public void SetAbility(string abilityName)
    {
        EnemyInfo(null);

        readyAbility = abilityName;

        if (activeCard != null)
        {
            if (abilityName == "Slash")
            {
                activeCard.ReadyCard("Slash", new Vector2(slashDamageMin, slashDamageMax), "Physical DMG", 0, "Hit your opponent with a basic attack", "AP");
            }
            else if (abilityName == "CriticalSlash")
            {
                activeCard.ReadyCard("Critical Slash", new Vector2(criticalSlashDamageMin, criticalSlashDamageMax), "Physical DMG", 0, "Hit your opponent with a critical attack", "AP");
            }
            else if (abilityName == "Cleave")
            {
                activeCard.ReadyCard("Cleave", new Vector2(cleaveDamageMin, cleaveDamageMax), "Physical DMG", 0, "With a sweeping strike, you hit all opponents in your way", "AP");
            }
            else if (abilityName == "Flurry")
            {
                activeCard.ReadyCard("Flurry", new Vector2(flurryIndividualDamageMin * 5, flurryIndividualDamageMax * 5), "Physical DMG", 0, "Hit your opponent with five times in quick succession", "AP");
            }
            else if (abilityName == "StormBarrage")
            {
                activeCard.ReadyCard("Storm Barrage", new Vector2(stormBarrageIndividualDamageMin * 5, stormBarrageIndividualDamageMax * 5), "Static DMG", stormBarrageCost, "Unleash a devastating ray of lightning at your target", "AP");
            }
            else if (abilityName == "Firebolt")
            {
                activeCard.ReadyCard("Firebolt", new Vector2(fireboltDamageMin, fireboltDamageMax), "Ember DMG", fireboltCost, "Throw a searing blast of fire at a target", "AP");
            }
            else if (abilityName == "ChillTouch")
            {
                activeCard.ReadyCard("Chill Touch", new Vector2(chillTouchDamageMin, chillTouchDamageMax), "Frost DMG", chillTouchCost, "Attempt to chill your opponent with a blast of frost from your hand", "AP");
            }
            else if (abilityName == "ChainLightning")
            {
                activeCard.ReadyCard("Chain Lightning", new Vector2(chainLightningDamageMin, chainLightningDamageMax), "Static DMG", chainLightningCost, "Call down an electric storm to crush all targets and send them reeling", "AP");
            }
            else if (abilityName == "CureWounds")
            {
                activeCard.ReadyCard("Cure Wounds", new Vector2(cureWoundsHealMin, cureWoundsHealMax), "Healing", cureWoundsCost, "With a cleansing surge, you mend your body of wounds", "AP");
            }
            else if (abilityName == "HealingPotion")
            {
                activeCard.ReadyCard("Healing Potion", new Vector2(healthPotionMin, healthPotionMax), "Healing", 1, "By imbibing a healing potion, you restore your vitality... tastes grim though", "Health Potions");
            }
        }

        //Debug.Log("Readied " + abilityName + " ability.");

        if (abilityName == "HealingPotion" || abilityName == "CureWounds")
        {
            combatManager.TargetEnemies(false);
            targetter.SetVisibility(true);
        }
        else
        {
            combatManager.TargetEnemies(true);
            targetter.SetVisibility(false);
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

            int damage = Random.Range(slashDamageMin, slashDamageMax);

            Debug.Log("Cast Slash on " + target.name);

            targetHealth.ChangeHeath(damage, true);
            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = damage.ToString();

            SpawnAttackEffect(target);

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
            SpawnAttackEffect(target);

            int damage = Random.Range(criticalSlashDamageMin, criticalSlashDamageMax);

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
            MouseLeft();
            foreach (var item in combatManager.enemyManager.enemies)
            {
                SpawnAttackEffect(item.gameObject);
            }

            int damage = Random.Range(cleaveDamageMin, cleaveDamageMax);

            EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

            string message = "Cast Cleave on ";

            foreach (var item in enemies)
            {
                message += item.gameObject.name + ", ";

                item.ChangeHeath(damage, true);

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

            StartCoroutine(IFlurryAttacks(0.05f, target, targetHealth));
            StartCoroutine(IFlurryAttacks(0.15f, target, targetHealth));
            StartCoroutine(IFlurryAttacks(0.25f, target, targetHealth));
            StartCoroutine(IFlurryAttacks(0.35f, target, targetHealth));
            StartCoroutine(IFlurryAttacks(0.7f, target, targetHealth));

            ResetAbility();

            StartCoroutine(IEndTurn(1f));
        }
        else
        {
            Debug.Log("You cannot target that character with this spell");
        }

    }

    private IEnumerator IFlurryAttacks(float delay, GameObject target, EnemyStats targetHealth)
    {
        yield return new WaitForSeconds(delay);

        if (targetHealth != null)
        {
            SpawnAttackEffect(target);

            int damage = Random.Range(flurryIndividualDamageMin, flurryIndividualDamageMax);
            targetHealth.ChangeHeath(damage, true);

            multihitTally += damage;
            multihitCount++;

            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = multihitTally.ToString();

            Debug.Log(multihitCount + " hits for " + multihitTally + " points of damage");

            if (multihitCount >= 5)
            {
                Debug.Log("Finish flurry");

                multihitCount = 0;
                multihitTally = 0;
            }
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

                StartCoroutine(IStormBarrage(0.05f, target, targetHealth));
                StartCoroutine(IStormBarrage(0.15f, target, targetHealth));
                StartCoroutine(IStormBarrage(0.25f, target, targetHealth));
                StartCoroutine(IStormBarrage(0.35f, target, targetHealth));
                StartCoroutine(IStormBarrage(0.7f, target, targetHealth));

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

    private IEnumerator IStormBarrage(float delay, GameObject target, EnemyStats targetHealth)
    {
        yield return new WaitForSeconds(delay);

        if (targetHealth != null)
        {
            SpawnAttackEffect(target);

            int damage = Random.Range(stormBarrageIndividualDamageMin, stormBarrageIndividualDamageMax);
            targetHealth.ChangeHeath(damage, true);

            multihitTally += damage;
            multihitCount++;

            combatManager.Dmg.SetActive(true);
            combatManager.DmgValue.text = multihitTally.ToString();

            Debug.Log(multihitCount + " hits for " + multihitTally + " points of damage");

            if (multihitCount >= 5)
            {
                Debug.Log("Finish flurry");

                multihitCount = 0;
                multihitTally = 0;
            }
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
                SpawnAttackEffect(target);
                int damage = Random.Range(fireboltDamageMin, fireboltDamageMax);

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
            MouseLeft();

            int cost = chillTouchCost;
            if (playerStats.CheckMana(cost))
            {
                SpawnAttackEffect(target);

                int damage = Random.Range(chillTouchDamageMin, chillTouchDamageMax);

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
            MouseLeft();

            int cost = chainLightningCost;
            if (playerStats.CheckMana(cost))
            {
                foreach (var item in combatManager.enemyManager.enemies)
                {
                    SpawnAttackEffect(item.gameObject);
                }

                EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

                string message = "Cast Chain Lightning on ";

                foreach (var item in enemies)
                {
                    int damage = Random.Range(chainLightningDamageMin, chainLightningDamageMax);

                    message += item.gameObject.name + ", ";

                    item.ChangeHeath(damage, true);
                    multihitTally += damage;
                }

                combatManager.Dmg.SetActive(true);
                combatManager.DmgValue.text = multihitTally.ToString();
                multihitTally = 0;

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
            MouseLeft();

            int cost = cureWoundsCost;
            if (playerStats.CheckMana(cost))
            {
                int heal = Random.Range(cureWoundsHealMin, cureWoundsHealMax);

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

    private void HealingPotion(GameObject target)
    {
        PlayerStats targetHealth = target.GetComponent<PlayerStats>();

        if (targetHealth != null)
        {
            MouseLeft();

            int cost = 1;
            if (playerStats.CheckPotions(cost))
            {
                int heal = Random.Range(healthPotionMin, healthPotionMax);

                Debug.Log(target.name + " used a healing potion");

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
                int restore = Random.Range(1 /*healthPotionMin, healthPotionMax*/, 1);

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

    private void SpawnAttackEffect(GameObject target)
    {
        if (attackFX != null)
        {
            GameObject attackRef = Instantiate(attackFX, spawnPos) as GameObject;

            ProjectileMovement projScript = attackRef.GetComponent<ProjectileMovement>();

            if (projScript != null)
            {
                projScript.target = target;
                projScript.moving = true;
            }
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

    #endregion
}