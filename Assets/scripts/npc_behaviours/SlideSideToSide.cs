using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSideToSide : INPCBehaviour
{
    private float waveWidth;
    private float period;
    private float startTime;
    private float midPoint;
    private bool hasSetInitialValues = false;

    public SlideSideToSide(float waveWidth, float period)
    {
        this.waveWidth = waveWidth;
        this.period = period;
    }

    public void Update(BasicEnemy enemy)
    {
        if (hasSetInitialValues == false)
        {
            midPoint = enemy.gameObject.transform.position.x;
            startTime = Time.realtimeSinceStartup;
            hasSetInitialValues = true;
        }

        var deltaTime = Time.realtimeSinceStartup - startTime;
        var sinX = Mathf.Sin(((deltaTime / period) * Mathf.PI * 2));
        var deltaX = waveWidth * sinX;
        enemy.gameObject.transform.position = new Vector3(midPoint + deltaX, enemy.gameObject.transform.position.y, 0);
    }
}
