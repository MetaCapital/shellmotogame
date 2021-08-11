using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBoats : MonoBehaviour {
	
	//visible in the inspector
	public GameObject[] boats;
	public int minBoats;
	public int maxBoats;
	
	//not visible 
	List<GameObject> moveBoats = new List<GameObject>();

	void Start () {
		//get a random amount of boats 
		float amount = Random.Range(minBoats, maxBoats);
		
		for(int i = 0; i < amount; i++){
			//for each boat in the loop, get a random one
			int randomBoat = Random.Range(0, boats.Length);
			//also get a random position in the river
			Vector3 position = new Vector3(Random.Range(-110, -80), -8.85f, Random.Range(-100, 100));
			
			//create the new boat and add it to the list of boats
			GameObject newBoat = Instantiate(boats[randomBoat], position, transform.rotation) as GameObject;
			moveBoats.Add(newBoat);
			
			//parent the boat to this river
			newBoat.transform.parent = gameObject.transform;
		}
	}
	
	void Update(){
		//move all boats in the water
		foreach(GameObject boat in moveBoats){
			boat.transform.Translate(transform.forward * Time.deltaTime * 15);
		}
	}
}
