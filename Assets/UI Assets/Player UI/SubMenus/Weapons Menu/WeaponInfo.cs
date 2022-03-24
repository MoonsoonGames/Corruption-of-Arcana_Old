using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    LoadSettings loadSettings;

    public Weapon currentWeapon;
    //public WeaponCards cards references to card icons

    public Text weaponName;
    public Image weaponImage;

    public void ShowWeaponInfo(Weapon weapon)
    {
        currentWeapon = weapon;

        if (currentWeapon != null)
        {
            weaponName.text = weapon.weaponName;
            weaponImage.sprite = weapon.weaponIcon;
            //change card info
        }
        else
        {
            weaponName.text = "Click on a weapon to view it's stats";
            weaponImage.sprite = null;
            //change card info
        }
    }

    public void EquipWeapon()
    {
        if (loadSettings == null)
        {
            loadSettings = GameObject.FindObjectOfType<LoadSettings>();
        }

        if (loadSettings != null)
        {
            loadSettings.equippedWeapon = currentWeapon;
        }
    }
}
