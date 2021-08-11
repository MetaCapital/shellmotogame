using UnityEngine;
using System.Collections;

public class deleteParticles : MonoBehaviour {
	
	public float lifetime = 1f;

	void Start(){
	//delete gameobject (particles) after 1 second
	Destroy(gameObject, lifetime);
	}
}
