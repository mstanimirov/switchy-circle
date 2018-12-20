using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

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

            UpdateScore();
            HandState(true, true);

            if (Random.Range(1, 3) == 1) {

                gems += 1;

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

        if (score > highScore)
        {

            highScore = score;

        }

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

        DestroyHand();
        ChangeGameState(GameState.Menu);

    }

    public void Settings()
    {

        ChangeGameState(GameState.Settings);

    }

    public void DailyGift()
    {

        ChangeGameState(GameState.DailyGift);

    }

    public void Shop()
    {

        ChangeGameState(GameState.Shop);

    }

    public void StartGame() {

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

        for (int i = 1; i < handSkins.Length; i++)
        {

            handSkins[i].isLocked = true;

        }

        SaveData();
        PlayerPrefs.DeleteAll();

    }

    #endregion

}
