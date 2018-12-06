using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMScript : MonoBehaviour {
    GameObject fScreen;
    GameObject iScreen;
    GameObject sScreen;
    GameObject mScreen;
    GameObject pScreen;
    GameObject cScreen;
    GameObject bScreen;
    // Use this for initialization
    void Start() {


        fScreen = GameObject.Find("FirstScreen");
        iScreen = GameObject.Find("eInfoScreen");
        sScreen = GameObject.Find("StatisticsScreen");
        mScreen = GameObject.Find("MeScreen");
        pScreen = GameObject.Find("PurchaseScreen");
        cScreen = GameObject.Find("CompetitionScreen");
        bScreen = GameObject.Find("BusinessStuffScreen");

        //Loads all the in game menu objects then disables them in the first frame so they can be set as active or inactive.

        iScreen.SetActive(false);
        sScreen.SetActive(false);
        mScreen.SetActive(false);
        pScreen.SetActive(false);
        cScreen.SetActive(false);
        bScreen.SetActive(false);

        GameObject.Find("GameMenu").SetActive(false);
    }
	
	// Update is called once per frame

    public void eInfoButton()
    {
        fScreen.SetActive(false);
        iScreen.SetActive(true);
    }

    public void StatisticsButton()
    {
        fScreen.SetActive(false);
        sScreen.SetActive(true);
    }

    public void MeButton()
    {
        fScreen.SetActive(false);
        mScreen.SetActive(true);
    }

    public void PurchaseButton()
    {
        fScreen.SetActive(false);
        pScreen.SetActive(true);
    }

    public void CompetitionButton()
    {
        fScreen.SetActive(false);
        cScreen.SetActive(true);
    }

    public void BusinessStuffButton()
    {
        fScreen.SetActive(false);
        bScreen.SetActive(true);
    }

    public void BackButton()
    {
        fScreen.SetActive(true);
        iScreen.SetActive(false);
        sScreen.SetActive(false);
        mScreen.SetActive(false);
        pScreen.SetActive(false);
        cScreen.SetActive(false);
        bScreen.SetActive(false);       
    }

    public void ChangeEmployees()
    {

    }

}
