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

    [SerializeField]
    private GameObject _rightWindGO;
    [SerializeField]
    private GameObject _leftWindGO;

    [SerializeField]
    private float _startDelay = 10;

    private bool _startGame;
    private float _roundTime;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    IEnumerator AddWindForce()
    {
        yield return new WaitForSeconds(_startDelay);

        _startGame = true;

        Direction _fallingDirection = (Direction)Random.Range(0, 2);

        if (_fallingDirection == Direction.left)
        {
            _leftWindGO.SetActive(true);
            _rigidbody.angularVelocity = _initVelocity;
        }
        else
        {
            _rightWindGO.SetActive(true);
            _rigidbody.angularVelocity = -_initVelocity;
        }

        yield return new WaitForSeconds(1f);
        _leftWindGO.SetActive(false);
        _rightWindGO.SetActive(false);

        yield return new WaitForSeconds(_startDelay);

        yield return StartCoroutine(AddWindForce());
    }

    void Update()
    {
        if (!_startGame)
            return;


        _roundTime -= Time.deltaTime;
        if (_roundTime <= 0)
            _onWinning.Invoke();

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2.0f)
                _rigidbody.angularVelocity = _initVelocity;
            else if (Input.mousePosition.x > Screen.width / 2.0f)
                _rigidbody.angularVelocity =- _initVelocity;
        }

        if (Vector3.Dot(transform.up, Vector3.up) < 0.01f)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.angularVelocity = 0;
            transform.position = _startPosition;
            _startGame = false;
            _leftWindGO.SetActive(false);
            _rightWindGO.SetActive(false);
            _onLosing.Invoke();
        }

        DisplayTimer();
    }

    public void Reset()
    {
        transform.localEulerAngles = Vector3.zero;

        _rigidbody.isKinematic = false;

        StopAllCoroutines();

        _startGame = false;

        _animator.enabled = true;
        
        transform.rotation = Quaternion.identity;

        _leftWindGO.SetActive(false);
        _rightWindGO.SetActive(false);

    }

    public void StartGame(float roundTime)
    {
        _roundTime = roundTime;
        StartCoroutine(AddWindForce());
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
        StopAllCoroutines();
        _leftWindGO.SetActive(false);
        _rightWindGO.SetActive(false);
    }
}
