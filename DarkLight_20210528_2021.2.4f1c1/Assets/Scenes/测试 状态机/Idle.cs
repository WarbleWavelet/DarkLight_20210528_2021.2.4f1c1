using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Idle : MonoBehaviour
{
    #region 属性
    Animator animator;
    #endregion

    #region 生命周期
    public void Init()
    {
        animator = GetComponent<Animator>();


    }

    void Update()
    {
        if (animator.GetBool(Constants_Animator.EnterIdle))
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
        animator.SetBool(Constants_Animator.EnterIdle,true);
        animator.SetBool(Constants_Animator.EnterRun, false);
        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);
    }

    public void Process()
    {

    }

    public void Exit()
    {
        animator.SetBool(Constants_Animator.EnterIdle, false);
    }
    #endregion


}


