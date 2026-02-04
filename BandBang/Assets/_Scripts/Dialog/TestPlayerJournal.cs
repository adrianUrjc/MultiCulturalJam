using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestPlayerJournal : MonoBehaviour
{
    public List<DictionarySymbolEntry> testEntries = new List<DictionarySymbolEntry>();
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
            playerJournal.discoverSymbol(entry.symbol);
            playerJournal.GuessMeaning(entry.english, entry.symbol);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}

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
