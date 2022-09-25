using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;
    // Start is called before the first frame update
    void Start()
    {
        Transform miniMapCameraTrans = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        miniMapCamera = miniMapCameraTrans.Find("MiniMapCamera").gameObject.GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlusClick()
    {
        miniMapCamera.orthographicSize--;
    }
    public void OnMinusClick()
    {
        miniMapCamera.orthographicSize++;
    }
}
