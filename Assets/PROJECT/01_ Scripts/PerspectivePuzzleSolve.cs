using System;
using UnityEngine;

public class PerspectivePuzzleSolve : MonoBehaviour, Puzzle
{
    #region Variables

    [Header("Puzzle Settings")] 
    [SerializeField] private float distanceTolerance;
    [SerializeField] private float angleTolerance;
    
    [Header("Puzzle Pieces")]
    [SerializeField] private GameObject puzzleSolution;
    [SerializeField] private GameObject[] puzzleComponents;
    [SerializeField] private GameObject puzzleResult;

    private enum PuzzleState : byte
    {
        notSolvable,
        solvable, 
        solved,
        fullyResolved
    }

    private static PuzzleState state;

    #endregion
    
    #region Solving
    public static void SolvePuzzle()
    {
        if (state == PuzzleState.solved) return;

        if (state == PuzzleState.solvable)
        {
            state = PuzzleState.solved;
        }
    }
    private void CheckPuzzleSolution()
    {
        if (state == PuzzleState.solved) return;

        if ((Vector3.Distance(PlayerBehaviour.Instance.playerPosition, puzzleSolution.transform.position) <=
             distanceTolerance)
            && Quaternion.Dot(PlayerBehaviour.Instance.playerRotation.normalized, puzzleSolution.transform.rotation.normalized) <=
            angleTolerance)
        {
            state = PuzzleState.solvable;
        }
        else
        {
            state = PuzzleState.notSolvable;
        }
    }
    #endregion

    private void Update()
    {
        #region Completion
        if (state == PuzzleState.fullyResolved) return;
        if (state == PuzzleState.solved)
        {
            puzzleResult.SetActive(true);

            foreach (GameObject puzzleComponent in puzzleComponents)
            {
                puzzleComponent.SetActive(false);
            }
            
           state =  PuzzleState.fullyResolved; 
            
           // REGISTER COMPLETION
           return;
        }
        #endregion
        
        CheckPuzzleSolution();
    }
}
