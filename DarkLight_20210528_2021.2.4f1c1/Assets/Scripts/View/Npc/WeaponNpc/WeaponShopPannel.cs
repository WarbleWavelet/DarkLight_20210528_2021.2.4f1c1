using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopPannel : ShopPannel
{

    [Tooltip("数据")]   public List<Item> itemList = new List<Item>();
    [Tooltip("数据")] [SerializeField] public  List<Item> itemSourceList = new List<Item>();
    //
    [Tooltip("对象")] [SerializeField] public List<GameObject> itemObjectList = new List<GameObject>();

    [Tooltip("父节点")] public Transform parent;
    public GameObject weaponShopItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        itemSourceList = DataHub._instance.weaponList;

        InitItem(itemSourceList);//赋值
        DisplayItemObject(itemList);//实例
    }

    // Update is called once per frame
    void Update()
    {
        coinLabel.text = Player._instance.coin.ToString(); 
        
        parent.GetComponent<UIGrid>().Reposition();
    }

    void InitItem(List<Item> itemSourceList)//赋值
    {
        foreach (Item item in itemSourceList)
        {
            WeaponShopItem weaponShopItem = new WeaponShopItem();
            weaponShopItem.SetValue(item);

            itemList.Add(item);
        }
     }
    void DisplayItemObject(List<Item>  itemList)//相比背包，这是没有格子固定位置的
    {
        if (itemObjectList != null)//只有一个Pannel，销毁之前的展品
        {
            foreach (GameObject itemObject in itemObjectList)
            {
                Destroy(itemObject);
            }
            itemObjectList.Clear();//不clear，list里面有很多Missing的对象
        }
        //重新展览
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject go = Instantiate(weaponShopItemPrefab);

            WeaponShopItem item = go.GetComponent<WeaponShopItem>();
            item.SetValue(itemList[i]);
            item.SetUI(itemList[i]);

            go.transform.parent = parent;
           go.transform.localPosition = new Vector3(0f, parent.transform.localPosition.y, 0f);
            go.transform.localScale = Vector3.one;

            itemObjectList.Add(go);
        }
        
    }
    //                 
    override public  void ShowWindow()
    {
        InitItem(itemSourceList);//赋值
        DisplayItemObject(itemList);//实例
                                    //

        coinLabel.text = Player._instance.coin.ToString();
       
        GetComponent<TweenPosition>().PlayForward();
        isShow = true;


    }
}
