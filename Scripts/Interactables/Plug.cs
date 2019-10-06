using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : Interactable
{
    Sink sink;

    private new void Start()
    {

        base.Start();//god this is ugly
        sink = GetComponentInParent<Sink>();
        hoverPrompt = "Pull Plug";
    }

    void FixedUpdate()
    {
    }

    public override void Interact()
    {
        sink.PullPlug();
    }

}
