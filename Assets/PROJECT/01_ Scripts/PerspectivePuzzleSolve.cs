using System;
using UnityEngine;

public class PerspectivePuzzleSolve : Puzzle
{
    #region Variables

    [Header("Puzzle Settings")] 
    [SerializeField] private float distanceTolerance;
    [SerializeField] private float angleTolerance;
    
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

        if (Vector3.Distance(PlayerBehaviour.Instance.playerPosition, puzzleSolution.transform.position) <=
             distanceTolerance
            && Quaternion.Dot(PlayerBehaviour.Instance.playerRotation.normalized, puzzleSolution.transform.rotation.normalized) <=
            angleTolerance)
        {
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
            return;
        }
        #endregion
        
        CheckPuzzleSolution();

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
