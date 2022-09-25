/****************************************************

   文件：
   作者：WWS
   日期：2022/09/08 21:11:02
   功能：在EquipPanel下的物体

*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : ItemGroup
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubItem()
    {

        //print("item脱下");
       // EquipPannel._instance.SubItem(dressType);//Panel来操作

    }


    public virtual void SetUI(EquipItem item)
    {
        if (countLabel != null)
        { 
            countLabel.text = count.ToString();
        }
       
    }


}
