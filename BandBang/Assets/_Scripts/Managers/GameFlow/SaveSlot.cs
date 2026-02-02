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
    public ALoader loader;
 
    public void SelectSlot(int slot)
    {
       //loader.route(Path.Combine("SaveSlot_", slot.ToString()));
    }
}



