using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour {
	
	//visible in the inspector
	public float lifetime;
	public float movespeed;

	void Start () {
		//destroy effect after lifetime
		Destroy(gameObject, lifetime);
	}
	
	void Update () {
		//move the effect up
		transform.Translate(Vector3.up * Time.deltaTime * movespeed);
	}
}
