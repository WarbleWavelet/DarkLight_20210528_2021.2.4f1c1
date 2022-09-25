using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MyCameraAdapter : MonoBehaviour
{

    public new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        AdaptCamera();
    }

    public void AdaptCamera()
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }

        float screenAspect = Screen.width / Screen.height;
        float designAspect = 1280 / (float)720;

        if (designAspect < screenAspect) //屏幕分辨率过大，宽度过长,则屏幕横向留出黑边,高度不变
        {
            float tarWidth = Screen.height * designAspect;//求出实际要显示的宽度
            float tarWidthRadio = tarWidth / Screen.width;//求出宽度百分比
            float posW = (1 - tarWidthRadio) / 2;//宽的起点
            camera.rect = new Rect(posW, 0, tarWidthRadio, 1);
        }
        else if (designAspect > screenAspect)//屏幕分辨率过小，高度过高，纵向留黑边,宽度不变
        {
            float tarHeight = Screen.width / designAspect;
            float tarHeightRadio = tarHeight / Screen.height;
            float posH = (1 - tarHeightRadio) / 2;
            camera.rect = new Rect(0, posH, 1, tarHeightRadio);
        }
        else
        {
            camera.rect = new Rect(0, 0, 1, 1);
        }
    }
}