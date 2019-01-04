using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class Shop : MonoBehaviour {

    public GameObject watchAd;

    [SerializeField] private ShopItem        itemPrefab;
    [SerializeField] private Transform       listContent;
    [SerializeField] private TextMeshProUGUI currentGems;

    private List<ShopItem>  listItems;

    public TextMeshProUGUI rewardText;

    public GameObject explosion;
    public GameObject explosionPrefab;

    void OnEnable()
    {

        Hand[] ballInfos = GameManager.instance.handSkins;

        if (listItems == null)
        {
            listItems = new List<ShopItem>();

            for (int i = 0; i < ballInfos.Length; i++)
            {
                listItems.Add(Instantiate(itemPrefab));
                listItems[i].transform.SetParent(listContent, false);
            }
        }

        for (int i = 0; i < ballInfos.Length; i++)
        {
            listItems[i].Setup(i);
        }

        watchAd.SetActive(GameManager.instance.IsAdReady());

    }

    void Update () {

        currentGems.text = GameManager.instance.gems.ToString();

        if (Input.GetKeyDown("escape"))
        {

            if (GameManager.instance.gameState != GameManager.GameState.DailyGift) {

                GameManager.instance.MainMenu();
                
            }

        }

        if (GameManager.instance.gameState == GameManager.GameState.DailyGift)
        {

            if (Input.GetMouseButtonDown(0))
            {

                GameManager.instance.ChangeGameState(GameManager.GameState.Shop);

            }

            if (Input.GetKeyDown("escape"))
            {

                GameManager.instance.ChangeGameState(GameManager.GameState.Shop);

            }

        }

    }

    public void WatchAd() {

        ShowOptions so = new ShowOptions();
        so.resultCallback = AdReward;

        GameManager.instance.DisplayAd("rewardedVideo", so);

    }

    public void AdReward(ShowResult sr)
    {

        if (sr == ShowResult.Finished)
        {

            rewardText.text = "+30";

            GameManager.instance.GetDailyGift(30);

            explosion = Instantiate(explosionPrefab);

        }

    }

}
