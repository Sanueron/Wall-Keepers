using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEditor.VersionControl;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using static InGamePurchasesManager;



public class InGamePurchasesManager : MonoBehaviour, IDetailedStoreListener
{
    public string environment = "production";

    #region Purchase products
    [Serializable]
    public class ConsumableItem
    {
        public string s_Name;
        public string s_Id;
        public string s_Desc;
        public float f_Price;
    }
    [Serializable]
    public class NonConsumableItem
    {
        public string s_Name;
        public string s_Id;
        public string s_Desc;
        public float f_Price;
    }
    [Serializable]
    public class SubscriptionItem
    {
        public string s_Name;
        public string s_Id;
        public string s_Desc;
        public float f_Price;
        public int i_duration; // In days
    }
    #endregion

    #region Receipt Data
    [Serializable]
    public class SkuDetails
    {
        public string productId;
        public string type;
        public string title;
        public string name;
        public string iconUrl;
        public string description;
        public string price;
        public long price_amount_micros;
        public string price_currency_code;
        public string skuDetailsToken;
    }

    [Serializable]
    public class PayloadData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
    }

    [Serializable]
    public class Payload
    {
        public string json;
        public string signature;
        public List<SkuDetails> skuDetails;
        public PayloadData payloadData;
    }

    [Serializable]
    public class Data
    {
        public string Payload;
        public string Store;
        public string TransactionID;
    }

    // Access objects
    public Data data;
    public Payload payload;
    public PayloadData payloadData;
    #endregion

    #region Purchase Objects
    IStoreController m_StoreController;
    public ConsumableItem consumableItem_5000Mana;
    public ConsumableItem consumableItem_30000Mana;
    public NonConsumableItem nonConsumableItem_RemoveAds;
    public SubscriptionItem subscriptionItem_seasonWizzardPass; // elite pass
    #endregion

    #region UI Variables to assign in Inspector
    [SerializeField] TextMeshProUGUI tmp_playerMana;
    #endregion

    #region Control Variables
    float f_playerCurrentMana;
    #endregion

    #region Purchase Complete SFXs
    [SerializeField] AudioSource aS_sfxs;
    [SerializeField] AudioClip aC_purchasedComplete;
    #endregion

    #region Unity Life Cycle

    async void Start()
    {
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            // An error occurred during services initialization.
        }
        

        SetUpBuilder();
    }

    #endregion

    #region Purchase related methods
    private void SetUpBuilder()
    {
        // Create builder 
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Set up products
        builder.AddProduct(consumableItem_5000Mana.s_Id, ProductType.Consumable);
        builder.AddProduct(consumableItem_30000Mana.s_Id, ProductType.Consumable);
        builder.AddProduct(nonConsumableItem_RemoveAds.s_Id, ProductType.NonConsumable);
        builder.AddProduct(subscriptionItem_seasonWizzardPass.s_Id, ProductType.Subscription);

        // Initialize
        UnityPurchasing.Initialize(this, builder);
    }
    #endregion

    #region Local Methods
    public void TryPurchase_Buy5000Mana()
    {
        m_StoreController.InitiatePurchase(consumableItem_5000Mana.s_Id);
    }
    public void Buy5000Mana() // Price 0,99€
    {
        // Play Purchased complete SFX
        aS_sfxs.PlayOneShot(aC_purchasedComplete);

        // Assign local var of mana obtained
        int i_manaObtained = 5000;
        // Assign Local Variable Current Mana
        f_playerCurrentMana = PlayerData.i_mana;
        // Add Mana 
        PlayerData.i_mana += i_manaObtained;
        StartCoroutine(SumUp(PlayerData.i_mana));
        // Save Data
        SaveData.instance.AssignDataAndSave();
    }

    public void TryPurchase_Buy30000Mana()
    {
        m_StoreController.InitiatePurchase(consumableItem_30000Mana.s_Id);
    }
    public void Buy30000Mana() // Price 0,99€
    {
        // Play Purchased complete SFX
        aS_sfxs.PlayOneShot(aC_purchasedComplete);

        // Assign local var of mana obtained
        int i_manaObtained = 30000;
        // Assign Local Variable Current Mana
        f_playerCurrentMana = PlayerData.i_mana;
        // Add Mana 
        PlayerData.i_mana += i_manaObtained;
        StartCoroutine(SumUp(PlayerData.i_mana));
        // Save Data
        SaveData.instance.AssignDataAndSave();
    }

    public void TryPurchase_RemoveAds()
    {
        m_StoreController.InitiatePurchase(nonConsumableItem_RemoveAds.s_Id);
    }
    private void BuyRemoveAds()
    {

    }

    public void TryPurchase_SeasonPass()
    {
        m_StoreController.InitiatePurchase(subscriptionItem_seasonWizzardPass.s_Id);
    }
    private void UnlockWizzardPassAccess()
    {

    }
    #endregion

    #region Check if Special Products Purchased (Used to avoid hacking)
    private void CheckIfNoAdsPayed(string id)
    {
        if(m_StoreController != null)
        {
            var product = m_StoreController.products.WithID(id);
            if(product != null)
            {
                if (product.hasReceipt) // Purchased
                {
                    PlayerData.b_removeAdsBought = true;
                }
                else // Not Purchased
                {
                    PlayerData.b_removeAdsBought = false;
                }
            }
        }
    }
    #endregion

    #region Spells Sum Up Effect Methods
    IEnumerator SumUp(float targetValue)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(targetValue - f_playerCurrentMana) / f_duration;
        while (f_playerCurrentMana != targetValue)
        {
            f_playerCurrentMana = Mathf.MoveTowards(f_playerCurrentMana, targetValue, rate * Time.deltaTime);
            tmp_playerMana.text = ((int)f_playerCurrentMana).ToString();
            yield return null;
        }
    }
    #endregion

    #region IStore Listener Methods
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Initialized failed " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("Initialized failed " + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        // Retrieve the purchased product reference
        var product = purchaseEvent.purchasedProduct;

        print("Purchase complete " + product.definition.id);

        // Distribute rewards checking which product was bought
        if(product.definition.id == consumableItem_5000Mana.s_Id)
        {
            /*
            // Get Quantity of purchases
            string receipt = product.receipt;
            data = JsonUtility.FromJson<Data>(receipt);
            payload = JsonUtility.FromJson<Payload>(data.Payload);
            payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

            int quantity = payloadData.quantity;

            for (int i = 0; i < quantity; i++)
            {
                Buy5000Mana();
            }
            */

            Buy5000Mana();
        }
        else if(product.definition.id == consumableItem_30000Mana.s_Id)
        {
            Buy30000Mana();
        }
        else if(product.definition.id == nonConsumableItem_RemoveAds.s_Id)
        {
            BuyRemoveAds();
        }
        else if(product.definition.id == subscriptionItem_seasonWizzardPass.s_Id)
        {
            UnlockWizzardPassAccess();
        }

        // To close
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase failed");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success initializing purchases");
        m_StoreController = controller;

        // Additional checks to avoid hacking. Checks if player has a receipt
        CheckIfNoAdsPayed(nonConsumableItem_RemoveAds.s_Id);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        print("Purchase failed " + failureDescription);
    }
    #endregion
}
