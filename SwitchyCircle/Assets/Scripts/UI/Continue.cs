using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour {

    public GameObject timerUI;
    public GameObject reviveButton;
    public GameObject restartButton;

    public SliderController timer;

    public float setMsToWait = 5000.0f;
    public float msToWait = 5f;

    public ulong timeOpened;

    void OnEnable()
    {

        Invoke("ShowTimer", 1f);
        Invoke("ShowReviveBtn", 1.1f);
        Invoke("ShowResetBtn", 3f);

    }

    void OnDisable()
    {

        timerUI.SetActive(false);
        reviveButton.SetActive(false);
        restartButton.SetActive(false);

    }

    void Start()
    {

        SetTimer();

        timerUI.SetActive(false);
        reviveButton.SetActive(false);
        restartButton.SetActive(false);

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

    public void SetTimer() {

        msToWait = setMsToWait;

        timer.minValue = 0;
        timer.maxValue = msToWait / 1000.0f;
        timer.value = msToWait / 1000.0f;

    }

    void ShowTimer() {

        timeOpened = (ulong)DateTime.Now.Ticks;

        timerUI.SetActive(true);

    }

    void ShowReviveBtn() {

        reviveButton.SetActive(true);

    }

    void ShowResetBtn() {

        restartButton.SetActive(true);

    }

}
