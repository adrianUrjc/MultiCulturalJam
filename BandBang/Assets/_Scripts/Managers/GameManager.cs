using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Patterns.Singleton;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : ASingleton<GameManager>, IManager
{
    public IManager.GameStartMode StartMode => IManager.GameStartMode.FIRST;
    [SerializeField, ReadOnly]
    public
    LoaderMono loader;
    [SerializeField]
    float autoSaveTime = 20f;

    public void Start()
    {
        Instance.StartManager();
        SceneManager.sceneLoaded += (scene, mode) => OnSceneChange();
    }
    public void StartManager()
    {
        loader = GetComponent<LoaderMono>();
        LoadData();
    }
    public void LoadData()
    {
        loader.LoadData();

        string FirsTime = "HasPlayedBefore";
        string FirstTimeplayed = "FirstTimePlayed";

//        Debug.Log(loader.GetValue<bool>(FirsTime));

        if (!loader.GetValue<bool>(FirsTime))
        {

            loader.SetValue(FirsTime, true);

  //          Debug.Log(loader.GetValue<bool>(FirsTime));

            loader.SetValue(FirstTimeplayed, DateTimeOffset.UtcNow.ToUnixTimeSeconds());

    //        Debug.Log(loader.GetValue<long>(FirstTimeplayed));

            loader.SaveData();
        }
    }
    public void OnSceneChange()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu": 
            //Buscar MainMenuController y encontrar las ranuras de guardado
            //asignarles la funcion on button click, esto es necesario 
            // porque se pierde la referencia al cambiar de escena
            Debug.Log("Setting Save Slot Buttons");
            MainMenuController mainMenuController = FindAnyObjectByType<MainMenuController>();

                int slotIndex = 0;
            GetComponentInChildren<SaveSlot>().SaveSlotSave();
            foreach (Transform child in mainMenuController.SaveSlots.transform.Find("HorizontalLayout"))
            {
                var buttonComp = child.GetComponent<Button>();
                int idx= slotIndex; // Capture the current value of slotIndex, estoy alucinando son las 0:24 de la noche y me estan matando las referencias de c#
                if (buttonComp != null)
                {
                    buttonComp.onClick.RemoveAllListeners();
                    buttonComp.onClick.AddListener(() => 
                    {
                        GetComponentInChildren<SaveSlot>().SelectSlot(idx);
                    });
                }
                slotIndex++;
            }
            break;
            default: 
            
            break;

        }
    }
    public void OnEnd()
    {
    }

    public void OnEndGame()
    {
    }

    public void OnStartGame()
    {
        AutoSave();
    }
    public void AutoSave()
    {
        loader.SaveData();
        DelayedActions.Do(AutoSave, autoSaveTime, this, "AutoSaveData");
    }
    public void SaveData()
    {
if(loader==null) return;
        loader.SetValue<long>("LastTimePlayed", DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        long totalTime = loader.GetValue<long>("LastTimePlayed") - loader.GetValue<long>("FirstTimePlayed");

        loader.SetValue<long>("TimePlayed", totalTime);

        loader.SaveData();
    }

    public void LoadLevel(int level)
    {
        GetComponent<LevelLoader>().LoadLevel((countries)level);


    }
    void OnDestroy()
    {
        SaveData();
    }
}
