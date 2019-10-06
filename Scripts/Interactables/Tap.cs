using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : Interactable
{
    bool on = false;
    float speed = 2.0f;
    [SerializeField] bool hot = false;
    [SerializeField] Sink sink;

    private new void Start()
    {
        base.Start();//god this is ugly
        if (sink == null) { sink = GetComponentInParent<Sink>(); }
        hoverPrompt = "Turn On";
    }

    void FixedUpdate()
    {
    }

    public override void Interact()
    {
        on = !on;
        if(on) { TurnOn(); } else { TurnOff();  }
        transform.Rotate(Vector3.up, 45f);
    }

    void TurnOn()
    {
        hoverPrompt = "Turn Off";
        sink.TapsOn();

    }

    void TurnOff()
    {
        hoverPrompt = "Turn On";
        sink.TapsOff();
    }
}
