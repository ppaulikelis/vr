using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneManager : MonoBehaviour
{
    public AudioSource BadGuySpeech;
    void Start()
    {
        StartCoroutine(DeathSceneLogic());
    }

    IEnumerator DeathSceneLogic()
    {

        yield return new WaitForSeconds(2);
        BadGuySpeech.Play();
        yield return new WaitForSeconds(9);
        SceneManager.LoadScene("Game");
    }
}
