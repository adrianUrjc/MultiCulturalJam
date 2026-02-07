using TMPro;
using UnityEngine;

public class UISnapPoint : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public PlayerJournal journal;
    public bool occupied;
    public string Word {
        set
        {
            word = value;
            wordText.text = word;
        }
    }
    [SerializeField]
    private string word;
    public  string symbol;
    public RectTransform rect => (RectTransform)transform;
    private void Awake()
    {
      //  wordText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        var temp = GetComponentInParent<JournalDiscoverWords>();
        journal = temp.playerJournal;
    }
    public void Occupy(string symb)
    {
        if(occupied) { return; }
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
