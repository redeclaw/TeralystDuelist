using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip MenuMusicOriginal;
    public AudioClip MenuMusicEdited;
    public AudioSource MusicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer.clip = MenuMusicEdited;
        MusicPlayer.PlayOneShot(MenuMusicOriginal);
        MusicPlayer.PlayScheduled(AudioSettings.dspTime + MenuMusicOriginal.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
