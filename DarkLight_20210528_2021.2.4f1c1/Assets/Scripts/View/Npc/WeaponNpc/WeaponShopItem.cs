using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopItem : Item
{

    public UISprite sprite;//icon_name
    public UILabel nameLabel;
    public UILabel descriptionLabel;
    public UILabel priceSellLabel;//price_sell
    //
    [Tooltip("输入数量的文本框的总节点")] public GameObject buyGo;
    [Tooltip("输入数量的文本框的总节点偏移量")] public Vector3 buyGoOffset;

    // Start is called before the first frame update
    void Start()
    {
        //一开始隐藏数量按钮
        buyGo.SetActive(false);
        buyGoOffset = buyGo.transform.position - transform.position;
        //拿到父节点grid的父节点scrollView的组件，用于实现拖拽
        GetComponent<UIDragScrollView>().scrollView = transform.parent.parent.GetComponent<UIScrollView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUI(Item item)
    {
        sprite.spriteName =icon_name;
        nameLabel.text=name;
        //描述
        string str = "";
        if (attack > 0) str += "攻击：+" + attack;
        if (defense > 0) str += "防御：+" + defense;
        if (speed > 0) str += "移速：+" + speed ;
        descriptionLabel.text =str;

        priceSellLabel.text="$"+price_buy;//pr
    }
    public void SetValue(Item item)
    {
        id = item.id;
        name = item.name;
        icon_name = item.icon_name;
        itemType = item.itemType;
        price_sell = item.price_sell;
        price_buy = item.price_buy;
        dressType = item.dressType;
        applyType = item.applyType;
        attack = item.attack;
        defense = item.defense;
        speed = item.speed;
        hp = item.hp;
        mp = item.mp;
    }


    public void OnBuyClick()
    {
        Transform panel = transform.parent.parent.parent.parent;
        panel.GetComponent<WeaponShopPannel>().Buy(price_buy);
    }
    //
    public void OnBuyClick_Amount()
    {
        //显示按钮
        buyGo.transform.position = transform.position + buyGoOffset;
        buyGo.SetActive(true);
    }
    public void OnBuyClick_Amount_OK()
    {
        Transform panel = transform.parent.parent.parent.parent;
        int amount = int.Parse(buyGo.GetComponentInChildren<UILabel>().text) ;
        int id = GetComponent<WeaponShopItem>().id;
        //
        buyGo.SetActive(false);
        //
        panel.GetComponent<WeaponShopPannel>().Buy(id, price_buy, amount);
    }
    public void Close()
    {
        buyGo.SetActive(false);
    }
}
