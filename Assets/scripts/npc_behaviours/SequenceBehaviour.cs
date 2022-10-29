using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBehaviour : INPCBehaviour
{
    private List<TimedBehaviour> timedBehaviours = new List<TimedBehaviour>();
    private int currentBehaviour = 0;
    private float segmentStartTime = 0f;

    public SequenceBehaviour()
    {
        segmentStartTime = Time.realtimeSinceStartup;
    }

    public SequenceBehaviour addBehaviour(INPCBehaviour behaviour, float duration)
    {
        timedBehaviours.Add(new TimedBehaviour(behaviour, duration));
        return this;
    }

    public void Update(BasicEnemy enemy)
    {
        if (timedBehaviours.Count <= 0)
        {
            return;
        }

        var behaviour = timedBehaviours[currentBehaviour];
        behaviour.behaviour.Update(enemy);

        if ((Time.realtimeSinceStartup > segmentStartTime + behaviour.duration) && (currentBehaviour < timedBehaviours.Count - 1))
        {
            currentBehaviour += 1;
            segmentStartTime = Time.realtimeSinceStartup;
        }
    }

    private class TimedBehaviour
    {
        public INPCBehaviour behaviour;
        public float duration;

        public TimedBehaviour(INPCBehaviour behaviour, float duration)
        {
            this.duration = duration;
            this.behaviour = behaviour;
        }
    }

}
