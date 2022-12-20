using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Controller : MonoBehaviour
{
    public GameObject ButtonsContainer;
    public GameObject LightsContainer;
    public List<Task3Button> Buttons = new List<Task3Button>();
    public Task3State State = Task3State.Neutral;
    public static event Action<Task3State> OnTask3StateChanged;
    public float ButtonInitialY;
    public List<GameObject> Lights = new List<GameObject>();
    public Color LightColor;

    private void Awake()
    {
        for(int i = 0; i < ButtonsContainer.transform.childCount; i++)
        {
            Buttons.Add(ButtonsContainer.transform.GetChild(i).gameObject.GetComponent<Task3Button>());
        }
        for (int i = 0; i < LightsContainer.transform.childCount; i++)
        {
            Lights.Add(LightsContainer.transform.GetChild(i).gameObject);
        }
        AudioManager.OnSoundEndPlaying += AudioManagerOnSoundEndPlaying;
    }

    private void Start()
    {
        ButtonInitialY = Buttons[0].transform.position.y;
    }

    private void OnDestroy()
    {
        AudioManager.OnSoundEndPlaying -= AudioManagerOnSoundEndPlaying;
    }

    private void Update()
    {
        foreach (Task3Button button in Buttons)
        {
            if (button.transform.localPosition.y <= 0.05)
            {
                button.EnableButton();
            }
            else
            {
                if(State != Task3State.PlayingTask) button.DisableButton();
            }
        }
    }

    private void AudioManagerOnSoundEndPlaying(SoundFile obj)
    {
        if(obj == SoundFile.BadGuy_Task3_Task)
        {
            UpdateState(Task3State.PlayingTask);
        }
    }



    public void UpdateState(Task3State newState)
    {
        State = newState;
        if(newState == Task3State.Neutral)
        {
            GameManager.Instance.UpdateGameState(GameState.TASK_4_OBJECT_CHOICE_FROM_ENVIRONMENT);
        }
        OnTask3StateChanged?.Invoke(newState);
    }

    public void DisableButtons()
    {
        foreach (Task3Button button in Buttons)
        {
            button.GetComponent<Rigidbody>().isKinematic = true;
            Vector3 previous = button.transform.position;
            button.transform.position = new Vector3(previous.x, ButtonInitialY, previous.z);
        }
    }

    public void EnableButtons()
    {
        foreach (Task3Button button in Buttons)
        {
            button.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void TurnOnLight(int index)
    {
        Lights[index].GetComponent<Renderer>().material.color = LightColor;
    }
}

public enum Task3State
{
    Neutral,
    PlayingTask,
    ReadingInput
}
