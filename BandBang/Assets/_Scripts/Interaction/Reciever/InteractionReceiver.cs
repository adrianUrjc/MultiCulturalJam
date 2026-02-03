
using UnityEngine;
using UnityEngine.Events;

public class InteractionReceiver : MonoBehaviour
{
    public UnityEvent onInteract;
    public virtual void Interact()
    {
        onInteract?.Invoke();
    }
    public virtual void Interact(GameObject interactor)
    {
        onInteract?.Invoke();
    }
}
