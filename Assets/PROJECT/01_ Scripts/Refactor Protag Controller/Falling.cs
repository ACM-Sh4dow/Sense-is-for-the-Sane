using UnityEngine;

public class Falling : Behaviour
{
    #region Variables

    private float gravityCurrent;
    private const float gravityIncrease = 0.35f;
    private const float gravityMax = 40f;

    public static float StartTime = float.MaxValue;
    public static float CoyoteTime = 0.25f;
    
    #endregion
    
    public void Begin()
    {
        gravityCurrent = 0;
        
        StartTime = Time.time;
    }

    public void Run()
    {
        if (GroundCheck.Execute(JackBehaviour.Instance.playerCollider, JackBehaviour.Instance.collisionLayers) != null)
        {
            JackBehaviour.Instance.End<Falling>();
            return;
        }
        
        gravityCurrent += gravityIncrease;

        gravityCurrent = Mathf.Min(gravityCurrent, gravityMax);

        var velocity = CollideAndSlide.Execute(
            JackBehaviour.Instance.playerCollider,
            JackBehaviour.Instance.collisionLayers,
            Vector3.down * (gravityCurrent * Time.deltaTime),
            JackBehaviour.Instance.transform.position,
            Vector3.down,
            0);
        JackBehaviour.Instance.transform.position += velocity;
    }

    public void End()
    {
        gravityCurrent = 0;
        StartTime = float.MaxValue;
    }
}
