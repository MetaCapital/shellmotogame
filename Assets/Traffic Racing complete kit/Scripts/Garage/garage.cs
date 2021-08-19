using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
//car variables
public class Car{
	public GameObject carPrefab;
	public string carName;
	public int carPrice;
}

public class garage : MonoBehaviour {
	
	//variables visible in the inspector
	public Slider speedSlider;
	public List<Car> cars;
    public GameObject boostpanel,boostbutton,fill1,fill2,fill3;
	//variables not visible in the inspector
	public static GameObject startPanel;
	public static GameObject startPanelContent;
	
	Animator screenTransition;
	GameObject currentCar;
	GameObject leftButton;
	GameObject rightButton;
	GameObject carName;
	GameObject carLock;
	GameObject carPrice;
	GameObject coinsLabel;
	GameObject locationPanel;
	GameObject buyWarning;
	GameObject newCarLabel;
	AudioSource buttonAudio;
	AudioSource buttonAudioShort;
	Vector3 newCarLabelStartPos;

	bool fading;
	bool newCarFade;


	
	private void Awake()
    {
      
    }
	public void connecPlayGames() {
		PlayGamesConnector.connect();
	}
    void Start(){
		// connects to Google Play Services at the Game Start
		
		boostpanel.SetActive(false);
        confirmpanel.SetActive(false);
		
        fill1.SetActive(false);
        fill2.SetActive(false);
        fill3.SetActive(false);
        //always unlock the first car
        PlayerPrefs.SetInt("" + cars[0].carName, 1);
	
	//find all the UI objects
	//--------------------------------------------------------------------------------------------------------
	carLock = GameObject.Find("locked car panel");
	carPrice = GameObject.Find("car price");
	
	//set the price of the price label to the price of the visible car
	carPrice.GetComponent<Text>().text = "" + cars[PlayerPrefs.GetInt("selectedCar")].carPrice;
	
	startPanel = GameObject.Find("Start panel");	
	startPanel.SetActive(true);
	if ( Manager.counter == false)
        {
            fading = true;
        }
	startPanelContent = GameObject.Find("start panel content");
	startPanelContent.SetActive(true);
	
	locationPanel = GameObject.Find("location panel");
	locationPanel.SetActive(false);
	
	buyWarning = GameObject.Find("Buy warning");
	buyWarning.SetActive(false);
	
	coinsLabel = GameObject.Find("coins");	
	//set the coins text to your total coins
	coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("coins");
	
	screenTransition = GameObject.Find("screen transition").GetComponent<Animator>();	
	
	leftButton = GameObject.Find("Left");
	rightButton = GameObject.Find("Right");
	
	newCarLabel = GameObject.Find("New car label");
	carName = GameObject.Find("car name");
	
	newCarLabelStartPos = newCarLabel.transform.position;
	newCarLabel.SetActive(false);
	
	checkCarSettings();
	
	buttonAudio = GetComponents<AudioSource>()[0];
	buttonAudioShort = GetComponents<AudioSource>()[1];
	
	checkButtons();
	}
	
	void Update(){
		
		//if start panel is fading out and not done yet, decrease its alpha by time.deltatime
		if (fading && startPanel.GetComponent<CanvasGroup>().alpha > 0){
		startPanel.GetComponent<CanvasGroup>().alpha -= Time.deltaTime * 2;
           
        }
	else if(fading){
	//if fading is true but the fading is done already, set fading false and remove the start panel completely
	fading = false;	
	startPanel.SetActive(false);
	}
	
	if(newCarLabel.activeSelf){
		newCarLabel.transform.Translate(Vector3.up * Time.deltaTime * 20);
		
		if(newCarFade && newCarLabel.GetComponent<CanvasGroup>().alpha > 0){
			newCarLabel.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
		}
		else if(newCarFade){
			newCarFade = false;
			newCarLabel.SetActive(false);
		}
	}
	}
	
	public void checkButtons(){
		//set the buttons that switch cars false if the last car of the array is visible
		if(PlayerPrefs.GetInt("selectedCar") < 1){
			leftButton.SetActive(false);	
		}
		else{
			leftButton.SetActive(true);	
		}
	
		if(PlayerPrefs.GetInt("selectedCar") == cars.Count - 1){
			rightButton.SetActive(false);	
		}
		else{
			rightButton.SetActive(true);	
		}
	}
	
	//start fading out and set start panel not active when player presses play
	public void play(){
		buttonAudio.Play();
		fading = true;	
	}
	
	//quit application
	public void quit(){
       
		Application.Quit();	
	}
	
	//instantiate next car of the array
	//save it as selected car
	//make it child of platform
	public void changeSelected(int direction){
		buttonAudioShort.Play();
		PlayerPrefs.SetInt("selectedCar", PlayerPrefs.GetInt("selectedCar") + direction);	
		
		Destroy(currentCar);
		
		checkCarSettings();
		checkButtons();
	}
	
	public void buyCar(){
		if(cars[PlayerPrefs.GetInt("selectedCar")].carPrice <= PlayerPrefs.GetInt("coins")){
			buttonAudio.Play();
			buyWarning.SetActive(true);
		}
	}
	
