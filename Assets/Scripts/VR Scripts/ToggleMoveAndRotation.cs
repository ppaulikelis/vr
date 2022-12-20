using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleMoveAndRotation : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider _moveProvider;
    [SerializeField] private ActionBasedContinuousTurnProvider _continuousTurnProvider;
    [SerializeField] private ActionBasedSnapTurnProvider _snapTurnProvider;
    [SerializeField] private InputActionProperty _leftHandMoveAction;
    [SerializeField] private InputActionProperty _rightHandMoveAction;

    private LocomotionProvider _turnProvider;

    private void Start()
    {
        if(_continuousTurnProvider.enabled && _snapTurnProvider.enabled)
        {
            throw new System.Exception("Either (not both) continuous or snap provider should be used.");
        }
        if (_continuousTurnProvider.enabled)
            _turnProvider = _continuousTurnProvider;
        else if(_snapTurnProvider.enabled)
            _turnProvider = _snapTurnProvider;
    }

    public void ChangeTurnProvider(int index)
    {
        switch(index)
        {
            case 0:
                _continuousTurnProvider.enabled = true;
                _turnProvider = _continuousTurnProvider;
                _snapTurnProvider.enabled = false;
                break;
            case 1:
                _snapTurnProvider.enabled = true;
                _turnProvider = _snapTurnProvider;
                _continuousTurnProvider.enabled = false;
                break;
            default:
                _continuousTurnProvider.enabled = true;
                _turnProvider = _continuousTurnProvider;
                _snapTurnProvider.enabled = false;
                break;
        }
    }

    private void Update()
    {
        var leftHandValue = _leftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var rightHandValue = _rightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var inputValue = leftHandValue + rightHandValue;

        if (inputValue == Vector2.zero)
            return;

        if (Mathf.Abs(inputValue.x) - Mathf.Abs(inputValue.y) > 0) // x is bigger
        {
            _turnProvider.enabled = true;
            _moveProvider.enabled = false;
        }
        else
        {
            _turnProvider.enabled = false;
            _moveProvider.enabled = true;
        }
    }
}
