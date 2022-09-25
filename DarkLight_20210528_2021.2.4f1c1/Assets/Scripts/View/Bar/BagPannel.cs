using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagPannel : Pannel
{
    [Tooltip("只装格子的那个节点")] public Transform gridContainer;
    [Tooltip("货币countLabel")] public Transform coinLabel;
    [Tooltip("物品的预制体模板")] public GameObject itemGroupPrefab;
    [Tooltip("读取文本的所有物品")] public List<Item> itemList = new List<Item>();
    [Tooltip("背包中所有各自的列表")] private List<Transform> gridList = new List<Transform>();
    [Tooltip("背包里所有物品的列表")] public List<ItemGroup> itemGroupList;//不能用iteGroup不然后父类数据拿到麻烦
    [Tooltip("背包里所有物品的列表")] public List<GameObject> itemGroupObjectList;//不能用iteGroup不然后父类数据拿到麻烦
    
    public static BagPannel _instance;

    void Awake()
    {
        _instance = this;
         
    }
    void Start()
    {
        InitGrids();//格子
        itemList = DataHub._instance.itemList;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _instance.isShow)//在背包打开的情况下点击
        {
            int createNewItemId = itemList[UnityEngine.Random.Range(0, itemList.Count)].id;//新建的物品的id 
            AddItem(createNewItemId);//克隆对象进背包
            if (EquipPannel._instance.fromEquipPannel == false)
            {
                DisplayItem(itemGroupList);
            }

            else
            {
                EquipPannel._instance.fromEquipPannel = false;
            }
        }
    }

    void InitGrids()//
    {
        for (int i = 0; i < gridContainer.childCount; i++)//装格子，20个一个个拖的做法太累
        {
            gridList.Add(gridContainer.GetChild(i));
        }
    }

   public void AddItem(int createNewItemId)//点击生成物品的测试函数，
    {
        //1 随机添加一个物品
          
        int itemGroupId ;

        if (itemGroupList.Count == 0)
        { 
             AddNewItem(createNewItemId);
        }
           
        else if (itemGroupList.Count<=gridList.Count)
        {
            for (int i = 0; i < itemGroupList.Count; i++)//找到图片名相同的grid的索引
            {
                itemGroupId = itemGroupList[i].id;//取得格子里面的物品的id,moonobehavior排斥

                if (itemGroupId == createNewItemId)//相同，到对应的i加加
                {
                    AddExistingItem(createNewItemId);
                    break;
                }

                if (i == itemGroupList.Count - 1 && itemGroupId != createNewItemId)//到最后
                {
                    AddNewItem(createNewItemId);//用拿到的id去对应的i实例
                    break;
                }

                if (gridList.Count <= itemGroupList.Count)
                {
                    print("满了");
                    break;//不加报Error
                }
            }
        }   
    }

   void AddNewItem(int id)//新增
    {
        if (itemGroupList.Count > gridList.Count)
            return;
        Item item= ItemTextAssetToList._instance.GetItemById(id);
        ItemGroup itemGroup = new ItemGroup();
        itemGroup.SetValue(item);
        itemGroup.count = 1;
 
       itemGroupList.Add(itemGroup);     
    }
    public void SubItem(int id)//旧减
    {
        for (int i = 0; i < itemGroupList.Count; i++)
        {
            if (id == itemGroupList[i].id)
            {
                itemGroupList[i].count--;
                if (itemGroupList[i].count <= 0)//减减到为空
                {

                    print("索引是"+i);
                        
                    itemGroupList.RemoveAt(i);
                }                 
            }
        }
        

        DisplayItem(itemGroupList);//刷新
    }
    public void RemoveItem(int id)
    {
        for (int i = 0; i < itemGroupList.Count; i++)
        {
            if (id == itemGroupList[i].id)
            {
                itemGroupList.RemoveAt(i);
            }
        }


        DisplayItem(itemGroupList);//刷新
    }
    public void AddExistingItem(int id)//旧增
    {
        foreach (ItemGroup itemGroup in itemGroupList)
        {
            if (itemGroup.id == id)
            {
                itemGroup.count++;
               
            }
        }

        foreach (GameObject go in itemGroupObjectList)
        {
            if (go.transform.childCount > 0)
            { 
                ItemGroup curItemGroup = go.GetComponent<ItemGroup>();
                int curID = curItemGroup.id;
                if (curID == id)
                {

                    go.transform.Find("CountLabel").GetComponent<UILabel>().text = (curItemGroup.count+1).ToString();  //更新显示
                }
            }

        }

    }

    
    public void DisplayItem(List<ItemGroup> itemGroupList)//拿到对应的数据表 itemGroupList，进行实例
    {
        
        if (itemGroupObjectList!=null)//只有一个Pannel，销毁之前的展品
        {
            foreach (GameObject itemGroupObject in itemGroupObjectList)
            {
                Destroy(itemGroupObject);
            }
            itemGroupObjectList.Clear();//不clear，list里面有很多Missing的对象
        }
        //重新展览
        for (int i = 0; i < itemGroupList.Count; i++)
        {
            GameObject go = Instantiate(itemGroupPrefab);

            ItemGroup itemGroup = go.GetComponent<ItemGroup>();
            itemGroup.SetValue(itemGroupList[i]);
            itemGroup.countLabel.text = itemGroup.count.ToString();
            go.GetComponent<UISprite>().spriteName =itemGroup.icon_name;
            
            go.transform.parent = gridList[i].transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            itemGroupObjectList.Add(go);
        }
    }
    //
    public void DressEquipItem(ItemGroup itemGroup)
    {
        //替换操作
        if (itemGroup.itemType != ItemType.Equip ) return;
        if (itemGroup.applyType != Player._instance.applyType && itemGroup.applyType != ApplyType.Common) return;
        

        Transform dressTrans = EquipPannel._instance.GetTransByDresssType(itemGroup.dressType);
        if (dressTrans.childCount != 0)//替换，多了回滚动作，其他一样
        {
            //卸下
            UnDressEquipItem(dressTrans.GetChild(0).GetComponent<EquipItem>());
        }


        int id = itemGroup.id;
        SubItem(id);//减减 或者 销毁移除
        EquipPannel._instance.AddItem(id);//装备
    }
    public bool UnDressEquipItem(EquipItem equipItem)
    {
        print("bag脱下");
        if (itemGroupList.Contains(equipItem))//有同类
        {
            AddExistingItem(equipItem.id);          
        }
        if (!itemGroupList.Contains(equipItem))//无同类
        {
            if (itemGroupObjectList.Count + 1 > gridList.Count)//满了，不能卸下
            {
                return false;
            }
            AddNewItem(equipItem.id);
        }

        return true;
    }
    //
    public override void ShowWindow()
    {
        base.ShowWindow();



        DisplayItem(itemGroupList);
    }
}