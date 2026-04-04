using UnityEngine;

public class AudibleZone : MonoBehaviour
{
    private GameObject player;

    //public GameObject[] audioObjects;

    private void Start()
    {
        player = PlayerBehaviour.Instance.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 1, gameObject);
            Debug.Log("IN AUDIO ZONE: " + name);

            //foreach (GameObject obj in audioObjects)
            //{
            //    AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 1);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 0, gameObject);

            //foreach (GameObject obj in audioObjects)
            //{
            //    AkUnitySoundEngine.SetRTPCValue("InAudibleZone", 0);
            //}
        }
    }
}
