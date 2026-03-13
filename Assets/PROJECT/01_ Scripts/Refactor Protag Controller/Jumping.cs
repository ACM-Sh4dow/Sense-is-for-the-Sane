using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : Behaviour
{
    #region Variables
    
    private const float duration = 0.5f;
    private const float airHoldTime = 0.1f;
    private const float force = 1f;
    
    private float startTime;
    private float jumpProgress;
    
    #endregion
    
    
    public void Begin()
    {
        startTime = Time.time;
    }

    public void Run()
    {
        if (Time.time >= startTime + duration)
        {
            if (Time.time >= startTime + duration + airHoldTime)
            {
                JackBehaviour.Instance.End<Jumping>();
            }
            return;
        }
        
        jumpProgress = (Time.time - startTime) / duration;
        var velocity = CollideAndSlide.Execute(
            JackBehaviour.Instance.playerCollider, 
            JackBehaviour.Instance.collisionLayers, 
            Vector3.up * (force * Time.deltaTime), 
            JackBehaviour.Instance.transform.position, 
            Vector3.up, 
            0);
        JackBehaviour.Instance.transform.position += Vector3.Lerp(velocity, Vector3.zero, jumpProgress);
    }

    public void End()
    {
        JackBehaviour.Instance.Begin<Falling>();
    }
}
