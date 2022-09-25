
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPannel : Pannel
{


    #region 字属


    public Transform headgear;
    public Transform anmor;
    public Transform leftHand;
    public Transform rightHand;
    public Transform shoe;
    public Transform accessory;
    public GameObject equipItemPrefab;

    public bool fromEquipPannel = false;

    public static EquipPannel _instance;

    public BagPannel bagPannel;
    public Player player;


    #endregion




    #region 生命

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        bagPannel = BagPannel._instance;
        player = Player._instance;
    }

    void Update()
    {
        // TODO:绑定位置
    }

    #endregion




    #region 点击


    public void OnHeadgearClick()//头盔
    {
        OnDressTypeClick(DressType.Headgear);
    }


    public void OnArmorClick()//盔甲
    {
        OnDressTypeClick(DressType.Armor);
    }


    public void OnRightHandClick()
    {
        OnDressTypeClick(DressType.RightHand);
    }


    public void OnLeftHandClick()
    {
        OnDressTypeClick(DressType.LeftHand);
    }


    public void OnShoeClick()
    {
        OnDressTypeClick(DressType.Shoe);
    }


    public void OnAccessoryClick()//饰品
    {
        OnDressTypeClick(DressType.Accessory);
    }


    //从装备栏点击进来
    public void OnDressTypeClick(DressType dressType)//筛选出 职业类型，Item类型（装备），部位类型的物品
    {
        List<ItemGroup> equipGroupLst = new List<ItemGroup>();
        foreach (ItemGroup itemGroup in bagPannel.itemGroupList)
        {
            if (itemGroup.dressType == dressType
                && (itemGroup.applyType == player.applyType 
                || itemGroup.applyType==ApplyType.Common)
                && itemGroup.itemType == ItemType.Equip)//找出职业，部位符合的装备
            {
                equipGroupLst.Add(itemGroup);
            }
        }
        fromEquipPannel = true;
        bagPannel.DisplayItem(equipGroupLst);

        DisableWindow();
        bagPannel.ShowWindow();
    }

    #endregion

  
    //
    public void AddItem(int id)
    {
        Item item = ItemTextAssetToList._instance.GetItemById( id);
        EquipItem newItem = new EquipItem();
        newItem.SetValue(item);
        //
        Transform t = GetTransByDresssType(item.dressType);
        GameObject itemGo;
        if (t.childCount > 0) //相同装备return
        {
            EquipItem oldItem = t.GetChild(0).GetComponent<EquipItem>();
            GameObject oldGo = t.GetChild(0).gameObject;
            if (oldItem.id == newItem.id)//同种装备
            {
                return;
            }
            else //不同装备
            {
            }
        }
        else//没装备
        {
            itemGo = Instantiate( this.equipItemPrefab);
            itemGo.GetComponent<EquipItem>().SetValue(item);
            player.AddProp(itemGo.GetComponent<EquipItem>());//属性点

            //图片 位置 父节点    //赋值，属性
            itemGo.transform.parent = t;
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localScale = Vector3.one;
            t.GetChild(0).GetComponent<UISprite>().spriteName = item.icon_name;

        }
    }

    public void UpdateItem(int id)
    {
        Item item = ItemTextAssetToList._instance.GetItemById(id);
        EquipItem newItem = new EquipItem();
        newItem.SetValue(item);
        //
        Transform t = GetTransByDresssType(item.dressType);
        GameObject itemGo =t.GetChild(0).gameObject;
        EquipItem oldItem = t.GetChild(0).GetComponent<EquipItem>();
        GameObject oldGo = t.GetChild(0).gameObject;
        //
        player.SubProp(oldItem);
        player.AddProp(newItem);




        oldItem.SetValue(newItem);
        oldItem.InitUI();
        oldItem.SetUI(newItem);


  


    }

    public void SubItem(DressType dressType)
    {
        print("pannel脱下");
        //销毁，传id
        Transform equipTrans= GetTransByDresssType(dressType);
        GameObject equipItem = equipTrans.GetChild(0).gameObject;

        if (BagPannel._instance.UnDressEquipItem(equipItem.GetComponent<EquipItem>()))//可以放回去
        {
            print("pannel脱下成功");
            player.SubProp(equipItem.GetComponent<EquipItem>());
           Destroy(equipItem);
        }

    }
                                              


    public Transform GetTransByDresssType(DressType dressType)//根据装备的部位类型，返回部位的transform
    {
        Transform equipTrans;
        switch (dressType)
        {
            case DressType.Headgear:
                equipTrans = headgear;
                break;
            case DressType.Armor:
                equipTrans = anmor;
                break;
            case DressType.LeftHand:
                equipTrans = leftHand;
                break;
            case DressType.RightHand:
                equipTrans = rightHand;
                break;
            case DressType.Shoe:
                equipTrans = shoe;
                break;
            case DressType.Accessory:
                equipTrans = accessory;
                break;
            default:
                throw new System.Exception("脱下装备出错");

        }
        return equipTrans; 
    }
} 
