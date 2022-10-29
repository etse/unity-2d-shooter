using System;
using UnityEngine;

public abstract class IWeapon: MonoBehaviour
{
    public static string name { get; set; }

    public abstract void shoot();
}
