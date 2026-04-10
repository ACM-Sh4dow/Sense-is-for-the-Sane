using UnityEngine;

public class AudibleZone : MonoBehaviour
{
    private GameObject player;

    //public GameObject[] audioObjects;

    private void Start()
    {
        player = PlayerBehaviour.Instance.gameObject;
        if (GetComponent<Collider>() == null) AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 1, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 1, gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 0, gameObject);
        }
    }
}
