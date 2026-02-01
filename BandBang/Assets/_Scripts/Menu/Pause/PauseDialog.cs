
using UnityEngine;
using DialogSystem.Runtime.Core;
public class PauseDialog : MonoBehaviour
{
    [SerializeField]
    DialogManager dialogManager;

    private void OnEnable()
    {
        Pause.OnPause.AddListener(HandlePause);
        Pause.OnResume.AddListener(HandleResume);
    }
    private void HandleResume()
    {
        dialogManager.ResumeAfterHistory();
    }
    private void HandlePause()
    {
        dialogManager.PauseForHistory();
    }
    private void OnDisable()
    {
        Pause.OnPause.RemoveListener(HandlePause);
        Pause.OnResume.RemoveListener(HandleResume);

    }
    
}

