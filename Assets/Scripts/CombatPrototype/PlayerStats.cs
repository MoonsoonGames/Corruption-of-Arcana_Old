using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 1f;
    float health = 1f;
    public float maxMana = 1f;
    float mana = 1f;

    public CombatManager combatManager;

    private void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    public float GetHealth()
    {
        return health;
    }

    public void ChangeHeath(float value, bool damage)
    {
        if (damage)
        {
            health = Mathf.Clamp(health - value, 0, maxHealth);

            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
        }
    }

    void Die()
    {
        combatManager.ShowEndScreen(false);
    }

    public float GetMana()
    {
        return mana;
    }

    public void ChangeMana(float value, bool spend)
    {
        if (spend)
        {
            mana = Mathf.Clamp(mana - value, 0, maxMana);
        }
        else
        {
            mana = Mathf.Clamp(mana + value, 0, maxMana);
        }
    }
}
