using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Road
{
    [RequireComponent(typeof(Animator))]
    public class FollowWP : MonoBehaviour
    {
        [SerializeField] private Transform[] _wayPoints;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private UnityEvent _onReachedDestination;
        private int _currentWP = 0;
        private float _speed;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartMovement()
        {
            transform.position = _wayPoints[0].position;
            _currentWP = 0;
            _speed = 0;
            
            StartCoroutine("MoveCoroutine");
        }

        public void StopMovement()
        {
            StopCoroutine("MoveCoroutine");
        }

        public void Run()
        {
            _speed = _runSpeed;
        }

        public void Walk()
        {
            _speed = _walkSpeed;
        }

        public void Stop()
        {
            _speed = 0;
        }

        IEnumerator MoveCoroutine()
        {
            var wait = new WaitForEndOfFrame();
            while (_currentWP < _wayPoints.Length-1)
            {
                Vector2 direction = _wayPoints[_currentWP + 1].position - transform.position;
                float distance = direction.magnitude;
                direction /= distance;
                transform.Translate(direction*_speed*Time.deltaTime);
                if (distance < 0.1f)
                {
                    _currentWP++;
                }
                if(!_animator)
                    _animator = GetComponent<Animator>();
                _animator.SetFloat("Horizontal", direction.x);
                _animator.SetFloat("Vertical", direction.y);
                _animator.SetBool("Moving",_speed>0.1f);
                yield return wait;
            }
            _animator.SetBool("Moving",false);
            _onReachedDestination.Invoke();

        }
        
    }
}
