
using UnityEngine;
using UnityEngine.Events;

public class InteractionReceiver : MonoBehaviour
{
    public UnityEvent onInteract;
    public void Interact()
    {
        onInteract?.Invoke();
    }
}
