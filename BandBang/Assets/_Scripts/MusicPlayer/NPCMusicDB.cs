using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class NPCMusicEntry
{
    public NPCs npc;
    public AudioClip musicClip;
}

[CreateAssetMenu(fileName = "NPCMusicDB", menuName = "ScriptableObjects/NPCMusicDB", order = 1)]
public class NPCMusicDB : ScriptableObject
{
    public AudioMixer NPCAudioMixer;
    [CustomLabel("")]
    public List<NPCMusicEntry> npcMusicEntries;

}