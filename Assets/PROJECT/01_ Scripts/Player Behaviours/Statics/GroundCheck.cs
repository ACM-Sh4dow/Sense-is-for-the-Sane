using Unity.VisualScripting;
using UnityEngine;

public static class GroundCheck
{
    public static RaycastHit? Execute(CapsuleCollider collider, LayerMask groundLayer)
    {
        #region Variables
        Vector3 capsuleCenter = collider.transform.TransformPoint(collider.center);
        float worldRadius = collider.radius * Mathf.Max(
            collider.transform.lossyScale.x, 
            collider.transform.lossyScale.z);
        float castLift = worldRadius; 
        float skinWidth = 0.2f;
        
        Vector3 origin = capsuleCenter - Vector3.up * ((collider.height / 2f - collider.radius) * collider.transform.lossyScale.y - castLift);
        float castDistance = castLift + skinWidth;
        #endregion
        
        #region Hit
        if (Physics.SphereCast(
                origin,
                worldRadius,
                Vector3.down,
                out RaycastHit hit,
                castDistance,
                groundLayer,
                QueryTriggerInteraction.Ignore))
        {
            return hit;
        }
        #endregion
        #region Miss
        return null;
        #endregion
    }
}

