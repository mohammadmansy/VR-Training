using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // ??? ?????? ???? ???????
        if (other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
            Debug.Log("?? ???? ???????!");

            if (SimpleStepManager.Instance != null)
            {
                SimpleStepManager.Instance.NextStep();
            }
        }
    }
}