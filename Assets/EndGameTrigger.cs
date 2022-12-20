using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] GameObject bag;
    [SerializeField] GameObject lightBar;
    [SerializeField] float heightAfterAnimation = 0.22f;
    [SerializeField] float speed = 0.05f;
    [SerializeField] AudioSource behindYou;
    [SerializeField] AudioSource scream;

    private float startHeight;
    private void Start()
    {
        startHeight = bag.transform.localPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bag.SetActive(true);
            behindYou.Play();
            StartCoroutine(AnimateBag(2f));
            StartCoroutine(ToggleSceneAfterSeconds(6f));
        }
    }

    private IEnumerator AnimateBag(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        scream.Play();
        while(bag.transform.localPosition.y > heightAfterAnimation)
        {
            bag.transform.position -= new Vector3(0, speed, 0);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        lightBar.SetActive(false);
    }

    private IEnumerator ToggleSceneAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("DeathScene");
    }
}
