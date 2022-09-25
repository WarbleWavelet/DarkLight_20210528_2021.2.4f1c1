using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionNpc : Npc
{
    public ShopPannel window;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("点击");
            window.ShowWindow();
        }
    }
}
