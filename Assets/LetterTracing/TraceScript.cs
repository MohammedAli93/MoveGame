using System;
using UnityEngine;
using UnityEngine.Events;

public class TraceScript : MonoBehaviour
{
	[SerializeField] private Color _traceColor = Color.white;

	[SerializeField] private UnityEvent _onSolving;

	[SerializeField] private UnityEvent _onGettingSolved;
	[SerializeField] private UnityEvent _onReseting;
	[SerializeField] private Vector2 _xyPrecision = new Vector2(0.5f,0.5f);
	[SerializeField] private TrailRenderer Tr;
	public bool IsDone;
	private Collider2D _collider2D;
	private bool _isMouseDown;
	[SerializeField] private bool _useLetterTracingNodes;
    [SerializeField] private MovingPlatform _movingNodes;
    [SerializeField] private LetterTracingNodes _letterTracingNodes;
	[SerializeField] private bool _useDistance = true;
	[SerializeField] private float _maxTimeOut = 0.5f;
	[SerializeField] private float _maxDistanceOut = 2f;
	private Vector2 _exitPosition;
	private float _distanceOut = 1f;
	private float _timeOut = 0;
	private bool _isOver;

	private void Awake()
	{
		_collider2D = GetComponent<Collider2D>();
        //if (GameManager.Instance.CurrLevel().Tracing().GetTraceColor() != Color.white)
        //{
        //    _traceColor = GameManager.Instance.CurrLevel().Tracing().GetTraceColor();
        //}
  //      if (_traceColor == Color.white)
		//{
		//	return;
		//}
		for (int i = 0; i < _onSolving.GetPersistentEventCount(); i++)
		{
			SpriteRenderer e = _onSolving.GetPersistentTarget(i) as  SpriteRenderer;
			if (e != null)
			{
				if (e.color.a < 1)
				{
					e.color = new Color(_traceColor.r,_traceColor.g,_traceColor.b,e.color.a);
				}
				else
				{
					e.color = _traceColor;
				}

				if (e.GetComponent<TrailRenderer>() != null)
				{
					e.GetComponent<TrailRenderer>().startColor = _traceColor;
					e.GetComponent<TrailRenderer>().endColor = _traceColor;
				}
			}
		}

		if (_useLetterTracingNodes)
		{
			if (_letterTracingNodes == null)
			{
				foreach (Transform child in transform)
				{
					if (child.GetComponent<LetterTracingNodes>() != null)
					{
						_letterTracingNodes = child.GetComponent<LetterTracingNodes>();
						return;
					}
				}
			}
		}
	}

	private void Update ()
	{
		_isMouseDown = Input.GetMouseButton(0);
		if (!IsDone)
		{
			_timeOut += Time.deltaTime;
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (_useLetterTracingNodes)
			{
				if (_letterTracingNodes.worldNode.Length > _letterTracingNodes.Index)
				{
					ResetAll();
				}
				else
				{
					Tr.emitting = false;
				}
			}
			else
			{
				if (_movingNodes.worldNode.Length > _movingNodes.Index)
				{
					ResetAll();
				}
				else
				{
					Tr.emitting = false;
				}	
			}
		}
		if (_timeOut > _maxTimeOut && _resetOnce )
		{
			ResetAll();
			_resetOnce = false;
		}
		if (!IsDone)
		{
			if (_useDistance)
			{
				var pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

				if (Vector2.Distance(pos, _exitPosition) > _maxDistanceOut)
				{
					ResetAll();
				}
				return;
			}
		}
	}

	private bool _resetOnce;
	public void ResetAll()
	{
		if (_useLetterTracingNodes)
		{
			_letterTracingNodes.Index = 0;
		}
		else
		{
			_movingNodes.Index = 0;
		}

		Tr.emitting = false;
		IsDone = false;
		_onReseting.Invoke();
		_timeOut = 0;
		Tr.Clear();
	}
	private void OnMouseOver()
	{
		if (EventSystemManager.IsOverUi())
		{
			return;
		}
		if (GameManager.Instance != null)
		{
			if (GameManager.Instance.IsTitleSoundPlaying() && GameManager.Instance.DontAnswerWhenTitle)
			{
				return;
			}	
		}
		if (!_isMouseDown)
		{
			return;
		}
		if (_useDistance)
		{
			_exitPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		}
		_isOver = false;
		_resetOnce = true;
		_timeOut = 0;
		var pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		if (_useLetterTracingNodes)
		{
			if (!IsDone && _letterTracingNodes.worldNode.Length <= _letterTracingNodes.Index)
			{
				IsDone = true;
				_onGettingSolved.Invoke();
				if (_collider2D != null)
				{
					_collider2D.enabled = false;
				}
			}
		}
		else
		{
			if (!IsDone && _movingNodes.worldNode.Length <= _movingNodes.Index)
			{
				IsDone = true;
				_onGettingSolved.Invoke();
				_collider2D.enabled = false;
			}
		}

		if (IsDone)
		{
			return;
		}
		
		if (Tr.emitting)
		{
			_onSolving.Invoke();
		}

		if (_useLetterTracingNodes)
		{
			if (Math.Abs(pos.x - _letterTracingNodes.worldNode[_letterTracingNodes.Index].x) < _xyPrecision.x&&(Math.Abs(pos.y - _letterTracingNodes.worldNode[_letterTracingNodes.Index].y) < _xyPrecision.y))
			{
				_letterTracingNodes.IncIndex();
				Tr.emitting = _isMouseDown;
			}
		}
		else
		{
			if (Math.Abs(pos.x - _movingNodes.worldNode[_movingNodes.Index].x) < _xyPrecision.x&&(Math.Abs(pos.y - _movingNodes.worldNode[_movingNodes.Index].y) < _xyPrecision.y))
			{
				_movingNodes.IncIndex();
				Tr.emitting = _isMouseDown;
			}	
		}
	}
	
	private void OnMouseExit()
	{
		//*Tr.emitting = false;
		if (_useLetterTracingNodes)
		{
			if (_letterTracingNodes.worldNode.Length <= _letterTracingNodes.Index)
			{
				Tr.emitting = false;
				return;
			}
		}
		else
		{
			if (_movingNodes.worldNode.Length <= _movingNodes.Index)
			{
				Tr.emitting = false;
				return;
			}
		}
		//_isOut = true;
		if (_useDistance)
		{
			_exitPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		}
		return;
		//*ResetAll();
	}

	public void PrintMessage(string message)
	{
		print(message);
	}
}