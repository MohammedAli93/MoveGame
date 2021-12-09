using UnityEngine;
using UnityEngine.Events;

public class TracingQuestion : MonoBehaviour
{
	[SerializeField] private Color _traceColor = Color.white;
	[SerializeField] private UnityEvent _onGettingDone;
	[SerializeField]private UnityEvent _doAfterTime;
	[Tooltip("Time to invoke \"Do After Time\" Event started when question ended")][SerializeField] private float _timeOfInvoking = 1f;
	[HideInInspector] public bool IsAnswered;

	public void NextQuestion()
	{
		IsAnswered = true;
		print("Question Answered");
		_onGettingDone.Invoke();
		ActivateAfterTime();
	}
	
	public void ActivateAfterTime()
	{
		Invoke("InvokedMethod",_timeOfInvoking);
	}
	private void InvokedMethod()
	{
		_doAfterTime.Invoke();
	}
	
	public void ChangeSpriteRendererColor(SpriteRenderer spriteRenderer)
	{
		spriteRenderer.color = _traceColor;
	}

	public Color GetTraceColor()
	{
		return _traceColor;
	}
}