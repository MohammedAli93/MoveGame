using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maze
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed;
        [SerializeField] private UnityEvent _onReachedDestination;

        private Rigidbody2D _rigidbody;
        private Animator _animator;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");
            if (direction.sqrMagnitude > 0.01f)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * (_playerSpeed * Time.fixedDeltaTime));
                _animator.SetFloat("Horizontal",direction.x);
                _animator.SetFloat("Vertical",direction.y);
                _animator.SetBool("Moving", true);
                
            }
            else
            {
                _animator.SetBool("Moving", false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Finish"))
            {
                _onReachedDestination.Invoke();
                this.enabled = false;
            }
        }
    }
}

