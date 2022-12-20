using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Controller : MonoBehaviour
{
    public bool IsActive = false;
    public GameObject GreenButton;
    public GameObject RedButton;

    private void Awake()
    {
        AudioManager.OnSoundEndPlaying += AudioManagerOnSoundEndPlaying;
    }

    private void OnDestroy()
    {
        AudioManager.OnSoundEndPlaying -= AudioManagerOnSoundEndPlaying;
    }

    private void Update()
    {
        if (IsActive && GameManager.Instance.GameState.Equals(GameState.TASK_1_BUTTON_CHOICE))
        {
            if(GreenButton.transform.localPosition.y <= 0.8 || RedButton.transform.localPosition.y <= 0.8)
            {
                IsActive = false;
                BadGuyManager.Instance.HandleTask1Answer();
            }
        }
    }

    private void AudioManagerOnSoundEndPlaying(SoundFile name)
    {
        if (name.Equals(SoundFile.BadGuy_Task1_Task))
        {
            IsActive = true;
            AudioManager.OnSoundEndPlaying -= AudioManagerOnSoundEndPlaying;
        }
    }
}
