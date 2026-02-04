using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogSystem.Runtime.Core;
using static DialogSystem.Runtime.Core.DialogManager;

public class DialogUIPlayer : MonoBehaviour

{
    [SerializeField] private
    DialogGraphModel dialogGraph;
    [ContextMenu("Play Dialog")]
   public void PlayDialog()
    {
        DialogManager.Instance.PlayDialogByDialogGraphModel(dialogGraph);
    }
}
