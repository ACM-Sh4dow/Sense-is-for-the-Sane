using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PerspectivePuzzleSolve : Puzzle
{
    #region Variables

    [Header("Puzzle Settings")] 
    [SerializeField] private float distanceTolerance;
    [Tooltip("1 being facing object, 0 facing away, value should be around 0.95. (DOESNT WORK VERTICALLY!)")]
    [SerializeField] [Range(0.8f,1)] private float angleTolerance = 0.975f;
    [Tooltip("Is the puzzle aligned vertically (above or below player)? If so, disables angle check, because angle doesn't work vertically.")]
    [SerializeField] private bool verticalAlignment;
    
    [SerializeField] private float dist;
    [SerializeField] private float angle;
    
    [Header("Puzzle Pieces")]
    [SerializeField] private GameObject puzzleSolution;
    [SerializeField] private GameObject[] puzzleComponents;
    [SerializeField] private GameObject puzzleResult;

    private bool puzzleSolveAttempted = false;
    public bool secondaryPuzzleComplete;

    public AK.Wwise.Event solvedAudioEvent;
    
    #endregion
    

    private void CheckPuzzleSolution()
    {
        if (state == State.solved) return;

        dist = Vector3.Distance(PlayerBehaviour.Instance.playerPosition, puzzleSolution.transform.position);
        angle = Quaternion.Dot(PlayerBehaviour.Instance.playerRotation.normalized,
            puzzleSolution.transform.rotation.normalized);
        
        if ( dist <= distanceTolerance)
        {
            if (!verticalAlignment && angle <= angleTolerance)
            {
                state = State.notSolvable;
                return;
            }
            state = State.solvable;
        }
        else
        {
            state = State.notSolvable;
        }
    }
    public override void AttemptPuzzle()
    {
        puzzleSolveAttempted = true;
    }

    private void Update()
    {
        #region Fully Resolving
        if (state == State.fullyResolved) return;
        if (state == State.solved)
        {
            puzzleResult.SetActive(true);
            if (solvedAudioEvent != null) solvedAudioEvent.Post(puzzleResult);

            foreach (GameObject puzzleComponent in puzzleComponents)
            {
                puzzleComponent.SetActive(false);
            }
            
            state =  State.fullyResolved;
            RegisterCompletion();
            PlayerBehaviour.Instance.activateCursor = false;
            return;
        }
        #endregion
        
        CheckPuzzleSolution();

        if (state == State.solvable && secondaryPuzzleComplete)
        {
            PlayerBehaviour.Instance.activateCursor = true;
        }
        else
        {
            PlayerBehaviour.Instance.activateCursor = false;
        }
        
        if (puzzleSolveAttempted)
        {
            SolvePuzzle();
            puzzleSolveAttempted = false;
        }
    }
    private void SolvePuzzle()
    {
        if (state == State.solved) return;
        if (!secondaryPuzzleComplete) return;

        if (state == State.solvable)
        {
            state = State.solved;
            Debug.Log("PerspectivePuzzleSolve: State set to SOLVED");
        }
        else
        {
            Debug.Log("PerspectivePuzzleSolve: State is NOT SOLVABLE");
        }
    }
}
