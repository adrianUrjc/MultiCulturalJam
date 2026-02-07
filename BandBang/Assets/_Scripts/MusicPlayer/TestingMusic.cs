using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMusic : MonoBehaviour
{
    [SerializeField]
    List<NPCs> m_NPCsToPlayAtStart;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Play()
    {
        NPCMusicPlayer.PlayNPCs(m_NPCsToPlayAtStart.ToArray());
    }
    public void Stop()
    {
        NPCMusicPlayer.Stop();
    }
}
