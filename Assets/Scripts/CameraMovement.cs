using UnityEngine;
public class CameraMovement : MonoBehaviour
{
	//camera move speed
	public int cameraSpeed;
	public float sensivity;
	private float mouseX;
	public int zoomOutDistance,zoomInDistance;
	public int initialCameraXRotation = 65;

	void Update()
	{
		HandleMouseRotation();
		HandleCameraMovement();
		HandleScroll();
		mouseX = Input.mousePosition.x;
	}

	private void HandleMouseRotation()
    {
		if (Input.GetMouseButton(2))
		{
			if(Input.mousePosition.x != mouseX)
            {
				var cameraRotationY = (Input.mousePosition.x - mouseX) * sensivity * Time.deltaTime;
				transform.Rotate(0,cameraRotationY,0);
            }
		}
	}
		
	private void HandleCameraMovement()
	{
		if (Input.GetKey("w"))
			transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);
		if (Input.GetKey("a"))
			transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
		if (Input.GetKey("s"))
			transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime);
		if (Input.GetKey("d"))
			transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
	}

	private void HandleScroll()
	{
		//zoom out
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			if (transform.position.y >= zoomOutDistance || transform.GetChild(0).rotation.x >= 75)
				return;
			transform.Translate((Vector3.up * 5) * (cameraSpeed * 5) * Time.deltaTime);
			transform.GetChild(0).Rotate(sensivity * (cameraSpeed / 2) * Time.deltaTime, 0, 0);
		}
		//zoom in
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
			if (transform.position.y <= zoomInDistance)
				return;
			transform.Translate((Vector3.down * 5) * (cameraSpeed * 5) * Time.deltaTime);
			transform.GetChild(0).Rotate(-sensivity * (cameraSpeed / 2) * Time.deltaTime,0,0);
		}
	}
}





