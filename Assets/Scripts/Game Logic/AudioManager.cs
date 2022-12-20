using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public Sound[] Sounds;
    public static event Action<SoundFile> OnSoundEndPlaying;

    private void Awake()
    {
        foreach(Sound s in Sounds)
        {
            s.source = s.Origin.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.SpatialBlend;
        }
    }

    private void Start()
    {
        Play(SoundFile.Ambient_Scary, 0);
    }

    public void Play(SoundFile name, float delayAfter)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name.Equals(name));
        if (s == null) return;
        Debug.Log("Playing");
        s.source.Play();
        StartCoroutine(HandleSoundEndPlaying(s.Name, ((2 - s.Pitch) * s.source.clip.length) + delayAfter));
    }

    private IEnumerator HandleSoundEndPlaying(SoundFile name, float time)
    {
        yield return new WaitForSeconds(time);
        OnSoundEndPlaying?.Invoke(name);
    }
}
