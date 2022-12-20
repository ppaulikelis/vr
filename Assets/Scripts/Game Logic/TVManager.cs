using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVManager : MonoBehaviourSingleton<TVManager>
{
    public GameObject TalkingScreen;
    public GameObject[] TaskScreens;

    private void Awake()
    {
        DisableAllScreens();
    }

    public void ShowTalkingScreen()
    {
        DisableAllScreens();
        TalkingScreen.SetActive(true);
    }

    public void ShowTaskScreen(int taskNr)
    {
        DisableAllScreens();
        TaskScreens[taskNr].SetActive(true);
    }

    public void DisableAllScreens()
    {
        TalkingScreen.SetActive(false);
        foreach (GameObject task in TaskScreens)
        {
            task.SetActive(false);
        }
    }
}
