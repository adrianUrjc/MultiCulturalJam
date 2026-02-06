using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [SerializeField, ReadOnly]

    int currentSlot = 0;
    [SerializeField]
    [ReadOnly]
    public LoaderMono loader;
    void Start()
    {
        loader = GetComponent<LoaderMono>();
    }
public void SetSlotIdx(int idx)
    {
         currentSlot = idx;
         if(loader==null) { Debug.Log("Loader not found"); return; }
        loader.ChangeAssetName("SaveSlot_" + idx.ToString());
        loader.RemoveLoadedValues();
        loader.LoadData();
    }
    public void SelectSlot(int slot)
    {
       SetSlotIdx(slot);
        if (!loader.GetValue<bool>("HasPlayedBefore"))
        {
            loader.SetValue("HasPlayedBefore", true);
            loader.SetValue("FirstTimePlayed", (int)System.DateTimeOffset.Now.ToUnixTimeSeconds());
        }
        GameManager.Instance.LoadLevel(loader.GetValue<int>("CurrentCountry"));
    }

    public void SaveSlotSave()
    {
        if (loader.GetValue<bool>("HasPlayedBefore"))
        {
            loader.SetValue("LastTimePlayed", (int)System.DateTimeOffset.Now.ToUnixTimeSeconds());
            int timePlayed = loader.GetValue<int>("LastTimePlayed") - loader.GetValue<int>("FirstTimePlayed");
            loader.SetValue("TimePlayed", timePlayed);
            loader.SaveData();
        }
    }
    void OnDestroy()
    {

       // SaveSlotSave();
    }
}



