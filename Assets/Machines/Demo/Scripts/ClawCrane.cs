using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClawCrane : MonoBehaviour
{
    [SerializeField]
    private BallColor _TargetColor;

    [SerializeField]
    private int _maxNumberToLose;

    [SerializeField]
    private Transform _ballsParent;

    [SerializeField]
    private Transform claw;

    [SerializeField]
    private Transform clawExtention;

    [SerializeField]
    private Transform firstPosition;

    [SerializeField]
    private Transform secondPosition;

    [SerializeField]
    private Transform rewardHandPosition;

    [SerializeField]
    private Transform rewardTargetPosition;

    [SerializeField]
    private Transform leftClaw;

    [SerializeField]
    private Transform rightClaw;

    [SerializeField]
    private float clawGrabAngle = 30.0f;

    [SerializeField]
    private Lever lever;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float speedOverDepth = 10.0f;

    [SerializeField]
    private float rotationScaler = 3.0f;

    [SerializeField]
    private float grabDuration = 1.0f;

    [SerializeField]
    private float grabStopDuration = 0.5f;

    [SerializeField]
    private float grabShift = 50.0f;

    [SerializeField]
    private float returnSpeed = 0.6f;

    [SerializeField]
    private float fallSpeed = 10.0f;

    [SerializeField]
    private AnimationCurve scaleOverZ;

    [SerializeField]
    private ClawCraneButton moveDownButton;

    [SerializeField]
    private float clawRotationDuration = 0.1f;

    [SerializeField]
    private CanvasGroup moveTutorialGroup;

    [SerializeField]
    private CanvasGroup pressTutorialGroup;

    [SerializeField]
    private AudioSource clawMoveSound;

    [SerializeField]
    private AudioSource rewardFallSound;

    [SerializeField]
    private UnityEvent _onWinning;
    [SerializeField]
    private UnityEvent _onLosing;
    [SerializeField]
    private UnityEvent _onCollectingBall;

    [SerializeField]
    private Button _SubmitBotton;

    private float position = 0.0f;

    private float positionZ = 0.0f;

    private Coroutine grabCoroutine;

    private bool pressTutorialHasBeenShown = false;

    private bool movingBack = false;

    private bool _collectedOneBall = false; 

    private List<GameObject> _initBalls = new List<GameObject>();

    private Hand _machinehand;
    bool _backtoPosition = false;

    private int _numberOfLosingBalls;

    private void Start()
    {
        _machinehand = GetComponentInChildren<Hand>();

        _backtoPosition = false;
        moveDownButton.onClick
            .AddListener(() =>
                {
                    if (grabCoroutine != null)
                        return;

                    grabCoroutine = StartCoroutine(Grab());
                    pressTutorialGroup.alpha = 0.0f;

                    _machinehand.OnHittingBall.AddListener(() => { _backtoPosition = true; });
                });

        _SubmitBotton.onClick.AddListener(() => {
            if (_collectedOneBall)
                _onWinning.Invoke();
            else
                _onLosing.Invoke();
        });

        if (_initBalls.Count == 0)
        {
            for (int i = 0; i < _ballsParent.childCount; i++)
            {
                if (_ballsParent.GetChild(i).gameObject.activeSelf)
                {
                    _initBalls.Add(_ballsParent.GetChild(i).gameObject);
                }
            }
        }

        moveTutorialGroup.alpha = 1.0f;
        moveTutorialGroup.blocksRaycasts = false;

        pressTutorialGroup.alpha = 0.0f;
        pressTutorialGroup.blocksRaycasts = false;

        _numberOfLosingBalls = _maxNumberToLose;
        _collectedOneBall = false;

    }

    private void OnDisable()
    {
        grabCoroutine = null;
        position = 0.0f;
        positionZ = 0.0f;

        StopAllCoroutines();
    }

    private void Update()
    {
        var input = grabCoroutine == null ? lever.Input : Vector2.zero;
        var xShift = input.x * moveSpeed;

        clawMoveSound.volume = movingBack || input.magnitude > 0.0f ? 0.5f : 0.0f;

        position += xShift * Time.deltaTime;
        position = Mathf.Clamp01(position);

        var zShift = input.y * speedOverDepth;

        if (input.magnitude > 0.0f)
        {
            moveTutorialGroup.alpha = 0.0f;
            if (!pressTutorialHasBeenShown)
            {
                pressTutorialGroup.alpha = 1.0f;
                pressTutorialHasBeenShown = true;
            }
        }

        positionZ += -zShift * Time.deltaTime;
        positionZ = Mathf.Clamp01(positionZ);

        claw.transform.localScale = Vector3.one * scaleOverZ.Evaluate(positionZ);

        claw.transform.localRotation = Quaternion.Euler(0, 0, xShift * rotationScaler);

        claw.transform.position = Vector3.Lerp(firstPosition.position, secondPosition.position, (position - 0.5f) * scaleOverZ.Evaluate(positionZ) + 0.5f * scaleOverZ.Evaluate(positionZ));
    }

    private IEnumerator RotateClaws(float angle)
    {
        var ratio = 0.0f;
        var initialLeftRotation = leftClaw.localRotation;
        var initialRightRotation = rightClaw.localRotation;

        var targetLeftRotation = Quaternion.AngleAxis(angle, Vector3.forward) * leftClaw.localRotation;
        var targetRightRotation = Quaternion.AngleAxis(-angle, Vector3.forward) * rightClaw.localRotation;

        while (ratio < 1.0f)
        {
            ratio += Time.deltaTime / clawRotationDuration;

            leftClaw.localRotation = Quaternion.Slerp(initialLeftRotation, targetLeftRotation, ratio);
            rightClaw.localRotation = Quaternion.Slerp(initialRightRotation, targetRightRotation, ratio);

            yield return null;
        }
    }

    private IEnumerator Grab()
    {
        var ratio = 0.0f;
        var initialPosition = clawExtention.localPosition;
        var targetPosition = clawExtention.localPosition + Vector3.down * grabShift;
        while (!_backtoPosition)
        {
            ratio += Time.deltaTime / (grabDuration / 2.0f);

            targetPosition = clawExtention.localPosition + Vector3.down * 10;
            clawExtention.localPosition = Vector3.Lerp(clawExtention.localPosition, targetPosition, ratio);

            yield return null;
        }
        

        yield return new WaitForSeconds(grabStopDuration);


        _backtoPosition = false;
        if (_machinehand.LastInteractable)
        {
            _machinehand.LastInteractable.transform.SetParent(rewardHandPosition);
            _machinehand.LastInteractable.transform.localPosition = Vector3.zero;
            _machinehand.LastInteractable.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        yield return StartCoroutine(RotateClaws(clawGrabAngle));

        movingBack = true;

        while (ratio > 0.0)
        {
            ratio -= Time.deltaTime / (grabDuration / 2.0f);
            clawExtention.localPosition = Vector3.Lerp(initialPosition, targetPosition, ratio);

            yield return null;
        }

        while (position > 0.0f || positionZ > 0.0f)
        {
            position = Mathf.MoveTowards(position, 0.0f, Time.deltaTime * returnSpeed);
            positionZ = Mathf.MoveTowards(positionZ, 0.0f, Time.deltaTime * returnSpeed);

            yield return null;
        }

        yield return StartCoroutine(RotateClaws(-clawGrabAngle));

        print("2");


        if (_machinehand.LastInteractable)
        {
            var initialRewardPosition = _machinehand.LastInteractable.transform.position;

            _machinehand.LastInteractable.transform.SetParent(rewardTargetPosition);

            var rewardRatio = 0.0f;
            while (rewardRatio < 1.0f)
            {
                rewardRatio += Time.deltaTime * fallSpeed;

                _machinehand.LastInteractable.GetComponent<Rigidbody2D>().isKinematic = false;
                _machinehand.LastInteractable.transform.position = Vector3.Lerp(initialRewardPosition, rewardTargetPosition.position, rewardRatio);

                yield return null;
            }

            CheckBalls(_machinehand.LastInteractable.GetComponent<Ball>());
        }


        movingBack = false;

        grabCoroutine = null;

    }

    private void CheckBalls(Ball collectedBall)
    {
        _machinehand.LastInteractable = null;

        if (collectedBall.Color == _TargetColor)
        {
            _numberOfLosingBalls--;
            if (_numberOfLosingBalls <= 0)
                _onLosing.Invoke();
        }
        else
        {
            _onCollectingBall.Invoke();
            _collectedOneBall = true;
        }

        bool _winner = true;
        for (int i = 0; i < _initBalls.Count; i++)
        {
            if (_initBalls[i].gameObject.activeSelf)
            {
                if (_initBalls[i].GetComponent<Ball>().Color != _TargetColor)
                {
                    _winner = false;
                    break;
                }
            }
        }

        if (_winner)
            _onWinning.Invoke();
    }

    public void ResetMachine()
    {
        moveTutorialGroup.alpha = 1.0f;
        moveTutorialGroup.blocksRaycasts = false;

        pressTutorialGroup.alpha = 0.0f;
        pressTutorialGroup.blocksRaycasts = false;

        _numberOfLosingBalls = _maxNumberToLose;
        _collectedOneBall = false;

        for (int i=0;i<_initBalls.Count;i++)
        {
            if (!_initBalls[i].activeSelf)
            {
                _initBalls[i].transform.SetParent(_ballsParent);
                _initBalls[i].transform.localPosition = Vector3.zero;
                _initBalls[i].SetActive(true);
                _initBalls[i].GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}