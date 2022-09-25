using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Run : MonoBehaviour
{
    #region 属性
    Animator animator;
    CharacterController ctrl;
    Entity entity;
    public float speed;
    #endregion

    #region 生命周期
    public void Init()
    {
        animator = GetComponent<Animator>();
        ctrl = GetComponent<CharacterController>();
        entity = GetComponent<Entity>();
        speed = 100f;


    }

    void Update()
    {
        if (animator.GetBool(Constants_Animator.EnterRun) && animator.GetInteger(Constants_Animator.SkillID) == Constants_Animator.Idle)
        {
            Process();
        }
    }
    #endregion

    #region 系统函数

    #endregion

    #region 辅助函数
    public void Enter()
    {

        animator.SetBool(Constants_Animator.EnterRun, true);
    }

    public void Process()
    {

        ctrl.SimpleMove( transform.forward * Time.deltaTime *speed);
    }

    public void Exit()
    {
        animator.SetBool(Constants_Animator.EnterRun, false);
    }
    #endregion


}


