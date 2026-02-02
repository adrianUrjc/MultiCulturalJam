using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [SerializeField,ReadOnly]
    
    int currentSlot = 0;
    [SerializeField]
    [ReadOnly]
    public ALoader loader;
    void Start()
    {
        loader = GetComponent<ALoader>();
    }

    public void SelectSlot(int slot)
    {
        currentSlot = slot;
        loader.ChangeAssetName("SaveSlot_" + slot.ToString());
        loader.RemoveLoadedValues();
        loader.LoadValues();
        GameManager.Instance.LoadLevel(loader.GetValue<int>("CurrentCountry"));
    }
}



