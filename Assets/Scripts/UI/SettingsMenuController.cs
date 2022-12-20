using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MenuCanvasController
{
    public void OpenAudioTab()
    {
        SettingsTabManager.Instance.SwitchTabs(SettingsTabType.AUDIO_TAB);
    }

    public void OpenVideoTab()
    {
        SettingsTabManager.Instance.SwitchTabs(SettingsTabType.VIDEO_TAB);
    }

    public void OpenOthersTab()
    {
        SettingsTabManager.Instance.SwitchTabs(SettingsTabType.OTHERS_TAB);
    }
}
