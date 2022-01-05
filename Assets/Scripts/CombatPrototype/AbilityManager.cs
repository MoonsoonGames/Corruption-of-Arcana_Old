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

    int multihitMax = 0;
    int multihitTally = 0;
    int multihitCount = 0;

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
            dmg = new Vector2(slashDamage.x, slashDamage.y);
        }
        else if (readyAbility == "CriticalSlash")
        {
            multihit = false;
            dmg = new Vector2(criticalSlashDamage.x, criticalSlashDamage.y);
        }
        else if (readyAbility == "Cleave")
        {
            multihit = true;
            dmg = new Vector2(cleaveDamage.x, cleaveDamage.y);
        }
        else if (readyAbility == "Flurry")
        {
            multihit = false;
            dmg = new Vector2((flurryDamage.x * 4) + flurryFinalHitDamage.x, (flurryDamage.y * 4) + flurryFinalHitDamage.y);
        }
        else if (readyAbility == "StormBarrage")
        {
            multihit = false;
            dmg = new Vector2(stormBarrageDamage.x * 3, stormBarrageDamage.y * 3);
        }
        else if (readyAbility == "Firebolt")
        {
            multihit = false;
            dmg = new Vector2(fireboltDamage.x, fireboltDamage.y);
        }
        else if (readyAbility == "ChillTouch")
        {
            multihit = false;
            dmg = new Vector2(chillTouchDamage.x, chillTouchDamage.y);
        }
        else if (readyAbility == "ChainLightning")
        {
            multihit = true;
            dmg = new Vector2(chainLightningDamage.x, chainLightningDamage.y);
        }
        else if (readyAbility == "CureWounds")
        {
            multihit = false;
            dmg = new Vector2(-cureWoundsHealing.x, -cureWoundsHealing.y);
        }
        else if (readyAbility == "HealingPotion")
        {
            multihit = false;
            dmg = new Vector2(-healthPotionHealing.x, -healthPotionHealing.y);
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
                activeCard.ReadyCard("Slash", new Vector2(slashDamage.x, slashDamage.y), "Physical DMG", 0, "Hit your opponent with a basic attack", "AP");
            }
            else if (abilityName == "CriticalSlash")
            {
                activeCard.ReadyCard("Critical Slash", new Vector2(criticalSlashDamage.x, criticalSlashDamage.y), "Physical DMG", 0, "Hit your opponent with a critical attack", "AP");
            }
            else if (abilityName == "Cleave")
            {
                activeCard.ReadyCard("Cleave", new Vector2(cleaveDamage.x, cleaveDamage.y), "Physical DMG", 0, "With a sweeping strike, you hit all opponents in your way", "AP");
            }
            else if (abilityName == "Flurry")
            {
                activeCard.ReadyCard("Flurry", new Vector2((flurryDamage.x * 4) + flurryFinalHitDamage.x, (flurryDamage.y * 4) + flurryFinalHitDamage.y), "Physical DMG", 0, "Hit your opponent with five times in quick succession", "AP");
            }
            else if (abilityName == "StormBarrage")
            {
                activeCard.ReadyCard("Storm Barrage", new Vector2(stormBarrageDamage.x * 3, stormBarrageDamage.y * 3), "Static DMG", stormBarrageCost, "Unleash a devastating ray of lightning at your target", "AP");
            }
            else if (abilityName == "Firebolt")
            {
                activeCard.ReadyCard("Firebolt", new Vector2(fireboltDamage.x, fireboltDamage.y), "Ember DMG", fireboltCost, "Throw a searing blast of fire at a target", "AP");
            }
            else if (abilityName == "ChillTouch")
            {
                activeCard.ReadyCard("Chill Touch", new Vector2(chillTouchDamage.x, chillTouchDamage.y), "Frost DMG", chillTouchCost, "Attempt to chill your opponent with a blast of frost from your hand", "AP");
            }
            else if (abilityName == "ChainLightning")
            {
                activeCard.ReadyCard("Chain Lightning", new Vector2(chainLightningDamage.x, chainLightningDamage.y), "Static DMG", chainLightningCost, "Call down an electric storm to crush all targets and send them reeling", "AP");
            }
            else if (abilityName == "CureWounds")
            {
                activeCard.ReadyCard("Cure Wounds", new Vector2(cureWoundsHealing.x, cureWoundsHealing.y), "Healing", cureWoundsCost, "With a cleansing surge, you mend your body of wounds", "AP");
            }
            else if (abilityName == "HealingPotion")
            {
                activeCard.ReadyCard("Healing Potion", new Vector2(healthPotionHealing.x, healthPotionHealing.y), "Healing", 1, "By imbibing a healing potion, you restore your vitality... tastes grim though", "Health Potions");
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

            int damage = (int)Random.Range(slashDamage.x, slashDamage.y);

            Debug.Log("Cast Slash on " + target.name);

            targetHealth.ChangeHeath(damage, true);
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
                SpawnAttackEffect(spawnPos.position, item.gameObject);
            }

            int damage = (int)Random.Range(cleaveDamage.x, cleaveDamage.y);

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
                SpawnAttackEffect(spawnPos.position, target);

                int damage = (int)Random.Range(chillTouchDamage.x, chillTouchDamage.y);

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
                string message = "Cast Chain Lightning on ";
                multihitMax = combatManager.enemyManager.enemies.Count;

                foreach (var item in combatManager.enemyManager.enemies)
                {
                    EnemyStats itemHealth = item.gameObject.GetComponent<EnemyStats>();
                    message += item.gameObject.name + ", ";

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
                int heal = (int)Random.Range(healthPotionHealing.x, healthPotionHealing.y);

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

    private IEnumerator IDelayDamage(Vector2 damageRange, float delay, Transform origin, GameObject target, EnemyStats targetHealth)
    {
        Vector3 originRef = origin.position;

        yield return new WaitForSeconds(delay);

        if (targetHealth != null)
        {
            SpawnAttackEffect(originRef, target);

            int damage = (int)Random.Range(damageRange.x, damageRange.y);
            targetHealth.ChangeHeath(damage, true);

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

    private void SpawnAttackEffect(Vector3 origin, GameObject target)
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