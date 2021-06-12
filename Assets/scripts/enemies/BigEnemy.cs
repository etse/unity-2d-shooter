using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : BasicEnemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        health = 50;
        score = 150;
    }
}
