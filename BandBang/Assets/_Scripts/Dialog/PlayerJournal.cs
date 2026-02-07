using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJournal : MonoBehaviour
{

    
    [SerializeField]
    public RealDictionary realDict;
    public Dictionary<string, string> englishToSymbol = new Dictionary<string, string>();
    public Dictionary<string, string> symbolToEnglish = new Dictionary<string, string>();

    public List<string> discoveredSymbols = new();
    public UnityEvent<string> OnNewDiscoveredSymbol=new();
    public UnityEvent JournalInitialized = new();




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
        JournalInitialized?.Invoke();
    }
    public void discoverSymbol(string symbol)
    {
       if(!discoveredSymbols.Contains(symbol))
        {

            discoveredSymbols.Add(symbol);
            OnNewDiscoveredSymbol.Invoke(symbol);
        }
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
