using UnityEngine;

public class MouseTraceController : MonoBehaviour
{
	void Update ()
	{
		var pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		transform.position = pos;
	}
}