using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShopItem : MonoBehaviour
{
    public UILabel priceLabel;
    public int price;

    
    // Start is called before the first frame update
    void Start()
    {
        price = int.Parse(priceLabel.text);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBuyClick()
    {
        GetComponentInParent<ShopPannel>().Buy(price);
    }
}
