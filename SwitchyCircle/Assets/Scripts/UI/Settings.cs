using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class Settings : MonoBehaviour, IStoreListener
{

    public static IStoreController m_StoreController;
    public static IExtensionProvider m_StoreExtensionProvider;

    public static string PRODUCT_AD_FREE = "adfree";
    public static string PRODUCT_AD_FREE_REVIVE = "adfreerevive";

    public ToggleController soundSwitch;
    public ToggleController musicSwitch;

    public Button adFreeUI;
    public Button adFreeReviveUI;

    public TextMeshProUGUI adFreePrice;
    public TextMeshProUGUI adFreeRevivePrice;

    void Awake () {

        if (m_StoreController == null)
        {

            InitializePurchasing();

        }

    }

    void OnEnable() {

        soundSwitch.Turn(PlayerPrefs.GetInt("sound") == 0 ? true : false);

        adFreeUI.interactable = !GameManager.instance.adFree;
        adFreeReviveUI.interactable = !GameManager.instance.adFreeRevive;

    }

    void Update () {
        
        if (soundSwitch.switching)
        {

            soundSwitch.Toggle(soundSwitch.isOn, "sound");

        }

        if (IsInitialized())
        {

            if (GameManager.instance.adFree)
            {

                adFreePrice.text = "PURCHASED";

                if (GameManager.instance.adFreeRevive)
                {

                    adFreeRevivePrice.text = "PURCHASED";

                }

            }
            else
            {

                adFreePrice.text = GetPrice(PRODUCT_AD_FREE);
                adFreeRevivePrice.text = GetPrice(PRODUCT_AD_FREE_REVIVE);

            }

        }

        if (Input.GetKeyDown("escape"))
        {

            GameManager.instance.MainMenu();

        }

    }

    public void purchauseAdFree() {

        BuyAdFree();

    }

    public void purchauseAdFreeRevive()
    {

        BuyAdFreeRevive();

    }

    public void InitializePurchasing()
    {

        if (IsInitialized())
        {

            return;

        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(PRODUCT_AD_FREE, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_AD_FREE_REVIVE, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);

    }


    private bool IsInitialized()
    {

        return m_StoreController != null && m_StoreExtensionProvider != null;

    }


    public void BuyAdFree()
    {

        BuyProductID(PRODUCT_AD_FREE);

    }


    public void BuyAdFreeRevive()
    {

        BuyProductID(PRODUCT_AD_FREE_REVIVE);

    }

    public string GetPrice(string productID)
    {

        Product product = m_StoreController.products.WithID(productID);

        return product.metadata.isoCurrencyCode + " " + product.metadata.localizedPrice;

    }

    void BuyProductID(string productId)
    {

        if (IsInitialized())
        {

            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));

                m_StoreController.InitiatePurchase(product);

            }
            else
            {

                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");

            }
        }
        else
        {

            Debug.Log("BuyProductID FAIL. Not initialized.");

        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {

        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;

    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {

        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);

    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_AD_FREE, StringComparison.Ordinal))
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            GameManager.instance.adFree = true;
            GameManager.instance.SaveData();

            adFreePrice.text = "PURCHASED";

            adFreeUI.interactable = false;

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_AD_FREE_REVIVE, StringComparison.Ordinal))
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            GameManager.instance.adFree = true;
            GameManager.instance.adFreeRevive = true;
            GameManager.instance.SaveData();

            adFreePrice.text = "PURCHASED";
            adFreeRevivePrice.text = "PURCHASED";

            adFreeUI.interactable = false;
            adFreeReviveUI.interactable = false;

        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;

    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

    }

}
