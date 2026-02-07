using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRecruiter : MonoBehaviour
{
    [SerializeField]
   public List<NPCs> npcsRecruited=new();
   public void RecruitNPC(NPCs npc)
    {
        if (npcsRecruited.Contains(npc))
        {
            Debug.LogWarning("NPC " + npc.ToString() + " is already recruited. Skipping.");
            return;
        }
        npcsRecruited.Add(npc);
    }
    public void RecruitNPC(int npc)
    {
       
        RecruitNPC((NPCs)npc);
    }
    public void PlayNPCMusic()
    {
        NPCMusicPlayer.PlayNPCs(npcsRecruited.ToArray());
    }
    public void StopMusic()
    {
        NPCMusicPlayer.Stop();
    }
}
