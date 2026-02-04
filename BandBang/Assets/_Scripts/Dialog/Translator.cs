using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class Translator : MonoBehaviour
{
    [SerializeField]
    PlayerJournal playerJournal;
    [SerializeField]
    RealDictionary realDictionary;

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
        return temp;
    }
    /// <summary>
    /// Para las opciones del jugador
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <returns></returns>
    public string TranslateTextToSymbolsPlayer(string dialogOption)
    {
        var temp = TranslateWithDict(dialogOption, playerJournal.englishToSymbol);

        return temp;
    }
    /// <summary>
    /// Para cuando el jugador elige una opci�n
    /// </summary>
    /// <param name="dialogOption"></param>
    /// <returns></returns>
    public string TranslateTextToEnglishPlayer(string dialogOption)
    {
        var temp = TranslateWithDict(dialogOption, playerJournal.englishToSymbol) ;

       var temp2 = TranslateWithDict(temp, realDictionary.SymbolToEnglish);
        return temp2;
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
            match => dict[match.Value]
        );

        Debug.Log(result);
        return result;
    }

    
}
