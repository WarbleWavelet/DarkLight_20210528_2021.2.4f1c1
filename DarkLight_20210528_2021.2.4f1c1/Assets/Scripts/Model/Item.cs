using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item:MonoBehaviour//物品信息类
{
    public int id;//000
    public new string name;
    public string icon_name;
    public ItemType itemType;
    public int price_sell;
    public int price_buy;//5
    public DressType dressType;//部位
    public ApplyType applyType;//职业
    public int attack;
    public int defense;
    public int speed;
    public int hp;
    public int mp;


    void Start()
    {

    }
}