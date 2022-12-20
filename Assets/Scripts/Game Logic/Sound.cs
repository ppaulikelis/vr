using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public SoundFile Name;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;
    public bool loop;
    [Range(0f, 1f)]
    public float SpatialBlend;
    public GameObject Origin;
    [HideInInspector]
    public AudioSource source;
}

public enum SoundFile
{
    //BadGuy
    BadGuy_Task1_Intro,
    BadGuy_Task1_Task,
    BadGuy_Task1_Answer,
    BadGuy_Task2_Task,
    BadGuy_Task2_IncorrectAnswer,
    BadGuy_Task2_CorrectAnswer,
    BadGuy_Task3_Task,
    BadGuy_Task4_Task,
    BadGuy_Task4_CorrectAnswer,
    BadGuy_Task4_IncorrectAnswer,
    BadGuy_Task5_Task,
    //Ambient
    Ambient_Scary
}
