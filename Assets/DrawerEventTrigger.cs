using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DrawerEventTrigger : MonoBehaviour
{
    [SerializeField] private SoundFile _triggerOnSoundFile;
    [SerializeField] private string _correctObjectTag = null;
    [SerializeField] private Transform _door;
    [SerializeField] private float _closedThreshold = 0.1f;
    [SerializeField] private string[] _ignoredTags;

    [SerializeField] public UnityEvent _correctAction;
    [SerializeField] public UnityEvent _wrongAction;

    private readonly Vector3 _closedDoorPosition = new Vector3(-0.9526301f, 0.6479968f, 0.01376276f);
    private readonly Vector3 _closedDoorRotation = Vector3.zero;

    private GameManager _gameManager;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        AudioManager.OnSoundEndPlaying += AudioManagerOnOnSoundEndPlaying;
    }

    private void AudioManagerOnOnSoundEndPlaying(SoundFile name)
    {
        if (!name.Equals(_triggerOnSoundFile))
            return;

        _collider.enabled = name.Equals(_triggerOnSoundFile);
        _meshRenderer.enabled = name.Equals(_triggerOnSoundFile);

        AudioManager.OnSoundEndPlaying -= AudioManagerOnOnSoundEndPlaying;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _collider = gameObject.GetComponent<Collider>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();

        _collider.enabled = false;
        _meshRenderer.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_ignoredTags.Contains(other.tag))
            return;

        if (Vector3.Distance(_door.localPosition, _closedDoorPosition) < _closedThreshold &&
            Vector3.Distance(_door.localRotation.eulerAngles, _closedDoorRotation) < _closedThreshold)
        {
            if (other.CompareTag(_correctObjectTag))
            {
                // correct answer
                Debug.Log("Correct answer");
                _correctAction.Invoke();
            }
            else
            {
                // incorrect answer
                Debug.Log("Wrong answer");
                _wrongAction.Invoke();
            }

            _door.GetComponent<Rigidbody>().isKinematic = true;
            _door.GetComponent<XRHingedInteractable>().enabled = false;

            this.gameObject.SetActive(false);
        }
    }
}
