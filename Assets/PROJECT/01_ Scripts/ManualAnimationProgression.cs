using System;
using Unity.Collections;
using UnityEngine;

public class ManualAnimationProgression : Puzzle, InteractionPoint
{
    private Animator animator;
    public float progress = 0f;

    [SerializeField] [Range(0,1)] private float solutionProgressMin;
    [SerializeField] [Range(0,1)] private float solutionProgressMax;

    public string animationName;
    [Range(0,1)] public float progressionRate;

    public static bool canProgress = false;

    public PerspectivePuzzleSolve PerspectivePuzzleSolve;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            throw new NullReferenceException();
        }

        animator.speed = 0;
    }

    // public static void SyncManualAnimationInput()
    // {
    //     canProgress = !canProgress;
    // }

    public void Interact()
    {
        canProgress = !canProgress;
    }

    private void Update()
    {
        if (!canProgress) return;
        if (state == State.fullyResolved) return;

        if (progress >= 1) progress = 0;
        progress += progressionRate;
        animator.Play(animationName, 0, progress);
        CheckSolution();
    }

    private void CheckSolution()
    {
        if (progress <= solutionProgressMin || progress >= solutionProgressMax) //currently starts at correct position
        {
            if (state == State.solved) return;
            state = State.solved;
            PerspectivePuzzleSolve.secondaryPuzzleComplete = true;
            RegisterCompletion();
        }
        else
        {
            state = State.solvable;
        }
    }
}
