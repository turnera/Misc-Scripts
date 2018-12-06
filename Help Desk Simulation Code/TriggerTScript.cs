using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTScript : MonoBehaviour {
    // Use this for initialization

    public Text tText;
	public Text tText2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "customer")
        {
            CustomerBehaviour customerScript = other.GetComponent<CustomerBehaviour>();
            customerScript.isCalled = true;
            customerScript.rendChassy.enabled = false;
            customerScript.rendLid.enabled = false;
            StartCoroutine(TChangeText("Hello!", 2.5f));
        }

        else if (other.tag == "frontEmployee1")
        {
            tText = other.GetComponentInChildren<Text>();
        }

		else if (other.tag == "frontEmployee2")
		{
			tText2 = other.GetComponentInChildren<Text>();
		}
    }


    public IEnumerator TChangeText(string newText, float time)
    {
        tText.text = newText;
        yield return new WaitForSeconds(time);
        tText.text = "";
        
    }

	public IEnumerator TChangeText2(string newText, float time)
	{
		tText2.text = newText;
		yield return new WaitForSeconds(time);
		tText2.text = "";

	}


}
