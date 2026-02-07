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
    public RectTransform JournalContainer;

    private void Start()
    {
        
    }
    public void DiscoverWord(string symbol)
    {
       
       var newWord = Instantiate(discoverWordPrefab, SymbolsGrid);
       newWord.GetComponentInChildren<WordUI>().SetWord(symbol);
        newWord.GetComponentInChildren<DraggableUISnapCenter>().bounds = JournalContainer;

    }
    public void OnJournalInitHandler()
    {
        foreach(var symbol in playerJournal.discoveredSymbols)
        {
            DiscoverWord(symbol);
        }
        foreach (var english in playerJournal.realDict.EnglishToSymbol.Keys)
        {
            var newSlot = Instantiate(wordSlotPrefab, WordsGrid);
            var snapUI = newSlot.GetComponentInChildren<UISnapPoint>();
            snapUI.Word = english;
            Debug.Log("Initializing slot for English word: " + english);


            //If the player has a guess for this word
            if ( ! playerJournal.englishToSymbol[english].Contains('*'))
            {
                snapUI.occupied = true;
                snapUI.symbol = playerJournal.englishToSymbol[english];

                MoveSymbolToSnapPoint(snapUI.symbol, snapUI);
            }
        }



        playerJournal.OnNewDiscoveredSymbol.AddListener(DiscoverWord);
    }
    public void MoveSymbolToSnapPoint(string symbol, UISnapPoint snapPoint)
    {
        var temp = SymbolsGrid.GetComponentsInChildren<WordUI>();
        foreach (var word in temp)
        {
            if (word.word == symbol)
            {
                var dragUI = word.GetComponentInChildren<DraggableUISnapCenter>();
                dragUI.SnapToPoint(snapPoint);
                return;
            }
        }
    }



}
