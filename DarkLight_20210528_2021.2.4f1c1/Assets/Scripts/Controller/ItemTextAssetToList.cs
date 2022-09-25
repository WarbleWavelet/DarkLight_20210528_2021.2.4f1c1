using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemTextAssetToList: MonoBehaviour
{
    [Tooltip("要添加的文本")] public TextAsset textAsset;
    [Tooltip("文本转成的字典")] public Dictionary<int, Item> dictionary = new Dictionary<int, Item>();
    [Tooltip("文本转成的列表")] public List<Item> list = new List<Item>();
    [Tooltip("药品列表")] public List<Item> potionList = new List<Item>();
    [Tooltip("装备列表")] public List<Item> weaponList = new List<Item>();




    public static ItemTextAssetToList _instance;

	void Awake()
    {
        _instance = this;

    
        dictionary = TextAssetToDictionary(textAsset);
        DictionaryToList(dictionary);
    }


    public Dictionary<int, Item> TextAssetToDictionary(TextAsset textAsset)//读取TextAsset，存储到Dictionary
    {
        string[] lineArray = textAsset.text.Split('\n');

        foreach (string line in lineArray)
        {
            string[] propertyArray = line.Split(',');//英文逗号
            //分流
            Item item = new Item();
            item.id = int.Parse(propertyArray[0]);
            item.name = propertyArray[1];
            item.icon_name = propertyArray[2];
            //根据类型在具体赋值
            switch (propertyArray[3])
            {
                case "Potion": item.itemType = ItemType.Potion; break;
                case "Equip": item.itemType = ItemType.Equip; break;
                case "Mat": item.itemType = ItemType.Mat; break;
                default: item.itemType = ItemType.Undefined; break;
            }
            //药品
            switch (item.itemType)
            {
                case ItemType.Potion://药品
                    {
                        item.hp = int.Parse(propertyArray[4]);
                        item.mp = int.Parse(propertyArray[5]);
                        item.price_sell = int.Parse(propertyArray[6]);
                        item.price_buy = int.Parse(propertyArray[7]);
                    }
                    break;
                case ItemType.Equip://装备
                    {
                        item.attack = int.Parse(propertyArray[4]);
                        item.defense = int.Parse(propertyArray[5]);
                        item.speed = int.Parse(propertyArray[6]);

                        switch (propertyArray[7])//部位
                        {
                            case "Headgear": item.dressType = DressType.Headgear; break;
                            case "Armor": item.dressType = DressType.Armor; break;
                            case "LeftHand": item.dressType = DressType.LeftHand; break;
                            case "RightHand": item.dressType = DressType.RightHand; break;
                            case "Shoe": item.dressType = DressType.Shoe; break;
                            case "Accessory": item.dressType = DressType.Accessory; break;
                            default:  break;
                        }
                        switch (propertyArray[8])//职业
                        {
                            case "Swordman": item.applyType = ApplyType.Swordman; break;
                            case "Magician": item.applyType = ApplyType.Magician; break;
                            case "Common": item.applyType = ApplyType.Common; break;
                            default:break;
                        }

                        item.price_sell = int.Parse(propertyArray[9]);
                        item.price_buy = int.Parse(propertyArray[10]);
                    }
                    break;
                default: break;
            }
            dictionary.Add(item.id, item);
        }
        
        return dictionary;
    }
    public List<Item> DictionaryToList(Dictionary<int,Item> dictionary)//读取文本，派生Item预制体，生成对象
    {
        
        for (int i = 0; i < dictionary.Count; i++)   //遍历字典，填充itemPrefab，加入列表
        {
            Item item = new Item(); //看着文本的id来改
            if (i > 2)
            {
                item = dictionary[2001 + i - 3];//装备
                weaponList.Add(item);
                
            }
            else //取值
            {
                item = dictionary[1001 + i];
                potionList.Add(item);//药品
            }
                

            list.Add(item);//加入
        }

        return list;
    }
    public Item GetItemById(int id)
    {
        dictionary.TryGetValue(id, out Item item);

        return item;
    }
}
