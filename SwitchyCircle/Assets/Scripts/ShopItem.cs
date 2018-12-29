using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    [SerializeField] private GameObject      locked;
    [SerializeField] private GameObject      unlocked;
    [SerializeField] private TextMeshProUGUI price;

    [SerializeField] private Color normalColor   = Color.white;
    [SerializeField] private Color selectedColor = Color.white;

    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;

    private int currentPlayerIndex;

    void Update () {
	
		backgroundImage.color = (currentPlayerIndex == GameManager.instance.currentHandIndex) ? backgroundImage.color = selectedColor : normalColor;

    }

    public void Setup(int playerIndex) {

        currentPlayerIndex = playerIndex;

        Hand handInfo = GameManager.instance.handSkins[currentPlayerIndex];

        price.text = handInfo.price.ToString();
        iconImage.sprite = handInfo.handSkin;

        locked.SetActive(handInfo.isLocked);
        unlocked.SetActive(!handInfo.isLocked);

    }

    public void SelectBall() {

        Hand handInfo = GameManager.instance.handSkins[currentPlayerIndex];

        GameManager.instance.audioManager.PlaySound("click");

        if (handInfo.isLocked)
        {

            if (GameManager.instance.gems >= handInfo.price)
            {

                GameManager.instance.gems -= handInfo.price;

                handInfo.isLocked = false;

                locked.SetActive(handInfo.isLocked);
                unlocked.SetActive(!handInfo.isLocked);

                PlayerData data = SaveSystem.LoadData();

                if (data.unlockedHandIndexes.Count == 5) {

                    GameManager.instance.UnlockAchievement(SCGPS.achievement_5_skins);

                }

            }
            else {

                Debug.Log("Not enough gems!");

            }

        }
        else {

            GameManager.instance.SwapHand(currentPlayerIndex);

        }

        GameManager.instance.SaveData();

    }

}
