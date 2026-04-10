using UnityEngine;

public abstract partial class  Puzzle: MonoBehaviour
{
    public enum Scene
    {
        Void,
        Apartment,
        FuneralHome
    }
    public Scene scene;
    
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
        switch (scene)
        {
            case Scene.Void:
                break;
            case Scene.Apartment:
                break;
            case Scene.FuneralHome:
                Overseer.Instance.GetManager<FuneralManager>().OnPuzzleComplete();
                break;
        }
    }

    public abstract void AttemptPuzzle();
}
