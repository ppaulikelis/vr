using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public GameState GameState;
    public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        UpdateGameState(GameState.INTRO);
    }

    public void UpdateGameState(GameState newState)
    {
        this.GameState = newState;

        switch (newState)
        {
            case GameState.INTRO:
                HandleIntroGameState();
                break;
            case GameState.TASK_1_BUTTON_CHOICE:
                break;
            case GameState.TASK_2_OBJECT_CHOICE:
                break;
            case GameState.TASK_3_SEQUENCE_PLAYER:
                break;
            case GameState.TASK_4_OBJECT_CHOICE_FROM_ENVIRONMENT:
                break;
            case GameState.TASK_5_ESCAPE:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleIntroGameState()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);
        UpdateGameState(GameState.TASK_1_BUTTON_CHOICE);
    }
}

public enum GameState {
    INTRO,
    TASK_1_BUTTON_CHOICE,
    TASK_2_OBJECT_CHOICE,
    TASK_3_SEQUENCE_PLAYER,
    TASK_4_OBJECT_CHOICE_FROM_ENVIRONMENT,
    TASK_5_ESCAPE
}