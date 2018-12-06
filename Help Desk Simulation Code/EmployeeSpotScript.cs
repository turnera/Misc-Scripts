using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeSpotScript : MonoBehaviour {


    public bool eReady;
    public float setupSkill = 3;
    EmployeeScript eScript1;
    EmployeeScript2 eScript2;
	bool e1Here;
	bool e2Here;

    // Use this for initialization
    void Start () {
        eReady = false;
		e1Here = false;
		e2Here = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(eReady == true && GameObject.Find("CustomerSpot").GetComponent<CustomerSpotScript>().cReady == true)
        {         
            Invoke("PerformSetup", setupSkill);
        }

	}

    private void OnTriggerEnter(Collider other)
    {   
        eReady = true;

		if (other.tag == "frontEmployee1")
		{
			eScript1 = other.GetComponent<EmployeeScript>();
			e1Here = true;
		}

		else if (other.tag == "frontEmployee2")
		{
			eScript2 = other.GetComponent<EmployeeScript2>();
			e2Here = true;
		}
    }

    private void OnTriggerExit(Collider other)
    {       
        eReady = false;
		e1Here = false;
		e2Here = false;
    }

    public void PerformSetup()
    {
		if (e1Here == true)
		{
			StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("A new Device huh...", 2));
			StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("Whew! Okay, you're all set to go.", 2));
			GameObject.Find("EventTrigger1").GetComponent<EventTriggerScript>().canLeave = true;
			eScript1.MoveToLocationCounter();
		}

		else if (e2Here == true)
		{
			StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText2("A new Device huh...", 2));
			StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText2("Whew! Okay, you're all set to go.", 2));
			GameObject.Find("EventTrigger2").GetComponent<EventTriggerScript2>().canLeave = true;
			eScript2.MoveToLocationCounter();
		}

		
    }

}
