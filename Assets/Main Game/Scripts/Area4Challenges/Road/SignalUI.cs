using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Road
{
    public class SignalUI : MonoBehaviour
    {
        [SerializeField] private Image _signalImage;
        [SerializeField] private Sprite _circle;
        [SerializeField] private Sprite _square;
        [SerializeField] private Sprite _triangle;
        [SerializeField] private Button[] _buttons;
        
        public void ShowSignal(RoadChallenge.Signal type)
        {
            switch (type)
            {
                case RoadChallenge.Signal.Circle:
                    _signalImage.sprite = _circle;
                    break;
                case RoadChallenge.Signal.Square:
                    _signalImage.sprite = _square;
                    break;
                case RoadChallenge.Signal.Triangle:
                    _signalImage.sprite = _triangle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            gameObject.SetActive(true);
            EnableButtons(true);
        }


        public void EnableButtons(bool enable)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {

                _buttons[i].interactable = enable;
            }
        }
    }
}
