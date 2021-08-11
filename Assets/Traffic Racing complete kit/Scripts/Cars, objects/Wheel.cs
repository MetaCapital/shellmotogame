using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Wheel : MonoBehaviour {
	
	//variables visible in the inspector
	public bool frontWheel;
	public float rotationSpeed;
	public int maxSteerRotation;
	public float steerRotationSpeed;
	
	//not visible in the inspector
	Vector3 currentPos;
	Vector3 lastPos;

	float minMoveDistanceToRotateWheel = 0.05f;
	float yRotation;
	float scrollSpeed;

	void Start(){
		//if the car is not in the garage scene, assign y rotation of this wheel
		if(SceneManager.GetActiveScene().name != "Garage"){
			yRotation = transform.localEulerAngles.y;
		}
	}

	void Update(){
		//check if car is not in the garage
		if(SceneManager.GetActiveScene().name != "Garage"){
			//assign scrollspeed
			scrollSpeed = FindObjectOfType<ScrollTexture>().scrollSpeed;
			//rotate wheel based on scrollspeed
			transform.Rotate (Vector3.forward * Time.deltaTime * scrollSpeed * rotationSpeed);
			
			//check if this is a frontwheel
			if(frontWheel && PlayerPrefs.GetInt("touchControls") != 1){
				
				//get object position
				currentPos = transform.position;
				
				//check if object moved enough to rotate it
				if(Vector3.Distance(currentPos, lastPos) > minMoveDistanceToRotateWheel){
					//rotate object based on move direction
					if(currentPos.z > lastPos.z){
						yRotation += Time.deltaTime * steerRotationSpeed;
					} 
					else if(currentPos.z < lastPos.z){
						yRotation += Time.deltaTime * -steerRotationSpeed;
					}
				} 
				else{
					//reset rotation if object is not moving
					transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
					yRotation = 0;
				}
				
				//assign the last position and y rotation
				lastPos = currentPos;
				yRotation = Mathf.Clamp (yRotation, -maxSteerRotation, maxSteerRotation);
				
				//only rotate the wheel if the count down is not visible
				if(!Manager.count){
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation + 180, transform.localEulerAngles.z);
				}
				else{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
				}
			}
		}

	}
}