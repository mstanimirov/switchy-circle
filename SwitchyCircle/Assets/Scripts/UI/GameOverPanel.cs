﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverPanel : MonoBehaviour {

    public GameObject shopButton;
    public GameObject restartButton;
    public GameObject facebookButton;
    public GameObject leaderboardButton;

    public GameObject scorePanel;
    
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI currentScoreUI;

    public List<Color> colors = new List<Color>();

    void OnEnable()
    {

        Image scoreImage = scorePanel.GetComponent<Image>();
        scoreImage.color = colors[GameManager.instance.handColorIndex];

        StartCoroutine("ShowButtons");

    }

    IEnumerator ShowButtons() {

        yield return new WaitForSeconds(0.1f);

        scorePanel.SetActive(true);
        restartButton.SetActive(true);
        highScoreUI.text = "BEST: " + GameManager.instance.highScore;
        currentScoreUI.text = GameManager.instance.score.ToString();

        yield return new WaitForSeconds(0.1f);

        shopButton.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        facebookButton.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        leaderboardButton.SetActive(true);


    }

    public void SetGameOver(bool state)
    {

        scorePanel.SetActive(state);
        shopButton.SetActive(state);
        restartButton.SetActive(state);
        facebookButton.SetActive(state);
        leaderboardButton.SetActive(state);

        if(!state)
            highScoreUI.text = "";
            
    }

}
