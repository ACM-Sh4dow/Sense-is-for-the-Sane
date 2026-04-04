using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FuneralManager : PuzzleTracker
{
    private bool flowerPuzzleCompleted;
    private bool paintingPuzzleCompleted;
    private bool pedestalPhase1Completed;
    private bool pedestalPhase2Completed;
    private bool casketPuzzleCompleted;

    [SerializeField] private GameObject flowerPerspectivePuzzle;
    [SerializeField] private GameObject casketPerspectivePuzzle;
    
    
    private void Start()
    {
        Overseer.Instance.AddManager(this);
        
    }

    public void OnPuzzleComplete()
    {
        #region Check Completion Progress - Unused
        /*int completedPuzzle = CheckCompletionProgress(Puzzles.ToArray()).Completed;

        switch (completedPuzzle)
        {
            case 0:
                //on x puzzles completed, play y layers of music
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;
                */
        #endregion

        PaintingSolved();
        FlowerSolved();
        //PedestalSolved();
        //CasketSolved();
    }

    private void PaintingSolved()
    {
        if (Puzzles[0].state == Puzzle.State.fullyResolved) 
        {
            paintingPuzzleCompleted = true;
            Debug.Log("painting done");
        }
    }

    private void FlowerSolved()
    {
        if (Puzzles[1].state == Puzzle.State.solved) // animation (1/2)
        {
            flowerPerspectivePuzzle.SetActive(true);
            Debug.Log("flower part 1 done");
        }
        else
        {
            flowerPerspectivePuzzle.SetActive(false);
        }

        if (Puzzles[2].state == Puzzle.State.fullyResolved) //resolve animation
        {
            Puzzles[1].state = Puzzle.State.fullyResolved;
        }
        
        if (Puzzles[1].state == Puzzle.State.fullyResolved && Puzzles[2].state == Puzzle.State.fullyResolved) //perspective (2/2)
        {
            flowerPuzzleCompleted = true;
            Debug.Log("flower part 2 done");
        }
    }

    private void PedestalSolved()
    {
        if (Puzzles[3].state == Puzzle.State.fullyResolved || Puzzles[4].state == Puzzle.State.fullyResolved) // (1/2)
        {
            pedestalPhase1Completed = true;
        }
        if (Puzzles[3].state == Puzzle.State.fullyResolved && Puzzles[4].state == Puzzle.State.fullyResolved) // (2/2)
        {
            pedestalPhase2Completed = true;
            casketPerspectivePuzzle.SetActive(true);
        }
    }

    private void CasketSolved()
    {
        if (Puzzles[5].state == Puzzle.State.fullyResolved) 
        {
            casketPuzzleCompleted = true;
        }
    }
}
