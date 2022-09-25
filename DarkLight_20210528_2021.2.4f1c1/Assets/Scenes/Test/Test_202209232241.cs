using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace NewBehaviourScript00
{
    public class Test_202209232241 : MonoBehaviour
    {
        #region 属性
        public GameObject prefab;
        #endregion

        #region 生命周期
        void Start()
        {
            prefab.AddComponent<DestroyForTime>().time = 3;
        }
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(prefab,transform.position,Quaternion.identity);
            }
        }
        #endregion 

        #region 系统函数

        #endregion 

        #region 辅助函数

        #endregion

    }
}



