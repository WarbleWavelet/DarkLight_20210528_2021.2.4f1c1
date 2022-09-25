using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillTextAssetToList : MonoBehaviour
{
    [Tooltip("要添加的文本")] public TextAsset textAsset;
    [Tooltip("文本转成的字典")] public Dictionary<int, Skill> dictionary = new Dictionary<int, Skill>();
    [Tooltip("文本转成的列表")] public List<Skill> list = new List<Skill>();


    void Awake()
    {

        dictionary = TextAssetToDictionary(textAsset);
        DictionaryToList(dictionary);
    }


    private Dictionary<int, Skill> TextAssetToDictionary(TextAsset textAsset)//读取TextAsset，存储到Dictionary
    {
        string[] lineArray = textAsset.text.Split('\n');

        foreach (string line in lineArray)
        {
            string[] propertyArray = line.Split(',');//英文逗号
            //分流
            Skill skill = new Skill();
            skill.id = int.Parse(propertyArray[0]) ;
            skill.name = propertyArray[1];
            skill.icon_name = propertyArray[2];
            skill.des = propertyArray[3];
            switch(propertyArray[4])
            {
                case "Passive": skill.effectType = EffectType.Passive;break;
                case "Buff": skill.effectType = EffectType.Buff; break;
                case "MultiTarget": skill.effectType = EffectType.MultiTarget; break;
                case "SingleTarget": skill.effectType = EffectType.SingleTarget; break;
            };
            switch(propertyArray[5])
            {
                case "Attack" :skill.effectProperty= EffectProperty.Attack; break;
                case "AttackSpeed" :skill.effectProperty= EffectProperty.AttackSpeed; break;
                case "Defense" :skill.effectProperty= EffectProperty.Defense; break;
                case "HP" :skill.effectProperty= EffectProperty.HP; break;
                case "MP" :skill.effectProperty= EffectProperty.MP; break;
                case "Speed" :skill.effectProperty= EffectProperty.Speed; break;
            };
            skill.effectValue = int.Parse(propertyArray[6]);
            skill.effectTime = int.Parse(propertyArray[7]);
            skill.costMp = int.Parse(propertyArray[8]);
            skill.coolTime = int.Parse(propertyArray[9]);
            switch(propertyArray[10])
            {
                case "Swordman": skill.applyType = ApplyType.Swordman; break;
                case "Magician": skill.applyType = ApplyType.Magician; break;
                case "Common" : skill.applyType=ApplyType.Common; break;

            };
            skill.roleLv= int.Parse(propertyArray[11]);
            switch(propertyArray[12])
            {
              case "Selef" :skill.effectTarget = EffectTarget.Player;break;
              case "Enemy" :skill.effectTarget = EffectTarget.Enenmy; break;
              case "Position" :skill.effectTarget = EffectTarget.Position; break;
            };
            skill.releaseDis = float.Parse(propertyArray[13]);
            //
            skill.skill_efx_name = propertyArray[14];
            skill.player_ani_name = propertyArray[15];
            skill.player_ani_time = float.Parse(propertyArray[16]);

            dictionary.Add(skill.id, skill);
        }

        return dictionary;
    }
    private List<Skill> DictionaryToList(Dictionary<int, Skill> dictionary)//读取文本，派生Skill预制体，生成对象
    {

        //遍历字典，填充skillPrefab，加入列表
        for (int i = 0; i < dictionary.Count; i++)
        {
            //看着文本的id来改
            Skill skill = new Skill();

            if (i <6)             
                skill = dictionary[4001 + i];//战士
            else
                skill = dictionary[5001 + i - 6];//法师

            list.Add(skill);//加入
        }

        return list;
    }
    public Skill GetSkillById(int id)
    {
        dictionary.TryGetValue(id, out Skill skill);

        return skill;
    }

}