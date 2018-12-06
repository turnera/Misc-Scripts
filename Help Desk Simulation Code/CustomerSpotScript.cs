using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpotScript : MonoBehaviour {

    public bool cReady;
	// Use this for initialization
	void Start () {
        cReady = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "customer")
        {
            ShowSetupLaptop(true);
            cReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "customer")
        {
            ShowSetupLaptop(false);          
            cReady = false;
        }
    }

    void ShowSetupLaptop(bool boolean)
    {
        GameObject.Find("SetupChassy").GetComponent<Renderer>().enabled = boolean;
        GameObject.Find("SetupLid").GetComponent<Renderer>().enabled = boolean;
        GameObject.Find("SetupLCD").GetComponent<Renderer>().enabled = boolean;
    }
}
