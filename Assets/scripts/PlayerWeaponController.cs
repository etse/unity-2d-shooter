using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponController: MonoBehaviour
{    
    private PlayerControls controls;
    private ShipUpgrades shipUpgrades;

    private void Awake()
    {
        controls = new PlayerControls();
        shipUpgrades = ShipUpgrades.getInstance();

        if (shipUpgrades.currentWeapon == null)
        {
            shipUpgrades.currentWeapon = PulseCannon.name;
        }

        if (!controls.Player.enabled)
        {
            controls.Player.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Shoot.IsPressed())
        {
            getActiveWeapon().shoot();
        }
    }

    private IWeapon getActiveWeapon()
    {
        var weapon = shipUpgrades.currentWeapon;
        if (weapon == PulseCannon.name)
        {
            return GetComponent<PulseCannon>();
        }
        else
        {
            return GetComponent<PulseCannon>();
        }
    }
}
