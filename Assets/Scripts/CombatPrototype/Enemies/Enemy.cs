using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string displayName;

    [TextArea(1, 4)]
    public string desciption;

    public Vector2 damage = new Vector2(18, 22);

    public E_DamageTypes damageType;

    GameObject player;

    private PlayerStats playerStats;

    private EnemyStats enemyStats;

    public string attackName = "Slash";

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
    }

    public void TakeTurn()
    {
        if (canAttack)
        {
            SpawnAttackEffect(player, damageType);

            int randDMG = (int)Random.Range(damage.x, damage.y);

            playerStats.ChangeHealth(randDMG, true, damageType, out int damageTaken, this.gameObject);

            //Debug.Log(gameObject.name + " cast " + attackName + " for " + randDMG + " damage. It's really effective!");
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
        if (display)
            descriptionInfo.ReadyCard(displayName, attackName, damage, damageType, desciption, sprite);
        else
            descriptionInfo.RemoveCard();
    }
}
