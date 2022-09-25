using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SkillAttack : MonoBehaviour
{
    #region 字属


    SkillType skillType = SkillType.None;
    Entity entity;
    public EKeyCode eKeyCode;
    public Transform target;

    public GameObject skillPrefab;
    //
    public bool exitSkill;
    public bool enterSkill;
    public float timer = 0f;
    public float time = 1f;
    public bool isTiming;

    public bool enterChase = false;

    [Tooltip("技能预制体")] public List<GameObject> skillGoList;
    [Tooltip("根据名字找预制体的字典")] public Dictionary<string, GameObject> skillDictionary = new Dictionary<string, GameObject>();
    [Tooltip("技能实例的地方")] public Transform skillTrans;
    [Tooltip("粒子播放时间，用来控制动画的播放时间")] public float skillTime;
    //
    public bool useSingleSkill = false;//使用单体技能找目标
    public bool useMultiSkill = false;
    public string skill1Clip;
    public string skill2Clip;
    [Tooltip("当前处理那个技能的选择目标状态")] public SkillItem curSkillItem;
    [Tooltip("群体技能认地面原点所以不能用Transform")] public Vector3 tarPos;
   new Animation animation;
    Animator animator;
    public Player player;

    public float stopDis=5f;
    GameSettings gameSettings = GameSettings._instance;


  public  SkillState skillState = SkillState.None;
    #endregion




    #region 生命周期
    public void Init()
    
    {
        InitSkill();
        InitClip();
        entity = GetComponent<Entity>();
        player = GetComponent<Player>();
        animation = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        gameSettings = GameSettings._instance;
        eKeyCode = EKeyCode.None;
        exitSkill = false;
        enterSkill = false;
        enterChase = false;
        timer = 0f;
        time = 1f;
        isTiming = false;
    }

    void Update()
    {
        if (entity.actionState != ActionState.SkillAttack || curSkillItem==null)
        {
            skillState = SkillState.None;
            target = null;
            tarPos = Vector3.zero;
            return;
        }
                                          
        if (UICamera.isOverUI == false &&Input.GetMouseButtonDown(0) )//需要点击目标的技能
        {
            if (curSkillItem.effectTarget == EffectTarget.Position)
            {
                Common.RayHit(Tags.Ground, ref tarPos);
            }
            else if (curSkillItem.effectTarget == EffectTarget.Enenmy)
            {
                Common.RayHit(Tags.Enemy, ref target);
            }
            skillState = SkillState.Chase;
        }
        if (curSkillItem.effectTarget == EffectTarget.Player) //直接生效在玩家上的技能
        {
            target = entity.transform;
            skillState = SkillState.Use;
        }



        if (target != null)
        {
            if (entity.IsArrived(target.position, stopDis) == false)
            {
                entity.Chase(target);
            }
            else
            {
                skillState = SkillState.Use;
            }
        }
        else if (tarPos != Vector3.zero)
        {
            if (entity.IsArrived(tarPos, stopDis) == false)
            {
                entity.Chase(tarPos);
            }
            else
            {
                skillState= SkillState.Use;

            }
        }   
       

         if (skillState == SkillState.Use)
        {
            UseSkill_EffectType(curSkillItem);
        }


    

        //if (Common.Distance(entity.transform, target) < stopDis)
        //{
        //    entity.actionState = ActionState.SkillAttack;
        //    enterSkill = true;
        //    enterChase = false;
        //}




        //if (enterChase == false
        //    && enterSkill == true
        //    && exitSkill == false
        //    && target != null
        //    && curSkillItem != null)
        //{


        //    Debug.LogFormat("释放技能{0}",curSkillItem.name);  
        //    UseSkill(curSkillItem);
        //    enterSkill = false;
        //    exitSkill = true;
        //}


        //if ( enterSkill== false  && exitSkill ==true )
        //{
        //    isTiming = true;
        //}

        //if (isTiming)
        //{
        //    timer = Common.Timer(() => {
        //        tarPos = Vector3.zero;
        //        target = null;
        //        exitSkill = false;
        //        entity.actionState = ActionState.Idle;
        //        isTiming = false;
        //    }, timer, time);
        //}
   

    }
    #endregion




    #region 技能


    public void EnterSkill(SkillItem skillItem)
    {
        curSkillItem = skillItem;
        entity.actionState = ActionState.SkillAttack;
    }


    public bool UseSkill(SkillItem skillItem)//使用技能看蓝够不够
    {

        if (skillItem == null)
        {
            return false;
        }
        player.isDamaged = true;
        int mp = skillItem.costMp;
        int remainMp = player.mp - mp;
        //
        if (remainMp < 0)//没蓝
        {
            return false;
        }
        else//成功
        {
          
            bool res = UseSkill_EffectType(curSkillItem);
            if (res)//防止满血加血等情况
            {
                player.mp = remainMp;
            }
            return res;//比如回血，蓝是够了，但没必要
        }
    }


    /// <summary>
    /// （Hp、Mp） 增强（攻防） 单目标 多目标
    /// </summary>
    /// <param name="skillItem"></param>
    /// <returns></returns>
    bool UseSkill_EffectType(SkillItem skillItem)
    {
        if (skillItem == null)
        {
            return false;
        }
        switch (skillItem.effectType)
        {
            case EffectType.Passive:
                {
                    return UseSkill_Passive(skillItem);
                }
            case EffectType.Buff:
                {
                    if (useMultiSkill) return false;

                    StartCoroutine(UseSkill_Buff(skillItem));
                    return true;
                }
            case EffectType.SingleTarget:
                {
                    
                    UseSkill_SingleTarget(curSkillItem);//单目标S
                    return true;
                }
            case EffectType.MultiTarget:
                {

                    gameSettings.SetLockTargetCursor();//一使用就切选择光标，跟单体不同
                    UseSkill_MultiTarget(curSkillItem);
                  
                    return true;
                }
            default: break;
        }
        return false;
    }



    /// <summary>
    /// 消耗性属性
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    bool UseSkill_Passive(Skill skill)//增益
    {

        bool isSuccesed = false;
        switch (skill.effectProperty)
        {
            case EffectProperty.HP:
                {
                    isSuccesed = player.AddHp((int)skill.effectValue);//满血，没蓝就失败
                }
                break;
            case EffectProperty.MP:
                {
                    isSuccesed = player.AddMp((int)skill.effectValue);
                }
                break;
        }
        if (isSuccesed)
        {
            ProcessSkill(skill, transform);
        }
        else
        {
            print("发动技能失败");
        }
        return isSuccesed;
    }


    IEnumerator UseSkill_Buff(Skill skill)//增强
    {

        switch (skill.effectProperty)   //属性
        {
            case EffectProperty.Attack: player.atk_normal *= skill.effectValue / 100f; break;
            case EffectProperty.Defense: player.defense *= skill.effectValue / 100f; break;
            case EffectProperty.AttackSpeed:
                {
                    player.atk_Speed += skill.effectValue / 100f;
                    player.ChangeAnimationSpeed(player.atk_Speed);
                }
                break;
        }

        print("输出" + skill.effectTime);

        ProcessSkill(skill, transform);  //技能状态       

        yield return new WaitForSeconds(skill.effectTime); //属性恢复
        switch (skill.effectProperty)
        {
            case EffectProperty.Attack: player.atk_normal /= skill.effectValue / 100f; break;
            case EffectProperty.Defense: player.defense /= skill.effectValue / 100f; break;
            case EffectProperty.AttackSpeed:
                {
                    player.atk_Speed -= skill.effectValue / 100f;
                    player.ChangeAnimationSpeed(player.atk_Speed);
                }
                break;
        }
    }

    void UseSkill_SingleTarget(SkillItem skillItem)//单目标
    {
        if (useSingleSkill == true) return;
        //
        Wolf enemy;
        useSingleSkill = true;//普攻光标切换为选择光标
        enemy = target.GetComponent<Wolf>();
        //
        ProcessSkill(skillItem, target);
        //

       
        transform.LookAt(target);
        DamageNormalOrCritical(enemy, skillItem);



        animator.SetInteger(Constants_Animator.SkillID,Constants_Animator.Skill1);
        GameObject effectPrefab = Resources.Load<GameObject>("Prefabs/" + curSkillItem.skill_efx_name);
       GameObject go= Instantiate(effectPrefab, target.transform.position, Quaternion.identity);

        TimerSvc.Instance.AddTimerTask((int id) => {
            entity.Idle();
            curSkillItem = null;
            skillState = SkillState.None;
            useSingleSkill = false;
        },  Common.GetLengthByName(animator, Constants_Animator.Skill_01),PETimeUnit.Second);

        
    }

    public void InstaniateEffectPrefabCritical()
    {

        print("1");
        if (entity!=null && entity.actionState!=ActionState.SkillAttack) return;
        if (curSkillItem == null) return;
        print("2");

    }

    /// <summary>
    /// 群体技能
    /// </summary>
    /// <param name="skill"></param>
    void UseSkill_MultiTarget(SkillItem skillItem)//多目标
    {
        if (useMultiSkill == true) return;
        useMultiSkill = true;

        GameObject effectPrefab = Resources.Load<GameObject>("Prefabs/" + skillItem.skill_efx_name);
        GameObject go = Instantiate(effectPrefab, target.localPosition, Quaternion.identity);//技能特效
        go.GetComponent<Attack_Sphere_One>().attack = DamageNormalOrCritical( skillItem);
        go.GetComponent<SphereCollider>().enabled = true;
        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Skill5);
        AudioSource.PlayClipAtPoint( Resources.Load<AudioClip>("Sword Swing"), entity.transform.position );
        //gameSettings.SetNormalCursor();
        //
        float len = Common.GetLengthByName(animator, Constants_Animator.Skill_05) ;
        TimerSvc.Instance.AddTimerTask(
            (int tid) => {
                curSkillItem = null;
                skillState = SkillState.None;
                useMultiSkill = false; //看表知道没有能持续时间
            }, len, PETimeUnit.Second
        );


    }

    void DamageNormalOrCritical(Wolf enemy,  SkillItem skillItem)
    {
        float r = UnityEngine.Random.Range(0f, 1f);  //产生技能伤害（普通、暴击）
        if (r < player.atk_crazy_rate)//暴击
        {
            print("暴击");
            float attack = player.atk_crazy * skillItem.effectValue / 100f;
            enemy.TakeDamage(attack);
        }
        else
        {
            print("普攻");
            float attack = player.atk_normal * skillItem.effectValue / 100f;
            enemy.TakeDamage(attack);
        }

    }

    float DamageNormalOrCritical(SkillItem skillItem)
    {
        float r = UnityEngine.Random.Range(0f, 1f);  //产生技能伤害（普通、暴击）
        float attack=0f;
        if (r < player.atk_crazy_rate)//暴击
        {
            print("暴击");
             attack = player.atk_crazy * skillItem.effectValue / 100f;
        }
        else
        {
            print("普攻");
             attack = player.atk_normal * skillItem.effectValue / 100f;
        }

        return attack;

    }
    #endregion




    #region 辅助函数
    KeyCode EKeyCode2KeyCode(EKeyCode eKeyCode)
    {

        switch (eKeyCode)
        {
            case EKeyCode.Alpha1: return KeyCode.Alpha1;
            case EKeyCode.Alpha2: return KeyCode.Alpha2;
            case EKeyCode.Alpha3: return KeyCode.Alpha3;
            case EKeyCode.Alpha4: return KeyCode.Alpha4;
            case EKeyCode.Alpha5: return KeyCode.Alpha5;
            case EKeyCode.Alpha6: return KeyCode.Alpha6;

            default: return KeyCode.None;
        }

    }

    void InitSkill()
    {
        skillDictionary.Clear();
        for (int i = 0; i < skillGoList.Count; i++)
        {
            GameObject go = skillGoList[i];
            // print(go.name);
            skillDictionary.Add(go.name, go);
        }
    }


    float SkillAtk(Skill skill)
    {
        //产生技能伤害（普通、暴击）并且实例HudText
        float r = UnityEngine.Random.Range(0f, 1f);
        transform.LookAt(target);
        float attack;
        if (r < player.atk_crazy_rate)//暴击
        {
            print("暴击");
            //玩家受伤      
            player.state = State.Skill2;
            attack = player.atk_crazy * skill.effectValue / 100f;
        }
        else
        {
            print("普攻");
            player.state = State.Skill1;
            attack = player.atk_normal * skill.effectValue / 100f;
        }
        return attack;
    }




    void ProcessSkill(Skill skill, Transform target)//实例特效，播放动画
    {

        string key = skill.skill_efx_name;   //实例特效
        //
        if (skillDictionary.TryGetValue(key, out GameObject value))
        {
            Instantiate(value, target.position, Quaternion.identity);
            //

            StartCoroutine(PlayAniClip());
        }
    }


    IEnumerator PlayAniClip()
    {
        player.isDamaged = true;
        yield return new WaitForSeconds(skillTime);
        player.isDamaged = false;
        player.state = State.Idle;

    }


    void InitClip()
    {
       
        skill1Clip = "Sword-Skill01";
        skill2Clip = "Sword-Skill02";
    }
    #endregion

}


public enum SkillType
{
    None,
    Single,//单人伤害
    Range,//范围伤害
    Multi,//多人伤害

}
/// <summary>
/// 面板上只显示4个按键，少一点
/// </summary>
public enum EKeyCode
{
    None, 
    Alpha1, 
    Alpha2, 
    Alpha3, 
    Alpha4, 
    Alpha5, 
    Alpha6
}



public enum SkillState
{
    None,
    Use,
    Chase,

}

