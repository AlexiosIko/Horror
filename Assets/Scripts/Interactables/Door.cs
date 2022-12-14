using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public string keyName;
    public bool locked = false;
    public override void Action()
    {
        if (locked == false) // Is unlocked
            Open();
        else // Door is locked
        {
            if (Inventory.instance.RemoveItem (keyName) == true)
                Unlock ();
            else
                WiggleDoor();
        }
    }
    void Unlock()
    {
        CanvasManager.instance.Chat ("You unlock the door ...", 1);
        locked = false; // UNLOCK
        PlayAudio ("unlock"); // Unlock sound effect
    }
    void WiggleDoor()
    {
        CanvasManager.instance.Chat ("You'll need a " + keyName + " for this door ...", 1);
        animator.Play("Wiggle");
        PlayAudio("wiggle");
    }
    void Open()
    {
        PlayAudio ("open");
        
        animator.Play("Action"); // Animate
        foreach (Transform t in transform) // Swap the layerMasks so we don't interact with this door again DUH
            t.gameObject.layer = LayerMask.GetMask("Default");
    }
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        prompt = "Press e to open door";
    }
    Animator animator;
}