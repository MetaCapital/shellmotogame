using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;
//using Firebase.Database;

public class Coin : MonoBehaviour {
	
	//variable visible in the inspector
	public int coins;
	public GameObject effect;
	public float magnetEffectDistance;
	
	//not visible in the inspector
	Manager manager;
	MeshRenderer meshRenderer;
	AudioSource audioSource;
	
	Vector3 localStartPosition;
	
	void Start(){
		//get the local start position of this coin
		localStartPosition = transform.localPosition;
       // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        

    }

    void OnEnable(){
		//get some components and assign the local start position
		manager = GameObject.FindObjectOfType<Manager>();
		meshRenderer = GetComponent<MeshRenderer>();
		audioSource = GetComponent<AudioSource>();
		GetComponent<Collider>().enabled = true;
		meshRenderer.enabled = true;
		
		if(localStartPosition != Vector3.zero)
			transform.localPosition = localStartPosition;
	}
	
	void OnTriggerEnter(Collider other){
		//add coins on trigger
		if(other.gameObject.GetComponent<CarControls>())
			addCoins();
	}
	
	void Update(){
		//move towards the car if it has a magnet
		CarControls carScript = GameObject.FindObjectOfType<CarControls>();
		if(carScript != null && carScript.magnet && Vector3.Distance(carScript.gameObject.transform.position, transform.position) < magnetEffectDistance){
			transform.position = Vector3.MoveTowards(transform.position, carScript.magnetObject.transform.position, Time.deltaTime * 30);
		}
	}
	
	public void addCoins(){
		//turn this object off, show an effect and add the coins to the manager
		audioSource.Play();
		manager.extraCoins += coins;
		GameObject coinEffect = Instantiate(effect, transform.position, transform.rotation);
		coinEffect.transform.parent = transform;
		GetComponent<Collider>().enabled = false;
		meshRenderer.enabled = false;
	}
}

