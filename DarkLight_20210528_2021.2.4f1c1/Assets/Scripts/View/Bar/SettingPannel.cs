using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPannel : Pannel
{


    public static SettingPannel _instance;

	void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void ShowWindow()
    {

        Debug.Log("开发中");
    }
}
