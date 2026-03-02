using System;
using UnityEngine;

public class PerspectivePuzzleSolve : MonoBehaviour
{
    #region Variables

    [Header("Puzzle Settings")] 
    [SerializeField] private float distanceTolerance;
    [SerializeField] private float angleTolerance;
    
    [Header("Puzzle Pieces")]
    [SerializeField] private GameObject puzzleSolution;
    [SerializeField] private GameObject[] puzzleComponents;
    [SerializeField] private GameObject puzzleResult;
    
    private bool isPuzzleSolved;

    #endregion
    
    #region Solving
    public void SolvePuzzle()
    {
        if (isPuzzleSolved) return;

        if ((Vector3.Distance(ProtagonistController.playerPosition, puzzleSolution.transform.position) <=
             distanceTolerance)
            && Quaternion.Dot(ProtagonistController.playerRotation.normalized, puzzleSolution.transform.rotation.normalized) <=
            angleTolerance)
        {
            puzzleResult.SetActive(true);

            foreach (GameObject puzzleComponent in puzzleComponents)
            {
                puzzleComponent.SetActive(false);
            }
            
            isPuzzleSolved = true;
        }
    }
    #endregion
    #region Setting Puzzle

    private void Update()
    {
        ProtagonistController.Instance.GivePuzzle(this);
    }

    #endregion
}
