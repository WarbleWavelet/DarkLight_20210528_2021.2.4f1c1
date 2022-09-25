using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class MoveByMouse : MonoBehaviour
{

    #region 属性

       float stopDis;
    public bool isDrag;
    public Vector3 tarPos;
    public GameObject effectPrefab;
    [Tooltip("点击特效的计时时间")] public float mouseBtnEffectTime = 1f;
    [Tooltip("点击特效的计时器")] private float mouseBtnEffectTimer = 0f;
    Entity entity;

    #endregion

    #region 生命周期
    public  void Init()
    {
        effectPrefab = Resources.Load<GameObject>("Prefabs/Efx_Click_Green");
        entity = GetComponent<Entity>();     
        stopDis = 1f;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) 
            && Common.RayHit(Tags.Ground, ref tarPos))// 得到targetPosition
        {
            entity.Idle();
            entity.actionState = ActionState.MoveByMouse;

            isDrag = true;
            Instantiate(effectPrefab, tarPos, Quaternion.identity);
        }

        if (entity.actionState == ActionState.MoveByMouse)
        {

            if (entity.IsArrived(tarPos,stopDis) == false) // Process
            {
                transform.LookAt(tarPos);
                entity.Run();
            }
            else
            {
                tarPos = Vector3.zero;
                entity.Idle();
                entity.actionState = ActionState.Idle;
            }
        }


        if (isDrag)// 得到targetPosition
        {
           
            if (Common.RayHit(Tags.Ground, ref tarPos))
            {
                mouseBtnEffectTimer=Common.Timer(() => {
                    mouseBtnEffectTimer = 0f;
                    Instantiate(effectPrefab, tarPos, Quaternion.identity);
                }, mouseBtnEffectTimer, mouseBtnEffectTime);
            }
        }




        if (Input.GetMouseButtonUp(0))// 得到targetPosition
        {
            isDrag = false;
        }

        if (entity.isArrived
            && entity.actionState == ActionState.MoveByMouse
            && tarPos != Vector3.zero)
        {
            tarPos = Vector3.zero;
        }

    }
    #endregion

    #region 系统函数

    #endregion


}



