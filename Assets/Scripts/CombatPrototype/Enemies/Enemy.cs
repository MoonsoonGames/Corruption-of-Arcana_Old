using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string displayName;
    public int guidebookOrder;

    [TextArea(1, 4)]
    public string desciption;

    AbilityManager abilityManager;
    GameObject player;
    private PlayerStats playerStats;
    private EnemyStats enemyStats;

    private bool canAttack = true;

    LoadSettings loadSettings;

    public EnemyDescription descriptionInfo;

    Sprite sprite;
    public Image image;

    public GameObject attackFX;
    public Transform spawnPos;

    #region Damage Type Colours
    [Header("Damage Type Colours - Image")]
    public Color physicalColour;
    public Color emberColour;
    public Color staticColour;
    public Color bleakColour;
    public Color septicColour;

    [Header("Damage Type Colours - Trail")]
    public Gradient physicalTrailColour;
    public Gradient emberTrailColour;
    public Gradient staticTrailColour;
    public Gradient bleakTrailColour;
    public Gradient septicTrailColour;
    #endregion

    private void Start()
    {
        sprite = image.sprite;

        if (displayName != null)
        {
            displayName = gameObject.name;
        }

        player = GameObject.Find("Player");

        playerStats = player.GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();

        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }

    public List<CardParent> basicAttacks;
    public int currentAttack = 0;
    public List<CardParent> spells;
    public int currentSpell = 0;

    public void AttemptTakeTurn()
    {
        if (enemyStats != null)
        {
            if (enemyStats.slow)
            {
                Debug.Log("slowed");
                float random = Random.Range(0f, 1f);

                if (random >= 0.3f)
                {
                    Debug.Log("slowed, but still took turn");
                    TakeTurn();
                }
            }
            else
            {
                TakeTurn();
            }
        }
    }

    void TakeTurn()
    {
        if (canAttack && enemyStats != null && abilityManager != null)
        {
            if (enemyStats.charm)
            {
                //charm code here
            }
            else if (enemyStats.silence)
            {
                basicAttacks[currentAttack].CastSpell(player, this.gameObject, abilityManager);
                currentAttack++;
                if (currentAttack >= basicAttacks.Count)
                {
                    currentAttack = 0;
                }
            }
            else if (enemyStats.skipTurn || enemyStats.sleepTurn)
            {
                currentSpell++;
                if (currentSpell >= spells.Count)
                {
                    currentSpell = 0;
                }

                currentAttack++;
                if (currentAttack >= basicAttacks.Count)
                {
                    currentAttack = 0;
                }
            }
            else
            {
                if (spells.Count > 0)
                {
                    if (spells[currentSpell].selfInterpretationUnlocked)
                    {
                        spells[currentSpell].CastSpell(this.gameObject, this.gameObject, abilityManager);
                        currentSpell++;
                        if (currentSpell >= spells.Count)
                        {
                            currentSpell = 0;
                        }
                    }
                    else if (spells[currentSpell].targetInterpretationUnlocked)
                    {
                        spells[currentSpell].CastSpell(player, this.gameObject, abilityManager);
                        currentSpell++;
                        if (currentSpell >= spells.Count)
                        {
                            currentSpell = 0;
                        }
                    }
                }
                else
                {
                    basicAttacks[currentAttack].CastSpell(player, this.gameObject, abilityManager);
                    currentAttack++;
                    if (currentAttack >= basicAttacks.Count)
                    {
                        currentAttack = 0;
                    }
                }
            }
        }
    }

    public float GetEndTurnDelay()
    {
        if (enemyStats != null)
        {
            if (enemyStats.charm)
            {
                return 0;
            }
            else if (enemyStats.silence)
            {
                return basicAttacks[currentAttack].targetEndTurnDelay;
            }
            else if (enemyStats.skipTurn || enemyStats.sleepTurn)
            {
                return 0;
            }
            else
            {
                if (spells.Count > 0)
                {
                    if (spells[currentSpell].selfInterpretationUnlocked)
                    {
                        return spells[currentSpell].selfEndTurnDelay;
                    }
                    else if (spells[currentSpell].targetInterpretationUnlocked)
                    {
                        return spells[currentSpell].targetEndTurnDelay;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return basicAttacks[currentAttack].targetEndTurnDelay;
                }
            }
        }
        else
        {
            return 0;
        }
    }

    public void GetAbilityInfo(out string attackName, out Vector2Int damage, out E_DamageTypes damageType, out string description)
    {
        attackName = "";
        damage = new Vector2Int(0, 0);
        damageType = E_DamageTypes.Physical;
        description = "";

        if (enemyStats != null)
        {
            if (enemyStats.charm)
            {
                attackName = "Skipping turn";
            }
            else if (enemyStats.silence)
            {
                attackName = basicAttacks[currentAttack].targetName;
                damage = basicAttacks[currentAttack].TotalDmgRange();
                damageType = basicAttacks[currentAttack].damageType;
                description = basicAttacks[currentAttack].targetDescription;
            }
            else if (enemyStats.skipTurn)
            {
                attackName = "Skipping turn";
            }
            else if (enemyStats.sleepTurn)
            {
                attackName = "Sleeping";
                description = "Target will awake upon taking damage";
            }
            else
            {
                if (spells.Count > 0)
                {
                    if (spells[currentSpell].selfInterpretationUnlocked)
                    {
                        attackName = spells[currentSpell].selfName;
                        damage = spells[currentSpell].selfHeal;
                        damageType = spells[currentSpell].restoreType;
                        description = spells[currentSpell].selfDescription;
                    }
                    else if (spells[currentSpell].targetInterpretationUnlocked)
                    {
                        attackName = spells[currentSpell].targetName;
                        damage = spells[currentSpell].TotalDmgRange();
                        damageType = spells[currentSpell].damageType;
                        description = spells[currentSpell].targetDescription;
                    }
                }
                else
                {
                    attackName = basicAttacks[currentAttack].targetName;
                    damage = basicAttacks[currentAttack].TotalDmgRange();
                    damageType = basicAttacks[currentAttack].damageType;
                    description = basicAttacks[currentAttack].targetDescription;
                }
            }
        }
    }

    private void SpawnAttackEffect(GameObject target, E_DamageTypes attackType)
    {
        if (attackFX != null)
        {
            GameObject attackRef = Instantiate(attackFX, spawnPos) as GameObject;

            #region Colour

            SpriteRenderer spriteRenderer = attackRef.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                if (attackType == E_DamageTypes.Physical)
                {
                    spriteRenderer.color = physicalColour;
                }
                if (attackType == E_DamageTypes.Ember)
                {
                    spriteRenderer.color = emberColour;
                }
                if (attackType == E_DamageTypes.Static)
                {
                    spriteRenderer.color = staticColour;
                }
                if (attackType == E_DamageTypes.Bleak)
                {
                    spriteRenderer.color = bleakColour;
                }
                if (attackType == E_DamageTypes.Septic)
                {
                    spriteRenderer.color = septicColour;
                }
            }

            Image image = attackRef.GetComponent<Image>();

            if (image != null)
            {
                if (attackType == E_DamageTypes.Physical)
                {
                    image.color = physicalColour;
                }
                if (attackType == E_DamageTypes.Ember)
                {
                    image.color = emberColour;
                }
                if (attackType == E_DamageTypes.Static)
                {
                    image.color = staticColour;
                }
                if (attackType == E_DamageTypes.Bleak)
                {
                    image.color = bleakColour;
                }
                if (attackType == E_DamageTypes.Septic)
                {
                    image.color = septicColour;
                }
            }

            TrailRenderer trail = attackRef.GetComponent<TrailRenderer>();

            if (trail != null)
            {
                if (attackType == E_DamageTypes.Physical)
                {
                    trail.colorGradient = physicalTrailColour;
                }
                if (attackType == E_DamageTypes.Ember)
                {
                    trail.colorGradient = emberTrailColour;
                }
                if (attackType == E_DamageTypes.Static)
                {
                    trail.colorGradient = staticTrailColour;
                }
                if (attackType == E_DamageTypes.Bleak)
                {
                    trail.colorGradient = bleakTrailColour;
                }
                if (attackType == E_DamageTypes.Septic)
                {
                    trail.colorGradient = septicTrailColour;
                }
            }

            #endregion

            ProjectileMovement projScript = attackRef.GetComponent<ProjectileMovement>();

            if (projScript != null)
            {
                projScript.target = target;
                projScript.moving = true;
            }
        }
    }

    public void DisplayCard(bool display)
    {
        if (loadSettings.CheckExposed(displayName) && display)
        {
            string attackName = "";
            Vector2Int damage = new Vector2Int(0, 0);
            E_DamageTypes damageType = E_DamageTypes.Physical;
            string abilityDescription = "";

            GetAbilityInfo(out attackName, out damage, out damageType, out abilityDescription);

            descriptionInfo.ReadyCard(displayName, attackName, damage, damageType, abilityDescription, sprite);
        }
        else
            descriptionInfo.RemoveCard();
    }
}
