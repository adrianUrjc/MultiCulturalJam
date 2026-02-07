using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyIfNotRecruited : MonoBehaviour
{
    public NPCRecruiter recruiter;
    public NPCs npc;
    private void Start()
    {

        DelayedActions.Do(() =>
        {

            if (!recruiter.npcsRecruited.Contains(npc)){
                Destroy(gameObject);

            }
        }
        
        , 0.20f,this);
    }
}
