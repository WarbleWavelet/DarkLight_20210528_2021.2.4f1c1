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
        if (animator.GetBool(Constants_Animator.Idle))
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
        animator.SetInteger(Constants_Animator.SkillID,Constants_Animator.Idle);
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


