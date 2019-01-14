using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using TMPro;

public class GameOver : MonoBehaviour {

    public GameObject tipsPanel;
    public GameObject revivePanel;
    public GameObject gameOverPanel;

    private Continue reviveController;
    private GameOverPanel gameOverController;

    private int timesToAd = 6;
    private int timesToRevive = 1;

    public int setTimesToAd = 6;
    public int setTimesToRevive = 1;

    void Awake()
    {

        timesToAd = setTimesToAd;
        timesToRevive = setTimesToRevive;

    }

    void OnEnable()
    {
        //Reset
        reviveController = revivePanel.GetComponent<Continue>();
        gameOverController = gameOverPanel.GetComponent<GameOverPanel>();

        reviveController.Close();
        gameOverController.SetGameOver(false);

        //Show
        if (!GameManager.instance.adFree) {

            timesToAd -= 1;

        }

        timesToRevive -= 1;

        if (timesToRevive < 0 && GameManager.instance.score >= 5 && GameManager.instance.IsAdReady())
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

        if (Input.GetKeyDown("escape"))
        {

            if (gameOverPanel.activeSelf)
            {

                if (GameManager.instance.gameState != GameManager.GameState.DailyGift)
                {

                    GameManager.instance.MainMenu();

                }

            }
            else if(revivePanel.activeSelf){

                NoThanks();

            }

        }

        if (GameManager.instance.gameState == GameManager.GameState.DailyGift)
        {

            if (Input.GetMouseButtonDown(0))
            {

                GameManager.instance.ChangeGameState(GameManager.GameState.GameOver);

            }

            if (Input.GetKeyDown("escape"))
            {

                GameManager.instance.ChangeGameState(GameManager.GameState.GameOver);

            }

        }

    }

    IEnumerator ShowGameOver() {

        yield return new WaitForSeconds(1f);

        if (timesToAd < 0 && GameManager.instance.IsAdReady())
        {

            GameManager.instance.DisplayAd("video");
            timesToAd = setTimesToAd;

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
        if (GameManager.instance.adFreeRevive)
        {

            //Change timer icon*

            GameManager.instance.Revive();

        }
        else {

            GameManager.instance.RequestRevive();
            
        }

        timesToAd = setTimesToAd;
        timesToRevive = setTimesToRevive;

        NoThanks();

    }

    public void NoThanks()
    {

        gameOverPanel.SetActive(true);

        tipsPanel.SetActive(false);
        revivePanel.SetActive(false);

        reviveController.Close();

        timesToRevive = setTimesToRevive;
        
    }

    #endregion

    #region Show Ad

    public void WatchAd()
    {

        ShowOptions so = new ShowOptions();
        so.resultCallback = AdReward;

        GameManager.instance.DisplayAd("rewardedVideo", so);

    }

    public void AdReward(ShowResult sr)
    {

        if (sr == ShowResult.Finished)
        {

            timesToAd = setTimesToAd;
            gameOverController.AdReward();
            
        }

    }

    #endregion

}
