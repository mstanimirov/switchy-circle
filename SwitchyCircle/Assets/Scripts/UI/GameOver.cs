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

    public int setTimesToRevive;

    private int timesToRevive = 3;

    void Start()
    {

        timesToRevive = setTimesToRevive;

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void OnEnable()
    {

        if (timesToRevive < 1)
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

        timesToRevive -= 1;
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

        //GameManager.instance.Revive();

        NoThanks();

    }

    public void NoThanks() {

        ShowGameOver();
        HideRevive();

    }

}
