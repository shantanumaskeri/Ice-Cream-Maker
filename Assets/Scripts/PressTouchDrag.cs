using UnityEngine;

public class PressTouchDrag : MonoBehaviour
{

	public GameObject objectToDrag;

	private Vector3 pressTouchPosition;

	private void Update()
    {
		pressTouchPosition = Input.mousePosition;
		pressTouchPosition.z = 10;
		pressTouchPosition = Camera.main.ScreenToWorldPoint(pressTouchPosition);

		objectToDrag.transform.position = pressTouchPosition;

		if (Input.mousePosition.y > 800.0f)
		{
			Cursor.visible = true;
			objectToDrag.SetActive(false);
		}
		else
		{
			Cursor.visible = false;
			objectToDrag.SetActive(true);
		}
	}

}
