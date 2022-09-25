using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectType
{ 
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}

public enum EffectTarget
{
    Player,
    Enenmy,
    Position
}

public enum EffectProperty
{ 
    Attack,
    Defense,
    Speed,
    AttackSpeed,
    HP,
    MP
}

public class Skill:MonoBehaviour
{

    /**
    4001,致命一击,  skill-02,伤害 250%,            SingleTarget,  Attack,250,0,6,1,Swordman,1,Enemy,1.5,,,0
    4002,盾牌精通,  skill-07,+防御 130%,           Buff,          Def,130,5,3,5,Swordman,3,Self,0,,,0
    4003,战嚎,      skill-05,+伤害 150%,           Buff,          Attack,150,60,15,60,Swordman,3,Self,0,,,0
    4004,回旋斩,    skill-03,攻击力 200%所有敌人,  MultiTarget,   Attack,200,0,10,20,Swordman,6,Self,0,,,0
    4005,加速攻击,  skill-06,攻击速度200%,         Buff,          AttackSpeed,200,20,20,30,Swordman,7,Self,0,,,0
    4006,巨型打击,  skill-04,攻击力 400%所有敌人,  MultiTarget,   Attack,400,0,20,20,Swordman,10,Position,7,,,0
     //*/
    public int id;                          // 技能id    
    public new string name;                 // 技能名称  
    public string icon_name;                //技能的图片名
    public string des;                      //技能描述 des= description 

    public ApplyType applyType;             //适用的角色
    public int lv;                          //技能等级
    public int roleLv;                      //角色等级解锁
    public float coolTime;                  //冷却时间
    public int costMp;                      //Mp消耗
    
    public EffectType effectType;           //作用类型，增益/增强 
    public EffectProperty effectProperty;   //作用属性
    public EffectTarget effectTarget;       //效果目标
    public float effectValue;               //作用强弱
    public float effectTime;                //作用时间
    public float releaseDis;                //作用距离
    
    public string skill_efx_name;           //技能特效文件名称
    public string player_ani_name;          //玩家动画文件名称
    public float player_ani_time;           //玩家动画时间



    public override string ToString()
    {
        string str = "";
        str += "\t" + id;
        str += "\t" + name;
        str += "\t" + icon_name;
        str += "\t" + des;
            
        str += "\t" + coolTime;
        str += "\t" + applyType;
        str += "\t" + lv;
        str += "\t" + roleLv;
        str += "\t" + costMp;
            
        str += "\t" + effectProperty;
        str += "\t" + effectTarget;
        str += "\t" + effectTime;
        str += "\t" + effectType;
        str += "\t" + effectValue;
        str += "\t" + releaseDis;
        //
        str += "\t" + skill_efx_name;
        str += "\t" + player_ani_name;
        str += "\t" + player_ani_time;

        return str;     
    }
}
