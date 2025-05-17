using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Flashlight flashlight = FindObjectOfType<Flashlight>();

            if (flashlight != null)
            {
                flashlight.ResetBattery();
                gameObject.SetActive(false);
            }
        }
    }
}