using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{

    public static IAPManager instance;

    public static IStoreController m_StoreController;
    public static IExtensionProvider m_StoreExtensionProvider; 

    public static string PRODUCT_AD_FREE = "adfree";
    public static string PRODUCT_AD_FREE_REVIVE = "adfreerevive";

    void Awake() {

        if (instance == null)
        {

            instance = this;
            
            return;

        }

        Destroy(gameObject);

    }

    void Start()
    {

        if (m_StoreController == null)
        {

            InitializePurchasing();

        }

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

    public string GetPrice(string productID) {

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

        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_AD_FREE_REVIVE, StringComparison.Ordinal))
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameManager.instance.adFree = true;
            GameManager.instance.adFreeRevive = true;
            GameManager.instance.SaveData();

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
