using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SymbolDiscoveredAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string beginingWord = "Symbol discovered: ";

    [SerializeField]
    PlayerJournal player;
    [SerializeField]
    Queue<string> discoveredSymbols = new();
    [SerializeField]
    float scale = 0.5f;
    [SerializeField]
    float duration = 0.1f;
    LTDescr popupId;
    Vector3 originalScale;
    CanvasGroup canvasGroup;
    void Start()

    {
        originalScale = gameObject.transform.localScale;
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        //suscribirse al evento
        if (player != null)
            player.OnNewDiscoveredSymbol.AddListener(EnqueueSymbols);
    }

    public void EnqueueSymbols(string newSymbol)
    {

        discoveredSymbols.Enqueue(beginingWord + newSymbol);
        if (popupId == null)
        {
            PopUpText();
        }

    }
    void CheckQueue()
    {
        if (discoveredSymbols.Count > 0)
        {
            if (popupId == null)
            {
                PopUpText();
            }
        }
    }
    void PopUpText()
    {
        text.text = discoveredSymbols.Dequeue();
        popupId = LeanTween.scale(gameObject, new Vector3(scale, scale), duration).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(canvasGroup, 1f, duration).setOnComplete(() =>
            {
                text.text="";
                canvasGroup.alpha = 1f; 
                gameObject.transform.localScale=originalScale;
                popupId = null;
                CheckQueue();
            });

        });



    }
}
