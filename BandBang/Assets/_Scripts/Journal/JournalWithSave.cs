using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalWithSave : PlayerJournal
{
    ALoader loader;
    SaveSlot saveSlot;
    private void OnDisable()
    {
        SaveToFile();
    }
    public override void ReadSaveFile()
    {
        var temp = GameObject.FindGameObjectWithTag("GameController");

        saveSlot =temp.GetComponentInChildren<SaveSlot>();
        if(saveSlot == null)
        {
                Debug.LogError("No SaveSlot found in the scene! JournalWithSave requires a SaveSlot to function properly.");
        }
        loader = saveSlot.loader;

        if (loader == null)
        {
            Debug.LogError("No loader found in the scene! JournalWithSave requires a SaveSlot to function properly.");
        }
        string knowSymbols = loader.GetValue<string>("KnownSymbols");
        string symbolsDict = loader.GetValue<string>("SymbolsDict");
        string englishDict = loader.GetValue<string>("EnglishDict");
        string[] symbols = knowSymbols.Split(',');
        foreach (var symbol in symbols)
        {
            Debug.Log("Discovering symbol: " + symbol);
            if (symbol != "")
                discoverSymbol(symbol);
        }
        string[] symbolPairs = symbolsDict.Split(',');
        string[] englishPairs = englishDict.Split(',');
        for (int i = 0; i < symbolPairs.Length; i++)
        {
            Debug.Log("Guessing symbol: " + symbolPairs[i] + " with meaning: " + englishPairs[i]);

            GuessMeaning(englishPairs[i], symbolPairs[i]);

        }
    }
    public void SaveToFile()
    {
        if (loader == null || saveSlot == null)
        {
            saveSlot = FindFirstObjectByType<SaveSlot>();
            if (saveSlot == null)
            {
                return;
            }
            loader = saveSlot.loader;
            if (loader == null)
            {
                return;
            }
        }
        loader = saveSlot.loader;
        string knownSymbols = string.Join(",", discoveredSymbols);
        string symbolDict = "";
        string englishDict = "";
        foreach (var kvp in symbolToEnglish)
        {
            Debug.Log("Saving symbol: " + kvp.Key + " with meaning: " + kvp.Value);
            symbolDict += kvp.Key + ",";
            englishDict += kvp.Value + ",";
        }
        loader.SetValue("KnownSymbols", knownSymbols);
        loader.SetValue("SymbolsDict", symbolDict);
        loader.SetValue("EnglishDict", englishDict);
        loader.SaveValues();
    }
}