using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int[] shopItems;
    public int[] shopItemsPrices;
    public Sprite[] shopItemsImages;
    
    public float money = 100f;
    public Text MoneyText;
    public GameObject panel;

    void Start()
    {
        MoneyText.text = "Currency: " + money.ToString();
        
    }

    public void Buy(int itemPrice)
    {
       
        if(money >= shopItemsPrices[itemPrice ])
        {
          money -= shopItemsPrices[itemPrice ];
          MoneyText.text = "Currency: " + money;
          
        }
        else
        {
          Debug.Log("Not enough money!");
        }
       
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            panel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ExitButton()
    {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
