using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRecruitMe : MonoBehaviour
{
    public NPCs npc;
    public void RecruitMe()
    {
        FindAnyObjectByType<NPCRecruiter>().RecruitNPC(npc);
    }
}
