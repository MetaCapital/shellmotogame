using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class CarControls : MonoBehaviour {
	
	//variables visible in the inspector
	[Range(0.5f, 2.5f)]
	public float movespeed;
	[Range(1, 10)]
	public float moveRange;
	
	public int highSpeed;
	public int boostSpeed;
	public float boostLength;
	public ParticleSystem sparks;
	public ParticleSystem boostParticles;
	public bool vibrateOnCollision;
	public int maxSteerRotation;
	public float steerRotationSpeed;
	public float nearMissDistance;
	public GameObject nearMissCoinEffect;
	public GameObject motorRider;
	public GameObject motorRiderRagdoll;
	public float coinMagnetTime;
	public GameObject magnetObject;
	
	[HideInInspector]
	public bool magnet;
	
	#if UNITY_ADS
	public bool showAdToContinue = true;
	#endif
	
	//variable not visible in the inspector
	bool crash;
	int collisionCount;
	GameObject smoke;
	GameObject carMesh;
	GameObject wrongDirectionLabel;
	AudioSource carAudioNormal;
	AudioSource carAudioBoost;
	Coroutine boostCoroutine;
	Coroutine magnetCoroutine;
	ObjectPool objectPool;
	
	Vector3 currentPos;
	Vector3 lastPos;
	float minMoveDistanceToRotate = 0.02f;
	float rotation;
	float lastDistance;
	
	Manager manager;
	CarSpawner carSpawner;
	ScrollTexture scrollTextureScript;
	
	void Awake(){
		if(SceneManager.GetActiveScene().name != "Garage"){
			//set the overall speed to the speed of this car
			manager = FindObjectOfType<Manager>();
			manager.carHighSpeed = highSpeed;	
			carSpawner = GameObject.FindObjectOfType<CarSpawner>();
			objectPool = GameObject.FindObjectOfType<ObjectPool>();
		}
	}
	
	void Start(){	
	//find the smoke particles and set them not active
	smoke = transform.Find("smoke").gameObject;	
	smoke.SetActive(false);	
	magnetObject.SetActive(false);
	boostParticles.Stop();	
	
	carMesh = transform.Find("car mesh").gameObject;
	scrollTextureScript = FindObjectOfType<ScrollTexture>();
	
	//get car audio
	carAudioNormal = GetComponents<AudioSource>()[0];
	carAudioBoost = GetComponents<AudioSource>()[1];
	
        rotation = carMesh.transform.localEulerAngles.y;
	wrongDirectionLabel = manager.gameUI.transform.Find("Wrong direction label").gameObject;
	}

	void Update(){		
		//check if car is not in garage
		if(SceneManager.GetActiveScene().name != "Garage"){
			updateRotation();
	
			#if UNITY_IOS || UNITY_ANDROID
			//if touch controls are enabled, check for mouse button 0 (touch on mobile devices) and make sure you're not clicking UI
			if(PlayerPrefs.GetInt("touchControls") == 0){
				if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()){
					//check car position and clicked position on the screen to move the car to the right side
					if(Input.mousePosition.x < Screen.width/2 && transform.position.z > -moveRange){
						transform.Translate(0, 0, movespeed * Time.deltaTime * -5);
					}
					else if(Input.mousePosition.x > Screen.width/2 && transform.position.z < moveRange){
						transform.Translate(0, 0, movespeed * Time.deltaTime * 5);
					} 
				}
			}
			//if you don't want to use touch controls, accelerometer controls are automatically enabled:
			else{
				//check car position and move it using accelerometer
				if(transform.position.z < moveRange && transform.position.z > -moveRange)
					transform.Translate(0, 0, Input.acceleration.x * movespeed * Time.deltaTime * 15);	
				
				//prevent car from going off the road
				if(transform.position.z >= moveRange){
					transform.position = new Vector3(transform.position.x, transform.position.y, moveRange - 0.001f);	
				}
			
				//prevent car from going off the road
				if(transform.position.z <= -moveRange){
					transform.position = new Vector3(transform.position.x, transform.position.y, -moveRange + 0.001f);		
				}
			}
	
			#else
			if(Input.GetKey(manager.leftKey) && transform.position.z > -moveRange){
				transform.Translate(0, 0, -movespeed * Time.deltaTime * 5);
			}
			if(Input.GetKey(manager.rightKey) && transform.position.z < moveRange){
				transform.Translate(0, 0, movespeed * Time.deltaTime * 5);
			}
			#endif
		}
	
		//while counting down in the beginning of the game, move car forward (just for a nice effect)
		if(Manager.count && transform.position.x > 2f)
			transform.Translate(-0.1f, 0, 0);	
	
		//if car has crashed, move it backwards
		if(crash)
			transform.Translate(0.3f, 0, 0);	
	
		if((transform.position.z < 0 && !carSpawner.leftSameDirection) || (transform.position.z > 0 && !carSpawner.rightSameDirection)){
			manager.extraCoins += (Manager.distance - lastDistance) * 200f;
			
			if(!wrongDirectionLabel.activeSelf)	
				wrongDirectionLabel.SetActive(true);
		}
		else if(wrongDirectionLabel.activeSelf){	
				wrongDirectionLabel.SetActive(false);
		}
	
		lastDistance = Manager.distance;
	
		if(GetComponent<Collider>().enabled)
			checkForNearMiss();
	}
	
	void updateRotation(){
		currentPos = transform.position;

		if(Vector3.Distance(currentPos, lastPos) > minMoveDistanceToRotate){

		if(currentPos.z > lastPos.z){
			rotation += Time.deltaTime * steerRotationSpeed;
		} 
		else if(currentPos.z < lastPos.z){
			rotation += Time.deltaTime * -steerRotationSpeed;
		}

		} 
		else{
			if(rotation > 0){
				rotation += Time.deltaTime * -steerRotationSpeed * 2;
				if(rotation < 0)
					rotation = 0;
			} 
			else if(rotation < 0){
				rotation += Time.deltaTime * steerRotationSpeed * 2;
				if(rotation > 0)
					rotation = 0;
			}
		}
		
		lastPos = currentPos;
		if(gameObject.name != "motorcycle 1(Clone)"&& gameObject.name != "motorcycle(Clone)"&& gameObject.name != "motorcycle2(Clone)")
        {
			rotation = Mathf.Clamp (rotation, -maxSteerRotation, maxSteerRotation);
		}
		else{
			rotation = Mathf.Clamp (rotation, -maxSteerRotation * 7, maxSteerRotation * 7);
		}
		
		if(!Manager.count){
		if(gameObject.name != "motorcycle 1(Clone)" && gameObject.name != "motorcycle(Clone)" && gameObject.name != "motorcycle2(Clone)"){
			if(PlayerPrefs.GetInt("touchControls") != 1){
				carMesh.transform.localEulerAngles = new Vector3(rotation/2, rotation + 180, carMesh.transform.localEulerAngles.z);
			}
			else{
				carMesh.transform.localEulerAngles = new Vector3(rotation/2, 180, carMesh.transform.localEulerAngles.z);
			}
		}
		else{
			carMesh.transform.localEulerAngles = new Vector3(-rotation, rotation/1.7f + 180, 0);
		}
		}
	}
	
	void checkForNearMiss(){
		foreach(MoveObject moveObjectScript in GameObject.FindObjectsOfType<MoveObject>()){
			
			Vector3 position = transform.position;
			Vector3 carPosition = moveObjectScript.gameObject.transform.position;
			
			if(moveObjectScript.car && !moveObjectScript.nearMiss && Vector3.Distance(carPosition, position) < nearMissDistance && !moveObjectScript.collision 
			&& moveObjectScript.gameObject.name != "boost(Clone)"){
				manager.extraCoins += 30;
				moveObjectScript.nearMiss = true;
				
				Vector3 coinEffectPosition = new Vector3(0.5f * (position.x + carPosition.x), 0.5f * (position.y + carPosition.y) + 1, 0.5f * (position.z + carPosition.z));
				Instantiate(nearMissCoinEffect, coinEffectPosition, nearMissCoinEffect.transform.rotation);
			}
		}
	}
	
	//on collision with car
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name != "Road manager" && col.gameObject.name != "boost(Clone)" && col.gameObject.name != "magnet(Clone)" && !crash){
			col.gameObject.GetComponent<MoveObject>().collision = true;
		
			if(vibrateOnCollision){
				#if UNITY_IOS || UNITY_ANDROID
				Handheld.Vibrate();
				#endif
			}
		
			//get audio, give it random duration and volume, and play it
			AudioSource audio = GetComponents<AudioSource>()[2];
			audio.pitch = Random.Range(1f, 3f);
			audio.volume = Random.Range(0.5f, 1f);
			audio.Play();	
		
			//get first contact point of the collision, instantiate sparks effect and move it to the contact point
			ContactPoint contact = col.contacts[0];
			ParticleSystem sparksEffect = Instantiate(sparks);
			sparksEffect.transform.position = contact.point;
	
			//add 1 to the collisions
			collisionCount++;
			
			//after 5 collisions, turn on smoke effect and give a text warning
			if(collisionCount == 5){
				smoke.SetActive(true);	
				foreach(Transform child in smoke.transform){
					child.gameObject.GetComponent<ParticleSystem>().Play();
				}
				
				StartCoroutine(warning());
			}
			
			//after more than 5 collisions, car crashes
			if(collisionCount > 5){
				#if !UNITY_ADS
				StartCoroutine(Crash());	
				#else
				if(Advertisement.IsReady() && !manager.didShowAd && showAdToContinue){
					hideCar();
					StartCoroutine(manager.showAdOption());
				}
				else{
					StartCoroutine(Crash());
				}
				#endif	
			}
		}
		else if(col.gameObject.name == "boost(Clone)"){
			objectPool.addToStorage(col.gameObject, col.gameObject.GetComponent<MoveObject>().poolID);
	
			if(Manager.boostVignette.activeSelf){
				scrollTextureScript.scrollSpeed -= boostSpeed;
				StopCoroutine(boostCoroutine);
			}

			boostCoroutine = StartCoroutine(boost());	
		}
		else if(col.gameObject.name == "magnet(Clone)"){
			objectPool.addToStorage(col.gameObject, col.gameObject.GetComponent<MoveObject>().poolID);
			
			if(magnetCoroutine != null)
				StopCoroutine(magnetCoroutine);
			
			magnetCoroutine = StartCoroutine(useMagnet());
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name != "boost(Clone)" && other.gameObject.name != "magnet(Clone)" && other.gameObject.name != "Road manager" && other.gameObject.name.Substring(0, 4) != "coin"){
			//play audio and car crashes
			AudioSource audio = GetComponents<AudioSource>()[2];
			audio.Play();
			#if !UNITY_ADS
				StartCoroutine(Crash());	
			#else
			if(Advertisement.IsReady() && !manager.didShowAd && showAdToContinue){
				hideCar();
				StartCoroutine(manager.showAdOption());
			}
			else{
				StartCoroutine(Crash());
			}
			#endif	
		}
	}
	
	void hideCar(){
		carMesh.SetActive(false);
		foreach(Collider collider in GetComponents<Collider>()){
			collider.enabled = false;
		}
	}
	
	IEnumerator useMagnet(){
		magnetObject.SetActive(true);
		magnet = true;
		
		yield return new WaitForSeconds(coinMagnetTime);
		
		magnetObject.SetActive(false);
		magnet = false;
	}
	
	IEnumerator boost(){
		carAudioNormal.Stop();
		carAudioBoost.Play();
		
		Manager.boostFlash.SetActive(true);
		
		yield return new WaitForSeconds(0.05f);
		
		Manager.boostFlash.SetActive(false);
		Manager.boostVignette.SetActive(true);
		
		boostParticles.Play();
		
		scrollTextureScript.scrollSpeed += boostSpeed;
		
		yield return new WaitForSeconds(boostLength);
		
		scrollTextureScript.scrollSpeed -= boostSpeed;
		Manager.boostVignette.SetActive(false);
		boostParticles.Stop();
		
		carAudioBoost.Stop();
		carAudioNormal.Play();
	}
	
	public IEnumerator Crash(){	
		if(!carMesh.activeSelf)
			carMesh.SetActive(true);
		
		//crash is true
		crash = true;
	
		if(motorRider && motorRiderRagdoll){
			GetComponent<BoxCollider>().enabled = false;
			Instantiate(motorRiderRagdoll, motorRider.transform.position, motorRider.transform.rotation);
			Destroy(motorRider);
		}
	
		//wait a moment
		yield return new WaitForSeconds(1.5f);
		
		//set new coins to distance * 200
		float newCoins = Manager.distance * 200f + manager.extraCoins;
		//save extra coins and than set the player to be game over
		PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + (int)newCoins);
		Manager.gameOver = true;
	
		//save distance if it's better than current best and set label active
		if(Manager.distance > PlayerPrefs.GetFloat("bestDistance")){
			PlayerPrefs.SetFloat("bestDistance", Manager.distance);
			Manager.bestDistanceLabel.SetActive(true);

			Debug.Log("Car Controls > Sahbi we are just playing games");
			/*
			if (Social.Active.localUser.authenticated)
			{

				//string leaderboardID = "CgkIj4jx5YUNEAIQBg";
				//string achievementID = "CgkIj4jx5YUNEAIQAg";
				string leaderboardID = "CgkI08WD3dYdEAIQAQ";
				string achievementID = "CgkI08WD3dYdEAIQAg";
				long distance = (long)(Manager.distance*100);
				Social.ReportProgress(achievementID, 100f, success => { });
				Social.ReportScore(distance, leaderboardID, success => { Debug.Log("Car Controls > Successful"); });
			}*/
		}
		
		//destroy car
		Destroy(gameObject);
	}
	
	public IEnumerator ContinueAfterAd(){
		
		collisionCount = 0;
		foreach(Transform child in smoke.transform){
			child.gameObject.GetComponent<ParticleSystem>().Stop();
		}
		smoke.SetActive(false);	
		
		foreach(Collider collider in GetComponents<Collider>()){
			collider.enabled = false;
		}
		
		carMesh.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		
		for(int i = 0; i < 15; i++){
			carMesh.SetActive(!carMesh.activeSelf);
			yield return new WaitForSeconds(0.15f);
		}
		
		carMesh.SetActive(true);
		foreach(Collider collider in GetComponents<Collider>()){
			collider.enabled = true;
		}
	}
	
	IEnumerator warning(){
		//set warning text active, wait some seconds and set it not active
		Manager.damageWarning.SetActive(true);
		
		yield return new WaitForSeconds(2);
		
		Manager.damageWarning.SetActive(false);
	}
}
