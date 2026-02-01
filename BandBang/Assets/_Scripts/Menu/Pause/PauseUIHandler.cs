using UnityEngine;

public class PauseUIHandler : MonoBehaviour
{
    GameObject pauseUI;
    private void OnEnable()
    {
        Pause.OnPause.AddListener(PauseHandler);
        Pause.OnResume.AddListener(ResumeHandler);
    }
    private void OnDisable()
    {
        Pause.OnPause.RemoveListener(PauseHandler);
        Pause.OnResume.RemoveListener(ResumeHandler);
    }


    void PauseHandler()
    {
        pauseUI.SetActive(true);
    }
    void ResumeHandler()
    {
        pauseUI.SetActive(false);
    }

    public void Continue() //llamado con el boton de continuar
    {
        Pause.ResumeGame();
    }
}
