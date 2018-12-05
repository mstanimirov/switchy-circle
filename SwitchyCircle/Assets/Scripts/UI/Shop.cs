using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private ShopItem        itemPrefab;
    [SerializeField] private Transform       listContent;
    [SerializeField] private TextMeshProUGUI currentGems;

    private List<ShopItem>  listItems;

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

    }

    void Update () {

        currentGems.text = GameManager.instance.gems.ToString();

	}
}
