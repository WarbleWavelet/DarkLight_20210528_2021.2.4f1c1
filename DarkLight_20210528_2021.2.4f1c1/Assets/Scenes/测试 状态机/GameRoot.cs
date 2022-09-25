using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

 

    public class GameRoot : MonoBehaviour
    {
    #region 属性
    public ResSvc resSvc;
    public AudioSvc audioSvc;
    public TimerSvc timerSvc;




    #region 单例
    private static GameRoot _instance;      

    public static GameRoot Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameRoot();
            }
return _instance;
        }
    }
    #endregion


    #endregion

    #region 生命周期
    void Awake()
    {
        resSvc = GetComponent<ResSvc>();
        audioSvc = GetComponent<AudioSvc>();
        timerSvc = GetComponent<TimerSvc>();


        resSvc.InitSvc();
        audioSvc.InitSvc();
        timerSvc.InitSvc();

    }

    void Update()
        {
            
        }
        #endregion 

        #region 系统函数

        #endregion 

        #region 辅助函数

        #endregion

    }




