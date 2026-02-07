using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRecruiter : MonoBehaviour
{
    [SerializeField]
    List<NPCs> npcsRecruited=new();
   public void RecruitNPC(NPCs npc)
    {
        npcsRecruited.Add(npc);
    }
    public void RecruitNPC(int npc)
    {
        RecruitNPC((NPCs)npc);
    }
}
