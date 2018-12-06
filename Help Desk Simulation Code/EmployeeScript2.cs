using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeScript2 : MonoBehaviour
{

	NavMeshAgent eAgent;
	Vector3 locationDone;
	Vector3 workStation;


	// Use this for initialization
	void Start()
	{
		eAgent = gameObject.GetComponent<NavMeshAgent>();
		locationDone = GameObject.Find("Exit").transform.position;
		eAgent.SetDestination(GameObject.Find("CounterTrigger2").transform.position);
	}

	// Update is called once per frame
	void Update()
	{
		if (!GameObject.FindGameObjectWithTag("customer") && GameObject.Find("TimeSystem").GetComponent<TimeSystemScript>().gameTime >= 17)
		{
			MoveToLocationDone();
		}
	}


	public void MoveToLocationShelf()
	{
		Vector3 locationShelf = GameObject.Find("ShelfTrigger").transform.position;
		eAgent.SetDestination(locationShelf);

	}


	public void MoveToLocationCounter()
	{
		Vector3 locationCounter = GameObject.Find("CounterTrigger2").transform.position;
		eAgent.SetDestination(locationCounter);

	}

	public void MoveToLocationTable()
	{
		Vector3 LocationTable = GameObject.Find("EmployeeSpot").transform.position;
		eAgent.SetDestination(LocationTable);
	}

	void MoveToLocationDone()
	{
		eAgent.SetDestination(locationDone);
	}






}
