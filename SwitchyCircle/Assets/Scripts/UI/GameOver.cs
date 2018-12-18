using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject tipsPanel;
    public GameObject revivePanel;
    public GameObject gameOverPanel;

    public Continue reviveController;

    public int setTimesToAd;
    public int setTimesToRevive;

    public int timesToAd = 3;
    public int timesToRevive = 5;

    void Start()
    {

        timesToAd = setTimesToAd;
        timesToRevive = setTimesToRevive;

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void OnEnable()
    {

        if (timesToRevive < 1 && GameManager.instance.score > 9)
        {

            Invoke("ShowRevive", 1f);

        }
        else {

            Invoke("ShowGameOver", 1f);

        }

        Invoke("HideTips", 1f);

    }

    void OnDisable()
    {

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void Update()
    {

        if (timesToRevive < 1 && reviveController.timerUI.activeInHierarchy) {

            if (reviveController.IsTimerReady()) {

                ShowGameOver();
                HideRevive();

            }

        }

    }

    void HideTips()
    {

        tipsPanel.SetActive(false);

    }

    void ShowGameOver() {

        timesToAd -= 1;
        timesToRevive -= 1;

        if (timesToAd < 1) {

            ShowAd();

        }

        gameOverPanel.SetActive(true);

        Debug.Log(timesToRevive);

    }

    void ShowRevive()
    {

        revivePanel.SetActive(true);

    }

    void HideGameOver()
    {

        gameOverPanel.SetActive(false);

    }

    void HideRevive()
    {

        timesToRevive = setTimesToRevive;
        revivePanel.SetActive(false);

    }

    public void Revive()
    {
        reviveController.SetTimer();
        timesToRevive = setTimesToRevive;

        GameManager.instance.RequestRevive();

        //NoThanks();

    }

    public void ShowAd() {

        timesToAd = setTimesToAd;

        GameManager.instance.DisplayAd();

    }

    public void NoThanks() {

        ShowGameOver();
        HideRevive();

    }

}
