using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Button giftButton;
    public TextMeshProUGUI gemsUI;
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI rewardText;

    public GameObject explosion;
    public GameObject explosionPrefab;

    public float msToWait = 5000.0f;

    private ulong lastDailyGift;

    void Start()
    {

        if (PlayerPrefs.GetString("LastDailyGift") != "") {

            lastDailyGift = ulong.Parse(PlayerPrefs.GetString("LastDailyGift"));

        }

        if (!IsGiftReady())
        {

            giftButton.interactable = false;

        }

    }

    void Update () {
		
        gemsUI.text = GameManager.instance.gems.ToString();

        if (!giftButton.IsInteractable())
        {

            if (IsGiftReady()) {

                giftButton.interactable = true;
                return;

            }

            ulong diff = ((ulong)DateTime.Now.Ticks - lastDailyGift);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (msToWait - m) / 1000.0f;

            string r = "";

            r += ((int)secondsLeft / 3600).ToString() + ":";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;

            r += ((int)secondsLeft / 60).ToString("00") + ":";

            r += ((int)secondsLeft % 60).ToString("00");

            timeText.text = r;

        }

        if (GameManager.instance.gameState == GameManager.GameState.DailyGift) {

            if (Input.GetMouseButtonDown(0)) {

                GameManager.instance.ChangeGameState(GameManager.GameState.Menu);

            }

        }

    }

    public void CollectGift()
    {

        lastDailyGift = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastDailyGift", lastDailyGift.ToString());

        giftButton.interactable = false;

        GetReward();

    }

    public bool IsGiftReady()
    {

        ulong diff = ((ulong)DateTime.Now.Ticks - lastDailyGift);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (msToWait - m) / 1000.0f;

        if (secondsLeft < 0) {

            timeText.text = "Ready!";
            return true;

        }

        return false;

    }

    public void GetReward() {

        int reward = UnityEngine.Random.Range(35, 50);

        rewardText.text = "+" + reward;

        GameManager.instance.GetDailyGift(reward);

        explosion = Instantiate(explosionPrefab);

    }

}
