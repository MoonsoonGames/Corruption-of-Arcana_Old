using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
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
    #endregion

    #region Health

    public int GetHealth()
    {
        return health;
    }

    public virtual void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken, GameObject attacker)
    {
        if (damage)
        {
            Flash(hitColour);

            damageTaken = (int)DamageResistance(value, damageType);

            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);

            OnTakeDamageStatus(attacker);

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
        else
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

    public void ApplyStatus(StatusParent status, int duration)
    {
        Debug.Log("Applied " + status.effectName);
        if (!statuses.ContainsKey(status))
        {
            statuses.Add(status, duration + 1);

            status.OnApply(this.gameObject);
        }
        else
        {
            if (statuses[status] < duration + 1)
            {
                statuses[status] = duration + 1;
            }
        }
    }

    public void OnTurnStartStatus()
    {
        foreach (var item in statuses)
        {
            item.Key.OnTurnStart(this.gameObject);
        }
    }

    public void OnTurnEndStatus()
    {
        Dictionary<StatusParent, int> statusesCopy = new Dictionary<StatusParent, int>();

        foreach (var item in statuses)
        {
            item.Key.OnTurnEnd(this.gameObject);

            int turnsLeft = item.Value - 1;
            Debug.Log(item.Key + " has " + turnsLeft + " turns left");
            if (turnsLeft > 0)
            {
                statusesCopy.Add(item.Key, turnsLeft);
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
            item.Key.OnTakeDamage(attacker, GameObject.FindObjectOfType<AbilityManager>());
        }
    }

    #endregion
}