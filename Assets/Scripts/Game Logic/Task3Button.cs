using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Button : MonoBehaviour
{
    public int Id;
    public Color EnabledColor;
    public Color DisabledColor;
    private Renderer Renderer;
    public AudioClip ButtonSound;
    private AudioSource AudioSource;
    public bool canPlaySoundAgain = true;
    public static event Action<int> OnButtonPress;

    private void Awake()
    {
        Renderer = gameObject.GetComponent<Renderer>();
        Renderer.material.color = DisabledColor;
        AudioSource = gameObject.GetComponent<AudioSource>();
        AudioSource.clip = ButtonSound;
    }

    public void EnableButton()
    {
        Renderer.material.color = EnabledColor;
        if(canPlaySoundAgain)
        {
            AudioSource.Play();
            canPlaySoundAgain = false;
            OnButtonPress?.Invoke(Id);
        }
    }

    public void DisableButton()
    {
        Renderer.material.color = DisabledColor;
        canPlaySoundAgain = true;
    }
}
