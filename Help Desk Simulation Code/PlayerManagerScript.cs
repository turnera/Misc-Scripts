using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

	bool inMenu;
    public GameObject hud;
    public GameObject igm;
	public GameObject employeeWindow;
	
    // Use this for initialization
	void Start () {
        inMenu = false;
    }
	
	// Update is called once per frame
	void Update () {

        //if player presses tab, opens up game menu TODO add a button to open up game menu as well for mobile support.
        if (Input.GetKeyDown(KeyCode.Tab) && inMenu == false)
        {
            OpenGameMenu();
            inMenu = true;
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && inMenu == true)
        {
            CloseGameMenu();
            inMenu = false;
        }
	}

	void OpenGameMenu()
	{
		igm.SetActive(true);
	}

	void CloseGameMenu()
	{
		igm.SetActive(false);
	}

	public void OpenEmployeeWindow()
    {
		if(employeeWindow)
		employeeWindow.SetActive(true);

		else
		employeeWindow.SetActive(false);
	}

	public void AddEmployeeBtn()
	{
		GameObject.Find("TimeSystem").GetComponent<TimeSystemScript>().spawnCraig = true;
	}
}
