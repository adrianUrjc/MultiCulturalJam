using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NPCMusicPlayer.PlayNPCs(NPCs.a);
    }
    public void Play()
    {
        NPCMusicPlayer.PlayNPCs(NPCs.a);
    }
}
