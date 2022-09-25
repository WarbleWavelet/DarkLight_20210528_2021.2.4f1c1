using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    #region 字属


    [Header("Player")]
    public List<int> maxExpLevelList;




    [Header("属性")]
    public new string name = "博无尘";
    [Tooltip("职业")] public ApplyType applyType = ApplyType.Swordman;
    public int level = 90;     
    public int maxLevel = 100;
   // [Tooltip("生命值")] public new float hp = 200;
    public int maxHp = 10000;
    public int mp = 80;
    public int maxMp = 10000;    
    public int exp = 100;
    public int maxExp = 10;
    public float defense = 10;
    public float speed = 10;
    [Tooltip("跑的速度")] public float runSpeed = 10f;
    public int point = 5;

    [Header("资源")]
    public int coin = 500;

    [Header("动画")]
    public string atkCriticalClip;
    public string runClip;
    public string castClip;

    //
    [Tooltip("测试看看玩家和目标的距离")] public float dis;
    [Tooltip("攻击特效")] public GameObject hudText_prefab;
    [Tooltip("限制每次攻击特效只放一次")] public bool hadShowed_atk_prefab=false;
    //




    [Header("状态机")]
    public bool enterChase = false;//可以想敌人移动，地面有另外组件控制
    public bool enterAtk; //可以进行攻击
    public bool processAtk;//处于连续攻击中
    public static Player _instance;
    public CharacterController ctrl;
   public MoveByMouse moveByMouse;



    public bool takeDameged;
    #endregion


    #region 生命


    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        moveByMouse = GetComponent<MoveByMouse>();
        if (null != GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

        InitClip();
        InitProperty();
        InitHudText();
       
        enterChase = false;
        PlayerStatus._instance.UpdatePlayerStatus();
        atk_crazy = atk_normal * (1 + atk_crazy_rate);


        takeDameged = false;

    }

    void Update() //拆成不同组件
    {

    }
    #endregion




    #region Prop改变


    public void AddProp(EquipItem item)//装备
    {
        atk_normal += item.attack;
        defense += item.defense;
        speed += item.speed;

        StatusPannel._instance.UpdateUI();
    }
    public void SubProp(EquipItem item)//装备
    {
        atk_normal -= item.attack;
        defense -= item.defense;
        speed -= item.speed;

        StatusPannel._instance.UpdateUI();
    }
    //
    public bool AddHp(int hp) //回血,还是不用item不然治愈技能不能复用
    {
        //
        if (this.hp >= maxHp) return false;
        //
        float remainHp = this.hp + hp;
        //
        if (remainHp > maxHp)
            this.hp = maxHp;
        else
            this.hp = remainHp;
        //
        PlayerStatus._instance.UpdatePlayerStatus();//UI刷新

        return true;
    }
    public bool AddMp(int mp) //回血,还是不用item不然治愈技能不能复用
    {
        //
        if (this.mp >= maxMp) return false;
        //
        int remainMp = this.mp + mp;
        //
        if (remainMp > maxMp)
            this.mp = maxMp;
        else
            this.mp = remainMp;
        //
        PlayerStatus._instance.UpdatePlayerStatus();//UI刷新

        return true;
    }
    public bool Heal(int hp, int mp)
    {
        if (hp == 0 && mp > 0) return AddMp(mp);//纯蓝药  
        else if (mp == 0 && hp > 0) return AddHp(hp);//纯血药  
        else if (hp > 0 && mp > 0) return (AddHp(hp) || AddMp(mp));
        else return false;

       
    }
    //
    public void AddExp(int exp)
    {
        int nextLevel = this.level + 1;
        //范围限制
        if (this.level >= maxLevel && this.exp >= maxExpLevelList[maxLevel])
        {
            this.level = maxLevel;
            this.exp = maxExpLevelList[maxLevel];
            return;
        }

        //
        int remainExp = this.exp + exp;
        while (remainExp >= maxExpLevelList[nextLevel])//考虑一次升多级的情况
        {
            remainExp -= maxExpLevelList[nextLevel];
            if (UpLevel())
            {
                continue;
            }
            else
            {
                break;
            }
        }
        //
        this.exp = remainExp;
    }
    private bool UpLevel()
    {
        int remainLevel = this.level + 1;
        //范围限制
        if (remainLevel > this.maxLevel)
        {
            return false;
        }
        this.level = remainLevel;

        return true;
    }
    //
    public bool AddCoin(int coin)
    {
        int remainCoin= this.coin + coin;
        //
        if (remainCoin < 0) return false;
        //
        this.coin = remainCoin;

        //
        return true;
    }
    //
   
    #endregion



    //
    #region 重写


    /// <summary>
    /// 跑动画器
    /// </summary>
    public override void Update_Animator()
    {
        switch (state)
        {
            case State.Idle: PlayAniClip(idleClip); break;
            case State.Walk: PlayAniClip(walkClip); break;
            case State.Run: PlayAniClip(runClip); break;
            case State.Attack1: PlayAniClip(attack1Clip); break;
            case State.AttackCritical: PlayAniClip(attack2Clip); break;
            case State.Damage1: PlayAniClip(damage1Clip); break;
            case State.Damage2: PlayAniClip(damage2Clip); break;
            //case State.Skill1: PlayAniClip(skill1Clip); break;//拆出去了，也用Animator
            //case State.Skill2: PlayAniClip(skill2Clip); break;
            case State.Cast: PlayAniClip(castClip); break;
            case State.Dead: PlayAniClip(deathClip); break;
        }

    }


    public override void TakeDamage(float attack)
    {
        if (state == State.Dead)
        { 
             return;
        }
        //根据什么防御力整合最终所受攻击力
        attack = Attack_Func(attack);//仅仅为了方便从Wolf贴过来的代码
                                     //
                                     // base.TakeDamage(attack); 
        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        if (r < missRate)
        {
            AudioSource.PlayClipAtPoint(missAudioClip, transform.position);
            hudTxt.Add("Miss", Color.gray, stayDuration);
        }
        else
        {
            float remainHp = hp - attack;
            if (remainHp > 0)
            {
                hp = remainHp;
                hudTxt.Add("-" + attack.ToString(), Color.red, stayDuration);


                if (takeDameged == false) //变色
                {     
                    takeDameged = true;                
                    animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Damage);
                    //
                    SetBody(Color.red);
                }
                TimerSvc.Instance.AddTimerTask((int tid) => {
                    animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
                    SetBody(Color.white);
                    takeDameged = false;
                },Constants_Animator.Time_Damage2Idle,PETimeUnit.Second);
               
            }
            else
            {
                hp = 0;
                Dead();
            }

        }
    }

    public override void Dead()
    {
        if (state == State.Dead)
        {
            return;
        }
        state = State.Dead;
        Destroy(gameObject, 5f);
    }

    #endregion



    #region 辅助




    void InitProperty()
    {
        if (PlayerPrefs.HasKey("SelectedPlayerName") == false) name = "123";
        mp = 1000;
        maxMp = 1000;
        atk_Speed = 1f;
        level = 12;    //默认n级
        maxLevel = 20;                   //   0  1    2    3    4    5   6    7UI上每个等级对应的maxLevel
        maxExpLevelList = new List<int> {
            100, 200, 300, 400, 500, 600, 700, 800 ,900,1000,
            1100,1200,1300,1400,1500,1600,1700,1800,1900,2000
        };
    }


    void InitClip()
    {
        animation = GetComponent<Animation>();

        atkCriticalClip = "Sword-AttackCritical";
        runClip = "Sword-Run";
        idleClip = "Sword-Idle";
        walkClip = "Sword-Walk";
        deathClip = "Sword-Death";
        attack1Clip = "Sword-Attack1";
        attack2Clip = "Sword-Attack2";
        damage1Clip = "Sword-TakeDamage1";
        damage2Clip = "Sword-TakeDamage2";
        castClip = "Sword-Cast";
    }


    void SetBody(Color color)
    {
        GetComponentInChildren<Renderer>().material.color = color;//变红
    }

    public float Attack_Func(float attack)
    {
        //具体公式看自己设计
        float remainAttack = attack - defense;
        if (remainAttack <= 0) remainAttack = 1;

        return remainAttack;
    }


  








  public   void ChangeAnimationSpeed(float speed)//改变制定动画的速度
    {
        Animation anim = GetComponent<Animation>();

        foreach (AnimationState aniState in anim)
        {
            print("输出" + aniState.name);
            if (aniState.name == "Sword-Attack1" || aniState.name == "Sword-Attack2")
            {
                aniState.speed = atk_Speed;
            }
        }
    }





    #endregion  

}

