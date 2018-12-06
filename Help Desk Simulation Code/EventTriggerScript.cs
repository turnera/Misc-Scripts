using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EventTriggerScript : MonoBehaviour
{

   
    public CustomerBehaviour bScript;
    GameObject customerInTrigger;
    public bool canLeave;
    float quickFixSkill; //how long in s it takes to complete a quickfix
    public bool pickingUp;
	

    // Use this for initialization
    void Start()
    {        
        canLeave = false;
        pickingUp = false;
		CashManager.cash = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (canLeave == true)
        {           
            bScript.MoveToLocationDone();
            canLeave = false;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "customer")
        {
            bScript = other.GetComponent<CustomerBehaviour>();
            bScript.isCalled = false;
            DisplayLaptopOnCounter(true);
            chooseEvent();
        }
        

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "customer")
        {
            DisplayLaptopOnCounter(false);
        }
    }

    

    public void chooseEvent() // Randomly chooses between QuickFix, LongFixHardware, LongFixSoftware, and NewDeviceSetup 
    {
        int chosenEvent = Random.Range(0, 3);
        

        switch (chosenEvent)
        {
            case 0: //QuickFix
                {
					quickFixSkill = GetQuickFixSkill();
					StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("Let's see...", 2));
					Invoke("QuickFix", quickFixSkill);
                }

                break;

            case 1: //LongFixHardware
                {
                    LongFix();
                }

                break;

            case 2: //NewDeviceSetup
                {
                    NewDeviceSetup();
                }
                break;
        }
    }

    public void QuickFix()
    {                
        StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("Done!", 2));
		CashManager.cash += 50;
        GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
		canLeave = true;
    }

    public void LongFix()
    {
        if (GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().doneSlot1Open == false || GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().doneSlot2Open == false)
        {
            StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("picking up? Let me get that for you", 2));
            pickingUp = true;
            GameObject.FindGameObjectWithTag("frontEmployee1").GetComponent<EmployeeScript>().MoveToLocationShelf();
        }


        else if (GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().noOpenRoomAvailable == false)
        {
            StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("This looks like a serious issue...", 2));
            StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("You'll have to drop it off here...", 2));
            canLeave = true;
			CashManager.cash += 100;
            GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
            GameObject.FindGameObjectWithTag("frontEmployee1").GetComponent<EmployeeScript>().MoveToLocationShelf();
            
        }

        

        else
        {
            StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("I'm sorry, it looks like we have too many devices to check in any more right now.", 2));
            canLeave = true;
        }

    }

    public void NewDeviceSetup()
    {
        StartCoroutine(GameObject.Find("GreetingTriggerT").GetComponent<TriggerTScript>().TChangeText("If you'd like help setting up your device, meet me at the table over there.", 2));
        GameObject.FindGameObjectWithTag("frontEmployee1").GetComponent<EmployeeScript>().MoveToLocationTable();
		CashManager.cash += 75;
        GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
        bScript.MoveToLocationTable();
    }

    void DisplayLaptopOnCounter(bool boolean)
    {
        bScript.rendChassy.enabled = boolean;
        bScript.rendLid.enabled = boolean;
    }

	float GetQuickFixSkill()
	{
		StatsController counter1Stats = GameObject.FindGameObjectWithTag("frontEmployee1").GetComponent<StatsController>();
		return counter1Stats.stats.QuickFixSkill;
	}

}

