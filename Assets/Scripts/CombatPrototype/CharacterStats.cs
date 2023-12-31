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
    public SliderValue previewSliderValue;
    public SliderValue manaSliderValue;

    public CombatManager combatManager;

    public Object killFX;
    public Object healFX;

    public Dictionary<StatusParent, int> statuses = new Dictionary<StatusParent, int>();

    protected virtual void Start()
    {
        ResetDamageMultipliers();
        audioManager = GameObject.FindObjectOfType<BGMManager>();
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

    #region Feedback

    #region Visual Feedback

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

    #region Hit Shake

    //apply screen shake - if player
    //apply character shake - preferred

    public float duration = 0.1f;
    public float intensityMultiplier = 0.005f;

    void CharacterShake(float duration, float intensity)
    {
        Debug.Log("Character shake");
        Vector3 originalPos = gameObject.transform.position;

        float randx = transform.position.x + Random.Range(-intensity, intensity);
        float randy = transform.position.y + Random.Range(-intensity, intensity);

        Vector3 newPos = new Vector3(randx, randy, transform.position.z);

        //Debug.Log(name + " shakes from " + transform.position + " to " + newPos);
        transform.position = newPos;

        StartCoroutine(ResetShake(originalPos, duration));
    }

    IEnumerator ResetShake(Vector3 originalPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(name + " returns from " + transform.position + " to " + originalPosition);
        transform.position = originalPosition;
    }

    #endregion

    #region Hit Particles

    public Object hitFX;

    void HitFX(Object FX)
    {
        if (FX != null)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Quaternion spawnRot = new Quaternion(0, 0, 0, 0);

            spawnPos.x = transform.position.x;
            spawnPos.y = transform.position.y;
            spawnPos.z = transform.position.z - 5f;

            Instantiate(FX, spawnPos, spawnRot);
        }
    }

    #endregion

    #endregion

    #region Audio Feedback

    BGMManager audioManager;
    public AudioClip[] hitSounds;
    public AudioClip[] deathSounds;

    void PlaySoundEffect(AudioClip[] audioClips)
    {
        if (audioClips.Length > 0 && audioManager != null)
            audioManager.PlaySoundEffect(GetSoundEffect(audioClips), 4f);
    }

    AudioClip GetSoundEffect(AudioClip[] soundArray)
    {
        if (soundArray.Length > 1)
            return soundArray[Random.Range(0, soundArray.Length)];
        else if (soundArray.Length == 1)
            return soundArray[0];
        else
            return null;
    } 

    #endregion

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

    public virtual void ChangeHealth(int value, bool damage, E_DamageTypes damageType, out int damageTaken, GameObject attacker, bool canBeCountered, Object attackHitFX)
    {
        damageTaken = 0;
        if (damage && value > 0)
        {
            Flash(hitColour);
            CharacterShake(duration, intensityMultiplier * value);
            HitFX(hitFX);
            HitFX(attackHitFX);
            PlaySoundEffect(hitSounds);

            damageTaken = (int)DamageResistance(value, damageType);

            health = Mathf.Clamp(health - damageTaken, 0, maxHealth);

            if (canBeCountered && attacker != null)
            {
                OnTakeDamageStatus(attacker);
            }

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
                PlaySoundEffect(deathSounds);
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

        if (previewSliderValue != null)
        {
            previewSliderValue.slider.value = health;
        }
    }

    public float DamageResistance(float damageValue, E_DamageTypes damageType)
    {
        switch (damageType)
        {
            case E_DamageTypes.Physical:
                return damageValue * physicalMultiplier;
            case E_DamageTypes.Ember:
                return damageValue * emberMultiplier;
            case E_DamageTypes.Static:
                return damageValue * staticMultiplier;
            case E_DamageTypes.Bleak:
                return damageValue * bleakMultiplier;
            case E_DamageTypes.Septic:
                return damageValue * septicMultiplier;
            case E_DamageTypes.Perforation:
                float lowestResistance = 1;
                if (lowestResistance < LowestResistanceFloat())
                {
                    lowestResistance = LowestResistanceFloat();
                }
                return damageValue * lowestResistance;
            default:
                return damageValue;
        }
    }

    public Vector2 DamageResistanceVector(Vector2 damageValue, E_DamageTypes damageType)
    {
        switch (damageType)
        {
            case E_DamageTypes.Physical:
                return damageValue * physicalMultiplier;
            case E_DamageTypes.Ember:
                return damageValue * emberMultiplier;
            case E_DamageTypes.Static:
                return damageValue * staticMultiplier;
            case E_DamageTypes.Bleak:
                return damageValue * bleakMultiplier;
            case E_DamageTypes.Septic:
                return damageValue * septicMultiplier;
            case E_DamageTypes.Perforation:
                return damageValue;
            default:
                return damageValue;
        }
    }

    public float CheckResistances(E_DamageTypes damageType)
    {
        switch (damageType)
        {
            case E_DamageTypes.Physical:
                return physicalMultiplier;
            case E_DamageTypes.Ember:
                return emberMultiplier;
            case E_DamageTypes.Static:
                return staticMultiplier;
            case E_DamageTypes.Bleak:
                return bleakMultiplier;
            case E_DamageTypes.Septic:
                return septicMultiplier;
            case E_DamageTypes.Perforation:
                float lowestResistance = 1;
                if (lowestResistance < LowestResistanceFloat())
                {
                    lowestResistance = LowestResistanceFloat();
                }
                Debug.Log("Highest Multiplier: " + lowestResistance);
                return lowestResistance;
            default:
                return 1;
        }
    }

    public E_DamageTypes LowestResistanceType()
    {
        float lowestResistance = 0f;
        E_DamageTypes lowestResistanceType = E_DamageTypes.Physical;

        if (lowestResistance < physicalMultiplier)
        {
            lowestResistance = physicalMultiplier;
            lowestResistanceType = E_DamageTypes.Physical;
        }
        if (lowestResistance < emberMultiplier)
        {
            lowestResistance = emberMultiplier;
            lowestResistanceType = E_DamageTypes.Ember;
        }
        if (lowestResistance < staticMultiplier)
        {
            lowestResistance = staticMultiplier;
            lowestResistanceType = E_DamageTypes.Static;
        }
        if (lowestResistance < bleakMultiplier)
        {
            lowestResistance = bleakMultiplier;
            lowestResistanceType = E_DamageTypes.Bleak;
        }
        if (lowestResistance < septicMultiplier)
        {
            lowestResistance = septicMultiplier;
            lowestResistanceType = E_DamageTypes.Septic;
        }

        return lowestResistanceType;
    }

    public float LowestResistanceFloat()
    {
        float lowestResistance = 0;

        if (lowestResistance < physicalMultiplier)
        {
            lowestResistance = physicalMultiplier;
        }
        if (lowestResistance < emberMultiplier)
        {
            lowestResistance = emberMultiplier;
        }
        if (lowestResistance < staticMultiplier)
        {
            lowestResistance = staticMultiplier;
        }
        if (lowestResistance < bleakMultiplier)
        {
            lowestResistance = bleakMultiplier;
        }
        if (lowestResistance < septicMultiplier)
        {
            lowestResistance = septicMultiplier;
        }

        return lowestResistance;
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

    public StatusIconSpawner iconSpawner;

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
            status.OnApply(this.gameObject, this.gameObject);

            if (statuses[status] < duration)
            {
                statuses[status] = duration;
            }
        }

        SetupStatusIcons();
    }

    public void OnTurnStartStatus()
    {
        foreach (var item in statuses)
        {
            item.Key.OnTurnStart(this.gameObject, this.gameObject);
        }

        SetupStatusIcons();
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

        SetupStatusIcons();
    }

    public void OnTakeDamageStatus(GameObject attacker)
    {
        foreach (var item in statuses)
        {
            item.Key.OnTakeDamage(attacker, this.gameObject, GameObject.FindObjectOfType<AbilityManager>());
        }

        SetupStatusIcons();
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

        SetupStatusIcons();
    }

    public void RemoveStatus(StatusParent status)
    {
        Dictionary<StatusParent, int> statusesCopy = new Dictionary<StatusParent, int>();

        foreach (var item in statuses)
        {
            if (item.Key != status)
            {
                statusesCopy.Add(item.Key, item.Value);
            }
        }

        statuses.Clear();

        foreach (var item in statusesCopy)
        {
            statuses.Add(item.Key, item.Value);
        }

        status.OnRemove(this.gameObject);
        SetupStatusIcons();
    }

    void SetupStatusIcons()
    {
        if (iconSpawner != null)
        {
            iconSpawner.SetupIcons(statuses);
        }
    }

    #endregion
}