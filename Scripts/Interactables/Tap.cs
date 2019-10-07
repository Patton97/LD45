using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : Interactable
{
    bool on = false;
    [SerializeField] Sink sink;

    private new void Start()
    {
        base.Start();//god this is ugly
        if (sink == null) { sink = GetComponentInParent<Sink>(); }
        UpdateTap();
    }

    public override void Interact()
    {
        on = !on;
        UpdateTap();
        transform.Rotate(Vector3.up, 45f);
    }

    void UpdateTap()
    {
        if(on)
        {
            prompt = "Turn On";
            sink.TapsOn();
        }
        else
        {
            prompt = "Turn On";
            sink.TapsOff();
        }
    }
}
