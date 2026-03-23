using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public abstract class PuzzleTracker : MonoBehaviour
{
    public List<Puzzle> Puzzles = new List<Puzzle>();


    public (int Completed, int Total) CheckCompletionProgress(Puzzle[] puzzleArray)
    {
        var total = puzzleArray.Length;
        var completed = 0;

        foreach (var puzzle in puzzleArray)
        { 
            if (puzzle.state == Puzzle.State.fullyResolved)
            {
                completed++;
            }
        }

        return (completed, total);
    }
}
