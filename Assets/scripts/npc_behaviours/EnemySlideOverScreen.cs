using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlideOverScreen : INPCBehaviour
{
    private Vector3 speed = new Vector3(0, -1, 0);

    public EnemySlideOverScreen()
    {

    }

    public EnemySlideOverScreen(Vector3 direction)
    {
        this.speed = direction;
    }


    public void Update(BasicEnemy enemy)
    {
        enemy.transform.position += this.speed * Time.deltaTime;
    }
}
