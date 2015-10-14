using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoundaryObject
{
	public float xMin,xMax,yMin,yMax;
}

public class DragSprite : MonoBehaviour 
{
	public BoundaryObject boundObject;
	Vector2 currentPos;
	public static GameObject itemBeingDragged;
	void Update()
	{

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				//hit.transform.GetComponent<GameObject>();
				itemBeingDragged=hit.collider.gameObject;
				//currentPos=hit.point;
				//hit.collider.transform.position =currentPos;

			}
		}
		if (Input.GetMouseButton(0))
		    {
				Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if(itemBeingDragged != null){

				itemBeingDragged.transform.position = new Vector3(temp.x,temp.y,-3.0f);
				itemBeingDragged.transform.position=new Vector3
					(
						Mathf.Clamp(itemBeingDragged.transform.position.x,boundObject.xMin,boundObject.xMax),
						Mathf.Clamp(itemBeingDragged.transform.position.y,boundObject.yMin,boundObject.yMax),
						-3.0f
						);

			}
			}
		if (Input.GetMouseButtonUp (0)) 
		{
			itemBeingDragged=null;
		}
	}	
}
