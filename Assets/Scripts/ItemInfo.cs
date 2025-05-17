using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public ShopManager shopManager;
    public int itemPrice;

    void Start()
    {
        // Получаем цену и отображаем
        //int price = shopManager.GetItemPrice(ItemID);
        //PriceText.text = "Price: " + price.ToString();
    }

    // Этот метод можно повесить на кнопку "Buy"
    public void BuyThisItem()
    {
        shopManager.Buy(ItemID);
    }
}
