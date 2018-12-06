using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystemScript : MonoBehaviour {

   
    public int gameTime;

    public GameObject panel;
    public Button openButton;
    public Text timeUI;
    int timeStart;
    public Text ampm;
    public Text yearText;
    public Text monthText;
    public Text weekText;
    public bool isOpen;
    CustomerSpawner spawner;
    bool oneTimeOpen = false;
    bool oneTimeClose = false;
    bool oneTimeFrontSpawn = false;
	bool oneTimeBackSpawn = false;
    int yearCount = 1;
    int monthCount = 1;
    int weekCount = 1;
    int dayCount = 1;

	public GameObject frontEmployee;
    public GameObject backEmployee;
	public GameObject craig;
	public bool spawnCraig = false;
	// Use this for initialization
	void Start () {
        isOpen = false;
        spawner = GameObject.Find("Spawnpoint").GetComponent<CustomerSpawner>();
        panel = GameObject.Find("StoreClosed");
        panel.SetActive(false);
        timeStart = (int)Time.time;
        Time.timeScale = 7;
        weekText.text = "1";
        monthText.text = "1";
        yearText.text = "1";

    }

    // Update is called once per frame
    void Update()
    {
        int currentTime = (int)Time.time - timeStart;
        

        if (currentTime % 5 == 0)
            gameTime = currentTime / 5;

		if (!oneTimeBackSpawn)
		{
			if (gameTime == 5)
			{
				Time.timeScale = 1;
				Instantiate(backEmployee, new Vector3(UnityEngine.Random.Range(2, 3), 0, -9.95f), Quaternion.identity);
				oneTimeBackSpawn = true;
			}
		}

		if (!oneTimeFrontSpawn)
        {
			if (gameTime == 6)
            {
                
                Instantiate(frontEmployee, new Vector3(UnityEngine.Random.Range(2, 3),0, -9.95f), Quaternion.identity);
				
				if (spawnCraig == true )
				{
					Instantiate(craig, new Vector3(UnityEngine.Random.Range(2, 3), 0, -9.95f), Quaternion.identity);
				}
                oneTimeFrontSpawn = true;
            }
        }
      
        if (!oneTimeOpen)
        {
            if (gameTime == 7)
            {
                OpenOrClose(true);               
                oneTimeOpen = true;
            }
        }

        if (!oneTimeClose)
        {
            if (gameTime >= 17)
            {
                if (!GameObject.FindGameObjectWithTag("customer") && !GameObject.FindGameObjectWithTag("frontEmployee1") && !GameObject.FindGameObjectWithTag("backEmployee") && !GameObject.FindGameObjectWithTag("frontEmployee2") && GameObject.Find("Spawnpoint").GetComponent<LockingTheDoor>().isLocked == true)
                {
                    OpenOrClose(false);
                    oneTimeClose = true;
                }
            }
        }

        if (gameTime >= 24)
        {
            timeUI.text = "0" + (gameTime - 24).ToString();
        }

        else if (gameTime < 10)
            timeUI.text = "0" + gameTime.ToString();

        else
        {
            timeUI.text = gameTime.ToString();
        }

        if(GameObject.Find("Spawnpoint").GetComponent<LockingTheDoor>().isLocked == true)
        {
            spawner.CancelInvoke();
        }
    }

    //true is open close is false
    
    void OpenOrClose(bool b)
	{ 
		isOpen = b;
        if (isOpen == true)
        {
            spawner.InvokeRepeating("SpawnCustomer", spawner.spawnTime, spawner.spawnRate);          
        }
        else if(isOpen == false)
        {

            dayCount = dayCount + 1;

            if (dayCount % 2 == 0)
            {
                weekCount++;
            }
            else if(weekCount % 4 == 0)
            {
                monthCount++;
				weekCount = 0;
            }

            else if (monthCount % 12 == 0)
            {
                yearCount++;
				yearCount = 0;
            }
            panel.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void OpenButtonPressed()
    {
        timeStart = (int)Time.time;
        panel.SetActive(false);
        oneTimeOpen = false;
        oneTimeClose = false;
        oneTimeFrontSpawn = false;
		oneTimeBackSpawn = false;
        weekText.text = weekCount.ToString();
        monthText.text = monthCount.ToString();
        yearText.text = yearCount.ToString();
        Time.timeScale = 7;
    }


}
