using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour {

    public GameObject imageLeft;
    public GameObject imageRight;
    public TextMeshProUGUI tipText;

    public Sprite[] panelImages;
    public string[] panelValues;

	void Update () {

        int messageState = GameManager.instance.messageState;

        Image imageLeftRenderer = imageLeft.GetComponent<Image>();
        Image imageRightRenderer = imageRight.GetComponent<Image>();

        tipText.text = panelValues[messageState];

        imageLeftRenderer.sprite = panelImages[messageState];
        imageRightRenderer.sprite = panelImages[messageState];

    }
}
