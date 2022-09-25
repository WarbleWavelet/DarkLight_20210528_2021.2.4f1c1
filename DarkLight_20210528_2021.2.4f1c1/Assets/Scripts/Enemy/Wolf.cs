using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Human
{
    #region 属性
    //
    [Tooltip("巡逻计时器")] public float patrolTimer = 0f;
    public float patrolTime = 2f;
    public float walkSpeed = 10f;
    public CharacterController ctrl;
    //


    //
    [Tooltip("生命值")] public float maxHp = 100f;

    //   

    //
    [Tooltip("攻速，一秒几下")] public int atk_rate = 1;
    [Tooltip("追击速度")] public float chaseSpeed = 20f;
    //
    [Tooltip("杀死可得经验值")] public int exp = 10;

    Entity entity;
    #endregion



    #region 生命

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
        //
        InitAniClip();
        //
        InitHudText();
        //
        entity = Entity._instance;
    }


    void InitAniClip()
    {
        //

        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Update_Animator();
        //巡逻
        patrolTimer = Common.Timer(Range, patrolTimer, patrolTime);
        Test_TakeDamage();
    }



    private void OnDestroy()
    {
        Destroy(hudTextGo);
    }

    #endregion

    //

    //
    public override void Update_Animator()
    {
        if (state == State.Idle)
        {
            PlayAniClip(idleClip);
        }
        switch (state)
        {
            case State.Idle: PlayAniClip(idleClip); break;
            case State.Walk:
                {
                    GetComponent<CharacterController>().SimpleMove(transform.forward * walkSpeed * Time.deltaTime);
                    PlayAniClip(walkClip);
                }
                break;
            case State.Attack1: PlayAniClip(attack1Clip); break;
            case State.AttackCritical: PlayAniClip(attack2Clip); break;
            case State.Damage1: PlayAniClip(damage1Clip); break;
            case State.Damage2: PlayAniClip(damage2Clip); break;
            case State.Dead: PlayAniClip(deathClip); break;
        }
    }

    //

    //
    public override void TakeDamageEffect()
    {
        PlayAniClip(damage1Clip);

        if (!isDamaged)//防止协程变红未恢复，就进行下一次变色
        {
            isDamaged = true;
            StartCoroutine(ShowBodyRed());
        }
    }

    public override void Dead()
    {
        PlayAniClip(deathClip);
        //移除一个，（补充满员）
        Spawn._instance.Remove(transform);
        //升级
        Player._instance.AddExp(exp);
        //任务进度
        QuestPannel._instance.progress++;
        //
        Destroy(gameObject, 1f);

    }


    /// <summary>
    ///  测试受伤动画
    /// </summary>
    private void Test_TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(Player._instance.atk_normal);
        }
    }


    //
    #region Wolf主动行为
    public override void Range()//视野
    {
        if (Player._instance == null) return;

        Transform target = Player._instance.transform;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < range_atk)
        {
                Attack(); 

        
        }
        else if (distance < range_sight)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }
    public override float Attack()//攻击
    {
        float r1 = UnityEngine.Random.Range(0f, 1f);
        if (r1 < 0.5) return 0f;//不想做计时，又要拆解麻烦


        float r = UnityEngine.Random.Range(0f, 1f);
        //
        target = Player._instance.transform;
        transform.LookAt(target);
        //
        float attack;
        if (r < atk_crazy_rate)//暴击
        {
            //玩家受伤      
            state = State.AttackCritical;
            attack = atk_crazy;
        }
        else
        {
            state = State.Attack1;
            attack = atk_crazy;

        }
        Player._instance.TakeDamage(attack);
        //
        return attack;
    }
    public override void Chase()//追击
    {
        Transform target = Player._instance.transform;
        transform.LookAt(target);
        ctrl.SimpleMove(transform.forward * Time.deltaTime * chaseSpeed);
        state = State.Walk;
    }
    public override void Patrol()//巡逻
    {
        int rState = UnityEngine.Random.Range(0, 2);
        //
        if (rState == 0) //Idle
        {
            state = State.Idle;

        }
        else if (rState == 1)//Walk
        {
            if (state == State.Idle)//上个状态==Idle
            {
                state = State.Walk;

            }
            else if (state == State.Walk)//上个状态==Walk
            {
                int rRotate = UnityEngine.Random.Range(0, 360);
                transform.Rotate(transform.up * rRotate);//y轴旋转
            }
            state = State.Walk;
        }
    }
    #endregion


    #region 系统
    private void OnMouseEnter()
    {
        if (entity ==null ||  entity.skillAttack.useMultiSkill || UICamera.isOverUI  )
        {
            return;
        }
        if (entity.skillAttack.useSingleSkill) //使用技能时
            GameSettings._instance.SetLockTargetCursor();
        else
            GameSettings._instance.SetAttackCursor();
    }

    private void OnMouseExit()
    {
        if (entity == null || entity.skillAttack.useMultiSkill)
        {
            return;
        }
        GameSettings._instance.SetNormalCursor();
    }
    #endregion



}
