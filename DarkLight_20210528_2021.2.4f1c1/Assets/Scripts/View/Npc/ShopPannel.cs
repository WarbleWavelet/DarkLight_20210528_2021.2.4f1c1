using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPannel : Pannel
{


    private int coin;
    public UILabel coinLabel;



        
    // Start is called before the first frame update

    void Start()
    {
        coin = Player._instance.coin;
        coinLabel.text = coin.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(int price)
    {
        int coin = Player._instance.coin;
        int remainCoin = coin - price;
        //        
        if (remainCoin < 0) return;//不够钱
        //           
        coin -= remainCoin;
        Player._instance.coin = coin;
        coinLabel.text = coin.ToString();
    }
    public void Buy(int id ,int price , int amount)
    {
        int coin = Player._instance.coin;
        //
        price *= amount;
        int remainCoin = coin - price * amount;
        //        
        if (remainCoin < 0) return;//不够钱
        //           
        coin = remainCoin;
        Player._instance.coin = coin;
        coinLabel.text = coin.ToString();
        //背包添加
        for (int i = 0; i < amount; i++)
        {
            BagPannel._instance.AddItem(id);
        }        
    }

}
