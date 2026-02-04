using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{

    public string TranslateTextToSymbols(string dialogOptions)
    {

        return dialogOptions + " **with symbols **";
    }
    public string TranslateTextToEnglishWithPlayerDict(string dialogOption)
    {
        return dialogOption;
    }


  
    string Translate(string s)
    {
        return s;
    }
}
