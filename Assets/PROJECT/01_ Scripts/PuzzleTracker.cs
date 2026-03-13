using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTracker : MonoBehaviour
{
    public bool[] puzzles;

    public void RegisterCompletion(byte id)
    {
        puzzles[id] = true;
    }

    public bool VerifyCompletion(byte id)
    {
        return puzzles[id];
    }
}
