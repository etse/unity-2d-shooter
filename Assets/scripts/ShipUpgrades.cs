using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpgrades
{
    private static ShipUpgrades Instance;
    public string currentWeapon;
    private int weaponUpgradeLevel = 1;

    private ShipUpgrades()
    {

    }

    public int getWeaponUpgradeLevel()
    {
        return weaponUpgradeLevel;
    }

    public void increaseWeaponLevel()
    {
        setWeaponUpgradeLevel(weaponUpgradeLevel + 1);
    }

    public void decreaseWeaponLevel()
    {
        setWeaponUpgradeLevel(weaponUpgradeLevel - 1);
    }

    public void setWeaponUpgradeLevel(int level)
    {
        weaponUpgradeLevel = level;
        if (level < 1)
        {
            weaponUpgradeLevel = 1;
        }
        if (level > 6)
        {
            weaponUpgradeLevel = 6;
        }
    }

    public static ShipUpgrades getInstance()
    {
        if (Instance == null)
        {
            Instance = new ShipUpgrades();
        }
        return Instance;
    }
}
