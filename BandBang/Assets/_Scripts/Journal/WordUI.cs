using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordUI : MonoBehaviour
{
    [SerializeField, ReadOnly]
    public
 string word;
 [SerializeField]
 TextMeshProUGUI wordText;
 public void Start()
    {
        wordText=GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetWord(string newWord)
    {
        word = newWord;
        BuildWord();
        //actualizar el texto del UI
    }
    private void BuildWord()//cuando journal ui tenga que construir todas las palabras se llama a este metodo
    {
        wordText.text = word;
        
    }

}
