using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRecruiter : MonoBehaviour
{
    [SerializeField]
    public
    List<NPCs> npcsRecruited = new();
    public void Start()
    {
        LoadNpcs();
    }
    public void LoadNpcs()
    {
        string npcs = GameManager.Instance.GetComponent<SaveSlot>()
            .loader.GetValue<string>("NPCsRecruited");

        string[] npcsArray = npcs.Split(',');

        foreach (var npc in npcsArray)
        {
            string trimmed = npc.Trim(); // MUY IMPORTANTE
            npcsRecruited.Add(StringToNpc(trimmed));
        }
    }

    public NPCs StringToNpc(string npc)
    {
        if (System.Enum.TryParse(npc, out NPCs result))
            return result;

        Debug.LogError($"NPC desconocido: {npc}");
        return default; // o lanza excepción si prefieres
    }

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
        GameManager.Instance.GetComponent<SaveSlot>().loader.SetValue<string>("NPCsRecruited", NpcsListToString());
    }
    public void PlayNPCMusic()
    {
        NPCMusicPlayer.PlayNPCs(npcsRecruited.ToArray());
    }
    public string NpcsListToString()
    {

        return string.Join(",", npcsRecruited);
    }
    public void StopMusic()
    {
        NPCMusicPlayer.Stop();
    }
}
