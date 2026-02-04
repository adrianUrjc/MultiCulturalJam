using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Runtime.Core;
using static DialogSystem.Runtime.Core.DialogManager;

public class DialogUITest : MonoBehaviour

{
    [SerializeField] private
    DialogGraphModel dialogGraph;
    [ContextMenu("Play Dialog")]
   public void PlayDialog()
    {
        DialogManager.Instance.PlayDialogByDialogGraph(dialogGraph.dialogGraph);
    }
}
