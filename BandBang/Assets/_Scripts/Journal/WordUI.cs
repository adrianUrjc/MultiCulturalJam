using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordUI : MonoBehaviour
{
    [ReadOnly]
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
        //actualizar el texto del UI
    }
    public void BuildWord()//cuando journal ui tenga que construir todas las palabras se llama a este metodo
    {
        wordText.text = word;
        
    }

}
