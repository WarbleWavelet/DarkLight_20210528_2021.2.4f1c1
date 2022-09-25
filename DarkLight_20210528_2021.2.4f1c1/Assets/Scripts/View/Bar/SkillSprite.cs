using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSprite : UIDragDropItem
{

    [Tooltip("对应哪个SkillItem")] public SkillItem skillItem;
    public int depth;
    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        skillItem = transform.parent.GetComponent<SkillItem>();
        transform.parent = transform.root;//用回root方式跳出去
        GetComponent<UISprite>().depth = 20;//最高就行
        
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if (surface != null && surface.tag == Tags.Shortcut )
        {
            surface.GetComponent<ShortcutItem>().AddSkillItem(skillItem);
        }
    }
}
