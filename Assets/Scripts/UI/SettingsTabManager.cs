using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SettingsTabType
{
    AUDIO_TAB,
    VIDEO_TAB,
    OTHERS_TAB
}

public class SettingsTabManager : MonoBehaviourSingleton<SettingsTabManager>
{
    //[SerializeField] private GameObject tabsContainer;
    private Dictionary<SettingsTabType, SettingsTabController> settingsTabControllers;
    private SettingsTabType lastActiveTabType = SettingsTabType.AUDIO_TAB;

    private void Awake()
    {
        settingsTabControllers = GetComponentsInChildren<SettingsTabController>(true).ToDictionary(controller => controller.tabType, controller => controller);
    }

    public void SwitchTabs(SettingsTabType type)
    {
        ChangeActiveTab(lastActiveTabType, false);
        ChangeActiveTab(type, true);
        lastActiveTabType = type;
    }

    private void ChangeActiveTab(SettingsTabType type, bool isActive)
    {
        SettingsTabController controller = settingsTabControllers[type];
        if (controller != null)
        {
            controller.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.Log("The tab " + type + " was not found");
        }
    }
}
