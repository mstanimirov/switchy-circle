using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public ToggleController soundSwitch;
    public ToggleController musicSwitch;
    
	void Start () {
		


	}

	void Update () {

        if (soundSwitch.switching)
        {

            soundSwitch.Toggle(soundSwitch.isOn, "sound");

        }

    }

}
