using UnityEngine;

public class PuzzleTracker : MonoBehaviour
{
    private bool[] puzzles = new bool[7];

    public void RegisterCompletion(byte id)
    {
        puzzles[id] = true;
    }

    public bool VerifyCompletion(byte id)
    {
        return puzzles[id];
    }
}