	public void buy(){
		buttonAudio.Play();
		PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cars[PlayerPrefs.GetInt("selectedCar")].carPrice);
		coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("coins");
		
		//save the car as unlocked
		PlayerPrefs.SetInt("" + cars[PlayerPrefs.GetInt("selectedCar")].carName, 1);
		
		//remove the lock to make the car drivable
		carLock.SetActive(false);
		buyWarning.SetActive(false);
		StartCoroutine(newCar(cars[PlayerPrefs.GetInt("selectedCar")].carName));
	}
	
	public void cancel(){
		buttonAudio.Play();
		buyWarning.SetActive(false);
	}
	
	public void race(){
		buttonAudio.Play();
		
		//show panel to choose your location
		locationPanel.SetActive(true);	
	}
    public void boostpanell()
    {
        boostpanel.SetActive(true);
    }
    private int chosen;
	private int remaining_coins;
    public GameObject confirmpanel;
    public void choosetoconfirm(int r)
    {
		// price of each boost
		int[] boost_price = new int[] { 150, 500, 1500 };
		// choice collected from the Boost Panel
		int choice = r;
		// computer remaining coins
		remaining_coins = PlayerPrefs.GetInt("coins") - boost_price[choice - 1];
		
		if (remaining_coins >= 0)
		{   //set boost choice
			chosen = choice;
			confirmpanel.SetActive(true);
		}
    }
    public void noboost()
    {
        confirmpanel.SetActive(false);
        boostpanel.SetActive(false);
    }
    public void boost()
    {

		PlayerPrefs.SetInt("coins", remaining_coins);
		coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("coins");
		
		switch (chosen)
        {
            case 1:
                fill1.SetActive(true);
                break;	
            case 2:
                fill2.SetActive(true);
                break;
            case 3:
                fill3.SetActive(true);
                break;
           
            default:
                break;
        }
        confirmpanel.SetActive(false);
        boostpanel.SetActive(false);
        boostbutton.GetComponent<Button>().enabled = false;
    }
    public void closepan()
    {
        boostpanel.SetActive(false);
    }
        public void back(){
		buttonAudioShort.Play();
		
		//go back to the garage
		locationPanel.SetActive(false);	
	}
	
	public void openScene(string scene){
		buttonAudioShort.Play();	
		StartCoroutine(loadScene(scene));
	}
	
	public void checkCarSettings(){
		//instantiate the selected car and make it a child of the rotating platform
		currentCar = Instantiate(cars[PlayerPrefs.GetInt("selectedCar")].carPrefab, Vector3.zero, transform.rotation) as GameObject;
		currentCar.transform.parent = GameObject.Find("Platform").transform;
		CarControls carControls = currentCar.GetComponent<CarControls>();
		carControls.magnetObject.SetActive(false);
		carControls.enabled = false;
		currentCar.GetComponents<AudioSource>()[0].Stop();
	
		//set the car name to the name of the visible car
		carName.GetComponent<Text>().text = "" + cars[PlayerPrefs.GetInt("selectedCar")].carName;

        //set the slider to the speed of the currently selected car by getting the highspeed from its car controls script
        if(PlayerPrefs.GetInt("selectedCar") == 0)
        {
           // speedSlider.value = cars[0].carPrefab.GetComponent<CarControls>().highSpeed;
            speedSlider.value = 6f;
        }
        else if (PlayerPrefs.GetInt("selectedCar") == 1)
        {
           // speedSlider.value = cars[1].carPrefab.GetComponent<CarControls>().highSpeed;
            speedSlider.value = 9f;
        }
        else if (PlayerPrefs.GetInt("selectedCar") == 2)
        {
           // speedSlider.value = cars[2].carPrefab.GetComponent<CarControls>().highSpeed;
            speedSlider.value = 11f;
        }
        else if (PlayerPrefs.GetInt("selectedCar") == 3)
        {
            //speedSlider.value = cars[3].carPrefab.GetComponent<CarControls>().highSpeed;
            speedSlider.value = 13f;
        }
        else 
        {
            
            speedSlider.value = cars[4].carPrefab.GetComponent<CarControls>().highSpeed;
        }
      //  speedSlider.value = cars[PlayerPrefs.GetInt("selectedCar")].carPrefab.GetComponent<CarControls>().highSpeed;
	
		if(PlayerPrefs.GetInt("" + cars[PlayerPrefs.GetInt("selectedCar")].carName) == 0){
			carLock.SetActive(true);
           
            carPrice.GetComponent<Text>().text = "" + cars[PlayerPrefs.GetInt("selectedCar")].carPrice;
		}
		else{
			carLock.SetActive(false);	
		}
	}
	
	IEnumerator loadScene(string scene){
		//open scene
		screenTransition.SetBool("transition", true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(scene);
	}
	
	IEnumerator newCar(string name){
		newCarLabel.GetComponent<CanvasGroup>().alpha = 1;
		newCarLabel.GetComponent<Text>().text = name + " unlocked!";
		newCarLabel.transform.position = newCarLabelStartPos;
		newCarLabel.SetActive(true);
		
		yield return new WaitForSeconds(1);
		
		newCarFade = true;
	}
}
