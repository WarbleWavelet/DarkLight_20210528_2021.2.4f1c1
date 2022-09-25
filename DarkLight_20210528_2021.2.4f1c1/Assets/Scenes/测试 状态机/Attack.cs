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
    public bool process;
    Animator animator;

    float atk;
    GameObject atk_prefab;
    GameObject atk_critical_prefab;
    float atk_normal;
    float atk_crazy;
  public  float atk_crazy_rate;

    public float atk_timer;
    public float curAniClipLen;
    int skillID = -1;
    public bool exit;

    float r;

    bool canAtk = false;

    AudioSource ads;
    AudioClip clip_atk_1;
    AudioClip clip_atk_2;
    AudioClip clip_atk_critical;

   public new  Animation animation;

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
        skillID = -1;
      //  stopDis = 2f;
      //  atk_crazy_rate = 0.8f;
        firstAtk = true;
        process = true;
        exit = false;

        isPush=false;
        pushTimer=0f;
        // time_push=2f;
        //oushSpeed=3f;

        inRng = false;





        gameRoot = GameObject.FindGameObjectWithTag(Tags.GameRoot).GetComponent<GameRoot>();
        timerSvc = gameRoot.timerSvc;
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0)
            && Common.RayHit(Tags.Enemy, ref target))
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
                    transform.LookAt(  Common.Trim_Vector3( target.position,V3_Trim.Y));
                    entity.Run();
                    inRng = false;
                }
                else 
                {
                    if (animator.GetBool(Constants_Animator.EnterRun)==true)
                    {
                        entity.Idle();
                    }
                    inRng = true;
                    canAtk = true;
                }

                if (inRng)
                {
                    if (canAtk && process==false)
                    {
                        process = true;
                        canAtk = false;
                        NorOrCriAtk();

                    } 
                    else if (process)  //一直计时 ,控制攻速
                    {
                        atk_timer = Common.Timer(() =>
                        {
                            process = false;
                            canAtk = true;

                        }, atk_timer, curAniClipLen);//攻击间隔和攻击时长
                    }


                    if (isPush)  //技能效果
                    {
                        //transform.position = Vector3.Lerp(transform.position, transform.position+transform.forward* para_push, time_push*Time.deltaTime);
                        target.position = Vector3.Lerp(target.position, target.position + Common.Trim_Vector3(transform.forward * pushSpeed, V3_Trim.Y), pushTime * Time.deltaTime);
                        pushTimer = Common.Timer(() => {
                           
                            isPush = false;
                        }, pushTimer, pushTime);
                    }
                }

            }
        }
        else {
            inRng = false;
            atk_timer = 0f;
            skillID = -1;
            stopDis = 2f;
            //atk_crazy_rate = 0.5f;
            firstAtk = true;
            process = false;
            exit = false;
            entity.actionState = ActionState.Idle;
        }









    }

    public void NorOrCriAtk()
    {
        entity.Idle();
    
        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        if (r < atk_crazy_rate) //暴击
        {
            curAniClipLen = 0.8f;
            AttackCriticalFunc();
        }
        else //平A
        {
            curAniClipLen = 1.967f;//atk1+atk2
            AttackFunc();
        }
        
    }



    public float AttackFunc()//攻击
    {
        Common.Trim_Vector3(target.position, V3_Trim.Y);


        //
        float attack;

        attack = atk_normal;//普攻
        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Attack1);

          
        //
        return attack;


    }
    //

    public float AttackCriticalFunc()//攻击
    {
        Common.Trim_Vector3(target.position, V3_Trim.Y);



        //
        float attack;

        attack = atk_crazy;  //暴击
        animator.SetInteger(Constants_Animator.Skill1, Constants_Animator.AttackCritical);

        timerSvc.AddTimerTask((int tid) => {
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
            animator.SetBool(Constants_Animator.EnterIdle, true)
            ;

        }, curAniClipLen, PETimeUnit.Second);

        return attack;


    }



    /// <summary>
    /// 动画时事件用到
    /// </summary>
    public void InstaniateEffectPrefab()
    {
        if (target == null)  return;
        if (entity.actionState != ActionState.Attack) return;

        Instantiate(atk_prefab, target.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(clip_atk_1, transform.position);
    }

    public void InstaniateEffectPrefabCritical()
    {
        if (target == null) return;
        if (entity.actionState != ActionState.Attack) return;

        Instantiate(atk_critical_prefab, target.transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(clip_atk_critical, transform.position);
    }
    #endregion


    #region 辅助函数


    #region Range Chase Attack TakeDamage Animator Dead




    /// <summary>
    /// 动画事件
    /// </summary>
    public void Push()
    {

        isPush = true;

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




