using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class Translator : MonoBehaviour
{
    PlayerJournal playerJournal;
    RealDictionary realDictionary;
    Dictionary<string, string> translations = new()
{
    { "I like", "Me gusta" },
    { "I", "Yo" },
    { "like", "gustar" },
    { "cats", "gatos" }
};
    string input = "I like cats. I like dogs.";

    private void Start()
    {
       
    }
    /// <summary>
    /// Cuando el NPC habla
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <returns></returns>
    public string TranslateTextToSymbolsReal(string dialogOption)
    {
        var temp = TranslateWithDict(dialogOption, realDictionary.EnglishToSymbol);
        return dialogOption + " **with player symbols **";
    }
    /// <summary>
    /// Para las opciones del jugador
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <returns></returns>
    public string TranslateTextToSymbols(string dialogOption)
    {
        var temp = TranslateWithDict(dialogOption, playerJournal.englishToSymbol);

        return dialogOption + " **with symbols **";
    }
    /// <summary>
    /// Para cuando el jugador elige una opción
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <returns></returns>
    public string TranslateTextToEnglishWithPlayerDict(string dialogOption)
    {
        var temp = TranslateWithDict(dialogOption, playerJournal.englishToSymbol) ;

        var temp2 = TranslateWithDict(temp, realDictionary.SymbolToEnglish);
        return dialogOption;
    }

    private string TranslateWithDict(string s, Dictionary<string,string> dict)
    {
        var keys = dict.Keys
   .OrderByDescending(k => k.Length)
   .Select(Regex.Escape);

        string pattern = @"\b(" + string.Join("|", keys) + @")\b";

        string result = Regex.Replace(
            s,
            pattern,
            match => translations[match.Value]
        );

        Debug.Log(result);
        return result;
    }

    
}
