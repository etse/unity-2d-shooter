using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAccelerating : INPCBehaviour
{
    private Vector3 speed = new Vector3(0, 0, 0);
    private Vector3 acceleration = new Vector3(0, -1, 0);

    public EnemyAccelerating()
    {

    }

    public EnemyAccelerating withSpeed(Vector3 speed)
    {
        this.speed = speed;
        return this;
    }

    public EnemyAccelerating withAcceleration(Vector3 acceleration)
    {
        this.acceleration = acceleration;
        return this;
    }

    public void Update(BasicEnemy enemy)
    {
        speed += acceleration * Time.deltaTime;
        enemy.transform.position += speed * Time.deltaTime;
    }
}
