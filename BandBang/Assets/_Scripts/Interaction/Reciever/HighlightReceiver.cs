using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightReceiver : MonoBehaviour
{
   

    [SerializeField] private float scaleMultiplier = 1.2f; // cuánto escalar
    [SerializeField] private float scaleDuration = 0.2f; // tiempo de la animación

    private Vector3 originalScale;

    void Start()
    {

     

        originalScale = transform.localScale; // guardamos la escala original
    }

    public void Highlight()
    {
       

        // Escalar con LeanTween
        LeanTween.scale(gameObject, originalScale * scaleMultiplier, scaleDuration).setEaseOutBack();
    }

    public void UnHighlight()
    {
     

        // Volver a la escala original
        LeanTween.scale(gameObject, originalScale, scaleDuration).setEaseInOutBack();
    }
}
