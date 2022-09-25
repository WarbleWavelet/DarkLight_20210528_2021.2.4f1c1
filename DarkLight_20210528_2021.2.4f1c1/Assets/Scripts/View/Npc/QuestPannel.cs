using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestPannel : Pannel
{


    public UIButton acceptButton;
    public UIButton submitButton;
    public UIButton cancelButton;


    public UILabel label;


    [Tooltip("奖励金币")] public int coin = 1000;   
    [Tooltip("完成的")] public int progress=0;
    [Tooltip("目标的")] public int goal=10;


    public static QuestPannel _instance;

	void Awake()
    {
        _instance = this;
    }

    public void OnAcceptClick()
    {
        acceptButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        submitButton.gameObject.SetActive(true);
        label.text = "你杀死了" + progress + "/" + goal + "只小野狼！\n奖励："+ coin + "金币\n\n";
    }
    public void OnSubmitClick()
    {
        if (progress >= goal)
        {
            label.text = "你已经杀死"+ goal+"只小野狼。\n恭喜你完成任务！\n你已经获得" + coin + "金币！";
            submitButton.gameObject.SetActive(false);
            Player._instance.AddCoin(coin);

        }
        else
        {
            label.text = "你杀死了" + progress + "/" + goal + "只小野狼！\n奖励：" + coin + "金币\n\n" + "你还未完成任务！";
        }
    }

}
