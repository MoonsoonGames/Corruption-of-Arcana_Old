using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewWeapon", menuName = "Combat/Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public string weaponName;
    [TextArea(3, 10)]
    public string weaponDescription;

    public Sprite weaponIcon;

    public List<CardParent> basicDeck;

    public void EquipWeapon(LoadSettings loadSettings)
    {
        loadSettings.equippedWeapon = this;
    }
}
