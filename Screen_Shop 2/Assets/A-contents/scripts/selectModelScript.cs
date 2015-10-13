using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class selectModelScript : MonoBehaviour 
{

	public Toggle toggleSelect;
	public GameObject Man;
	public GameObject Women;

	void Start () 
	{

		toggleSelect = GetComponent<Toggle>();

		Man.SetActive (false);
		Women.SetActive (false);


	}

	void Update () 
	{

		if(toggleSelect.isOn)
		{

			if(toggleSelect.tag.Equals("model1"))
			{

				Man.SetActive (true);
				Women.SetActive (false);


			}else if(toggleSelect.tag.Equals("model2"))
				{

					Women.SetActive (true);
					Man.SetActive (false);

				}

		}

		toggleSelect.isOn = false;
	}
}
