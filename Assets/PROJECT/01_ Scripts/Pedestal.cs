using System;
using UnityEngine;

public class Pedestal : Puzzle, InteractionPoint
{
    [Tooltip("SET THIS to True if pedestal is holding Painting, set False if holding Flowers.")]
    [SerializeField] private bool isPaintingPedestal;

    [SerializeField] private GameObject wallIntact;
    [SerializeField] private GameObject wallShattered;
    [SerializeField] private GameObject puzzleResult;
    [SerializeField] private GameObject soundObject;

    public AK.Wwise.Event placingAudioEvent;

    public void Interact()
    {
        AttemptPuzzle();
    }

    public override void AttemptPuzzle()
    {
        if ((Overseer.Instance.GetManager<FuneralManager>().flowerPuzzleCompleted && !isPaintingPedestal) || 
            (Overseer.Instance.GetManager<FuneralManager>().paintingPuzzleCompleted && isPaintingPedestal))
        {
            PedestalCompleted();
        }
    }
    

    private void PedestalCompleted()
    {
        state = State.fullyResolved;
        RegisterCompletion();
            
        puzzleResult.SetActive(true);

        if (placingAudioEvent != null) placingAudioEvent.Post(gameObject);

        ShatterWall();
    }

    private void ShatterWall()
    {
        wallShattered.SetActive(true);
        wallIntact.SetActive(false);
        AkUnitySoundEngine.PostEvent("Fnrl_Wall_Shatter", soundObject);
    }
}
