using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MenuCanvasController
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettingsMenu()
    {
        MenuCanvasManager.Instance.SwitchCanvas(MenuCanvasType.SETTINGS_MENU);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
