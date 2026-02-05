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
        loader = saveSlot.loader;
        string knowSymbols = loader.GetValue<string>("KnownSymbols");
        string symbolsDict = loader.GetValue<string>("SymbolDict");
        string englishDict = loader.GetValue<string>("EnglishDict");
        string[] symbols = knowSymbols.Split(',');
        foreach (var symbol in symbols)
        {
            if (symbol != "")
                discoverSymbol(symbol);
        }
        string[] symbolPairs = symbolsDict.Split(',');
        string[] englishPairs = englishDict.Split(',');
        for (int i = 0; i < symbolPairs.Length; i++)
        {

            GuessMeaning(englishPairs[i], symbolPairs[i]);

        }
    }
    public void SaveToFile()
    {
        string knownSymbols = string.Join(",", discoveredSymbols);
        string symbolDict = "";
        string englishDict = "";
        foreach (var kvp in symbolToEnglish)
        {
            symbolDict += kvp.Key + ",";
            englishDict += kvp.Value + ",";
        }
        loader.SetValue("KnownSymbols", knownSymbols);
        loader.SetValue("SymbolDict", symbolDict);
        loader.SetValue("EnglishDict", englishDict);
    }
}