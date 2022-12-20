using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [SerializeField] private List<ButtonSpring> _buttonList;
    [SerializeField] private List<string> _buttonAction;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _rightCombo = "0314";
    [SerializeField] private List<Transform> _doors;

    private bool isOpened = false;

    private void Update()
    {
        for (int i = 0; i < _buttonList.Count; i++)
        {
            if (_buttonList[i].IsPressed)
            {
                switch (_buttonAction[i])
                {
                    case "x":
                        _text.text = string.Empty;
                        break;
                    case "o":
                        IsRightCombo(_text.text);
                        break;
                    default:
                        _text.text += int.Parse(_buttonAction[i]); 
                        if(_text.text.Length > 4)
                        {
                            _text.text = _text.text.Remove(0, 1);
                        }
                        break;
                }
            }

        }
    }

    public void IsRightCombo(string combo) 
    {
        bool isCorrect = combo.Equals(_rightCombo);

        //if(GameManager.Instance.GameState.Equals(GameState.TASK_5_ESCAPE))
        //{
            if (isCorrect && !isOpened)
            {
                isOpened = true;
                foreach (var door in _doors)
                {
                    door.Rotate(door.up, 70 + UnityEngine.Random.Range(0, 10));
                }
            }
        //}
        
        if(!isCorrect)
        {
            Player.Instance.TakeDamage();
        }

        Debug.Log(isCorrect);
    }
}
