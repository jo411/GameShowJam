using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public List<AudioClip> songs;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TryPlayNewSong", .2f, 20.0f);
    }

    void TryPlayNewSong()
    {
        if(!musicSource.isPlaying)
        {
            musicSource.clip = Extensions.GetRandomElement(songs);
            musicSource.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
