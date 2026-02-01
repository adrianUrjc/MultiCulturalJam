
using UnityEngine.Events;
using UnityEngine;

public class Pause
{
    public static UnityEvent OnPause;
    public static UnityEvent OnResume;

    public static void PauseGame()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnPause?.Invoke();
    }
    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OnResume?.Invoke();
    }
}