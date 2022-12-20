using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MenuCanvasType
{
    INITIAL_MENU,
    SETTINGS_MENU
}

public class MenuCanvasManager : MonoBehaviourSingleton<MenuCanvasManager>
{
    private Dictionary<MenuCanvasType, MenuCanvasController> menuCanvasControllers;
    private MenuCanvasType lastActiveCanvasType = MenuCanvasType.INITIAL_MENU;
    private Stack<MenuCanvasType> canvasHistory = new Stack<MenuCanvasType>();

    private void Awake()
    {
        menuCanvasControllers = GetComponentsInChildren<MenuCanvasController>(true).ToDictionary(controller => controller.canvasType, controller => controller);
    }

    public void SwitchCanvas(MenuCanvasType type)
    {
        if (!canvasHistory.Contains(lastActiveCanvasType))
        {
            canvasHistory.Push(lastActiveCanvasType);
        }
        ChangeCanvasState(lastActiveCanvasType, false);
        ChangeCanvasState(type, true);
        lastActiveCanvasType = type;
    }

    private void ChangeCanvasState(MenuCanvasType type, bool isActive)
    {
        MenuCanvasController controller = menuCanvasControllers[type];
        if (controller != null)
        {
            controller.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.Log("The menu " + type + " was not found");
        }
    }

    private void SwitchCanvasBack(MenuCanvasType type)
    {
        ChangeCanvasState(lastActiveCanvasType, false);
        ChangeCanvasState(type, true);
        lastActiveCanvasType = type;
    }

    public void SwitchToPreviousCanvas()
    {
        if (canvasHistory.Count == 0) return;
        MenuCanvasType previousCanvas = canvasHistory.Pop();
        SwitchCanvasBack(previousCanvas);
    }
}
