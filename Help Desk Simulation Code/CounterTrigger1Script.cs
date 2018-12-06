using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTrigger1Script : MonoBehaviour {

    public bool employee1IsReady;
	public StatsController counter1Stats;

    // Use this for initialization

    //employee is ready lets us control the crowd so they don't rush the counter when the employee isn't at the counter
    void Start () {
        employee1IsReady = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        employee1IsReady = true;
		counter1Stats = other.GetComponent<StatsController>();


        //if customer is picking up their PC, sets picking up to false and makes the customer leave
        if (GameObject.Find("EventTrigger1").GetComponent<EventTriggerScript>().pickingUp == true)
        {
            GameObject.Find("EventTrigger1").GetComponent<EventTriggerScript>().pickingUp = false;
            GameObject.Find("EventTrigger1").GetComponent<EventTriggerScript>().canLeave = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        employee1IsReady = false;
    }
}
