using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pannel : MonoBehaviour
{
    [Tooltip("父类Window的字段，不用设置，可调用而已")] public bool isShow = false;
    [Tooltip("关闭Pannel的按钮")] public UIButton closeButton;

    public virtual void ShowWindow()
    {

        if (GetComponent<UIDragDropItem>() == false)
        {
            gameObject.AddComponent<ExampleDragDropItem>();
        }
        GetComponent<TweenPosition>().PlayForward();
        isShow = true;
    }
    public void DisableWindow()
    {
        GetComponent<TweenPosition>().PlayReverse();
        isShow = false;
    }







}
