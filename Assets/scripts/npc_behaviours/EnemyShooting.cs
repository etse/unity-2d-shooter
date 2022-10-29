using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : INPCBehaviour
{
    public void Update(BasicEnemy enemy)
    {
        enemy.shoot();
    }
}
