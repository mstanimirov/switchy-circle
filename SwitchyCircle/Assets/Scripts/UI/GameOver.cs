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

    void Start()
    {

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void OnEnable()
    {

        gameOverPanel.SetActive(true);
        tipsPanel.SetActive(false);

    }

    void OnDisable()
    {

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void Update()
    {

        

    }

    void HideTips()
    {

        tipsPanel.SetActive(false);

    }

    void ShowGameOver() {

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

        revivePanel.SetActive(false);

    }

    public void Revive()
    {
        reviveController.SetTimer();

        GameManager.instance.RequestRevive();

        //NoThanks();

    }

    public void ShowAd() {

        GameManager.instance.DisplayAd("video");

    }

    public void NoThanks() {

        ShowGameOver();
        HideRevive();

    }

}
