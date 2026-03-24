using UnityEngine;

public abstract class  Puzzle: MonoBehaviour
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

    private void Start()
    {
        Debug.Log("Checking for Puzzle Tracker");

        if (Overseer.Instance.GetManager<FuneralManager>() != null)
        {
            Debug.Log("Code do a work");
        }
    }
}
