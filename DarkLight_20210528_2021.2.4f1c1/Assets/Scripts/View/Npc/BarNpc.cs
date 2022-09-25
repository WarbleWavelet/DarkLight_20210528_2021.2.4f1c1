using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNpc: Npc
{
    public QuestPannel quest;

    

    void Start()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("点击");
            quest.ShowWindow();
        }
    }

}
