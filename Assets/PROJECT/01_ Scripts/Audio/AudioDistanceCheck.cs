using UnityEngine;

public class AudioDistanceCheck : MonoBehaviour
{
    private float distanceFromTarget;
    private float angleFromTargetRotation;
    private Transform playerPos;

    public float indicatorAudibleDistance = 1;
    
    void Start()
    {
        playerPos = PlayerBehaviour.Instance.transform;
    }

    void Update()
    {
        distanceFromTarget = (transform.position - playerPos.position).magnitude;
        angleFromTargetRotation = Vector3.Angle(transform.forward, playerPos.forward);

        if (distanceFromTarget/indicatorAudibleDistance < 2)
        {
            float indicatorVolume = distanceFromTarget/indicatorAudibleDistance;

            if (angleFromTargetRotation > 90)
            {
                indicatorVolume += (angleFromTargetRotation - 90) / 90;
            }
            
            AkUnitySoundEngine.SetRTPCValue("PuzzleDistance", indicatorVolume);
        }
    }
}
