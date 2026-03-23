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

    private static bool puzzleSolveAttempted = false;
    #endregion
    
    #region Solving

    public static void AttemptPuzzle()
    {
        puzzleSolveAttempted = true;
    }
    public void SolvePuzzle()
    {
        if (state == Puzzle.State.solved) return;

        if (state == Puzzle.State.solvable)
        {
            state = Puzzle.State.solved;
        }
    }

    private void CheckPuzzleSolution()
    {
        if (state == Puzzle.State.solved) return;

        if ((Vector3.Distance(PlayerBehaviour.Instance.playerPosition, puzzleSolution.transform.position) <=
             distanceTolerance)
            && Quaternion.Dot(PlayerBehaviour.Instance.playerRotation.normalized, puzzleSolution.transform.rotation.normalized) <=
            angleTolerance)
        {
            state = Puzzle.State.solvable;
        }
        else
        {
            state = Puzzle.State.notSolvable;
        }
    }
    #endregion

    private void Update()
    {
        #region Completion
        if (state == Puzzle.State.fullyResolved) return;
        if (state == Puzzle.State.solved)
        {
            puzzleResult.SetActive(true);

            foreach (GameObject puzzleComponent in puzzleComponents)
            {
                puzzleComponent.SetActive(false);
            }
            
           state =  Puzzle.State.fullyResolved; 
            
           // REGISTER COMPLETION
           return;
        }
        #endregion
        
        CheckPuzzleSolution();

        if (puzzleSolveAttempted == true)
        {
            SolvePuzzle();
            puzzleSolveAttempted = false;
        }
    }
}
