using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviourSingleton<Player>
{
    public int Health = 3;
    public static event Action<int> OnPlayerTakeDamage;
    public Image DamageOverlay;
    public float Duration;
    public float FadeSpeed;
    private float DurationTimer;
    public AudioSource DeathSound;
    public List<GameObject> PoisonCapsules;

    private void Awake()
    {
        DamageOverlay.color = new Color(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, 0);
    }

    private void Update()
    {
        if(DamageOverlay.color.a > 0)
        {
            DurationTimer += Time.deltaTime;
            if(DurationTimer > Duration)
            {
                float tempAlpha = DamageOverlay.color.a;
                tempAlpha -= Time.deltaTime * FadeSpeed;
                DamageOverlay.color = new Color(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, tempAlpha);
            }
        }
    }

    public void TakeDamage()
    {
        Health--;
        PoisonCapsules[Health].gameObject.SetActive(false);

        if (Health <= 0)
        {
            StartCoroutine(LoadSceneAfterSeconds(3f));
        }
        DeathSound.Play();
        DurationTimer = 0;
        DamageOverlay.color = new Color(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, 1);
        OnPlayerTakeDamage?.Invoke(Health);
    }

    private IEnumerator LoadSceneAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("DeathScene");
    }
}
