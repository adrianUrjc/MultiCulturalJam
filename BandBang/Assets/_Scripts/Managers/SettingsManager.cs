using System;
using System.Collections;
using System.Collections.Generic;
using Character.Settings;
using Managers;
using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LoaderMono))]
public class SettingsManager : ASingleton<SettingsManager>, IManager, ILoaderUser
{
    public IManager.GameStartMode StartMode => IManager.GameStartMode.FIRST;
    [SerializeField, ExposedScriptableObject]
    GroupValues settingsValues;
    [SerializeField]
    GameObject settingsGo;
    List<UISettingsElement> uiElements = new();
    public UnityEvent onSettingsChange;
    #region MANAGERLOGIC
    public void OnValuesChange()
    {
        Debug.Log($"[{name}] Han habido cambios");
        onSettingsChange.Invoke();
    }
    public void SetValue<T>(string name, T value)//cambia de valor y aplica(pero no se guarda)
    {
        if (settingsValues == null) return;
        settingsValues.SetValue<T>(name, value);
        OnValuesChange();
    }
    public T GetValue<T>(string name)
    {
        return settingsValues.GetValue<T>(name);
    }
    public void StartManager()
    {
        Debug.Log($"[{name}]Inciando...");
        LoadData();
    }
    public void OnStartGame()
    {

    }
    [ContextMenu("Cargar archivos")]
    public void LoadData()
    {
        settingsValues = GetComponent<ALoader>().LoadValues();
        if (settingsGo != null)
        {
            uiElements.AddRange(settingsGo.transform.GetComponentsInChildren<UISettingsElement>(true));
            foreach (var element in uiElements)
            {
                element.Init();
                switch (element.DataType)
                {
                    case VALUE_TYPE.BOOL:
                        element.Subscribe<bool>(ChangeTemporalData);
                        break;
                    case VALUE_TYPE.FLOAT:
                        element.Subscribe<float>(ChangeTemporalData);
                        break;
                    case VALUE_TYPE.STRING:
                        element.Subscribe<string>(ChangeTemporalData);
                        break;
                }
            }
            OnValuesChange();

        }
    }
    public void ChangeTemporalData<T>(string uiName, T value)
    {
        //var dataValue= FindAnyObjectByType<Character.Settings.Settings>().GetValue<T>(uiName);
        //Debug.Log("[UIManager]Cambiando el valor en " + uiName + " : " + value);//ya sabemos que funciona
        //Decir a settings que cambie valor y aplique(pero de momento no guarda)
        SetValue<T>(uiName, value);
    }
    [ContextMenu("Guardar archivos")]
    public void SaveData()
    {
        GetComponent<ALoader>().SaveValues(settingsValues);
    }


    public void OnEnd()
    {
        SaveData();
    }

    public void OnEndGame()
    {
        SaveData();
    }

    #endregion
}
