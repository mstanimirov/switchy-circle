using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;

public class GameOverPanel : MonoBehaviour {

    public GameObject shopButton;
    public GameObject restartButton;
    public GameObject facebookButton;
    public GameObject leaderboardButton;

    public GameObject scorePanel;
    
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI currentScoreUI;

    public List<Color> colors = new List<Color>();

    private Texture2D shareTexture;

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

        if (GameManager.instance.score > GameManager.instance.highScore)
        {

            highScoreUI.text = "NEW BEST!";

            GameManager.instance.highScore = GameManager.instance.score;
            GameManager.instance.ReportScore(GameManager.instance.score);

            GameManager.instance.SaveData();
            
        }
        else {

            highScoreUI.text = "BEST: " + GameManager.instance.highScore;

        }

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

    public void ShareHighScore() {

        StartCoroutine("CaptureScreenshot");

    }

    IEnumerator CaptureScreenshot()
    {

        yield return new WaitForEndOfFrame();

        shareTexture = Sharing.CaptureScreenshot();

        Sharing.ShareTexture2D(shareTexture, "screenshot", "Check out my new score on Switchy Circle! Can you beat me? https://play.google.com/store/apps/details?id=com.stanimirov.switchycircle");

    }

}
