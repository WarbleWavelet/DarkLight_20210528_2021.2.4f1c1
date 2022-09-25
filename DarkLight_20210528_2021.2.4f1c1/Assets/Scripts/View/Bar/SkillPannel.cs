using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPannel : Pannel
{
    public List<Skill> skillList;
    public List<SkillItem> skillItemList;
    public List<GameObject> skillItemObjectList;
    public GameObject skillItemPrefab;
    public Transform skillItemObjectContainer;
    public Transform grid;
    public static SkillPannel _instance;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        skillList = DataHub._instance.skillList;
        InitSkillItem();
        DisplayItem(skillItemList);
        grid.GetComponent<UIGrid>().enabled = true;
    }
    public void InitSkillItem()//初始化
    {
        foreach (Skill skill in skillList)
        {
            SkillItem skillItem = new SkillItem();
            //
            skillItem.SetValue(skill);
            //
            skillItemList.Add(skillItem);
        }
    }
    public void DisplayItem(List<SkillItem> skillItemList)
    {

        for (int i = 0; i < skillItemList.Count; i++)
        {
            GameObject go = Instantiate(skillItemPrefab);
            SkillItem skillItem = go.GetComponent<SkillItem>();

            skillItem.SetValue(skillItemList[i]);
            skillItem.SetUI();

            go.transform.parent = skillItemObjectContainer;
            float firstyX = skillItemObjectContainer.position.x;
            float firstyY = skillItemObjectContainer.position.y;
            float firstyZ = skillItemObjectContainer.position.z;

            go.transform.localPosition = new Vector3(firstyX, firstyY - 110f * i, firstyZ);
            go.transform.localScale = Vector3.one;

            skillItemObjectList.Add(go);
        }
    }

    public new void ShowWindow()//每次打开调用以
    {
        GetComponent<TweenPosition>().PlayForward();
        isShow = true;

        for (int i = 0; i < skillItemObjectContainer.childCount; i++)
        {
            
            skillItemObjectContainer.GetChild(i).GetComponent<SkillItem>().SetUI();
            skillItemObjectContainer.GetChild(i).GetComponent<SkillItem>().UnlockSkillItem();
 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
