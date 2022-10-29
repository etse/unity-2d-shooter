using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedBehaviour : INPCBehaviour
{
    private List<INPCBehaviour> behaviours = new List<INPCBehaviour>();

    public CombinedBehaviour addBehaviour(INPCBehaviour behaviour)
    {
        behaviours.Add(behaviour);
        return this;
    }

    public void Update(BasicEnemy enemy)
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.Update(enemy);
        }
    }
}
