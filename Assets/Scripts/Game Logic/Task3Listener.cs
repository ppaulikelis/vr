using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Listener : MonoBehaviour
{
    public Task3Controller Controller;
    public Task3Player T3Player;
    public bool Listening = false;
    public int Presses = 0;

    private void Awake()
    {
        Task3Controller.OnTask3StateChanged += OnTask3StateChanged;
        Task3Button.OnButtonPress += OnButtonPress;
        Controller = GetComponent<Task3Controller>();
        T3Player = GetComponent<Task3Player>();
    }

    private void OnDestroy()
    {
        Task3Controller.OnTask3StateChanged -= OnTask3StateChanged;
        Task3Button.OnButtonPress -= OnButtonPress;
    }

    private void OnButtonPress(int obj)
    {
        if(Listening)
        {
            Debug.Log(obj);
            if (T3Player.SoundsSequence[Presses] == obj)
            {
                Presses++;
            }
            else
            {
                Listening = false;
                Player.Instance.TakeDamage();
                Controller.DisableButtons();
                StartCoroutine(ChangeStateAfterDelay(Task3State.PlayingTask));
            }
            if(Presses == T3Player.SoundCount)
            {
                Listening = false;
                Controller.TurnOnLight(T3Player.RoundsPlayed);
                T3Player.RoundsPlayed++;
                T3Player.SoundCount++;
                Controller.DisableButtons();
                if (T3Player.RoundsPlayed == T3Player.RoundCount)
                {
                    StartCoroutine(ChangeStateAfterDelay(Task3State.Neutral));
                }
                else StartCoroutine(ChangeStateAfterDelay(Task3State.PlayingTask));
            }
        }
    }

    private void OnTask3StateChanged(Task3State obj)
    {
        if(obj == Task3State.ReadingInput)
        {
            Controller.EnableButtons();
            Listening = true;
            Presses = 0;
        }
    }

    IEnumerator ChangeStateAfterDelay(Task3State state)
    {
        yield return new WaitForSeconds(3);
        Controller.UpdateState(state);
    }
}
