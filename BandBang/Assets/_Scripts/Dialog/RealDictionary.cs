using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RealDictionary", menuName = "Dialog/RealDictionary")]
public class RealDictionary : ScriptableObject
{
    [SerializeField]
    private List<Entry> dictionary;
    Dictionary<string, string> englishToSymbol = new Dictionary<string, string>();
    Dictionary<string, string> symbolToEnglish = new Dictionary<string, string>();

    public Dictionary<string, string> EnglishToSymbol { get { 
            
            if(englishToSymbol.Count == 0)
            {
                initLookUp();
               
            }
            return englishToSymbol; } }

    public Dictionary<string, string> SymbolToEnglish
    {
        get
        {

            if (symbolToEnglish.Count == 0)
            {
                initLookUp();

            }
            return symbolToEnglish;
        }
    }

    void initLookUp()
    {
        englishToSymbol = new Dictionary<string, string>();
       /* foreach (var entry in items)
        {
            if (!itemLookup.ContainsKey(entry.itemID))
            {
                itemLookup[entry.itemID] = entry.prefab;
            }
            else
            {
                Debug.LogWarning($"Duplicate ItemID found: {entry.itemID}. Only the first prefab will be used.");
            }

        }*/
    }
}

[System.Serializable]
public class Entry
{
    public string english;
    public Sprite symbol;
}
