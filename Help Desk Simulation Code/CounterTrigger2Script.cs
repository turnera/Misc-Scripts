using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTrigger2Script : MonoBehaviour
{

    public bool employee2IsReady;

    // Use this for initialization

    //employee is ready lets us control the crowd so they don't rush the counter when the employee isn't at the counter
    void Start()
    {
        employee2IsReady = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        employee2IsReady = true;


        //if customer is picking up their PC, sets picking up to false and makes the customer leave
        if (GameObject.Find("EventTrigger2").GetComponent<EventTriggerScript2>().pickingUp == true)
        {
            GameObject.Find("EventTrigger2").GetComponent<EventTriggerScript2>().pickingUp = false;
            GameObject.Find("EventTrigger2").GetComponent<EventTriggerScript2>().canLeave = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        employee2IsReady = false;
    }
}
