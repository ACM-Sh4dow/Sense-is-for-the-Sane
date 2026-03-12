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
        solved
    }

    private PuzzleState state;

    #endregion
    
    #region Solving
    public void SolvePuzzle()
    {
        if (state == PuzzleState.solved) return;

        if (state == PuzzleState.solvable)
        {
            puzzleResult.SetActive(true);

            foreach (GameObject puzzleComponent in puzzleComponents)
            {
                puzzleComponent.SetActive(false);
            }

            state = PuzzleState.solved;
        }
    }
    public void CheckPuzzleSolution()
    {
        if (state == PuzzleState.solved) return;

        if ((Vector3.Distance(ProtagonistController.playerPosition, puzzleSolution.transform.position) <=
             distanceTolerance)
            && Quaternion.Dot(ProtagonistController.playerRotation.normalized, puzzleSolution.transform.rotation.normalized) <=
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
    #region Setting Puzzle

    private void Update()
    {
        ProtagonistController.Instance.GivePuzzle(this);
        CheckPuzzleSolution();
    }

    #endregion
}
