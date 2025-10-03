using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketChecker : MonoBehaviour
{
    private XRSocketInteractor socket;
    private bool hasObject = false;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();

        if (socket == null)
        {
            Debug.LogError("❌ مفيش XR Socket Interactor!");
        }
        else
        {
            Debug.Log("✅ Socket جاهز على: " + gameObject.name);
        }
    }

    void OnEnable()
    {
        if (socket != null)
        {
            socket.selectEntered.AddListener(OnObjectPlaced);
        }
    }

    void OnDisable()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnObjectPlaced);
        }
    }

    void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (hasObject) return;

        hasObject = true;
        GameObject placedObject = args.interactableObject.transform.gameObject;

        Debug.Log("✅ تم وضع الأوبجكت: " + placedObject.name);

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