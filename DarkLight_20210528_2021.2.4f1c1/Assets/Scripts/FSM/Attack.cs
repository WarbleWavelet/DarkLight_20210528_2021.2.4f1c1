using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Attack : MonoBehaviour
{


    #region 字属

    public Transform target;
    public Vector3 tarPos;
    Entity entity;

    public float stopDis;


    public bool firstAtk;
    Animator animator;

    GameObject atk_prefab;
    GameObject atk_critical_prefab;
  public float atk_normal;
  public float atk_crazy;
  public  float atk_crazy_rate;

    public float atk_timer;
     public   float curAniClipLen;


    bool canAtk = false;
    bool process = false;
    bool exit = false;

    AudioSource ads;
    AudioClip clip_atk_1;
    AudioClip clip_atk_2;
    AudioClip clip_atk_critical;

   public new  Animation animation;



    [Header("技能推开敌人")]
 public   bool isPush;
 public  float pushTimer;
 public  float pushTime;
    public float pushSpeed;
    public bool inRng;

    TimerSvc timerSvc;
    GameRoot gameRoot;
    #endregion

    #region 生命周期


    public void Init()
    {
        entity = GetComponent<Entity>();

        animator = GetComponent<Animator>();
        ads = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();

        atk_prefab = Resources.Load<GameObject>("Prefabs/atk_prefab");
        atk_critical_prefab = Resources.Load<GameObject>("Prefabs/Skill_Sword");
        clip_atk_1 = Resources.Load<AudioClip>("slash-normal"); ;
        clip_atk_2 = Resources.Load<AudioClip>("slash-normal2");
        clip_atk_critical = Resources.Load<AudioClip>("slime-hit");



        atk_timer = 0f;
      //  stopDis = 2f;
      //  atk_crazy_rate = 0.8f;
        firstAtk = true;
        exit = false;

        isPush=false;
        pushTimer=0f;
         pushTime=0.2f;
 pushSpeed=100f;
    //oushSpeed=3f;

    inRng = false;





        gameRoot = GameObject.FindGameObjectWithTag(Tags.GameRoot).GetComponent<GameRoot>();
        timerSvc = gameRoot.timerSvc;
    }

    void Update()
    {
        if (entity.skillAttack != null && entity.skillAttack.curSkillItem != null)
            return;

            if (Input.GetMouseButtonDown(0)
            && Common.RayHit(Tags.Enemy, ref target)
            && UICamera.isOverUI == false //NGUI不是点击UI  
           
            )
        {

            entity.actionState = ActionState.Attack;
            atk_timer = 0f;
        }

        if (entity.actionState == ActionState.Attack)
        {

            if (target != null) //Enter
            {
             
                if (entity.IsArrived(target, stopDis) == false) // 追击
                {
                    transform.LookAt(Common.Trim_Vector3(target.position, V3_Trim.Y));
                    entity.Run();
                    inRng = false;
                }
                else
                {
                    if (animator.GetBool(Constants_Animator.EnterRun) == true)
                    {
                        entity.Idle();
                    }
                    inRng = true;
                    canAtk = true;
                }

                if (inRng)
                {
                    if (canAtk)//A一下
                    {
                        canAtk = false;
                        process = true;

                    }
                    if (process)
                    {
                        atk_timer = Common.Timer(() => {   
                            AttackFunc(ref curAniClipLen);
                            canAtk = true;
                            process = false;
                        }, atk_timer, curAniClipLen);
                    }


                    if (isPush)  //技能效果
                    {
                        target.position = Vector3.Lerp(target.position, target.position + Common.Trim_Vector3(transform.forward * pushSpeed, V3_Trim.Y), pushTime * Time.deltaTime);
                        pushTimer = Common.Timer(() =>
                        {

                            isPush = false;
                        }, pushTimer, pushTime);
                    }
                }

            }
        else
        {
            inRng = false;
            atk_timer = 0f;
            firstAtk = true;
            exit = false;
            entity.actionState = ActionState.Idle;
        }
    }
    }


    #region 攻击


  public void AttackFunc(ref float curAniClipLen)
    {
        entity.Idle();
    
        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        if (r < atk_crazy_rate) //暴击
        {
            curAniClipLen = clip_atk_critical.length;
            AttackCriticalFunc();
        }
        else //平A
        {
            curAniClipLen = (clip_atk_1.length+clip_atk_2.length);
            AttacNormalkFunc();
        }
        
    }



    public float AttacNormalkFunc()//攻击
    {
        Common.Trim_Vector3(target, V3_Trim.Y);

        transform.LookAt(target);
        //
        float attack = atk_normal;//普攻
        target.GetComponent<Wolf>().TakeDamage(attack);


       animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Attack1);
        timerSvc.AddTimerTask((int tid) => {
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
            animator.SetBool(Constants_Animator.EnterIdle, true);

        }, curAniClipLen, PETimeUnit.Second);
        //
        return attack;


    }
    //

    public float AttackCriticalFunc()//攻击
    {
        Common.Trim_Vector3(target, V3_Trim.Y);
        transform.LookAt(target);


        //
        float attack = atk_crazy;  //暴击

        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.AttackCritical);

        timerSvc.AddTimerTask((int tid) => {
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
            animator.SetBool(Constants_Animator.EnterIdle, true) ;

        }, curAniClipLen, PETimeUnit.Second);

        target.GetComponent<Wolf>().TakeDamage(attack);
        return attack;

    }

    #endregion  

   





    #endregion


    #region 辅助函数


    #region 动画事件

    public void InstaniateEffectPrefabCritical()
    {
        if (target == null) return;
        if (entity.actionState != ActionState.Attack) return;

        //  AttackCriticalFunc();//攻击
        Instantiate(atk_critical_prefab, target.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(clip_atk_critical, transform.position);
    }


    /// <summary>
    /// 动画事件
    /// </summary>
    public void Push()
    {
        if (entity.actionState != ActionState.Attack) return;
        isPush = true;

    }

    /// <summary>
    /// 动画事件 (攻击时打断，)
    /// </summary>
    public void Exit()
    {
        if (entity.actionState != ActionState.Attack) return;
        if (entity.actionState == ActionState.MoveByMouse)
        {
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
            animator.SetBool(Constants_Animator.EnterIdle, true);
        }

    }

    /// <summary>
    /// 动画时事件用到
    /// </summary>
    public void InstaniateEffectPrefab()
    {
        if (target == null) return;
        if (entity.actionState != ActionState.Attack) return;

        // AttacNormalkFunc();
        Instantiate(atk_prefab, target.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(clip_atk_1, transform.position);
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.5f);
        ads.clip = clip_atk_critical;
        ads.Play();
    }

    #endregion


    #endregion


}




