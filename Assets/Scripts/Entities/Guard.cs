using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Entity
{
    string text = "You are NOT allowed to be in here";
    public override void Action()
    {
        base.Action(); // Does nothing currently

        PlayAudio(sounds[0]);
        
        CanvasManager.instance.Chat (text);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName ("angry") == false)
            animator.CrossFade ("angry", 0.5f);
        // Forward point to a vector infront of guard
        StartCoroutine(LookAtAndBack(player.position, transform.localPosition + transform.forward, 2));
    }
}