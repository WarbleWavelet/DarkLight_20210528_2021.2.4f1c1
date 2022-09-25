using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    #region 属性

    Animator animator;
    Idle idle;
    Run run;
    //
    MoveByMouse moveByMouse;

    public ActionState actionState= ActionState.Idle;    
    //
    //      
    public Attack attack;
    public SkillAttack skillAttack;
    public bool isArrived = false;
    public Transform target;

    public static Entity _instance;

    

    #endregion

    #region 生命
    void Start()
    {
        _instance = this;
        animator =GetComponent<Animator>();
        moveByMouse=GetComponent<MoveByMouse>();
        idle=GetComponent<Idle>();
        run=GetComponent<Run>();
        attack=GetComponent<Attack>(); 
       skillAttack = GetComponent<SkillAttack>();

        idle.Init();
        run.Init();
        moveByMouse.Init();
        attack.Init();
       skillAttack.Init();




        isArrived = false;
    }

    void Update()
    {





    }


    #endregion

    #region 系统函数

    #endregion

    #region 辅助函数

    public bool IsArrived( Vector3 tarPos,float stopDis)
    {
        isArrived = 0f == Common.Dinstance(transform.position, tarPos, stopDis);
        return isArrived;
    
    }

    public bool IsArrived( Transform target, float stopDis)
    {
        isArrived= 0f == Common.Dinstance(transform.position, target.position, stopDis);
        return isArrived;
    
    }
    public void Idle()
    {


        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);  
        run.Exit();
        idle.Enter();

      
    }

    public void Idle1()
    {


        animator.SetInteger(Constants_Animator.SkillID, Constants_Animator.Idle);



    }

    public void Run()
    {
        idle.Exit();
        run.Enter();
    }

    public void Chase(Transform target)
    {
        transform.LookAt(target.position);
        Run();
    }

    public void Chase(Vector3 tarPos)
    {
        transform.LookAt(tarPos);
        Run();
    }


    #endregion

}


public enum ActionState
{
    Idle,
    MoveByMouse,
    Attack,
    SkillAttack,
    Chase

}



