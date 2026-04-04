using System;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceBasedAnimation : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private Vector3 playerPosition;

    [SerializeField]
    private Transform distanceCheckObject;
    
    [SerializeField] private string animationName;
    [SerializeField] private float animationStartDistance;
    [SerializeField] private float animationEndDistance;

    private float animationProgressLastFrame;
    private float animationDelta = 0;
    public bool logAnimDelta;

    public AK.Wwise.Event audioEvent;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (audioEvent != null) audioEvent.Post(gameObject);

        if (animator == null)
        {
            throw new NullReferenceException();
        }
        
        animator.speed = 0;
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            throw new NullReferenceException();
        }

        if(distanceCheckObject == null)
        {
            distanceCheckObject = transform;
            Debug.Log("Parent set:" + transform.position);
        }
    }

    private void Update()
    {
        playerPosition = player.transform.position;

        float distanceFromPlayer =  Vector3.Distance(playerPosition, distanceCheckObject.position);
        float animationProgress;

        if (distanceFromPlayer <= animationEndDistance)
        {
            animationProgress = 0f;
            animationDelta = 0f;
        }
        else if (distanceFromPlayer >= animationStartDistance)
        {
            animationProgress = 1f;
            animationDelta = 0f;
        }
        else
        {
            var rangeDifference = Math.Max(animationStartDistance, animationEndDistance) 
                                - Math.Min(animationStartDistance, animationEndDistance);
            
            var playerDifference = distanceFromPlayer - animationEndDistance;
            
            animationProgress = playerDifference / rangeDifference;

            animationDelta = (animationProgressLastFrame - animationProgress) / Time.deltaTime;
            if (logAnimDelta) Debug.Log("Anim delta for " + name + "  -  " + animationDelta);
            
        }
        
        animator.Play(animationName, 0, animationProgress);
        AkUnitySoundEngine.SetRTPCValue("DistanceAnimationRate", animationDelta, gameObject);

        animationProgressLastFrame = animationProgress;
    }
}
