using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighterScript : MonoBehaviour
{
    [SerializeField]
    TaggedDetector2D detector;

    private GameObject lastTarget;
  
    private void UpdateTarget(GameObject go)
    {
        GameObject newTarget = go;
        
        if(lastTarget != null)
        {
           changeView(lastTarget, false);

        }
        if(newTarget != null)
        {
            changeView(newTarget, true);
        }

        lastTarget = newTarget;
        

    }
    private void changeView(GameObject go, bool highlighted)
    {
        if (highlighted)
        {

            go.GetComponentInChildren<HighlightReceiver>()?.Highlight();

        }
        else
        {
            go.GetComponentInChildren<HighlightReceiver>()?.UnHighlight();

        }
    }
    private void Start()
    {
        lastTarget = null;
        detector.onTargetChanged.AddListener(UpdateTarget);
    }
    private void OnDisable()
    {
        detector?.onTargetChanged.RemoveListener(UpdateTarget);
    }
}
