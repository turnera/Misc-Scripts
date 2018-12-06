using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inProgressTriggerScript : MonoBehaviour {

    public Image image;
    public bool inProgressSlot1Open;
    public bool repairComplete;
    public bool inTrigger;

    // Use this for initialization
    void Start () {
        inProgressSlot1Open = true;
        repairComplete = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(repairComplete == true && inTrigger == true)
        {
            PutLaptopOnDoneShelf();           
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        repairComplete = false;

        if (other.tag == "backEmployee" && other.GetComponent<BackEmployeeScript>().hasLaptop == true)
        {
            PutLaptopOnInProgressShelf();
            other.GetComponent<BackEmployeeScript>().hasLaptop = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.tag == "backEmployee" && inProgressSlot1Open == false)
        {
            WorkOnLaptop();
        }

       if (image.fillAmount == 1)
        {
            LaptopReady();
            ShowInProgressLaptop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void PutLaptopOnDoneShelf()
    {
        GameObject.FindGameObjectWithTag("backEmployee").GetComponent<BackEmployeeScript>().MoveToLocationShelf();
        image.GetComponent<Image>().color = Color.white;
        image.fillAmount = 0;
        inProgressSlot1Open = true;
        GameObject.FindGameObjectWithTag("backEmployee").GetComponent<BackEmployeeScript>().hasLaptop = true;
    }

    void PutLaptopOnInProgressShelf()
    {
        inProgressSlot1Open = false;
        GameObject.Find("InProgressLaptop1").GetComponent<Renderer>().enabled = true;
        GameObject.Find("LaptopLidSlot1").GetComponent<Renderer>().enabled = true;
        GameObject.Find("LaptopLCD1").GetComponent<Renderer>().enabled = true;
    }

    void WorkOnLaptop()
    {
        image.fillAmount = image.fillAmount + .05f * Time.deltaTime;
        inTrigger = true;
    }

    void LaptopReady()
    {
        image.GetComponent<Image>().color = Color.green;
        repairComplete = true;
    }

    void ShowInProgressLaptop()
    {
        GameObject.Find("InProgressLaptop1").GetComponent<Renderer>().enabled = false;
        GameObject.Find("LaptopLidSlot1").GetComponent<Renderer>().enabled = false;
        GameObject.Find("LaptopLCD1").GetComponent<Renderer>().enabled = false;
    }
            
}
