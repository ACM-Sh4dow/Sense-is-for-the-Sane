using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FuneralManager : PuzzleTracker
{
    private void Start()
    {
        Overseer.Instance.AddManager(this);
    }

    public void OnPuzzleComplete()
    {
        int completedPuzzle = CheckCompletionProgress(Puzzles.ToArray()).Completed;

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

        }

        /*
         * if(flowerpuzzle == fullyResolved)
         * {
         *      turn off ambience
         * }
         * 
         *  if(flowerpuzzle == fullyResolved)
         * {
         *      turn off ambience
         * }
         *  if(flowerpuzzle == fullyResolved)
         * {
         *      turn off ambience
         * }
         */
    }

}
