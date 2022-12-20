using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showMenuButtonLeft;
    public InputActionProperty showMenuButtonRight;
    public Transform head;
    public float spawnDistance = 2;

    public Slider audioSlider;
    public AudioListener audioListener;

    public TextMeshProUGUI heightText;
    public XROrigin xrOrigin;
    public ToggleMoveAndRotation turnProviders;


    public void ChangeVolume(Slider slider)
    {
        
    }
    public void ChangeTurnProvider(TMP_Dropdown dropDown)
    {
        Debug.Log(dropDown.value);
        turnProviders.ChangeTurnProvider(dropDown.value);
    }
    public void IncreaseHeight()
    {
        xrOrigin.CameraYOffset += 0.05f;
        heightText.text = xrOrigin.CameraYOffset.ToString();
    }
    public void DecreaseHeight()
    {
        xrOrigin.CameraYOffset -= 0.05f;
        heightText.text = xrOrigin.CameraYOffset.ToString();
    }

    private void Start()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        if (showMenuButtonLeft.action.WasPressedThisFrame() || showMenuButtonRight.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            //menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;

    }
}
