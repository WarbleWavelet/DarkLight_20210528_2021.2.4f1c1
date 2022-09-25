using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPannel : Pannel
{
   
    private float attack ;
    private float defense;
    private float speed;
    private int point;
    [Tooltip("数字部分的Label")] public UILabel attackLabel;
    [Tooltip("数字部分的Label")] public UILabel defenseLabel;
    [Tooltip("数字部分的Label")] public UILabel speedLabel;
    [Tooltip("数字部分的Label")] public UILabel pointLabel;


    public static StatusPannel _instance;

	void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        GetValue();
        SetLabel();
    }


   public void UpdateUI()
    {
        GetValue();
        SetLabel();
    }

    //点击了相应的加按钮
    public void OnAttackClick()
    {
        bool havePoint = ConsumePoint();
        if (!havePoint) return;

        attack++;
        SetLabel();  
    }
    public void OnDefenseClick()
    {
        bool havePoint = ConsumePoint();
        if (!havePoint) return;

        defense++;
        SetLabel();
    }
    public void OnSpeedClick()
    {
        bool havePoint = ConsumePoint();
        if (!havePoint) return;

        speed++;
        SetLabel();
    }
    //够钱就扣，返回true；不然还钱，返回false
    private bool ConsumePoint()
    {
        point--;
        if (point < 0)
        {
            point++;
            return false;
        } 

        return true;
    }

    public void ResetValue ()//重置
    {
        GetValue();
        SetLabel();
    }
    public void SetValue()//应用修改
    {
        Player._instance.atk_normal = attack;
        Player._instance.defense=defense;
        Player._instance.speed=speed;
        Player._instance.point=point;
    }

    public void GetValue()//到玩家那里获取数据
    {
        attack =  Player._instance.atk_normal;
        defense = Player._instance.defense;
        speed =  Player._instance.speed;
        point =  Player._instance.point;
    }
    public void SetLabel()//输出到状态板的Label
    {
        attackLabel.text = attack.ToString();
        defenseLabel.text = defense.ToString();
        speedLabel.text = speed.ToString();
        pointLabel.text = point.ToString();
    }

}
