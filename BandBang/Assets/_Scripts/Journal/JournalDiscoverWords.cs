using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalDiscoverWords : MonoBehaviour
{
    public PlayerJournal playerJournal;

    public GameObject wordSlotPrefab;
    public GameObject discoverWordPrefab;
    public Transform SymbolsGrid;
    public Transform WordsGrid;
    private void Start()
    {
        playerJournal.OnNewDiscoveredSymbol.AddListener(DiscoverWord);
        InitSlots();
    }
    public void DiscoverWord(string symbol)
    {
       
       var newWord = Instantiate(discoverWordPrefab, SymbolsGrid);
       newWord.GetComponentInChildren<WordUI>().SetWord(symbol);
    
    }
    void InitSlots()
    {
        foreach (var english in playerJournal.realDict.EnglishToSymbol.Keys)
        {
            var newSlot = Instantiate(wordSlotPrefab, WordsGrid);
            newSlot.GetComponentInChildren<UISnapPoint>().Word = english;
            Debug.Log("Initializing slot for English word: " + english );
        }
    }


}
