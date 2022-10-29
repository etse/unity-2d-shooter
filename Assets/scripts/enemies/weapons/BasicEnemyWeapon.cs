using System;
using System.Collections.Generic;
using UnityEngine;


class BasicEnemyWeapon: MonoBehaviour, IEnemyWeapon
{
    private float cooldown = 0.5f;
    private float cooldownUntil;
    public BasicBullet bullet;

    private void Start()
    {
        cooldownUntil = 0;
    }

    public void shoot(Transform enemyTransform)
    {
        if (Time.time > cooldownUntil)
        {
            cooldownUntil = Time.time + cooldown;
            var bulletInstance = Instantiate(bullet, enemyTransform.position, Quaternion.Euler(Vector3.forward * 180));
            bulletInstance.speed = -8f;
            bulletInstance.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            bulletInstance.friendly = false;
        }
    }
}
