using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Random = UnityEngine.Random;

public class TheDrunk : MonoBehaviour
{
    enum Direction { right = 0, left = 1 };

    [SerializeField]
    private TMP_Text _timerText;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _velocityAcceleration;

    [SerializeField]
    private float _initVelocity;

    [SerializeField]
    private UnityEvent _onWinning;
    [SerializeField]
    private UnityEvent _onLosing;

    [SerializeField] private Animator _animator;

    private bool _startGame;
    private float _roundTime;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }


    void Update()
    {
        if (!_startGame)
            return;


        _roundTime -= Time.deltaTime;
        if (_roundTime <= 0)
            _onWinning.Invoke();

        if (Input.mousePosition.x < Screen.width / 2.0f)
            _rigidbody.angularVelocity += _velocityAcceleration;
        else if (Input.mousePosition.x > Screen.width / 2.0f)
            _rigidbody.angularVelocity -= _velocityAcceleration;

        if (Vector3.Dot(transform.up, Vector3.up) < 0.01f)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.angularVelocity = 0;
            transform.position = _startPosition;
            _startGame = false;
            _onLosing.Invoke();
        }

        DisplayTimer();
    }

    public void Reset()
    {
        transform.localEulerAngles = Vector3.zero;

        _rigidbody.isKinematic = false;

        Direction _fallingDirection = (Direction)Random.Range(0, 2);

        if(_fallingDirection == Direction.left)
            _rigidbody.angularVelocity = _initVelocity;
        else
            _rigidbody.angularVelocity = -_initVelocity;

        _startGame = false;

        _animator.enabled = true;
        
        transform.rotation = Quaternion.identity;
        
    }

    public void StartGame(float roundTime)
    {
        _roundTime = roundTime;
        _startGame = true;
    }

    private void DisplayTimer()
    {
        _timerText.gameObject.SetActive(_startGame);

        float minutes = Mathf.FloorToInt(_roundTime / 60);
        float seconds = Mathf.FloorToInt(_roundTime % 60);

        _timerText.text = minutes + ":" + seconds;
    }

    public void DisableRB()
    {
        _startGame = false;
        //_rigidbody.isKinematic = true;
        //_rigidbody.angularVelocity = 0;
        Destroy(_rigidbody);
        transform.rotation = Quaternion.identity;
        transform.position = _startPosition;
    }
}
