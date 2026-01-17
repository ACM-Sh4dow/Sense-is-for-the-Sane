using UnityEngine;

public class ProtagonistController : MonoBehaviour
{
    #region Variables

    private static Vector2 movementInput;

    [SerializeField] float movementSpeed;
    [SerializeField] float gravity;
    private const float MaxSlopeAngle = 55f;
    private const int MaxDepth = 3;

    private float capsulePointFromCenter;
    private float capsuleRadius;
    
    [SerializeField] private float aimSensitivity;
    private float targetYaw;
    private float targetPitch;
    private static Vector2 lookInput;

    private const float BottomClamp = -30;
    private const float TopClamp = 35;
    private const int RotationOffset = -30;
    
    private Vector3 velocity;
    
    #endregion
    #region Initialization

    void Start()
    {
        var playerCollider = GetComponent<CapsuleCollider>();
        capsuleRadius = playerCollider.radius;
        float capsuleHeight =  playerCollider.height;
        capsulePointFromCenter = (capsuleHeight / 2) - capsuleRadius;
        
        Cursor.lockState = CursorLockMode.Locked;
        targetYaw = transform.rotation.eulerAngles.y;
    }
    
    #endregion
    #region Synchronise Input
    
    public static void SyncMovementInput(Vector2 input)
    {
        movementInput = input;
    }

    public static void SyncLookInput(Vector2 input)
    {
        lookInput = input;
    }

    #endregion
    #region Collide and Slide
    
    private Vector3 CollideAndSlide(Vector3 velocity, Vector3 position, Vector3 targetDirection, int depth)
    {
        #region Base Case

        if (depth >= MaxDepth)
        {
            return Vector3.zero;
        }

        #endregion
        #region Logic
        
        float distance = velocity.magnitude;
        
        if (distance < 0.0001f)
        {
            return Vector3.zero;
        }
        
        RaycastHit hit;
        
        Vector3 point1 = new Vector3(position.x, position.y + capsulePointFromCenter, position.z);
        Vector3 point2 = new Vector3(position.x, position.y - capsulePointFromCenter, position.z);
        
        if (Physics.CapsuleCast(
                point1, 
                point2, 
                capsuleRadius * 0.95f, // Slightly smaller to avoid self-collision
                velocity.normalized,
                out hit,
                distance))
        {
            Vector3 snapToSurface = velocity.normalized * (hit.distance - 0.001f); // Small offset
            Vector3 remainder = velocity - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            float scale;
            
            if (angle <= MaxSlopeAngle)
            {
                scale = 1;
            }
            else
            {
                scale = 1 - Vector3.Dot(
                    new Vector3(hit.normal.x, 0, hit.normal.z).normalized,
                    -new Vector3(targetDirection.x, 0, targetDirection.z).normalized);
            }
            
            remainder = Vector3.ProjectOnPlane(remainder, hit.normal) * scale;
            
            return snapToSurface + CollideAndSlide(
                remainder,
                position + snapToSurface,
                targetDirection,
                depth + 1);
        }
        
        #endregion
        #region Default Case
        
        return velocity;
        
        #endregion
    }
    
    #endregion
    #region Look
    
    private void Look()
    {
        targetYaw += lookInput.x * aimSensitivity * Time.deltaTime;
        targetPitch += lookInput.y * aimSensitivity * Time.deltaTime;

        targetYaw = ClampAngle(targetYaw, float.MinValue, float.MaxValue);
        targetPitch = ClampAngle(targetPitch, BottomClamp, TopClamp);

        transform.rotation = Quaternion.Euler(
            0f,
            targetYaw + RotationOffset,
            0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    
    #endregion

    private void Update()
    {
        #region Movement
        
        Vector3 newMovementInput = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * newMovementInput;
        
        velocity = moveDirection * movementSpeed;
        velocity = CollideAndSlide(velocity * Time.deltaTime, transform.position, moveDirection, 0);
        
        transform.position += velocity;
        
        #endregion
        #region Look

        Look();

        #endregion
    }
}