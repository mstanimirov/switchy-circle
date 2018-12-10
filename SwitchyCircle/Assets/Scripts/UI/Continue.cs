using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour {

    public GameObject timerUI;
    public GameObject reviveButton;
    public GameObject restartButton;

    public GameObject       tipsPanel;
    public SliderController timer;

    public float setMsToWait;

    private float msToWait;
    private ulong timeOpened;

    void Start () {

        tipsPanel.SetActive(true);
        timerUI.SetActive(false);
        reviveButton.SetActive(false);
        restartButton.SetActive(false);

    }

    void OnEnable()
    {

        Invoke("HideTipsPanel", 1f);
        Invoke("ShowTimer", 1.1f);
        Invoke("ShowReviveBtn", 1.2f);
        Invoke("ShowResetBtn", 3f);

    }

    void OnDisable()
    {

        tipsPanel.SetActive(true);
        timerUI.SetActive(false);
        reviveButton.SetActive(false);
        restartButton.SetActive(false);

    }

    void Update()
    {

          

    }

    public bool IsTimerReady() {

        ulong diff = ((ulong)DateTime.Now.Ticks - timeOpened);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (msToWait - m) / 1000.0f;

        if (secondsLeft < 0) {
            
            return true;

        }

        timer.value = secondsLeft;

        return false;

    }

    void ResetTimer() {

        msToWait = setMsToWait;
        timeOpened = (ulong)DateTime.Now.Ticks;
        
        timer.minValue = 0;
        timer.maxValue = msToWait / 1000.0f;
        timer.value = msToWait / 1000.0f;

    }

    void ShowTimer()
    {

        ResetTimer();
        timerUI.SetActive(true);

    }

    void ShowReviveBtn()
    {

        reviveButton.SetActive(true);

    }

    void ShowResetBtn()
    {

        restartButton.SetActive(true);

    }

    void HideTipsPanel()
    {

        tipsPanel.SetActive(false);

    }

}
