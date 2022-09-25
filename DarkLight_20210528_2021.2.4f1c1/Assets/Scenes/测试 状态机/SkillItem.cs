using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class SkillItem : MonoBehaviour
{
    #region 字属


    public Transform target;
    public Vector3 tarPos;
    Entity entity;




    public bool enterChase;
    public bool enterAttack;
    public bool processAttack;
    Animator animator;

    float atk;
    GameObject atk_prefab;
    float atk_normal;
    float atk_crazy;
    float atk_crazy_rate;

    public float atk_timer;
    public float atk_time;
    int skillID = -1;
    bool isFirstAtk = false;

    public KeyCode keyCode;
    public SkillType skillType;

    #endregion

    #region 生命周期


    public void Init()
    {
        entity = GetComponent<Entity>();
        enterChase = false;
        enterAttack = false;
        processAttack = false;
        isFirstAtk = false;
        animator = GetComponent<Animator>();
        atk_prefab = Resources.Load<GameObject>("Prefabs/atk_prefab");

        atk_timer = 0f;
        atk_time = 1f;
        skillID = -1;
    }

    void Update()
    {


        if (Input.GetKeyDown(keyCode))// && Common.RayHit(Tags.Enemy, ref target, ref tarPos))
        {
            entity.actionState = ActionState.SkillAttack;

            if (skillType == SkillType.Single)
            { 
            
            }

            entity.Idle();
            atk_timer = 0f;
            enterChase = true;
            animator.SetBool(Constants_Animator.Idle, false);
        }

        if (entity.actionState != ActionState.Attack)
        {
            enterChase = false;
            enterAttack = false;
            processAttack = false;
            isFirstAtk = false;
            target = null;
            atk_timer = 0f;
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);

            return;
        }

        if (enterChase == false && enterAttack && target != null)
        {
            processAttack = false;
        }

        if (enterAttack && false == processAttack)
        {
            if (isFirstAtk == false)
            {
                AttackFunc();
                processAttack = false;
                isFirstAtk = true;
            }
            atk_timer = Common.Timer(() => {
                AttackFunc();
                processAttack = false;
                // animator.SetInteger("SkillID", -1);
            }, atk_timer, atk_time);
        }



    }

    public void InstaniateEffectPrefab()
    {
        if (target == null)
        {
            return;
        }
        Instantiate(atk_prefab, target.transform.position, Quaternion.identity);

    }
    #endregion

    #region 系统函数

    #endregion

    #region 辅助函数


    #region Range Chase Attack TakeDamage Animator Dead


    public float AttackFunc()//攻击
    {
        transform.LookAt(target);
        //
        float attack;
        if (UnityEngine.Random.Range(0f, 1f) < atk_crazy_rate)//暴击
        {
            attack = atk_crazy;  //暴击
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Attack1);
        }
        else
        {
            attack = atk_normal;//普攻
            animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.AttackCritical);
        }

        //
        return attack;
    }
    //





    #endregion


    #endregion

}



