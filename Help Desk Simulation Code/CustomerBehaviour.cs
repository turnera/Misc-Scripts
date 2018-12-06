using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerBehaviour : MonoBehaviour {

    public CustomerSpawner CustomerSpawner;
    public NavMeshAgent agent;

    public GameObject chassy;
    public GameObject lid;
    public Renderer rendLid;
    public Renderer rendChassy;
    public GameObject chassy2;
    public GameObject lid2;
    public Renderer rendLid2;
    public Renderer rendChassy2;

    public Text cText;
    
    public bool isCalled = false;
    public bool customerArrived = true;
    Vector3 counter1;
    Vector3 counter2;
    Vector3 locationDone;
    Vector3 locationTable;
	CounterTrigger1Script eTrigger1;
	CounterTrigger2Script eTrigger2;
    int eChoice;
	bool oneTimeMove = false;

    // Use this for initialization

    void Start () {

        chassy = GameObject.Find("LaptopChassy");
        lid = GameObject.Find("LaptopLid");
        rendLid = lid.GetComponent<Renderer>();
        rendChassy = chassy.GetComponent<Renderer>();
		chassy2 = GameObject.Find("LaptopChassy2");
		lid2 = GameObject.Find("LaptopLid2");
		rendLid2 = lid2.GetComponent<Renderer>();
		rendChassy2 = chassy2.GetComponent<Renderer>();

		eTrigger1 = GameObject.Find("CounterTrigger1").GetComponent<CounterTrigger1Script>();
		eTrigger2 = GameObject.Find("CounterTrigger2").GetComponent<CounterTrigger2Script>();

        counter1 = GameObject.Find("EventTrigger1").transform.position;
		counter2 = GameObject.Find("EventTrigger2").transform.position;

        locationDone = GameObject.Find("Exit").transform.position;
        locationTable = GameObject.Find("CustomerSpot").transform.position;
        StartCoroutine(CChangeText("Hi!", 2));

		if(isCalled)
			ApproachCounter();
	}


    // Update is called once per frame
    void Update()
    {
		//if both employees are available and the customer is called, choose an employee at random to approach
		if (oneTimeMove == false && isCalled == true)
		{
			ApproachCounter();
			oneTimeMove = true;
		}
		
		
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {                   
                    customerArrived = true;
					oneTimeMove = false;
                }
            }

        }
    }

	void ApproachCounter()
	{
		if (eTrigger1.employee1IsReady == true && eTrigger2.employee2IsReady == true)
		{
			eChoice = Random.Range(1, 3);
			MoveToLocationCounter(eChoice);
		}

		else if (eTrigger1.employee1IsReady == true && eTrigger2.employee2IsReady == false)
		{
			MoveToLocationCounter(1);
		}

		else if (eTrigger1.employee1IsReady == false && eTrigger2.employee2IsReady == true)
		{
			MoveToLocationCounter(2);
		}
	}

    void MoveToLocationCounter(int choice)
    {
        if (choice == 1)
        agent.SetDestination(counter1);

		if (choice == 2)
		agent.SetDestination(counter2);
        
    }

    public void MoveToLocationTable()
    {       
        agent.SetDestination(locationTable);
    }

    public void MoveToLocationDone()
    {
        if (agent != null)
        {
            agent.SetDestination(locationDone);
            StartCoroutine(CChangeText("Yay! Thank you!", 2));
        }
    }

    public IEnumerator CChangeText(string newText, float time)
    {
        cText.text = newText;
        yield return new WaitForSeconds(time);
        cText.text = "";

    }
}

    

