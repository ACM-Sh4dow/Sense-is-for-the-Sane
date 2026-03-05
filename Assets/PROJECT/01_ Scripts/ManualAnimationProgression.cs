using System;
using UnityEngine;

public class ManualAnimationProgression : MonoBehaviour
{
    private Animator animator;
    private float progress = 0f;

    public string animationName;
    [Range(0,1)] public float progressionRate;

    public static bool canProgress = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            throw new NullReferenceException();
        }

        animator.speed = 0;
    }

    public static void SyncManualAnimationInput()
    {
        canProgress = !canProgress;
    }

    private void Update()
    {
        if (!canProgress) return;

        progress += progressionRate;
        animator.Play(animationName, 0, progress);
    }
}
