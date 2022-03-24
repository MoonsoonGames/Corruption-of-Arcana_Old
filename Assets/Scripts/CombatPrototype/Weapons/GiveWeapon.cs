using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeapon : MonoBehaviour
{
    LoadSettings loadSettings;

    public void GiveSpecifiedWeapon(Weapon weapon)
    {
        if (loadSettings == null)
        {
            loadSettings = GameObject.FindObjectOfType<LoadSettings>();
        }

        if (loadSettings != null)
        {
            loadSettings.AddWeapon(weapon);
        }
    }
}
