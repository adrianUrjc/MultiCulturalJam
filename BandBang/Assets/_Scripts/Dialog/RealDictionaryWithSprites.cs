using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "RealDictionaryWithSprites", menuName = "Dialog/RealDictionaryWithSprites")]
public class RealDictionaryWithSprites : RealDictionary
{
    [SerializeField]
    private List<DictionarySymbolSpritesEntry> dictionaryS;

    protected override void initLookUp()
    {
        Debug.Log("Initializing RealDictionary lookups...");
        englishToSymbol = new Dictionary<string, string>();
        symbolToEnglish = new Dictionary<string, string>();
        foreach (var entry in dictionaryS)
        {
            //get the last number of the sprite name, after the '_'
            // character, and use it as the index for the symbol in the string

            string symbolIndex = entry.symbol.name.Split('_').ToList<string>().Last<string>();
            string symbolKey = "<sprite=" + symbolIndex + ">";
            if (!englishToSymbol.ContainsKey(entry.english))
            {
                englishToSymbol[entry.english] = symbolKey;
            }
            else
            {
                Debug.LogWarning($"Duplicate English word found: {entry.english}. Only the first symbol will be used.");
            }
            if (!symbolToEnglish.ContainsKey(symbolKey))
            {
                symbolToEnglish[symbolKey] = entry.english;
            }
            else
            {
                Debug.LogWarning($"Duplicate Symbol found: {symbolKey}. Only the first English word will be used.");
            }
        }
      
    }
}

[System.Serializable]
public class DictionarySymbolSpritesEntry
{
    public string english;
    public Sprite symbol;
}
