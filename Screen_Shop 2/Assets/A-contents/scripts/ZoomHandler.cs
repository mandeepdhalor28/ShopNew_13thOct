using UnityEngine;
using System.Collections;

public class ZoomHandler : MonoBehaviour {
	public GameObject obj;
	public float zoomSpeed = 0.02f;

	void Update()
	{
		if (Input.touchCount == 2) {
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPreviousPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePreviousPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPreviousPos - touchOnePreviousPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

			obj.transform.localScale = new Vector2(obj.transform.localScale.x+deltaMagnitudediff * -zoomSpeed,obj.transform.localScale.y+deltaMagnitudediff * -zoomSpeed);
			//obj.transform.localScale.x += deltaMagnitudediff * zoomSpeed;
			obj.transform.localScale = new Vector2(Mathf.Max (obj.transform.localScale.x, 0.1f),Mathf.Max (obj.transform.localScale.y, 0.1f));
			obj.transform.localScale = new Vector2(Mathf.Min (obj.transform.localScale.x, 1.5f),Mathf.Min (obj.transform.localScale.y, 1.5f));
		}
	}
}