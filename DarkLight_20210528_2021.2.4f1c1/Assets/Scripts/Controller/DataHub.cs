using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHub : MonoBehaviour
{
    //物品
    public ItemTextAssetToList itemTextAssetToList;
    public List<Item> itemList;
    public List<Item> potionList;//药品
    public List<Item> weaponList;//装备
    //技能
    public SkillTextAssetToList skillTextAssetToList;
    public List<Skill> skillList;


    public static DataHub _instance;

	void Awake()
    {
        _instance = this;
        itemList = itemTextAssetToList.list;
       skillList = skillTextAssetToList.list;

        potionList = itemTextAssetToList.potionList;
        weaponList = itemTextAssetToList.weaponList;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Skill skill in skillList)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
