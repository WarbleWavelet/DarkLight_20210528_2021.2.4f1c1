using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class Common 
{


    #region RayHit



    public static bool RayHit(string tag, ref Transform target, ref Vector3 tarPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//点生成射线
        RaycastHit hit;
        bool isCollided = Physics.Raycast(ray, out hit);//是否碰到
        if (isCollided && hit.collider.CompareTag(tag))
        {
            tarPos = hit.point;
            target = hit.collider.transform;
            return true;
        }
        else
        {
            tarPos = Vector3.zero;
            return false;
        }
    }

    public static bool RayHit(string tag, ref Vector3 tarPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//点生成射线
        RaycastHit hit;
        bool isCollided = Physics.Raycast(ray, out hit);//是否碰到
        if (isCollided && hit.collider.CompareTag(tag))
        {
           tarPos = hit.point;
            return true;
        }
        else
        {
            tarPos = Vector3.zero;
            return false;
        }
    }

    public static bool RayHit(string tag, ref Transform target)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//点生成射线
        RaycastHit hit;
        bool isCollided = Physics.Raycast(ray, out hit);//是否碰到
        if (isCollided && hit.collider.CompareTag(tag))
        {
            target = hit.collider.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion





    #region Dis
   public static float Distance(Transform from, Transform to)
    {
        return  Vector3.Distance(from.position, to.position);
    }

   public static float Dinstance(Vector3 from, Vector3 to, float stopDis)
    {

        float dis = Vector3.Distance(from, to);
        if (dis < stopDis)
        {
            return 0f;
        }
        else
        {
            return dis;
        }
    }
    #endregion



    #region Timer




    public delegate void DelegateVoid();//定义委托类型
    public static float Timer(DelegateVoid timingFunc,DelegateVoid overTimeFunc,  float timer, float time)//定时执行函数
    {
        timer += Time.deltaTime;

        if (timer > time)
        {
            timer = 0f;
            overTimeFunc();
        }
        else
        {
            timingFunc();
        }
        return timer;
    }

    public static float Timer(DelegateVoid overTimeFunc, float timer, float time)//定时执行函数
    {
        timer += Time.deltaTime;

        if (timer > time)
        {
            timer = 0f;
            overTimeFunc();
        }
        else
        {
        }
        return timer;
    }
    public delegate float DelegateFloat();//定义委托类型
    public static float Timer(DelegateFloat overTimeFunc, float timer, float time)//定时执行函数
    {
        timer += Time.deltaTime;

        if (timer > time)
        {
            timer = 0f;
            overTimeFunc();
        }
        else
        {
        }
        return timer;
    }

    public static float Timer(DelegateVoid startFunc, DelegateFloat overTimeFunc, float timer, float time)//定时执行函数
    {
      
        if (timer > time)
        {
            timer = 0f;
            overTimeFunc();
        }
        else
        {
             timer += Time.deltaTime;
        }
        return timer;
    }
    #endregion  





    public static Vector3 Trim_Vector3(Vector3 v, V3_Trim type)
    {
        Vector3 sub=new Vector3();
        switch ( type )
        {
            case  V3_Trim.X:
                {
                    sub = new Vector3(v.x,0f,0f);
                }
                break;                 case V3_Trim.Y :
                {
                    sub = new Vector3( 0f,v.y,0f);
                }
                break;                 case V3_Trim.Z :
                {
                    sub = new Vector3(0f,0f,v.z );
                }
                break;
            default: { sub = Vector3.zero; } break;
        }

        v=v- sub;

        return v;

    }

     public static void Trim_Vector3(Transform t, V3_Trim type)
    {
        Vector3 pos = t.position;
        Vector3 sub=new Vector3();
        switch (type)
        {
            case V3_Trim.X:
                {
                    sub = new Vector3(pos.x, 0f, 0f);
                }
                break;
            case V3_Trim.Y:
                {
                    sub = new Vector3(0f, pos.y, 0f);
                }
                break;
            case V3_Trim.Z:
                {
                    sub = new Vector3(0f, 0f, pos.z);
                }
                break;
            default: { sub = Vector3.zero; } break;
        }

        pos =pos- sub;
        t.position = pos;
    }



    public static float GetLengthByName(Animator animator, string name)
    {
        float length = 0;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(name))
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }
}


public enum V3_Trim
{
    None,
    X,
    Y,
    Z

}




