using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using GooglePlayGames;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public AudioManager audioManager;

    #region Inspector Variables

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject dailyGiftUI;
    
    [SerializeField] private ShakeController shakeController;

    [SerializeField] private HandController     handPrefab;
    [SerializeField] private CircleController   circlePrefab;

    #endregion

    #region Member Variables

    public GameState gameState;

    public HandController   hand;
    public CircleController circle;

    public Hand[]       handSkins;
    public List<Color>  colors = new List<Color>();

    public int setTimesToRevive;

    public string ANDROID_RATE_URL = "market://details?id=com.stanimirov.switchycircle";

    #endregion

    #region Properties

    public int score;
    public int messageState;
    public int handColorIndex = 0;
    public int handHoverIndex = 0;

    public int gems;
    public int highScore;
    public int currentHandIndex;

    public int handSpeed;
    public int handDirection;

    public int playedGames;

    public bool adFree;
    public bool adFreeRevive;

    #endregion

    public enum GameState {

        Menu,
        Shop,
        GamePlay,
        GameOver,
        Settings,
        DailyGift

    }

    #region Unity Methods

    void Awake()
    {

        if (instance == null) {

            instance = this;

            //Unity ads init
            Advertisement.Initialize("2962040");

            //Google play services
            PlayGamesPlatform.Activate();
            OnConnect();

            return;

        }

        Destroy(gameObject);      
        
    }

    void Start()
    {

        //Init
        LoadData();
        MainMenu();

    }

    void Update () {

        if (gameState != GameState.GamePlay) {

            return;

        }

        //Gameplay
        if (Input.GetMouseButtonDown(0)) {

            if (handColorIndex != handHoverIndex) {

                GameOver();
                messageState = 3;
                return;

            }

            audioManager.PlaySound("success");

            UpdateScore();
            HandState(true, true);

            if (Random.Range(1, 3) == 1) {

                switch (Random.Range(1, 5)) {

                    case 2:
                        gems += 2;
                        break;
                    default:
                        gems += 1;
                        break;

                }

            }

            handSpeed = hand.Speed;
            handDirection = hand.Direction;

        }

	}

    #endregion

    public void RequestRevive()
    {

        ShowOptions so = new ShowOptions();
        so.resultCallback = Revive;

        DisplayAd("revivevideo", so);

    }

    public void ChangeGameState(GameState newGameState)
    {

        switch (newGameState)
        {

            case GameState.GameOver:

                gameOverUI.SetActive(newGameState == GameState.GameOver);
                dailyGiftUI.SetActive(newGameState == GameState.DailyGift);


                break;
            case GameState.DailyGift:

                dailyGiftUI.SetActive(newGameState == GameState.DailyGift);

                break;
            default:

                menuUI.SetActive(newGameState == GameState.Menu);
                shopUI.SetActive(newGameState == GameState.Shop);
                settingsUI.SetActive(newGameState == GameState.Settings);
                gamePlayUI.SetActive(newGameState == GameState.GamePlay);
                gameOverUI.SetActive(newGameState == GameState.GameOver);
                dailyGiftUI.SetActive(newGameState == GameState.DailyGift);

                break;

        }

        gameState = newGameState;

    }

    #region DailyGift

    public void GetDailyGift(int reward)
    {

        gems += reward;

        DailyGift();
        SaveData();

    }

    #endregion

    #region Ad Managment

    public void DisplayAd(string type) {

        Advertisement.Show(type);        

    }

    public void DisplayAd(string type, ShowOptions so)
    {

        Advertisement.Show(type, so);

    }

    public bool IsAdReady() {

        return Advertisement.IsReady();

    }

    #endregion

    #region Play services

    public void OnConnect() {

        Social.localUser.Authenticate((bool success) => {

            OnConnectionResponse(success);

        });

    }

    public void OnConnectionResponse(bool authenticated) {

        if (authenticated)
        {

            Debug.Log("Success");

        }
        else {

            Debug.Log("Failed");

        }

    }

    public void ShowLeaderboard() {

        if (Social.localUser.authenticated)
        {

            Social.ShowLeaderboardUI();

        }
        else {

            OnConnect();

        }

    }

    public void ShowAchievements()
    {

        if (Social.localUser.authenticated)
        {

            Social.ShowAchievementsUI();

        }
        else
        {

            OnConnect();

        }

    }

    public void UnlockAchievement(string achievementID) {

        Social.ReportProgress(achievementID, 100.0f, (bool success) => {

            Debug.Log("Unlocked achievment -> " + success.ToString());

        });

    }

    public void ReportScore(int score)
    {

        Social.ReportScore(score, SCGPS.leaderboard_highscore, (bool success) => {

            Debug.Log("Reported score to leaderboard -> " + success.ToString());

        });

    }

    #endregion

    #region GamePlay

    public void ResetGame()
    {

        score = 0;
        messageState = 1;

        handSpeed = 200;
        handDirection = -1;

    }

    private void UpdateScore()
    {

        score++;

        if (score > 0 && score < 3)
        {

            messageState = 2;

        }
        else
        {

            messageState = 0;

        }

        if (score % 5 == 0 && hand.Speed < 340)
        {

            hand.Speed += 35;

        }

    }

    public void Revive()
    {

        messageState = 0;

        CreateHand();
        ChangeGameState(GameState.GamePlay);

    }

    public void Revive(ShowResult sr)
    {

        if (sr == ShowResult.Finished)
        {

            messageState = 0;

            CreateHand();
            ChangeGameState(GameState.GamePlay);

        }

    }

    #endregion

    #region Hand Managment

    private void CreateHand() {

        DestroyHand();

        hand = Instantiate(handPrefab);
        hand.skin = handSkins[currentHandIndex];

        hand.Speed = handSpeed;
        hand.Direction = handDirection;

        HandState(false, true);

    }

    private void DestroyHand() {

        if (hand != null)
        {

            GameObject newExplosion;

            if (handColorIndex < 2)
            {

                newExplosion = Instantiate(hand.skin.explosion_red);
                newExplosion.transform.rotation = hand.transform.rotation;

            }
            else if(handColorIndex > 1){
                
                newExplosion = Instantiate(hand.skin.explosion_blue);
                newExplosion.transform.rotation = hand.transform.rotation;

            }

            audioManager.PlaySound("death");
            Destroy(hand.gameObject);

        }
        else {

            Debug.Log("No hand to destroy");

        }

    }

    private void HandState(bool dir, bool col) {

        if (dir) {

            hand.SwitchDirection();

        }

        if (col) {

            handColorIndex = hand.ChangeColor();

        }

    }

    public void SwapHand(int handIndex) {

        currentHandIndex = handIndex;

    }

    #endregion

    #region Button Functions

    public void MainMenu() {

        messageState = 1;

        if (!audioManager.IsSoundPlaying("background")) {

            //audioManager.PlaySound("background");

        }

        DestroyHand();
        ChangeGameState(GameState.Menu);

    }

    public void Settings()
    {

        ChangeGameState(GameState.Settings);

    }

    public void DailyGift()
    {

        audioManager.PlaySound("pops");

        ChangeGameState(GameState.DailyGift);

    }

    public void Shop()
    {
        
        ChangeGameState(GameState.Shop);

    }

    public void StartGame() {

        if (audioManager.IsSoundPlaying("background"))
        {

            //audioManager.StopSound("background");

        }

        playedGames++;

        switch (playedGames) {

            case 25:

                UnlockAchievement(SCGPS.achievement_25_games);

                break;
            case 50:

                UnlockAchievement(SCGPS.achievement_50_games);

                break;
            case 100:

                UnlockAchievement(SCGPS.achievement_100_games);

                break;
            case 150:

                UnlockAchievement(SCGPS.achievement_150_games);

                break;
            default:
                break;

        }

        ResetGame();
        CreateHand();
        ChangeGameState(GameState.GamePlay);

    }    

    public void GameOver()
    {

        SaveData();
        DestroyHand();

        shakeController.StartShake();
        
        ChangeGameState(GameState.GameOver);
        
    }

    public void RateUs() {

        #if UNITY_EDITOR
        Debug.Log("Opening store...");
        #elif UNITY_ANDROID
        Application.OpenURL(ANDROID_RATE_URL);
        #endif

    }

    #endregion

    #region Save System

    public void SaveData() {

        SaveSystem.SaveData(this);

    }

    public void LoadData() {

        PlayerData data = SaveSystem.LoadData();

        if (data != null) {

            gems = data.gems;
            highScore = data.highScore;
            currentHandIndex = data.currentHandIndex;

            playedGames = data.playedGames;

            adFree = data.adFree;
            adFreeRevive = data.adFreeRevive;

            for (int i = 0; i < data.unlockedHandIndexes.Count; i++) {

                handSkins[data.unlockedHandIndexes[i]].isLocked = false;

            }

            return;

        }

        ResetData();

    }

    public void ResetData() {

        gems = 0;
        highScore = 0;
        currentHandIndex = 0;
        playedGames = 0;

        adFree = false;
        adFreeRevive = false;

        for (int i = 1; i < handSkins.Length; i++)
        {

            handSkins[i].isLocked = true;

        }

        SaveData();
        PlayerPrefs.DeleteAll();

    }

    #endregion
        
}
