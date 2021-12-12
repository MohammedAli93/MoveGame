using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Road
{
    public class RoadChallenge : Challenge
    {
        
        public enum Signal
        {
            Circle,
            Square,
            Triangle
        }

        private Signal _currentSignal;
        [SerializeField] private FollowWP _player;
        [SerializeField] private SignalUI _signalUI;
        [SerializeField] private Range _timeGap;

        private bool _firstTime = true;
        
        public override void StartChallenge()
        {
            _player.StartMovement();
            _firstTime = true;
            ShowSignal();
        }

        public override void ResetChallenge()
        {
            CancelInvoke("ShowSignal");
            _player.StopMovement();
            StartChallenge();
        }

        public void SelectCircle()
        {
            _signalUI.gameObject.SetActive(false);
            if (_currentSignal == Signal.Circle)
            {
                _player.Walk();
                Invoke("ShowSignal",_timeGap.Value);
            }
            else
            {
                FailChallengeWithDelay();
            }
        }
        
        public void SelectSquare()
        {
            _signalUI.gameObject.SetActive(false);
            if (_currentSignal == Signal.Square)
            {
                _player.Stop();
                Invoke("ShowSignal",_timeGap.Value);
            }
            else
            {
                FailChallengeWithDelay();
            }
        }

        public void SelectTriangle()
        {
            _signalUI.gameObject.SetActive(false);
            if (_currentSignal == Signal.Triangle)
            {
                _player.Run();
                Invoke("ShowSignal",_timeGap.Value);
            }
            else
            {
                FailChallengeWithDelay();
            }
        }

        public void ShowSignal()
        {
            _player.Stop();
            
            if (_firstTime)
            {
                _currentSignal = Signal.Circle;
                _firstTime = false;
            }
            else
            {
                Signal signal = 0;
                do
                {
                    signal = (Signal)Random.Range(0, 3);
                } while (signal == _currentSignal);

                _currentSignal = signal;
                
            }
            
            _signalUI.ShowSignal(_currentSignal);
        }
        public void OnChallengeFinished()
        {
            CancelInvoke("ShowSignal");
        }
    }
    

    [System.Serializable]
    public class Range
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public float Value
        {
            get => Random.Range(_min, _max);
        }
    }
}