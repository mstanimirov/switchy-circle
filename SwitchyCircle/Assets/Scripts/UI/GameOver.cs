using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject shopButton;
    public GameObject restartButton;
    public GameObject facebookButton;
    public GameObject leaderboardButton;

    public GameObject tipsPanel;
    public GameObject scorePanel;

    public TextMeshProUGUI  highScoreUI;
    public TextMeshProUGUI  currentScoreUI;

    public List<Color> colors = new List<Color>();

    void Start()
    {

        tipsPanel.SetActive(true);
        scorePanel.SetActive(false);
        shopButton.SetActive(false);
        restartButton.SetActive(false);
        facebookButton.SetActive(false);
        leaderboardButton.SetActive(false);
        highScoreUI.text = "";
        currentScoreUI.text = "";

    }

    void OnEnable()
    {

        Image scoreImage = scorePanel.GetComponent<Image>();

        scoreImage.color = colors[GameManager.instance.handColorIndex];

        Invoke("HideTipsPanel", 1f);
        Invoke("ShowResetBtn", 1f);
        Invoke("ShowShopBtn", 1.1f);
        Invoke("ShowSocialBtn", 1.2f);
        Invoke("ShowLeaderboardsBtn", 1.2f);

    }

    void OnDisable()
    {

        tipsPanel.SetActive(true);
        scorePanel.SetActive(false);
        shopButton.SetActive(false);
        restartButton.SetActive(false);
        facebookButton.SetActive(false);
        leaderboardButton.SetActive(false);
        highScoreUI.text = "";

    }

    void HideTipsPanel() {

        tipsPanel.SetActive(false);

    }

    void ShowResetBtn() {

        scorePanel.SetActive(true);
        restartButton.SetActive(true);
        highScoreUI.text = "BEST: " + GameManager.instance.highScore;
        currentScoreUI.text = GameManager.instance.score.ToString();

    }

    void ShowShopBtn()
    {

        shopButton.SetActive(true);

    }

    void ShowSocialBtn()
    {

        facebookButton.SetActive(true);

    }

    void ShowLeaderboardsBtn()
    {

        leaderboardButton.SetActive(true);

    }
    
}
