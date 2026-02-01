using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] TaggedDetector2D detector;
    

    [SerializeField] private Animator animator;


    public void Interact()
    {
        if (detector.HasTarget())
        {
            detector.GetTarget().GetComponentInChildren<InteractionReceiver>()?.Interact();
        }
    }
}
