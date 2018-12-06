using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockingTheDoor : MonoBehaviour {

    public bool isLocked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "backEmployee" && GameObject.Find("TimeSystem").GetComponent<TimeSystemScript>().gameTime >= 17)
        {
            isLocked = true;
            other.GetComponent<BackEmployeeScript>().bAgent.SetDestination(GameObject.Find("Exit").transform.position);
        }

        else
        {
            isLocked = false;
        }

    }
}
