using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moversc : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
       
    }
  
    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Transform>().Translate(new Vector3(0,0,-15f*Time.deltaTime));
        if (this.gameObject.GetComponent<Transform>().position.x >= 170f)
        {
          
            if (this.gameObject.name.Equals("streetscreen")|| this.gameObject.name.Equals("streetscreen (2)"))
            {
                this.gameObject.GetComponent<Transform>().position = new Vector3(-59, -10.2f, 13.1f);
            }
            else
            {
                this.gameObject.GetComponent<Transform>().position = new Vector3(-58.1f, -10.2f, -12.8f);
            }
           
        }
    }
}
