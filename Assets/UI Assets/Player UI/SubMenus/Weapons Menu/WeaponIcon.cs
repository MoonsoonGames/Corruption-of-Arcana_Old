using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    LoadSettings loadSettings;
    WeaponSpawner weaponSpawner;
    public Weapon weapon;

    public Text weaponName;
    public Image weaponImage;

    public void IconClicked()
    {
        weaponSpawner.IconClicked(weapon);
    }

    public void Setup(WeaponSpawner newWeaponSpawner)
    {
        weaponSpawner = newWeaponSpawner;

        if (weapon != null)
        {
            weaponName.text = weapon.weaponName;
            weaponImage.sprite = weapon.weaponIcon;
        }
    }
}
