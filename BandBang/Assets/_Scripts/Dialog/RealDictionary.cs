using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "RealDictionary", menuName = "Dialog/RealDictionary")]
public class RealDictionary : ScriptableObject
{
    [SerializeField]
    private List<DictionarySymbolEntry> dictionary;
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
        symbolToEnglish = new Dictionary<string, string>();
        foreach (var entry in dictionary)
        {
            if (!englishToSymbol.ContainsKey(entry.english))
            {
                englishToSymbol[entry.english] = entry.symbol;
            }
            else
            {
                Debug.LogWarning($"Duplicate English word found: {entry.english}. Only the first symbol will be used.");
            }
            if (!symbolToEnglish.ContainsKey(entry.symbol))
            {
                symbolToEnglish[entry.symbol] = entry.english;
            }
            else
            {
                Debug.LogWarning($"Duplicate Symbol found: {entry.symbol}. Only the first English word will be used.");
            }
        }
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
public class DictionarySymbolEntry
{
    public string english;
    public string symbol;
}
