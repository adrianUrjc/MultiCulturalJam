using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightReceiver : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer objectRenderer;
    [SerializeField]
    private Color highlightColor = Color.yellow;
    private Color originalColor;
    void Start()
    {
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<SpriteRenderer>();
        }
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.color;
        }
    }
    public void Highlight()
    {
        objectRenderer.color = highlightColor;
    }
    public void UnHighlight()
    {
        objectRenderer.color = originalColor;
    }
}
