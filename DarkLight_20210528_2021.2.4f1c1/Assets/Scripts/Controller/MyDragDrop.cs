using UnityEngine;

public class MyDragDrop : UIDragDropItem
{
    [Tooltip("开始拖拽的物体的位置")]
    private Vector3 startPos = Vector3.zero;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();

        startPos = base.transform.position;//记录开始位置，为了回滚到原来的位置
    }
    protected override void OnDragDropRelease(GameObject surface)
    {

        base.OnDragDropRelease(surface);
        print(surface.tag);

        if (surface.tag == "Grid")
        {
            //transform.position = surface.transform.position;//物体放入格子里

            transform.parent = surface.transform;
            transform.localPosition = Vector3.zero;
        }
        else if (surface.tag == "Good")
        {
            //位置交换
            transform.position = surface.transform.position;
            surface.transform.position = startPos;
        }
        else if (surface.tag == "Shortcut")
        {
            ItemGroup item=GetComponent<ItemGroup>();
            //
            surface.GetComponent<ShortcutItem>().AddBagItem(item);
            //
            BagPannel._instance.RemoveItem(item.id);
        }
        else//脱到不该脱的位置
        {
            transform.position = startPos;

        }

    }

}
