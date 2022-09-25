using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SkillAttack : MonoBehaviour
{
    #region 属性
    SkillType skillType = SkillType.None;
    Entity entity;
    public EKeyCode eKeyCode;
    public Transform target;
    public Vector3 tarPos;

    public GameObject skillPrefab;
    //
    public bool exitSkill;
    public bool enterSkill;
    public float timer = 0f;
    public float time = 1f;
    public bool isTiming;

    public bool enterChase = false;

    #endregion

    #region 生命周期
    public void Init()
    {
        entity = GetComponent<Entity>();
        eKeyCode = EKeyCode.Alpha1;
        exitSkill = false;
        enterSkill = false;
        enterChase = false;
        timer = 0f;
        time = 1f;
        isTiming = false;
    }

    void Update()
    {

        //if (actionState == ActionState.SkillAttack 
        //    && skillAttack.enterChase==true)
        //{
        //    target = skillAttack.target;
        //    if ( skillAttack.enterChase == true) //Enter
        //    {
        //        if (IsArrived( target)==false) // Process
        //        {
        //            transform.LookAt(tarPos);
        //            Run();
        //        }
        //        else
        //        {
        //            skillAttack.enterChase = false; //释放技能
        //            skillAttack.enterSkill = true;
        //            Idle();
        //        }
        //    }
        //} 


        if ( // entity.actionState == ActionState.None && //优先度最高
            Input.GetKeyDown(  EKeyCode2KeyCode(eKeyCode))) //123456
        {
            entity.actionState = ActionState.SkillAttack;
            
        }
        if (entity.actionState == ActionState.SkillAttack   )
        {
            if (Common.RayHit(Tags.Enemy, ref target)  && Input.GetMouseButtonDown(0) && enterChase == false)
            {
                enterChase = true;
            }
        }


        if (enterChase == false
            && enterSkill == true
            && exitSkill == false
            && target != null)
        {
                GameObject go = Instantiate(skillPrefab, target.position, Quaternion.identity);
                enterSkill = false;
                exitSkill = true;
        }


        if (false==enterSkill && true ==exitSkill )
        {
            isTiming = true;
        }

        if (isTiming)
        {

            timer = Common.Timer(() => {
                tarPos = Vector3.zero;
                target = null;
                exitSkill = false;
                entity.actionState = ActionState.Idle;
                isTiming = false;
            }, timer, time);
        }
    }






    #endregion

    #region 系统函数

    #endregion

    #region 辅助函数
    KeyCode EKeyCode2KeyCode(EKeyCode eKeyCode)
    {

        switch (eKeyCode)
        {
            case EKeyCode.Alpha1 :   return KeyCode.Alpha1;
            case EKeyCode.Alpha2 :   return KeyCode.Alpha2;
            case EKeyCode.Alpha3 :   return KeyCode.Alpha3;
            case EKeyCode.Alpha4 :   return KeyCode.Alpha4;
            case EKeyCode.Alpha5 :   return KeyCode.Alpha5;
            case EKeyCode.Alpha6:    return KeyCode.Alpha6;
  
            default:return KeyCode.None;
        }

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
    Alpha1, Alpha2, Alpha3, Alpha4, Alpha5, Alpha6

}




