﻿using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public GameObject[] toSwitches;

	public bool isOn;
	public string specialDoll;


	void Start () {
		isOn = false;
		specialDoll = "normal";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void switchTriggerOn(){
		isOn = true;
		foreach (GameObject toSwitch in toSwitches)
			toSwitch.GetComponent<receiveSignal>().activate ();
	}

	public void switchTriggerOff(){
		isOn = false;
		foreach (GameObject toSwitch in toSwitches)
			toSwitch.GetComponent<receiveSignal>().deactivate ();
	}

	public void switchTrigger(){
		foreach (GameObject toSwitch in toSwitches)
			toSwitch.GetComponent<receiveSignal> ().activate ();
	}
}
