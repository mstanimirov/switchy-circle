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

    public bool start = false;

    void OnEnable()
    {

        StartCoroutine("ShowButtons");

    }

    IEnumerator ShowButtons()
    {

        yield return new WaitForSeconds(0.1f);

        timeOpened = (ulong)DateTime.Now.Ticks;
        timerUI.SetActive(true);

        SetTimer();

        yield return new WaitForSeconds(0.1f);

        reviveButton.SetActive(true);

        yield return new WaitForSeconds(1f);

        restartButton.SetActive(true);


    }

    public bool IsTimerReady() {

        ulong diff = ((ulong)DateTime.Now.Ticks - timeOpened);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (msToWait - m) / 1000.0f;

        if (secondsLeft < 0) {

            start = false;
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

        start = true;

    }

    public void Close() {

        timerUI.SetActive(false);
        reviveButton.SetActive(false);
        restartButton.SetActive(false);

        start = false;

    }

}
