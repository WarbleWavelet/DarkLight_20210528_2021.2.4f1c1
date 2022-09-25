using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public Texture2D attackCursor;
    public Texture2D lockTargetCursor;
    public Texture2D normalCursor;
    public Texture2D npcTalkCursor;
    public Texture2D pickCursor;

    public static GameSettings _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto );//图形，焦点
    }
    public void SetLockTargetCursor()
    {
        Cursor.SetCursor(lockTargetCursor, Vector2.zero, CursorMode.Auto);//图形，焦点
    }
    public void SetAttackCursor()
    {
        Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);//图形，焦点
    }
    public void SetNpcTalkCursor()
    {
        Cursor.SetCursor(npcTalkCursor, Vector2.zero, CursorMode.Auto);//图形，焦点
    }
    public void SetPickCursorr()
    {
        Cursor.SetCursor(pickCursor, Vector2.zero, CursorMode.Auto);//图形，焦点
    }

}
