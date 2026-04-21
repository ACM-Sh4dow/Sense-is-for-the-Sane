using UnityEngine;

public class Walking : PhysicsBehaviour
{
    #region Variables
    private static Vector2 input;
    private Vector3 velocity;
    
    public static float movementSpeed = 2.5f;
    private float LastFootstepTime = float.MaxValue;
    public static float SecondsBetweenFootsteps = 0.83f;
    #endregion
    
    public static void SyncInput(Vector2 Input)
    {
        input = Input;
    }
    
    public void Begin()
    {
        AkUnitySoundEngine.PostEvent("Footstep_Scratch", PlayerBehaviour.Instance.gameObject);
        LastFootstepTime = Time.time;
    }

    public void Run()
    {
        #region Footsteps

        if (LastFootstepTime + SecondsBetweenFootsteps < Time.time)
        {
            AkUnitySoundEngine.PostEvent("Footstep", PlayerBehaviour.Instance.gameObject);
            LastFootstepTime = Time.time;
        }
        #endregion
        #region FallCheck
        if (GroundCheck.Execute(PlayerBehaviour.Instance.playerCollider, PlayerBehaviour.Instance.collisionLayers) == null
            && !PlayerBehaviour.Instance.InState<Falling>())
        {
            PlayerBehaviour.Instance.Begin<Falling>();
        }
        #endregion
        
        #region Logic
        Vector3 newMovementInput = new Vector3(input.x, 0, input.y);
        Vector3 moveDirection = Quaternion.Euler(0, PlayerBehaviour.Instance.transform.eulerAngles.y, 0) * newMovementInput;

        velocity = moveDirection * movementSpeed;
        velocity = CollideAndSlide.Execute(
            PlayerBehaviour.Instance.playerCollider,
            PlayerBehaviour.Instance.collisionLayers,
            velocity * Time.deltaTime, 
            PlayerBehaviour.Instance.transform.position, 
            moveDirection, 
            0);

        PlayerBehaviour.Instance.transform.position += velocity;
        #endregion
    }

    public void End()
    {
        LastFootstepTime = float.MaxValue;
    }
}
