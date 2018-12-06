using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfTriggerScript : MonoBehaviour {

    public bool awaitingSlot1Open;
    public bool awaitingSlot2Open;
    public bool noOpenRoomAvailable;
    public bool doneSlot1Open;
    public bool doneSlot2Open;
    public bool noDoneRoomAvailable;
	public GameObject eventTrigger1;
	public GameObject eventTrigger2;
	EventTriggerScript eScript1;
	EventTriggerScript2 eScript2;

	// Use this for initialization
	void Start () {

        awaitingSlot1Open = true;
        awaitingSlot2Open = true;
        noOpenRoomAvailable = false;
        doneSlot1Open = true;
        doneSlot2Open = true;
        noDoneRoomAvailable = false;
		eScript1 = eventTrigger1.GetComponent<EventTriggerScript>();
		eScript2 = eventTrigger2.GetComponent<EventTriggerScript2>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "frontEmployee1" && eScript1.pickingUp == true) || (other.tag == "frontEmployee2" && eScript2.pickingUp == true))
        {
            if (doneSlot1Open == false)
            {
                GameObject.Find("DoneSlot1").GetComponent<Renderer>().enabled = false;
                doneSlot1Open = true;               
            }

            else
            {
                GameObject.Find("DoneSlot2").GetComponent<Renderer>().enabled = false;
                doneSlot2Open = true;
            }

			if (other.tag == "frontEmployee1")
				other.GetComponent<EmployeeScript>().MoveToLocationCounter();

			else if (other.tag == "frontEmployee2")
				other.GetComponent<EmployeeScript2>().MoveToLocationCounter();

        }

        else if ((other.tag == "frontEmployee1" && eScript1.pickingUp == false) || (other.tag == "frontEmployee2" && eScript2.pickingUp == false))
        {
            FindOpenSlot();

            if (GameObject.Find("InProgressLaptop1").GetComponent<inProgressTriggerScript>().inProgressSlot1Open == true)
                    GameObject.FindGameObjectWithTag("backEmployee").GetComponent<BackEmployeeScript>().MoveToLocationShelf();

			if (other.tag == "frontEmployee1")
				other.GetComponent<EmployeeScript>().MoveToLocationCounter();

			else if (other.tag == "frontEmployee2")
				other.GetComponent<EmployeeScript2>().MoveToLocationCounter();
		}

        else if (other.tag == "backEmployee")
        {

            if (other.GetComponent<BackEmployeeScript>().hasLaptop == true)
            {
                if (doneSlot1Open == true)
                {
                    GameObject.Find("DoneSlot1").GetComponent<Renderer>().enabled = true;
                    other.GetComponent<BackEmployeeScript>().MoveToLocationBench();
                    doneSlot1Open = false;
                    other.GetComponent<BackEmployeeScript>().hasLaptop = false;
                    
                }

                else if (doneSlot2Open == true)
                {
                    GameObject.Find("DoneSlot2").GetComponent<Renderer>().enabled = true;
                    other.GetComponent<BackEmployeeScript>().MoveToLocationBench();
                    other.GetComponent<BackEmployeeScript>().hasLaptop = false;
                    doneSlot2Open = false;
                    noDoneRoomAvailable = false;
                }
                
            }


            if (awaitingSlot1Open == false)
            {
                GameObject.Find("AwaitingSlot1").GetComponent<Renderer>().enabled = false;
                other.GetComponent<BackEmployeeScript>().MoveToLocationBench();
                awaitingSlot1Open = true;
                other.GetComponent<BackEmployeeScript>().hasLaptop = true;

            }

            else if (awaitingSlot2Open == false)
            {
                GameObject.Find("AwaitingSlot2").GetComponent<Renderer>().enabled = false;
                other.GetComponent<BackEmployeeScript>().MoveToLocationBench();
                awaitingSlot2Open = true;
                other.GetComponent<BackEmployeeScript>().hasLaptop = true;
            }

            else
            {
                other.GetComponent<BackEmployeeScript>().MoveToLocationBench();
            }

            

        }
    }
    public void FindOpenSlot()
    {
        if(awaitingSlot1Open == true)
        {
            GameObject.Find("AwaitingSlot1").GetComponent<Renderer>().enabled = true;
            awaitingSlot1Open = false;
        }

        else if(awaitingSlot2Open == true)
        {
            GameObject.Find("AwaitingSlot2").GetComponent<Renderer>().enabled = true;
            awaitingSlot2Open = false;
            noOpenRoomAvailable = true;

        }

    }

}
