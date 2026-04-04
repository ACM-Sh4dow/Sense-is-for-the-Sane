using UnityEngine;

public class AudioDistanceCheck : MonoBehaviour
{
    private float distanceFromTarget;
    public float angleFromTargetRotation;
    private Transform playerCamPos;

    public float audibleDistanceRange = 3;
    [Range(0f, 180f)]
    public float audibleAngleRange = 60;
    public AK.Wwise.Event audioEvent;
    
    void Start()
    {
        playerCamPos = PlayerBehaviour.Instance.CameraHolder.transform;
        
        if (audioEvent != null)
        {
            audioEvent.Post(gameObject);
        }
    }

    void Update()
    {
        distanceFromTarget = (transform.position - playerCamPos.position).magnitude;
        angleFromTargetRotation = Vector3.Angle(transform.forward, playerCamPos.forward);

        if (distanceFromTarget/ audibleDistanceRange < 2)
        {
            float indicatorVolume = distanceFromTarget/ audibleDistanceRange;

            if (angleFromTargetRotation > audibleAngleRange)
            {
                indicatorVolume += (angleFromTargetRotation - audibleAngleRange) / (180 - audibleAngleRange);
            }
            
            AkUnitySoundEngine.SetRTPCValue("DistanceFromTarget", indicatorVolume, gameObject);
        }
    }
}
