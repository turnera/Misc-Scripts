using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EventTriggerScript2 : MonoBehaviour
{


	public CustomerBehaviour bScript;
	GameObject customerInTrigger;
	public bool canLeave;
	float quickFixSkill;
	public bool pickingUp;

	public GameObject GreetingTrigger;
	TriggerTScript TTScript;

	// Use this for initialization
	void Start()
	{
		canLeave = false;
		pickingUp = false;
		TTScript = GreetingTrigger.GetComponent<TriggerTScript>();
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
		if (other.tag == "customer")
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
					StartCoroutine(TTScript.TChangeText2("Let's see...", 2));
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
		StartCoroutine(TTScript.TChangeText2("Done!", 2));
		CashManager.cash += 50;
		GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
		canLeave = true;
	}

	public void LongFix()
	{
		if (GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().doneSlot1Open == false || GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().doneSlot2Open == false)
		{
			StartCoroutine(TTScript.TChangeText2("picking up? Let me get that for you", 2));
			pickingUp = true;
			GameObject.FindGameObjectWithTag("frontEmployee2").GetComponent<EmployeeScript2>().MoveToLocationShelf();
		}


		else if (GameObject.Find("ShelfTrigger").GetComponent<ShelfTriggerScript>().noOpenRoomAvailable == false)
		{
			StartCoroutine(TTScript.TChangeText2("This looks like a serious issue...", 2));
			StartCoroutine(TTScript.TChangeText2("You'll have to drop it off here...", 2));
			canLeave = true;
			CashManager.cash += 100;
			GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
			GameObject.FindGameObjectWithTag("frontEmployee2").GetComponent<EmployeeScript2>().MoveToLocationShelf();

		}



		else
		{
			StartCoroutine(TTScript.TChangeText2("I'm sorry, it looks like we have too many devices to check in any more right now.", 2));
			canLeave = true;
		}

	}

	public void NewDeviceSetup()
	{
		StartCoroutine(TTScript.TChangeText2("If you'd like help setting up your device, meet me at the table over there.", 2));
		GameObject.FindGameObjectWithTag("frontEmployee2").GetComponent<EmployeeScript2>().MoveToLocationTable();
		CashManager.cash += 75;
		GameObject.Find("CashValue").GetComponent<Text>().text = CashManager.cash.ToString();
		bScript.MoveToLocationTable();
	}

	void DisplayLaptopOnCounter(bool boolean)
	{
		bScript.rendChassy2.enabled = boolean;
		bScript.rendLid2.enabled = boolean;
	}

	float GetQuickFixSkill()
	{
		StatsController counter2Stats = GameObject.FindGameObjectWithTag("frontEmployee2").GetComponent<StatsController>();
		return counter2Stats.stats.QuickFixSkill;
	}

}

