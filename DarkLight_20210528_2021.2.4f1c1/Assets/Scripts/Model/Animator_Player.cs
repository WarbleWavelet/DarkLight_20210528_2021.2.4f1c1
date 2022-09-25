using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Animator_Player : Human
{

    
   // public new State state = State.Idle;
    private Player player;
    //
    public string runClip;
    //

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        state = State.Idle;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.state==State.Idle)
        {
            PlayAniClip(idleClip);
        }
        else if (player.state == State.Run)
        {
            PlayAniClip(runClip);
        }
    
    }

}
