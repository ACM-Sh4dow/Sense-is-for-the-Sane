using UnityEngine;

public class Hopping : Behaviour
{
    private static Vector2 input;
    private Vector3 velocity;
    
    private const float movementSpeed = 5f;
    
    public static void SyncInput(Vector2 Input)
    {
        input = Input;
    }
    
    public void Begin()
    {
        
    }

    public void Run()
    {
        Vector3 newMovementInput = new Vector3(input.x, 0, input.y);
        Vector3 moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * newMovementInput;

        velocity = moveDirection * movementSpeed;
        velocity = CollideAndSlide.Execute(
            JackBehaviour.Instance.playerCollider,
            JackBehaviour.Instance.collisionLayers,
            velocity * Time.deltaTime, 
            JackBehaviour.Instance.transform.position, 
            moveDirection, 
            0);

        JackBehaviour.Instance.transform.position += velocity;
    }

    public void End()
    {
        
    }
}
