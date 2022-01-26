using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    #region Setup

    public int maxHealth = 120;
    protected int health = 120;
    public int maxMana = 120;
    protected int mana = 120;

    public SliderValue healthSliderValue;
    public SliderValue manaSliderValue;

    public CombatManager combatManager;

    public Object killFX;
    public Object healFX;

    Dictionary<StatusParent, int> statuses = new Dictionary<StatusParent, int>();

    protected virtual void Start()
    {
        ResetDamageMultipliers();
    }

    #endregion

    #region Turn Inhibitors

    [HideInInspector]
    public bool charm;
    [HideInInspector]
    public bool revealEntry;
    [HideInInspector]
    public bool skipTurn;
    [HideInInspector]
    public bool sleepTurn;
    [HideInInspector]
    public bool silence;
    [HideInInspector]
    public bool slow;

    #endregion

    #region Flash

    public Image image;
    public Color normalColour = new Color(255, 255, 255, 255);
    public Color healColour = new Color(0, 255, 0, 255);
    public Color hitColour = new Color(255, 0, 0, 255);
    protected Color flashColour;
    protected float p = 0;

    protected void Flash(Color newColour)
    {
        CancelInvoke();
        p = 0;

        flashColour = newColour;
        image.color = flashColour;

        InvokeRepeating("RevertColour", 0f, 0.05f);
    }

    protected void RevertColour()
    {
        image.color = LerpColour(flashColour, normalColour, p);

        p += 0.1f;

        if (p == 1)
        {
            CancelInvoke();
            p = 0;
        }
    }

    protected Color LerpColour(Color a, Color b, float i)
    {
        Color lerpColour = new Color(0, 0, 0, 0);

        lerpColour.r = Mathf.Lerp(flashColour.r, normalColour.r, i);
        lerpColour.g = Mathf.Lerp(flashColour.g, normalColour.g, i);
        lerpColour.b = Mathf.Lerp(flashColour.b, normalColour.b, i);
        lerpColour.a = Mathf.Lerp(flashColour.a, normalColour.a, i);

        return lerpColour;
    }

    #endregion

    #region Received Damage Multipliers

    [Header("Received Damage Multipliers")]
    public float basePhysicalMultiplier = 1f;
    public float baseEmberMultiplier = 1f;
    public float baseStaticMultiplier = 1f;
    public float baseBleakMultiplier = 1f;
    public float baseSepticMultiplier = 1f;

    protected float physicalMultiplier = 1f;
    protected float emberMultiplier = 1f;
    protected float staticMultiplier = 1f;
    protected float bleakMultiplier = 1f;
    protected float septicMultiplier = 1f;

    #region Functions

    public void ResetDamageMultipliers()
    {
        physicalMultiplier = basePhysicalMultiplier;
        emberMultiplier = baseEmberMultiplier;
        staticMultiplier = baseStaticMultiplier;
        bleakMultiplier = baseBleakMultiplier;
        septicMultiplier = baseSepticMultiplier;
    }

    public void ResetDamageMultipliersSpecific(bool physicalRes, bool emberRes, bool staticRes, bool bleakRes, bool septicRes)
    {
        if (physicalRes)
            physicalMultiplier = basePhysicalMultiplier;

        if (emberRes)
            emberMultiplier = baseEmberMultiplier;

        if (staticRes)
            staticMultiplier = baseStaticMultiplier;

        if (bleakRes)
            bleakMultiplier = baseBleakMultiplier;

        if (septicRes)
            septicMultiplier = baseSepticMultiplier;
    }

    public void AdjustDamageMultipliers(float physicalAdjust, float emberAdjust, float staticAdjust, float bleakAdjust, float septicAdjust)
    {
        physicalMultiplier += physicalAdjust;
        emberMultiplier += emberAdjust;
        staticMultiplier += staticAdjust;
        bleakMultiplier += bleakAdjust;
        septicMultiplier += septicAdjust;
    }

    #endregion

    #endregion

    #region Health

    public int GetHealth()
    {
        return health;
    }

    public virtual void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken, GameObject attacker)
    {
        damageTaken = 0;
        if (damage && value > 0)
        {
            Flash(hitColour);

            damageTaken = (int)DamageResistance(value, damageType);

            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);

            OnTakeDamageStatus(attacker);

            RemoveSleepStatus();

            /*
            if (killFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(killFX, spawnPos, spawnRot);
            }
            */

            if (health <= 0)
            {
                Die();
            }
        }
        else if (!damage && value > 0)
        {
            damageTaken = 0;
            Flash(healColour);
            health = Mathf.Clamp(health + value, 0, maxHealth);

            if (healFX != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0);
                Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

                spawnPos.x = transform.position.x;
                spawnPos.y = transform.position.y;
                spawnPos.z = transform.position.z - 5f;

                Instantiate(healFX, spawnPos, spawnRot);
            }
        }

        if (healthSliderValue != null)
        {
            healthSliderValue.slider.value = health;
        }
    }

    public float DamageResistance(float damageValue, E_DamageTypes damageType)
    {
        if (damageType == E_DamageTypes.Physical)
        {
            return damageValue * physicalMultiplier;
        }
        if (damageType == E_DamageTypes.Ember)
        {
            return damageValue * emberMultiplier;
        }
        if (damageType == E_DamageTypes.Static)
        {
            return damageValue * staticMultiplier;
        }
        if (damageType == E_DamageTypes.Bleak)
        {
            return damageValue * bleakMultiplier;
        }
        if (damageType == E_DamageTypes.Septic)
        {
            return damageValue * septicMultiplier;
        }
        if (damageType == E_DamageTypes.Perforation)
        {
            return damageValue * 1.5f;
        }

        return damageValue;
    }

    public Vector2 DamageResistanceVector(Vector2 damageValue, E_DamageTypes damageType)
    {
        if (damageType == E_DamageTypes.Physical)
        {
            return damageValue * physicalMultiplier;
        }
        if (damageType == E_DamageTypes.Ember)
        {
            return damageValue * emberMultiplier;
        }
        if (damageType == E_DamageTypes.Static)
        {
            return damageValue * staticMultiplier;
        }
        if (damageType == E_DamageTypes.Bleak)
        {
            return damageValue * bleakMultiplier;
        }
        if (damageType == E_DamageTypes.Septic)
        {
            return damageValue * septicMultiplier;
        }
        if (damageType == E_DamageTypes.Perforation)
        {
            return damageValue * 1.5f;
        }

        return damageValue;
    }

    public float HealthPercentage()
    {
        return (float)health / (float)maxHealth;
    }

    protected virtual void Die()
    {

    }

    #endregion

    #region Arcana

    public int GetMana()
    {
        return mana;
    }

    public virtual void ChangeMana(int value, bool spend)
    {
        if (spend)
        {
            mana = Mathf.Clamp(mana - value, 0, maxMana);
        }
        else
        {
            mana = Mathf.Clamp(mana + value, 0, maxMana);
        }

        if (manaSliderValue != null)
        {
            manaSliderValue.slider.value = mana;
        }
    }

    public bool CheckMana(float value)
    {
        return mana >= value;
    }

    #endregion

    #region Statuses

    public virtual void ApplyStatus(StatusParent status, GameObject caster, int statusDuration)
    {
        //Debug.Log("Applied " + status.effectName);

        int duration = statusDuration;

        if (caster == this.gameObject)
        {
            duration++;
        }

        if (!statuses.ContainsKey(status))
        {
            statuses.Add(status, duration);

            status.OnApply(this.gameObject, this.gameObject);
        }
        else
        {
            if (statuses[status] < duration)
            {
                statuses[status] = duration;
            }
        }
    }

    public void OnTurnStartStatus()
    {
        foreach (var item in statuses)
        {
            item.Key.OnTurnStart(this.gameObject, this.gameObject);
        }
    }

    public void OnTurnEndStatus()
    {
        Dictionary<StatusParent, int> statusesCopy = new Dictionary<StatusParent, int>();

        foreach (var item in statuses)
        {
            item.Key.OnTurnEnd(this.gameObject);

            int turnsLeft = item.Value - 1;
            //Debug.Log(item.Key + " has " + turnsLeft + " turns left");
            if (turnsLeft > 0)
            {
                statusesCopy.Add(item.Key, turnsLeft);
                item.Key.OnTurnEnd(this.gameObject);
            }
            else
            {
                item.Key.OnRemove(this.gameObject);
            }
        }

        statuses.Clear();

        foreach (var item in statusesCopy)
        {
            statuses.Add(item.Key, item.Value);
        }
    }

    public void OnTakeDamageStatus(GameObject attacker)
    {
        foreach (var item in statuses)
        {
            item.Key.OnTakeDamage(attacker, this.gameObject, GameObject.FindObjectOfType<AbilityManager>());
        }
    }

    public void RemoveSleepStatus()
    {
        Dictionary<StatusParent, int> statusesCopy = new Dictionary<StatusParent, int>();

        foreach (var item in statuses)
        {
            if (item.Key.sleepTurn == false)
            {
                statusesCopy.Add(item.Key, item.Value);
            }
            else
            {
                //Debug.Log("Damage taken, remove sleep status");
                item.Key.OnRemove(this.gameObject);
            }
        }

        statuses.Clear();

        foreach (var item in statusesCopy)
        {
            statuses.Add(item.Key, item.Value);
        }
    }

    #endregion
}