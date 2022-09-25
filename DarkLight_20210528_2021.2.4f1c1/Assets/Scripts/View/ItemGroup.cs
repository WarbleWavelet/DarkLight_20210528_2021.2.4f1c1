/****************************************************

   文件：
   作者：WWS
   日期：2022/09/08 21:11:02
   功能：在BagPanel下的物体

*****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroup : Item
{


    #region 字属


    //比Item额外多的
    [Tooltip("信息提示框")] public GameObject tipGo;
    [Tooltip("信息提示框的文本")] public UILabel tipLabel;
    [Tooltip("数量的文本框")] public UILabel countLabel;
    [Tooltip("数量")] public int count = 0;
    [Tooltip("信息提示框显示时间")] public float activeTime = 0.2f;
    [Tooltip("信息提示框计时器")] public float timer = 0f;

    public float onButtonClickTime = 0.5f;
    public float onButtonClickTimer = 0f;

    public EquipPannel equipPannel;
    public BagPannel bagPannel;
    public Player player;


    #endregion  





    #region 生命


    void Start()
    {
        countLabel.text = count.ToString();
        GetComponent<UISprite>().spriteName = icon_name;

        equipPannel = EquipPannel._instance;
        bagPannel=BagPannel._instance;
        player=Player._instance;

    }


   public void InitUI()
    {
        if(countLabel!=null)
            countLabel.text = count.ToString();
        GetComponent<UISprite>().spriteName = icon_name;

        equipPannel = EquipPannel._instance;
        bagPannel = BagPannel._instance;
        player = Player._instance;

    }
    void Update()
    {

    }
    #endregion


    #region Tips



    public void ShowTip()//显示提示框文本
    {
        tipGo.SetActive(true);

        string tip;
        switch (itemType)
        {
            case ItemType.Potion://药品
                {
                    tip = "名称：" + name;
                    tip += "\n类型：药品";
                    tip += "\n效果：Hp+" + hp;
                    tip += "\n效果：Mp+" + mp;
                    tip += "\n售价：" + price_sell;
                }
                break;
            case ItemType.Equip://装备
                {
                    string dressTypeStr = "";
                    switch (dressType)//部位
                    {
                        case DressType.Headgear: dressTypeStr = "头部"; break;
                        case DressType.Armor: dressTypeStr = "身体"; break;
                        case DressType.LeftHand: dressTypeStr = "左手"; break;
                        case DressType.RightHand: dressTypeStr = "右手"; break;
                        case DressType.Shoe: dressTypeStr = "脚"; break;
                        case DressType.Accessory: dressTypeStr = "饰品"; break;
                    }
                    string applyTypeStr = "";
                    switch (applyType)//职业
                    {
                        case ApplyType.Swordman: applyTypeStr = "战士"; break;
                        case ApplyType.Magician: applyTypeStr = "魔法师"; break;
                        case ApplyType.Common: applyTypeStr = "通用"; break;
                    }
                    //打印
                    tip = "名称：" + name;
                    tip += "\n类型：装备";
                    tip += "\n部位类型：" + dressTypeStr;
                    tip += "\n职业类型：" + applyTypeStr;
                    tip += "\n攻击：" + attack;
                    tip += "\n防御：" + defense;
                    tip += "\n速度：" + speed;
                    tip += "\n售价：" + price_sell;
                }
                break;
            default:
                {
                    tip = "名称：未命名";
                    tip += "\n类型：未定义";
                }
                break;
        }
        tipLabel.text = tip;
    }
    #endregion  

    public void DisableTip()//信息框计时隐藏
    {
        tipGo.SetActive(false);

    }
   
    public void SetValue(Item item)//父类的数据赋值给子类，因为数据是父类的
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
    public void SetValue(ItemGroup item)//父类的数据赋值给子类，因为数据是父类的
    {
        count = item.count;//多了
        //
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
    


    /// <summary>
    /// 双击进行装备  ,挂载到双击事件上
    /// </summary>
    public void OnButtonDoubleClick()
    {
        print("1");
        //装备
        if ((itemType == ItemType.Equip && applyType == player.applyType)
            || applyType == ApplyType.Common)
        {

            UseDressItem();
        }
        else if (itemType == ItemType.Potion) //药品
        {
            UsePotionItem();
        }
    }


    void UseDressItem()
    {
        print("2");
        Transform dressTrans = equipPannel.GetTransByDresssType(this.dressType);

        print("3");




        ItemGroup newItem = this;
        int newId = newItem.id;
        DressType newDressType = newItem.dressType;
        if (dressTrans.childCount <= 0) //没有装备
        {
            equipPannel.AddItem(newId);//穿上新的
            bagPannel.SubItem(newItem.id);//取出新的        
        }
        else //有装备
        { 
            Transform t = dressTrans.GetChild(0);
            ItemGroup oldItem = t.GetComponent<ItemGroup>();
            if (oldItem.id == newItem.id)//同种装备
            {
                print("4");
                return;
            }
            else//不同种装备
            {
                print("5");
                int oldId = oldItem.id;
                DressType oldDressType = oldItem.dressType;

                bagPannel.AddItem(oldId); //放回旧的  

                //
                bagPannel.SubItem(newId);//取出新的
                equipPannel.UpdateItem(newId);//穿上新的

            }
        }
    }


    void UsePotionItem()
    { 
    
                   bool isSuccese = player.Heal(hp, mp);

            if (isSuccese)
            {
                count--;
                if (count <= 0)
                {
                    Destroy(gameObject);
                }
                countLabel.text = count.ToString();
            }
    }


    public virtual void SetUI()
    {
        
    }

 
}


