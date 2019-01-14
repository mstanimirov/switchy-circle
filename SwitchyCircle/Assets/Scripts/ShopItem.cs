using EasyMobile;
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

    [SerializeField] private Color lockedColor = Color.white;
    [SerializeField] private Color unlockedColor = Color.white;

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

        iconImage.color = handInfo.isLocked ? lockedColor : unlockedColor;

        locked.SetActive(handInfo.isLocked);
        unlocked.transform.localPosition = handInfo.isLocked ? new Vector3(0.0f, 15.0f, 0.0f) : Vector3.zero;

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

                iconImage.color = handInfo.isLocked ? lockedColor : unlockedColor;
                
                locked.SetActive(handInfo.isLocked);
                unlocked.transform.localPosition = Vector3.zero;

                PlayerData data = SaveSystem.LoadData();

                if (data.unlockedHandIndexes.Count == 5) {

                    GameManager.instance.UnlockAchievement(SCGPS.achievement_5_skins);

                }

            }
            else {

                #if UNITY_EDITOR
                Debug.Log("Not enough gems!");
                #elif UNITY_ANDROID
                NativeUI.ShowToast("Not enough gems!");
                #endif

            }

        }
        else {

            GameManager.instance.SwapHand(currentPlayerIndex);

        }

        GameManager.instance.SaveData();

    }

}
