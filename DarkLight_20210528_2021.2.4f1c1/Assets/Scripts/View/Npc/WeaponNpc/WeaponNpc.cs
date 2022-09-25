using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNpc : Npc
{
    public ShopPannel window;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            window.ShowWindow();
        }
    }
}
