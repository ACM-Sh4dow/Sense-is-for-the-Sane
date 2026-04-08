using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FuneralManager : PuzzleTracker
{
    public bool flowerPuzzleCompleted;
    public bool paintingPuzzleCompleted;
    private bool pedestalPhase1Completed;
    private bool pedestalPhase2Completed;
    private bool casketPuzzleCompleted;
    
    public PerspectivePuzzleSolve casketPerspectivePuzzle;
    public Transition alternateTransition;
    
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
        PedestalSolved(); 
        CasketSolved();
        MusicLayers();
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
        if (Puzzles[1].state == Puzzle.State.solved) // animation (secondary puzzle complete set in manual animation) (1/2)
        {
            Debug.Log("flower part 1 done");
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
            casketPerspectivePuzzle.secondaryPuzzleComplete = true;
        }
    }

    private void CasketSolved()
    {
        if (Puzzles[5].state == Puzzle.State.fullyResolved) 
        {
            casketPuzzleCompleted = true;
            alternateTransition.Teleport();
        }
    }

    private void MusicLayers()
    {
        if (pedestalPhase1Completed || pedestalPhase2Completed)
        {
            AkUnitySoundEngine.SetState("PuzzlesCompleted", "One");
        }

        if (pedestalPhase1Completed && pedestalPhase2Completed)
        {
            AkUnitySoundEngine.SetState("PuzzlesCompleted", "Two");
        }

        if (casketPuzzleCompleted)
        {
            AkUnitySoundEngine.SetState("PuzzlesCompleted", "CasketComplete");
        }
    }
}
