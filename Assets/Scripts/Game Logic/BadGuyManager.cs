using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyManager : MonoBehaviourSingleton<BadGuyManager>
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        AudioManager.OnSoundEndPlaying += AudioManagerOnSoundEndPlaying;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        AudioManager.OnSoundEndPlaying -= AudioManagerOnSoundEndPlaying;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.TASK_1_BUTTON_CHOICE:
                TVManager.Instance.ShowTalkingScreen();
                AudioManager.Instance.Play(SoundFile.BadGuy_Task1_Intro, 1);
                break;
            case GameState.TASK_2_OBJECT_CHOICE:
                TVManager.Instance.ShowTalkingScreen();
                AudioManager.Instance.Play(SoundFile.BadGuy_Task2_Task, 0);
                break;
            case GameState.TASK_3_SEQUENCE_PLAYER:
                TVManager.Instance.ShowTalkingScreen();
                AudioManager.Instance.Play(SoundFile.BadGuy_Task3_Task, 0.5f);
                break;
            case GameState.TASK_4_OBJECT_CHOICE_FROM_ENVIRONMENT:
                TVManager.Instance.ShowTalkingScreen();
                AudioManager.Instance.Play(SoundFile.BadGuy_Task4_Task, 0);
                break;
            case GameState.TASK_5_ESCAPE:
                TVManager.Instance.ShowTalkingScreen();
                AudioManager.Instance.Play(SoundFile.BadGuy_Task5_Task, 0);
                break;
        }
    }

    private void AudioManagerOnSoundEndPlaying(SoundFile name)
    {
        switch (name)
        {
            case SoundFile.BadGuy_Task1_Intro:
                AudioManager.Instance.Play(SoundFile.BadGuy_Task1_Task, 0);
                break;
            case SoundFile.BadGuy_Task1_Task:
                TVManager.Instance.ShowTaskScreen(0);
                break;
            case SoundFile.BadGuy_Task1_Answer:
                Player.Instance.TakeDamage();
                GameManager.Instance.UpdateGameState(GameState.TASK_2_OBJECT_CHOICE);
                break;
            case SoundFile.BadGuy_Task2_Task:
                TVManager.Instance.ShowTaskScreen(1);
                break;
            case SoundFile.BadGuy_Task2_CorrectAnswer:
                GameManager.Instance.UpdateGameState(GameState.TASK_3_SEQUENCE_PLAYER);
                break;
            case SoundFile.BadGuy_Task2_IncorrectAnswer:
                GameManager.Instance.UpdateGameState(GameState.TASK_3_SEQUENCE_PLAYER);
                break;
            case SoundFile.BadGuy_Task3_Task:
                TVManager.Instance.ShowTaskScreen(2);
                break;
            case SoundFile.BadGuy_Task4_Task:
                TVManager.Instance.ShowTaskScreen(3);
                break;
            case SoundFile.BadGuy_Task4_CorrectAnswer:
                GameManager.Instance.UpdateGameState(GameState.TASK_5_ESCAPE);
                break;
            case SoundFile.BadGuy_Task4_IncorrectAnswer:
                GameManager.Instance.UpdateGameState(GameState.TASK_5_ESCAPE);
                break;
        }
    }

    public void HandleTask1Answer()
    {
        TVManager.Instance.ShowTalkingScreen();
        AudioManager.Instance.Play(SoundFile.BadGuy_Task1_Answer, 3);
    }

    public void HandleTask2CorrectAnswer()
    {
        TVManager.Instance.ShowTalkingScreen();
        AudioManager.Instance.Play(SoundFile.BadGuy_Task2_CorrectAnswer, 3);
    }

    public void HandleTask2IncorrectAnswer()
    {
        TVManager.Instance.ShowTalkingScreen();
        Player.Instance.TakeDamage();
        AudioManager.Instance.Play(SoundFile.BadGuy_Task2_IncorrectAnswer, 3);
    }

    public void HandleTask4CorrectAnswer()
    {
        TVManager.Instance.ShowTalkingScreen();
        AudioManager.Instance.Play(SoundFile.BadGuy_Task4_CorrectAnswer, 3);
    }

    public void HandleTask4IncorrectAnswer()
    {
        TVManager.Instance.ShowTalkingScreen();
        Player.Instance.TakeDamage();
        AudioManager.Instance.Play(SoundFile.BadGuy_Task4_IncorrectAnswer, 3);
    }
}