using UnityEngine;

public class Unlockingmouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }
}
