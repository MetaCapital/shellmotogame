using UnityEngine;
using System.Collections;

public class SimpleCarWheel : MonoBehaviour {

	public float rotationSpeed;
	ScrollTexture scrollScript;

	void Start(){
		//find the scroll script
		scrollScript = FindObjectOfType<ScrollTexture>();
	}

	void Update(){
		//check if there is a movement script and rotate the wheel based on scrollspeed
		if(transform.root.gameObject.GetComponent<MoveObject>() != null){
			transform.Rotate(Vector3.forward * Time.deltaTime * scrollScript.scrollSpeed * rotationSpeed * transform.root.gameObject.GetComponent<MoveObject>().randomSpeed);
		}
	}

}


