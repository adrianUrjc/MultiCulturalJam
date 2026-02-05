using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
   //clase que maneja el desplazamiento de las paginas
   [SerializeField] int pageIdx = 0;//las paginas van de dos en dos, entonces el 0 es para las paginas 1 y 2, el 1 para las paginas 3 y 4, etc
   [SerializeField] GameObject pageContainer;
   [SerializeField] List<GameObject> pages;
   [SerializeField] Button nextButton;
   [SerializeField] Button prevButton;
    void Start()
    {
        foreach (Transform child in pageContainer.transform)
        {
            pages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
     BuildJournal();
        
    }
    void BuildJournal()
    {
        //leer de mi actual saveslot las palabras conocidas y las que intenta averiguar para dejarlo montado
        //se podria leer para dejar marcado el pageIdx guardado
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        UpdatePages();
    }
    public void NextPage()
    {
        if (pageIdx < pages.Count/2 - 1)
        {
            pageIdx++;
            UpdatePages();
        }
    }
    public void PrevPage()
    {
        if (pageIdx > 0)
        {
            pageIdx--;
            UpdatePages();
        }
    }
    void UpdatePages()
    {
        //Paginas
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == pageIdx * 2 || i == pageIdx * 2 + 1)// caso 0, toma page[0] y page[1]
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
        //Botones
        prevButton.gameObject.SetActive(pageIdx > 0);
        nextButton.gameObject.SetActive(pageIdx < pages.Count/2 - 1);
    }
}
