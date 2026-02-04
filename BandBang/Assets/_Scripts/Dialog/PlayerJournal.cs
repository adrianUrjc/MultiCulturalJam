using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJournal : MonoBehaviour
{
    [SerializeField]
    RealDictionary realDict;
    public Dictionary<string, string> englishToSymbol = new Dictionary<string, string>();
    public Dictionary<string, string> symbolToEnglish = new Dictionary<string, string>();

    public List<string> discoveredSymbols = new(); 

    private void Start()
    {


        englishToSymbol = new Dictionary<string, string>();
        symbolToEnglish = new Dictionary<string, string>();
        foreach (var key in realDict.SymbolToEnglish.Keys)
        {
            englishToSymbol[key] = "****";
            symbolToEnglish[key] = "****";
        }
    }
    public void discoverSymbol(string symbol)
    {
        discoveredSymbols.Add(symbol);
    }
    public void GuessMeaning(string englishWord, string symbol)
    {
        englishToSymbol[symbol] = englishWord;
        symbolToEnglish[englishWord] = symbol;

    }
    public void UnGuessMeaning(string englishWord, string symbol)
    {
        englishToSymbol[englishWord] = "****";
        symbolToEnglish[symbol] = "****";
    }
    




}
