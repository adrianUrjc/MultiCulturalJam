using UnityEngine;

public class UISnapPoint : MonoBehaviour
{
    PlayerJournal journal;
    public bool occupied;

    string word;
    string symbol;
    public RectTransform rect => (RectTransform)transform;

    public void Occupy(string symb)
    {
        
        occupied = true;
        symbol = symb;
        journal.GuessMeaning(word, symbol);


        //llamar al journal para decirle el symbol recibido y word==symbol
    }
    public void Vacate()
    {
        occupied = false;

        journal.UnGuessMeaning(word, symbol);

        //llamar al journal de que se ha quitado el 
    }
}
