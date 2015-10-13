using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragSpriteScriptsShirt : MonoBehaviour{

	private Vector3 dist;

	public GameObject draggableObject;

	public Vector3 startPos;

	public Vector3 currentPos;

	public float xPos;

	public float yPos;

	public bool startMouseRefresh;

	void Start () {

		startMouseRefresh = false;

	}
	

	void Update () {

		if(Input.GetMouseButton(0)){

			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit,1000.0f)){

				if(hit.transform.tag == "Shirt"){


					currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

					print (xPos + "||"+ yPos);

					if(currentPos.z!=-9){

						currentPos.z= -9;

					}
				//this.GetComponent<Collider>().transform.position = currentPos;

					OnDragScript();

					}

				}
			}
		}



	public void OnDragScript(){

		//print ("dragging");

//		currentPos = new Vector3(xPos,yPos,0);

		draggableObject.transform.position = currentPos;

	}


	void OnTriggerEnter(Collider other) {

		//print ("head is collided");

		}

}
