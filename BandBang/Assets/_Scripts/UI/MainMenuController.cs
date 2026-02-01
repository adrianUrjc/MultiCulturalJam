using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MenuStates
    {
        MAINMENU,
        SAVESELECT,
        SETTINGS,
        CREDITS,
    }

    [SerializeField]
    GameObject MainMenuButtons;

    [SerializeField]
    GameObject Credits;

    [SerializeField]
    GameObject SaveSlots;

    [SerializeField]
    GameObject Settings;
    [SerializeField]
    Button backButton;
    MenuStates currentState = MenuStates.MAINMENU;
    void Start()
    {

        foreach (Transform childButton in MainMenuButtons.transform)
        {
            Button buttonComp = childButton.GetComponent<Button>();
            if (buttonComp != null)
            {
                switch (childButton.name)
                {
                    case "PlayButton":
                        buttonComp.onClick.AddListener(() => MainMenuButton(MenuStates.SAVESELECT));
                        break;
                    case "SettingsButton":
                        buttonComp.onClick.AddListener(() => MainMenuButton(MenuStates.SETTINGS));
                        break;
                    case "CreditsButton":
                        buttonComp.onClick.AddListener(() => MainMenuButton(MenuStates.CREDITS));
                        break;
                    case "ExitButton":
                        //provisional, luego cambiar por el metodo de GameManager
                        buttonComp.onClick.AddListener(() => Application.Quit());
                        break;
                }
            }
        }
        backButton.onClick.AddListener(() => MainMenuButton(MenuStates.MAINMENU));
        HandleState();

    }

    public void MainMenuButton(MenuStates targetState)
    {
        currentState = targetState;
        HandleState();
    }


    void HandleState()
    {
        MainMenuButtons.SetActive(currentState == MenuStates.MAINMENU);
        Credits.SetActive(currentState == MenuStates.CREDITS);
        SaveSlots.SetActive(currentState == MenuStates.SAVESELECT);
        Settings.SetActive(currentState == MenuStates.SETTINGS);
        if (currentState == MenuStates.MAINMENU)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }
}
