using System.Collections;
using System.Collections.Generic;
using DialogSystem.Runtime.Core;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimator2D))]
    
public class NPCAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogManager.Instance.onDialogEnter+=BeginTalking;
        DialogManager.Instance.onDialogExit+=BeginIdle;
    }

   void BeginTalking()
    {
        GetComponent<SpriteAnimator2D>().SetAnimation(SpriteAnim.Speak);
    }
    void BeginIdle()
    {
        GetComponent<SpriteAnimator2D>().SetAnimation(SpriteAnim.Idle);
        
    }
    void OnDestroy()
    {
        if(DialogManager.Instance==null) return;
         DialogManager.Instance.onDialogEnter-=BeginTalking;
        DialogManager.Instance.onDialogExit-=BeginIdle;
    }
}
