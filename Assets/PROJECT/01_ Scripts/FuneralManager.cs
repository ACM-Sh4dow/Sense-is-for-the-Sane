using System.Collections.Generic;
using UnityEngine;

public class FuneralManager : MonoBehaviour
{
    public List<Puzzle> Puzzles = new List<Puzzle>();

    private void Start()
    {
        Overseer.Instance.GetManager<PuzzleTracker>().puzzles = new bool[Puzzles.Count];
    }

    private void Update()
    {
        
    }


}
