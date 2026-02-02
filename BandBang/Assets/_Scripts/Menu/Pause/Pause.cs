
using UnityEngine.Events;
using UnityEngine;

public class Pause
{
    public static UnityEvent OnPause = new ();
    public static UnityEvent OnResume= new();

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
        //Cursor.visible = false; de momento necesito el raton :)
        Cursor.lockState = CursorLockMode.Locked;
        OnResume?.Invoke();
    }
}