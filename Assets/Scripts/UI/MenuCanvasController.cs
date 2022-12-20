using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    public MenuCanvasType canvasType;
    public void GoBack()
    {
        MenuCanvasManager.Instance.SwitchToPreviousCanvas();
    }
}
