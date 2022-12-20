using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Player : MonoBehaviour
{
    public Task3Controller Controller;
    public int RoundCount = 3;
    public int RoundsPlayed = 0;
    public int SoundCount = 3;
    public List<int> SoundsSequence = new List<int>();

    private void Awake()
    {
        Task3Controller.OnTask3StateChanged += OnTask3StateChanged;
        Controller = GetComponent<Task3Controller>();
    }

    private void OnDestroy()
    {
        Task3Controller.OnTask3StateChanged -= OnTask3StateChanged;
    }

    private void OnTask3StateChanged(Task3State obj)
    {
        if (obj == Task3State.PlayingTask)
        {
            SoundsSequence.Clear();
            StartCoroutine(PlayTask());
        }
    }

    IEnumerator PlayTask()
    {
        Controller.DisableButtons();
        GenerateSequence();
        int numberOfSoundsPlayed = 0;
        while(numberOfSoundsPlayed < SoundCount)
        {
            Controller.Buttons[SoundsSequence[numberOfSoundsPlayed]].EnableButton();
            yield return new WaitForSeconds(2);
            Controller.Buttons[SoundsSequence[numberOfSoundsPlayed]].DisableButton();
            yield return new WaitForSeconds(1);
            numberOfSoundsPlayed++;
        }
        Controller.UpdateState(Task3State.ReadingInput);
    }

    public void GenerateSequence()
    {
        System.Random rnd = new System.Random();
        for(int i = 0; i < SoundCount; i++)
        {
            SoundsSequence.Add(rnd.Next(0, 6));
        }
    }
}
