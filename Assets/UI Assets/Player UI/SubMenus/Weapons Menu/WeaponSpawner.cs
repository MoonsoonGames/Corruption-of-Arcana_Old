using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    LoadSettings loadSettings;
    public Object prefab;
    public WeaponInfo weaponInfo;

    private void Start()
    {
        loadSettings = LoadSettings.instance;
        WeaponsMenuOpened();
    }

    public void WeaponsMenuOpened()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (prefab != null)
        {
            foreach (var item in loadSettings.earnedWeapons)
            {
                Debug.Log(item.weaponName);
                GameObject weaponObject = Instantiate(prefab, this.gameObject.transform) as GameObject;

                WeaponIcon weaponIcon = weaponObject.GetComponent<WeaponIcon>();

                if (weaponIcon != null)
                {
                    weaponIcon.weapon = item;

                    weaponIcon.Setup(this);
                }

                Debug.Log(weaponObject.name);
            }
        }
    }

    public void IconClicked(Weapon weapon)
    {
        weaponInfo.ShowWeaponInfo(weapon);
    }
}