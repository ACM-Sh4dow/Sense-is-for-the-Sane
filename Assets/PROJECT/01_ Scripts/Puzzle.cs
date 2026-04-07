using UnityEngine;

public abstract partial class  Puzzle: MonoBehaviour
{
    public enum State : byte
    {
        notSolvable,
        solvable,
        solved,
        fullyResolved
    }

    public State state;

    public string puzzleName;

    public void RegisterCompletion()
    {
        Overseer.Instance.GetManager<FuneralManager>().OnPuzzleComplete();
    }

    public abstract void AttemptPuzzle();
}
