using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔍 حد دخل المنطقة: " + other.gameObject.name);

        if (triggered) return;

        // جرب كل الاحتمالات
        if (other.gameObject.name.Contains("Camera") ||
            other.CompareTag("MainCamera") ||
            other.CompareTag("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggered = true;
            Debug.Log("✅ اللاعب دخل المنطقة!");

            if (SimpleStepManager.Instance != null)
            {
                SimpleStepManager.Instance.NextStep();
            }
            else
            {
                Debug.LogError("❌ SimpleStepManager مش موجود!");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!triggered)
        {
            Debug.Log("🔄 لسه جوا المنطقة: " + other.gameObject.name);
        }
    }

    public void ResetZone()
    {
        triggered = false;
    }
}