using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum NPCs
{
    Drummer,
    Saxophonist,
    Fiddler,
    Guitarist,
    Sitarist,
    ShehnaiPlayer,
}

public class NPCMusicPlayer
{


    public static Dictionary<NPCs, AudioClip> npcMusicDict;
    public static AudioMixer NPCAudioMixer;
    static NPCMusicPlayerInScene nPCMusicPlayerInScene;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        npcMusicDict = new Dictionary<NPCs, AudioClip>();
        NPCMusicDB db = Resources.Load<NPCMusicDB>("Music/NPCMusicDB");
        foreach (var entry in db.npcMusicEntries)
        {
            npcMusicDict[entry.npc] = entry.musicClip;
        }
        NPCAudioMixer = db.NPCAudioMixer;
    }

    public static void PlayNPCs(params NPCs[] npc)
    {
        CheckMusicPlayerInScene();
        nPCMusicPlayerInScene.PlayNPCs(npc);
    }
    public static void Stop()
    {
        CheckMusicPlayerInScene();
        GameObject.Destroy(nPCMusicPlayerInScene.gameObject);
    }

    private static void CheckMusicPlayerInScene()
    {
        if (nPCMusicPlayerInScene == null)
        {
            GameObject go = new GameObject("DelayedActions");

            nPCMusicPlayerInScene = go.AddComponent<NPCMusicPlayerInScene>();
        }
    }

}

public class NPCMusicPlayerInScene : MonoBehaviour
{

    public void PlayNPCs(params NPCs[] npc)
    {
        //destruir antiguos hijos
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var n in npc)
        {
            GameObject go = new GameObject("NPCMusic_" + n.ToString());
            go.transform.parent = transform;
            var player = go.AddComponent<AudioSource>();
            player.clip = NPCMusicPlayer.npcMusicDict[n];
            player.outputAudioMixerGroup = NPCMusicPlayer.NPCAudioMixer.FindMatchingGroups("Master")[0];
            player.loop = true;
            player.Play();

        }

    }
}
