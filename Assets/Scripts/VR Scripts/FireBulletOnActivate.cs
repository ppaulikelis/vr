using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireSpeed = 20f;
    [SerializeField] private GameObject fakePlunger;

    public bool isReloaded = true;
    public bool isReset = true;

    private void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(FireBullet);
    }

    private void Update()
    {
        fakePlunger.SetActive(isReloaded);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (isReloaded && isReset)
        {
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.LookRotation(transform.forward));
            StartCoroutine(DisableCollisionForSeconds(0.05f, spawnedBullet.GetComponent<Collider>()));
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            isReloaded = false;
            isReset = false;
            Destroy(spawnedBullet, 5);
        }
    }

    IEnumerator DisableCollisionForSeconds(float seconds, Collider bulletCollider)
    {
        bulletCollider.enabled = false;
        yield return new WaitForSeconds(seconds);
        bulletCollider.enabled = true;
    }
}
