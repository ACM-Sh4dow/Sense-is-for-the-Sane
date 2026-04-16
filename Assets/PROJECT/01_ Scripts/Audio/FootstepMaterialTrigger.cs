using UnityEngine;

public class FootstepMaterialTrigger : MonoBehaviour
{
    private GameObject player;

    public string switchName;

    void Start()
    {
        player = PlayerBehaviour.Instance.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            AkUnitySoundEngine.SetSwitch("FootstepsSwitch", switchName, player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            AkUnitySoundEngine.SetSwitch("FootstepsSwitch", "Wood", player);
        }
    }
}
