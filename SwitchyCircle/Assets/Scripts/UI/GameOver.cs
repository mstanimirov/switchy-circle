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

    public int setTimesToRevive = 3;

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

            Invoke("ShowRevive", 0.1f);

        }
        else {

            Invoke("ShowGameOver", 0.1f);

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
                Invoke("HideRevive", 1f);

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

        GameManager.instance.Revive();

    }

    public void NoThanks() {

        ShowGameOver();
        Invoke("HideRevive", 1f);

    }

}
