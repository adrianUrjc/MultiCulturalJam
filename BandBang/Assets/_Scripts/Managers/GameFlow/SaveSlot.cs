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
    public ALoader loader;
    void Start()
    {
        loader = GetComponent<ALoader>();
    }
public void SetSlotIdx(int idx)
    {
         currentSlot = idx;
         if(loader==null) { Debug.Log("Loader not found"); return; }
        loader.ChangeAssetName("SaveSlot_" + idx.ToString());
        loader.RemoveLoadedValues();
        loader.LoadValues();
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
            loader.SaveValues();
        }
    }
    void OnDestroy()
    {

       // SaveSlotSave();
    }
}



