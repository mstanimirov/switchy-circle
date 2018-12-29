using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public ToggleController soundSwitch;
    public ToggleController musicSwitch;

    public Button adFreeUI;
    public Button adFreeReviveUI;
    
	void Awake () {

        soundSwitch.Turn(PlayerPrefs.GetInt("sound") == 0 ? true : false);

        adFreeUI.interactable = !GameManager.instance.adFree;
        adFreeReviveUI.interactable = !GameManager.instance.adFreeRevive;

    }

    void Update () {
        
        if (soundSwitch.switching)
        {

            soundSwitch.Toggle(soundSwitch.isOn, "sound");

        }

    }

    public void purchauseAdFree() {

        if (true) { //Payment check

            GameManager.instance.adFree = true;

            GameManager.instance.SaveData();

            Debug.Log("Ads removed");
            adFreeUI.interactable = false;

        }

    }

    public void purchauseAdFreeRevive()
    {

        if (true)
        { //Payment check

            GameManager.instance.adFree = true;
            GameManager.instance.adFreeRevive = true;

            GameManager.instance.SaveData();

            Debug.Log("Add free revive");
            adFreeUI.interactable = false;
            adFreeReviveUI.interactable = false;

        }

    }

}
