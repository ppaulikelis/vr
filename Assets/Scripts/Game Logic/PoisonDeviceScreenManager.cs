using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoisonDeviceScreenManager : MonoBehaviour
{
    public Slider PoisonBarSlider;
    public TextMeshProUGUI PoisonStatusText;
    public Color NoPoisonStatus;
    public Color Level1PoisonStatus;
    public Color Level2PoisonStatus;
    public Color Level3PoisonStatus;
    void Awake()
    {
        PoisonBarSlider.value = 0;
        PoisonStatusText.text = "> Status: No Poison Found";
        PoisonStatusText.color = NoPoisonStatus;
        Player.OnPlayerTakeDamage += Player_OnPlayerTakeDamage;
    }

    private void OnDestroy()
    {
        Player.OnPlayerTakeDamage -= Player_OnPlayerTakeDamage;
    }

    private void Player_OnPlayerTakeDamage(int health)
    {
        SetPoisonBar(health);
    }

    public void SetPoisonBar(int health)
    {
        PoisonBarSlider.value = 3 - health;
        switch (health)
        {
            case 3:
                PoisonStatusText.text = "> Status: No Poison Found";
                PoisonStatusText.color = NoPoisonStatus;
                break;
            case 2:
                PoisonStatusText.text = "> Status: Poison Level 1";
                PoisonStatusText.color = Level1PoisonStatus;
                break;
            case 1:
                PoisonStatusText.text = "> Status: Poison Level 2";
                PoisonStatusText.color = Level2PoisonStatus;
                break;
            case 0:
                PoisonStatusText.text = "> Status: You are dead :)";
                PoisonStatusText.color = Level3PoisonStatus;
                break;
        }
    }
}
