using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerJournal : MonoBehaviour
{
    public List<DictionarySymbolEntry> testEntries = new List<DictionarySymbolEntry>();
    [SerializeField]
    PlayerJournal playerJournal;
    // Start is called before the first frame update
    void Start()
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
