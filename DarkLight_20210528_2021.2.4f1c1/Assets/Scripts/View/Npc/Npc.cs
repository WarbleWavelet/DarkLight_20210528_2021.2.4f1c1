using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Transform panel;
    private void OnMouseEnter()
    {
        GameSettings._instance.SetNpcTalkCursor();
    }
    private void OnMouseExit()
    {
        GameSettings._instance.SetNormalCursor();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag =="Player")
        {
            panel.GetComponent<Pannel>().DisableWindow();
        }
            
    }
}
