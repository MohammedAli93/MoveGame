using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireWorks : MonoBehaviour
{
	[SerializeField] private Camera _camera;

	private void Awake()
	{
		if (_camera == null)
		{
			_camera = Camera.main;
		}
	}

	public void FireStars()
	{
		if (_camera == null)
		{
			print("<color=red>No Camera Attached</color>");
			return;
		}
		transform.position = new Vector2(_camera.ScreenToWorldPoint(Input.mousePosition).x,_camera.ScreenToWorldPoint(Input.mousePosition).y);
		GetComponent<ParticleSystem>().Play();
	}
}