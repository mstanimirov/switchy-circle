using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject tipsPanel;
    public GameObject revivePanel;
    public GameObject gameOverPanel;

    private Continue reviveController;
    private GameOverPanel gameOverController;

    public int timesToAd = 3;
    public int timesToRevive = 5;

    void OnEnable()
    {
        //Reset
        reviveController = revivePanel.GetComponent<Continue>();
        gameOverController = gameOverPanel.GetComponent<GameOverPanel>();

        reviveController.Close();
        gameOverController.SetGameOver(false);

        //Show

        timesToAd -= 1;
        timesToRevive -= 1;

        if (timesToRevive < 0 && GameManager.instance.score >= 10 && GameManager.instance.IsAdReady())
        {

            StartCoroutine("ShowRevive");

        }
        else {

            StartCoroutine("ShowGameOver");

        }


    }

    void OnDisable()
    {

        tipsPanel.SetActive(true);
        revivePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    void Update()
    {

        if (reviveController.start) {

            if (reviveController.IsTimerReady()) {

                StartCoroutine("ShowGameOver");

            }

        }

    }

    IEnumerator ShowGameOver() {

        yield return new WaitForSeconds(1f);

        if (timesToAd < 0 && GameManager.instance.IsAdReady())
        {

            GameManager.instance.DisplayAd("video");
            timesToAd = 3;

        }

        gameOverPanel.SetActive(true);
        
        tipsPanel.SetActive(false);
        revivePanel.SetActive(false);

        reviveController.Close();

    }

    IEnumerator ShowRevive()
    {

        yield return new WaitForSeconds(1f);

        revivePanel.SetActive(true);

        tipsPanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    #region Button Functions

    public void Restart()
    {

        gameOverController.SetGameOver(false);
        GameManager.instance.StartGame();

    }

    public void MainMenu()
    {

        gameOverController.SetGameOver(false);
        GameManager.instance.MainMenu();

    }

    public void Revive()
    {

        //Show Ad
        GameManager.instance.RequestRevive();
        timesToAd = 3;
        timesToRevive = 5;

        NoThanks();

    }

    public void NoThanks()
    {

        gameOverPanel.SetActive(true);

        tipsPanel.SetActive(false);
        revivePanel.SetActive(false);

        reviveController.Close();

        timesToRevive = 5;
        
    }

    #endregion

}
