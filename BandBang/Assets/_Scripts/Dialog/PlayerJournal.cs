using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJournal : MonoBehaviour
{

    
    [SerializeField]
    protected RealDictionary realDict;
    public Dictionary<string, string> englishToSymbol = new Dictionary<string, string>();
    public Dictionary<string, string> symbolToEnglish = new Dictionary<string, string>();

    public List<string> discoveredSymbols = new(); 

    private void Start()
    {

        ReadSaveFile();
        foreach (var key in realDict.SymbolToEnglish.Keys)
        {
            if(!symbolToEnglish.ContainsKey(key))
            symbolToEnglish[key] = "****";
        }
        foreach (var key in realDict.EnglishToSymbol.Keys)
        {
            if (!englishToSymbol.ContainsKey(key))
                englishToSymbol[key] = "*** (unkown, trying to guess " + key + ") ";
        }

    }
    public void discoverSymbol(string symbol)
    {
       if(!discoveredSymbols.Contains(symbol)) 
       discoveredSymbols.Add(symbol);
    }
    public void GuessMeaning(string englishWord, string symbol)
    {
        englishToSymbol[englishWord] = symbol;
        symbolToEnglish[symbol] = englishWord;
        Debug.Log(englishToSymbol[englishWord]);

    }
    public void UnGuessMeaning(string englishWord, string symbol)
    {
        englishToSymbol[englishWord] = "*** (unkown, trying to guess " + englishWord + ") ";
        symbolToEnglish[symbol] = "****";
    }
    public virtual void ReadSaveFile()
    {
        string knownSymbols = "";
    }




}
