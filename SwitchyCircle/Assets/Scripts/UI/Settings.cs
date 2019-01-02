using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class Settings : MonoBehaviour
{

    public ToggleController soundSwitch;
    public ToggleController musicSwitch;

    public Button adFreeUI;
    public Button adFreeReviveUI;

    public TextMeshProUGUI adFreePrice;
    public TextMeshProUGUI adFreeRevivePrice;

    void OnEnable() {

        soundSwitch.Turn(PlayerPrefs.GetInt("sound") == 0 ? true : false);

        adFreeUI.interactable = !GameManager.instance.adFree;
        adFreeReviveUI.interactable = !GameManager.instance.adFreeRevive;        

    }

    void Update () {

        if (soundSwitch.switching)
        {

            soundSwitch.Toggle(soundSwitch.isOn, "sound");

        }

        if (Input.GetKeyDown("escape"))
        {

            GameManager.instance.MainMenu();

        }

        if (GameManager.instance.adFree)
        {

            adFreeUI.interactable = false;
            adFreePrice.text = "PURCHASED";

            if (GameManager.instance.adFreeRevive)
            {

                adFreeReviveUI.interactable = false;
                adFreeRevivePrice.text = "PURCHASED";

            }
            else
            {

                adFreeRevivePrice.text = IAPManager.instance.GetPrice(IAPManager.PRODUCT_AD_FREE_REVIVE);

            }

        }
        else {

            adFreePrice.text = IAPManager.instance.GetPrice(IAPManager.PRODUCT_AD_FREE);
            adFreeRevivePrice.text = IAPManager.instance.GetPrice(IAPManager.PRODUCT_AD_FREE_REVIVE);
            
        }

    }

    public void purchauseAdFree() {

        IAPManager.instance.BuyAdFree();

    }

    public void purchauseAdFreeRevive()
    {

        IAPManager.instance.BuyAdFreeRevive();

    }

}
