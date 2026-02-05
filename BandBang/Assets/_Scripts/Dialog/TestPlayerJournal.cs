using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class TestPlayerJournal : MonoBehaviour
{
    public List<DictionarySymbolSpritesEntry> testEntries = new List<DictionarySymbolSpritesEntry>();
    [SerializeField]
    PlayerJournal playerJournal;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayersJournal(); 
    }
    public void UpdatePlayersJournal()
    {
        foreach (var entry in testEntries)
        {

            string symbolIndex = entry.symbol.name.Split('_').ToList<string>().Last<string>();
            string symbolKey = "<sprite=" + symbolIndex + ">";
            playerJournal.discoverSymbol(symbolKey);
            playerJournal.GuessMeaning(entry.english, symbolKey);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}


#if UNITY_EDITOR
[CustomEditor(typeof(TestPlayerJournal))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector first
        DrawDefaultInspector();

        // Get reference to the target script
        TestPlayerJournal myScript = (TestPlayerJournal)target;

        // Add a button
        if (GUILayout.Button("Apply new translations"))
        {
            myScript.UpdatePlayersJournal();
        }
    }
}

#endif