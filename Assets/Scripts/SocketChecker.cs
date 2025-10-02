using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class SocketChecker : MonoBehaviour
{
    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnObjectPlaced);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectPlaced);
    }

    void OnObjectPlaced(SelectEnterEventArgs args)
    {
        Debug.Log("🔧 تم وضع الأوبجكت!");

        if (SimpleStepManager.Instance != null)
        {
            SimpleStepManager.Instance.NextStep();
        }
    }
}